using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Factor;
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
        private readonly IGenericRepository<User> _userRepository;
        public FactorService(IGenericRepository<Factor> factorRepository, IGenericRepository<FactorDetails> factorDetailsRepository, IGenericRepository<User> userRepository)
        {
            _factorRepository = factorRepository;
            _factorDetailsRepository = factorDetailsRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region factor
        //public async Task<Guid> CreateBlankFactor(CreateFactorDto factor)
        //{
        //    try
        //    {
        //        var user = await _userRepository.GetEntityById(factor.UserId);
        //        if (user == null) return Guid.Empty;
        //        var newFactor = new Factor
        //        {
        //            UserId = user.Id,
        //            Code = Guid.NewGuid(),
        //            Name = factor.Name,
        //            Description = factor.Description,
        //            Prepayment = factor.Prepayment,
        //            DeliveryDate = factor.DeliveryDate,
        //            FactorStatus = FactorStatus.Open,
        //            taxation = factor.taxation
        //        };
        //        await _factorRepository.AddEntity(newFactor);
        //        await _factorDetailsRepository.SaveChanges();
        //        return newFactor.Code;
        //    }
        //    catch (Exception e)
        //    {
        //        return Guid.Empty;
        //    }
        //}

        //public async Task<bool> CreateFactorDetails(CreateFactorDetailsDto factorDetails)
        //{
        //    try
        //    {
        //        var factor = await _factorRepository.GetQuery().AsQueryable()
        //            .SingleOrDefaultAsync(x => x.Code == factorDetails.FactorCode);
        //        if (factor == null) return false;
        //        var newDetails = new FactorDetails
        //        {
        //            FactorId = factor.Id,
        //            Name = factorDetails.Name,
        //            Description = factorDetails.Description,
        //            Count = factorDetails.Count,
        //            Discount = factorDetails.Discount,
        //            Packaging = factorDetails.Packaging,
        //            Price = factorDetails.Price
        //        };
        //        await _factorDetailsRepository.AddEntity(newDetails);
        //        await _factorDetailsRepository.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //public async Task<ReadMainFactorDto> GetFinialFactorToConfirm(Guid factorCode)
        //{
        //    var factor = await _factorRepository.GetQuery().Include(x => x.FactorDetails).Include(x => x.User).AsQueryable()
        //        .SingleOrDefaultAsync(x => x.Code == factorCode);
        //    if (factor == null || !factor.FactorDetails.Any()) return null;
        //    await CalculateFactor(factorCode);
        //    return new ReadMainFactorDto
        //    {
        //        Name = factor.Name,
        //        Description = factor.Name,
        //        FinalPrice = factor.FinalPrice,
        //        Code = factor.Code,
        //        Prepayment = factor.Prepayment,
        //        taxation = factor.taxation,
        //        DeliveryDate = factor.DeliveryDate,
        //        FactorStatus = factor.FactorStatus.ToString(),
        //        FirstName = factor.User.FirstName,
        //        LastName = factor.User.LastName,
        //        Mobile = factor.User.Mobile,
        //        UserCode = factor.User.UniqueCode.ToString("N"),
        //        TotalDiscount = factor.TotalDiscount,
        //        TotalPrice = factor.TotalPrice,
        //        CreateFactorDetailsDtos = factor.FactorDetails.Select(x => new CreateFactorDetailsDto
        //        {
        //            Name = x.Name,
        //            Description = x.Description,
        //            Count = x.Count,
        //            Discount = x.Discount,
        //            FactorCode = factor.Code,
        //            Packaging = x.Packaging,
        //            Price = x.Price

        //        }).ToList()

        //    };

        //}

        //public async Task<CreateFactorResult> PublishFactor(Guid factorCode)
        //{
        //    try
        //    {
        //        var factor = await _factorRepository.GetQuery().Include(x => x.FactorDetails).Include(x => x.User).AsQueryable()
        //            .SingleOrDefaultAsync(x => x.Code == factorCode);
        //        if (factor == null || !factor.FactorDetails.Any()) return CreateFactorResult.FactorNotFound;
        //        await CalculateFactor(factorCode);
        //        if(factor.FactorStatus != FactorStatus.Open) return CreateFactorResult.IsAlreadyPublish;
        //        factor.FactorStatus = FactorStatus.Waiting;
        //        await _factorRepository.SaveChanges();
        //        return CreateFactorResult.Success;
        //    }
        //    catch (Exception e)
        //    {
        //        return CreateFactorResult.Error;
        //    }
        //}

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

        #region disposs

        public async ValueTask DisposeAsync()
        {
            await _factorDetailsRepository.DisposeAsync();
            await _factorRepository.DisposeAsync();
        }

        #endregion

    }
}
