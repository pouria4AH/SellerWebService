using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Presell;
using SellerWebService.DataLayer.Entities.TempV2;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class PresellService : IPresellService
    {
        #region ctor
        private readonly IGenericRepository<Presell> _presellRepository;

        public PresellService(IGenericRepository<Presell> presellRepository)
        {
            _presellRepository = presellRepository;
        }

        #endregion

        public async Task<presellResult> Create(CreatePresellDto presell)
        {
            try
            {
                var c = await _presellRepository.GetQuery().AsQueryable().AnyAsync(x =>
                    !x.IsDelete && (x.Mobile == presell.Mobile || x.Phone == presell.Phone));
                if (c) return presellResult.Exists;

                var newPresell = new Presell
                {
                    CompanyName = presell.CompanyName,
                    Mobile = presell.Mobile,
                    FirstName = presell.FirstName,
                    LastName = presell.LastName
                };
                if (presell.Email != null) newPresell.Email = presell.Email;
                if (presell.Phone != null) newPresell.Phone = presell.Phone;
                await _presellRepository.AddEntity(newPresell);
                await _presellRepository.SaveChanges();
                return presellResult.Success;
            }
            catch (Exception e)
            {
                return presellResult.Error;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_presellRepository != null) await _presellRepository.DisposeAsync();
        }

    }
}
