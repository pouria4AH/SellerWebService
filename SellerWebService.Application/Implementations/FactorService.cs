using System.ComponentModel.DataAnnotations;
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
        public async Task<Guid> CreateBlankFactor(CreateFactorDto factor)
        {
            try
            {
                var user = await _userRepository.GetEntityById(factor.UserId);
                if (user == null) return Guid.Empty;
                var newFactor = new Factor
                {
                    UserId = user.Id,
                    Code = Guid.NewGuid(),
                    Name = factor.Name,
                    Description = factor.Description,
                    Prepayment = factor.Prepayment,
                    DeliveryDate = factor.DeliveryDate,
                    FactorStatus = FactorStatus.Open
                };
                if (factor.taxation != null || factor.taxation != 0) newFactor.taxation = factor.taxation;
                await _factorRepository.AddEntity(newFactor);
                await _factorDetailsRepository.SaveChanges();
                return newFactor.Code;
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> CreateFactorDetails(CreateFactorDetailsDto factorDetails)
        {
            try
            {
                var factor = await _factorRepository.GetEntityById(factorDetails.FactorId);
                if (factor == null) return false;
                var newDetails = new FactorDetails
                {
                    FactorId = factor.Id,
                    Name = factorDetails.Name,
                    Description = factorDetails.Description,
                    Count = factorDetails.Count,
                    Discount = factorDetails.Discount,
                    Packaging = factorDetails.Packaging,
                    Price = factorDetails.Price
                };
                await _factorDetailsRepository.AddEntity(newDetails);
                await _factorDetailsRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
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
