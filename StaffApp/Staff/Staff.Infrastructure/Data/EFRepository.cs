using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Staff.Model.Entities;
using Staff.Model.Interfaces;

namespace Staff.Infrastructure.Data
{
    public class EFRepository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TEntity : class, IEntityWithKey<TKey>
    {
        private readonly ISpecification<TEntity> _specification;

        public EFRepository(DbContext context, ISpecification<TEntity> specification) : this(context)
        {
            _specification = specification;
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _specification != null && _specification.Criteria != null
                ? _dbContext.Set<TEntity>().Where(_specification.Criteria)
                : _dbContext.Set<TEntity>();
        }

        private static Lazy<Func<TKey, object[]>> _toKey;
        private Func<TKey, object[]> ToKey => _toKey.Value;

        static bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive ||
                new Type[] {
                    typeof(Enum),
                    typeof(String),
                    typeof(Decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetGenericArguments()[0]))
                ;
        }

        private Func<TKey, object[]> KeyToObjectArray()
        {
            if (IsSimpleType(typeof(TKey)))
            {
                return key => new object[] { key };
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private readonly DbContext _dbContext;

        public EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;

            if (_toKey == null)
            {
                lock (_toKey)
                {
                    if (_toKey == null)
                    {
                        _toKey = new Lazy<Func<TKey, object[]>>(
                            () => KeyToObjectArray()
                            , LazyThreadSafetyMode.ExecutionAndPublication
                            );
                    }
                }
            }
        }

        public TEntity Find(TKey key)
        {
            return _dbContext.Set<TEntity>().Find(ToKey(key));
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
