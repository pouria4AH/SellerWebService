using _0_framework.Entities;
using Microsoft.EntityFrameworkCore;
using SellerWebService.DataLayer.Context;

namespace SellerWebService.DataLayer.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SellerContext _dbSellerContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(SellerContext dbSellerContext, DbSet<TEntity> dbSet)
        {
            _dbSellerContext = dbSellerContext;
            _dbSet = dbSet;
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task AddEntity(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeEntities(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await AddEntity(entity);
            }
        }

        public async Task<TEntity> GetEntityById(long entityId)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id == entityId);
        }

        public void EditEntity(TEntity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        public void DeleteEntity(TEntity entity)
        {
            entity.IsDelete = true;
            EditEntity(entity);
        }

        public async Task DeleteEntity(long entityId)
        {
            TEntity entity = await GetEntityById(entityId);
            if (entity != null) DeleteEntity(entity);
        }

        public void DeletePermanent(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeletePermanentEntities(List<TEntity> entities)
        {
            _dbSellerContext.RemoveRange(entities);
        }

        public async Task DeletePermanent(long entityId)
        {
            TEntity entity = await GetEntityById(entityId);
            if (entity != null) DeletePermanent(entity);
        }

        public async Task SaveChanges()
        {
            await _dbSellerContext.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
            if (_dbSellerContext != null)
            {
                await _dbSellerContext.DisposeAsync();
            }
        }

    }

}
