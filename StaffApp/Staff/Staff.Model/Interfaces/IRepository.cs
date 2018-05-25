using System.Collections.Generic;
using System.Linq;
using Staff.Model.Entities;

namespace Staff.Model.Interfaces
{
    public interface IEntityWithKey<TKey>
    {
        TKey EntityKey { get; }
    }

    public interface IRepositoryWithKey<TKey, TEntity>
        where TEntity : IEntityWithKey<TKey>
    {
        TEntity Find(TKey key);
    }

    public interface IUpdateableRepository<TEntity>
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }

    public interface IQueryableRepository<TEntity>
    {
        IQueryable<TEntity> AsQueryable();
    }

    public interface IRepository<TKey, TEntity> :
         IQueryableRepository<TEntity>
        , IRepositoryWithKey<TKey, TEntity>
        , IUpdateableRepository<TEntity>
        where TEntity : IEntityWithKey<TKey>
    {
    }
}
