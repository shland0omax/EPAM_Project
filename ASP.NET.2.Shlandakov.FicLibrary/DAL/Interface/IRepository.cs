using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.DTO;

namespace DAL.Interface
{
    public interface IRepository<TEntity> where TEntity: IDalEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetManyByPredicate(Expression<Func<TEntity, bool>> predicate);
        void Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity entity);
    }
}