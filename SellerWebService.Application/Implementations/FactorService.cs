using _0_framework.Extensions;
using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Factor;
using SellerWebService.DataLayer.DTOs.Paging;
using SellerWebService.DataLayer.Entities.Account;
using SellerWebService.DataLayer.Entities.Factor;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class FactorService : IFactorService
    {
        #region ctor
        private readonly IGenericRepository<Factor> _factorRepository;
        private readonly IGenericRepository<FactorDetails> _factorDetailsRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        public FactorService(IGenericRepository<Factor> factorRepository, IGenericRepository<FactorDetails> factorDetailsRepository, IGenericRepository<User> userRepository, IGenericRepository<Customer> customerRepository)
        {
            _factorRepository = factorRepository;
            _factorDetailsRepository = factorDetailsRepository;
            _customerRepository = customerRepository;
        }

        #endregion

        #region factor
        public async Task<Guid> CreateBlankFactor(CreateFactorDto factor, Guid storeCode)
        {
            try
            {
                var customer = await _customerRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.UniqueCode == factor.CustomerCode && x.StoreCode == storeCode);
                if (customer == null) return Guid.Empty;
                var newFactor = new Factor
                {
                    CustomerId = customer.Id,
                    Code = Guid.NewGuid(),
                    Name = factor.Name,
                    Description = factor.Description,
                    Prepayment = factor.Prepayment,
                    DeliveryDate = factor.DeliveryDate,
                    FactorStatus = FactorStatus.Open,
                    taxation = factor.taxation,
                    StoreCode = storeCode,
                    ExpiredDate = DateTime.Now.AddDays(factor.longDayToExpired)
                };

                await _factorRepository.AddEntity(newFactor);
                await _factorDetailsRepository.SaveChanges();
                return newFactor.Code;
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> CreateFactorDetails(List<CreateFactorDetailsDto> factorDetails, Guid storeCode, Guid factorCode)
        {
            try
            {
                var factor = await _factorRepository.GetQuery().Include(x => x.FactorDetails)
                    .AsQueryable()
                    .SingleOrDefaultAsync(x => x.Code == factorCode && x.StoreCode == storeCode && !x.IsDelete);
                if (factor == null) return false;

                if (factor.FactorDetails.Any() || factor.FactorDetails != null)
                    _factorDetailsRepository.DeletePermanentEntities(factor.FactorDetails.ToList());

                var list = new List<FactorDetails>();
                foreach (var item in factorDetails)
                {
                    var newDetails = new FactorDetails
                    {
                        FactorId = factor.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Count = item.Count,
                        Discount = item.Discount,
                        Packaging = item.Packaging,
                        Price = item.Price
                    };
                    list.Add(newDetails);
                }
                await _factorDetailsRepository.AddRangeEntities(list);
                await _factorDetailsRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<ReadMainFactorDto> GetFinialFactorToConfirm(Guid factorCode, Guid storeCode)
        {
            var factor = await _factorRepository.GetQuery().Include(x => x.FactorDetails).Include(x => x.Customer).AsQueryable()
                .SingleOrDefaultAsync(x => x.Code == factorCode && x.StoreCode == storeCode);

            if (factor == null || !factor.FactorDetails.Any()) return null;

            await CalculateFactor(factorCode);
            return new ReadMainFactorDto
            {
                Name = factor.Name,
                Description = factor.Name,
                FinalPrice = factor.FinalPrice,
                Code = factor.Code,
                Prepayment = factor.Prepayment,
                taxation = factor.taxation,
                DeliveryDate = factor.DeliveryDate,
                FactorStatus = factor.FactorStatus.GetEnumName(),
                Status = factor.FactorStatus,
                FirstName = factor.Customer.FirstName,
                LastName = factor.Customer.LastName,
                Mobile = factor.Customer.Mobile,
                CustomerCode = factor.Customer.UniqueCode.ToString("N"),
                TotalDiscount = factor.TotalDiscount,
                TotalPrice = factor.TotalPrice,
                CreateFactorDetailsDtos = factor.FactorDetails.Select(x => new CreateFactorDetailsDto
                {
                    Name = x.Name,
                    Description = x.Description,
                    Count = x.Count,
                    Discount = x.Discount,
                    Packaging = x.Packaging,
                    Price = x.Price

                }).ToList()

            };

        }

        public async Task<CreateFactorResult> PublishFactor(Guid factorCode, Guid storeCode)
        {
            try
            {
                var factor = await _factorRepository.GetQuery().Include(x => x.FactorDetails).AsQueryable()
                    .SingleOrDefaultAsync(x => x.Code == factorCode && x.StoreCode == storeCode);
                if (factor == null || !factor.FactorDetails.Any()) return CreateFactorResult.FactorNotFound;
                await CalculateFactor(factorCode);
                if (factor.FactorStatus != FactorStatus.Open) return CreateFactorResult.IsAlreadyPublish;
                factor.FactorStatus = FactorStatus.Waiting;
                await _factorRepository.SaveChanges();
                return CreateFactorResult.Success;
            }
            catch (Exception e)
            {
                return CreateFactorResult.Error;
            }
        }

        public async Task<ReadFullFactorDto> GetFinialFactor(Guid factorCode, Guid storeCode)
        {
            var factore = await _factorRepository.GetQuery().AsQueryable().Include(x => x.FactorDetails)
                .SingleOrDefaultAsync(x => x.Code == factorCode && x.StoreCode == storeCode && !x.IsDelete);
            if (factore == null || !factore.FactorDetails.Any()) return null;
            return new ReadFullFactorDto
            {
                Name = factore.Name,
                Description = factore.Description,
                Code = factore.Code,
                StoreCode = storeCode,
                FactorStatus = factore.FactorStatus.GetEnumName(),
                FinalPrice = factore.FinalPrice,
                taxation = factore.taxation,
                Prepayment = factore.Prepayment,
                TotalPrice = factore.TotalPrice,
                TotalDiscount = factore.TotalDiscount,
                IsFinalPaid = factore.IsFinalPaid,
                FinalFactorPaymentState = factore.FinalFactorPaymentState,
                DeliveryDate = factore.DeliveryDate,
                FirstFactorPaymentState = factore.FirstFactorPaymentState,
                FinalPaymentDate = factore.FinalPaymentDate,
                FinalTracingCode = factore.FinalTracingCode,
                FirstPaymentDate = factore.FirstPaymentDate,
                FirstTracingCode = factore.FirstTracingCode,
                IsFirstPaid = factore.IsFirstPaid,
                CreateFactorDetailsDtos = factore.FactorDetails.Select(x => new CreateFactorDetailsDto
                {
                    Name = x.Name,
                    Description = x.Description,
                    Count = x.Count,
                    Discount = x.Discount,
                    Price = x.Price,
                    Packaging = x.Packaging
                }).ToList()

            };
        }

        #endregion

        #region calculate

        private async Task CalculateFactor(Guid factorCode)
        {
            var factor = await _factorRepository.GetQuery().Include(x => x.FactorDetails).AsQueryable()
                .SingleOrDefaultAsync(x => x.Code == factorCode);

            foreach (var factorDetails in factor.FactorDetails)
            {
                factor.TotalPrice += factorDetails.Price * factorDetails.Count;
                factor.TotalDiscount += factorDetails.Discount * factorDetails.Count;
            }

            factor.FinalPrice = factor.TotalPrice - factor.TotalDiscount;
            if (factor.taxation != 0)
                factor.FinalPrice = (long)Math.Ceiling((decimal)(factor.FinalPrice + ((factor.taxation * factor.FinalPrice) / 100)));
            _factorRepository.EditEntity(factor);
            await _factorRepository.SaveChanges();
        }

        #endregion

        #region change state
        public async Task<bool> RejectFactor(Guid factorCode, Guid storeCode)
        {
            var factore = await _factorRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(x => x.Code == factorCode && !x.IsDelete && x.StoreCode == storeCode);
            if (factore == null) return false;
            factore.FactorStatus = FactorStatus.Reject;
            _factorRepository.EditEntity(factore);
            await _factorRepository.SaveChanges();
            return true;
        }

        public async Task<bool> ReadyToFinalPayedFactor(Guid factorCode, Guid storeCode)
        {
            var factore = await _factorRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(x => x.Code == factorCode && !x.IsDelete && x.StoreCode == storeCode && x.Prepayment != 100);
            if (factore == null) return false;
            factore.FactorStatus = FactorStatus.ReadyToFinalPayed;
            _factorRepository.EditEntity(factore);
            await _factorRepository.SaveChanges();
            return true;
        }

        public async Task<bool> DeliveredFactor(Guid factorCode, Guid storeCode)
        {
            var factore = await _factorRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(x => x.Code == factorCode && !x.IsDelete && x.StoreCode == storeCode);
            if (factore == null) return false;
            factore.FactorStatus = FactorStatus.Delivered;
            _factorRepository.EditEntity(factore);
            await _factorRepository.SaveChanges();
            return true;
        }

        public async Task<bool> AcceptedFactor(AcceptedFactorDto accepted, Guid factorCode, Guid storeCode)
        {
            try
            {
                var factore = await _factorRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.Code == factorCode && !x.IsDelete && x.StoreCode == storeCode);
                if (factore == null) return false;
                var res = await ExpiredFactor(factore);
                factore.FactorStatus = res ? FactorStatus.Expired : FactorStatus.Accepted;
                factore.FirstFactorPaymentState = accepted.PaymentState;
                factore.FirstPaymentDate = accepted.PaymenyDate;
                factore.FirstTracingCode = accepted.TracingCode;
                _factorRepository.EditEntity(factore);
                await _factorRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ReadyFactor(AcceptedFactorDto? accepted, Guid factorCode, Guid storeCode)
        {
            var factore = await _factorRepository.GetQuery().AsQueryable()
                .Include(x => x.Customer)
                .SingleOrDefaultAsync(x => x.Code == factorCode && !x.IsDelete && x.StoreCode == storeCode);
            if (factore == null) return false;
            if (factore.Prepayment != 100)
            {
                factore.FinalFactorPaymentState = accepted.PaymentState;
                factore.FinalPaymentDate = accepted.PaymenyDate;
                factore.FinalTracingCode = accepted.TracingCode;
            }
            factore.FactorStatus = FactorStatus.Ready;
            _factorRepository.EditEntity(factore);
            await _factorRepository.SaveChanges();
            return true;
        }
        public async Task<bool> ExpiredFactor(Factor factor)
        {
            if (factor.ExpiredDate < DateTime.Now && factor.FactorStatus == FactorStatus.Waiting)
            {
                factor.FactorStatus = FactorStatus.Expired;
                _factorRepository.EditEntity(factor);
                await _factorRepository.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<FilterFactorDto> FilterFactor(FilterFactorDto filter)
        {
            var query = _factorRepository.GetQuery()
                .Include(x => x.Customer)
                .ThenInclude(x => x.StoreData).AsQueryable();

            if (filter.MinFinalPrice != null)
                query = query.Where(x => !x.IsDelete && x.FinalPrice >= filter.MinFinalPrice);

            if (filter.MaxFinalPrice != null)
                query = query.Where(x => !x.IsDelete && x.FinalPrice <= filter.MaxFinalPrice);


            switch (filter.FilterFactorStatus)
            {
                case FilterFactorStatus.All:
                    query = query.Where(x => !x.IsDelete);
                    break;
                case FilterFactorStatus.Accepted:
                    query = query.Where(x => !x.IsDelete && x.FactorStatus == FactorStatus.Accepted);
                    break;
                case FilterFactorStatus.Delivered:
                    query = query.Where(x => !x.IsDelete && x.FactorStatus == FactorStatus.Delivered);
                    break;
                case FilterFactorStatus.Expired:
                    query = query.Where(x => !x.IsDelete && x.FactorStatus == FactorStatus.Expired);
                    break;
                case FilterFactorStatus.Open:
                    query = query.Where(x => !x.IsDelete && x.FactorStatus == FactorStatus.Open);
                    break;
                case FilterFactorStatus.Ready:
                    query = query.Where(x => !x.IsDelete && x.FactorStatus == FactorStatus.Ready);
                    break;
                case FilterFactorStatus.Reject:
                    query = query.Where(x => !x.IsDelete && x.FactorStatus == FactorStatus.Reject);
                    break;
                case FilterFactorStatus.Waiting:
                    query = query.Where(x => !x.IsDelete && x.FactorStatus == FactorStatus.Waiting);
                    break;
            }

            switch (filter.FilterFactorOrder)
            {
                case FilterFactorOrder.CreateDate_Aec:
                    query = query.Where(x => !x.IsDelete).OrderBy(x => x.CreateDate);
                    break;
                case FilterFactorOrder.CreateDate_Des:
                    query = query.Where(x => !x.IsDelete).OrderByDescending(x => x.CreateDate);
                    break;
                case FilterFactorOrder.Price_Asc:
                    query = query.Where(x => !x.IsDelete).OrderBy(x => x.FinalPrice);
                    break;
                case FilterFactorOrder.Price_Des:
                    query = query.Where(x => !x.IsDelete).OrderByDescending(x => x.FinalPrice);
                    break;





            }

            switch (filter.FinalFirstFilterFactorPaymentState)
            {
                case FilterFactorPaymentState.All:
                    query = query.Where(x => !x.IsDelete);
                    break;
                case FilterFactorPaymentState.BankCheck:
                    query = query.Where(x => !x.IsDelete && x.FinalFactorPaymentState == FactorPaymentState.BankCheck);
                    break;
                case FilterFactorPaymentState.BankCreditCard:
                    query = query.Where(x => !x.IsDelete && x.FinalFactorPaymentState == FactorPaymentState.BankCreditCard);
                    break;
                case FilterFactorPaymentState.Portal:
                    query = query.Where(x => !x.IsDelete && x.FinalFactorPaymentState == FactorPaymentState.Portal);
                    break;

            }
            
            switch (filter.FirstFilterFactorPaymentState)
            {
                case FilterFactorPaymentState.All:
                    query = query.Where(x => !x.IsDelete);
                    break;
                case FilterFactorPaymentState.BankCheck:
                    query = query.Where(x => !x.IsDelete && x.FirstFactorPaymentState == FactorPaymentState.BankCheck);
                    break;
                case FilterFactorPaymentState.BankCreditCard:
                    query = query.Where(x => !x.IsDelete && x.FirstFactorPaymentState == FactorPaymentState.BankCreditCard);
                    break;
                case FilterFactorPaymentState.Portal:
                    query = query.Where(x => !x.IsDelete && x.FirstFactorPaymentState == FactorPaymentState.Portal);
                    break;

            }

            if (filter.Name != null)
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%") && !x.IsDelete);

            if (filter.Prepayment != null)
                query = query.Where(x => x.Prepayment == filter.Prepayment && !x.IsDelete);

            if (filter.CustomerCode != null)
                query = query.Where(x => !x.IsDelete && x.Customer.UniqueCode == filter.CustomerCode);
            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntities, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetProduct(allEntities).SetPaging(pager);
        }

        #endregion



        #region disposs

        public async ValueTask DisposeAsync()
        {
            await _factorDetailsRepository.DisposeAsync();
            await _factorRepository.DisposeAsync();
            await _customerRepository.DisposeAsync();
        }

        #endregion

    }
}
