using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.Mappers;
using DAL.Interface;
using ORM;

namespace DAL.Concrete
{
    public class Repository<T,U>: IRepository<T>
        where T: class, IDalEntity
        where U: class, IOrmEntity
    {
        private readonly FicLibraryDB context;

        public Repository(FicLibraryDB context)
        {
            this.context = context;
        } 

        public IEnumerable<T> GetAll()
        {
            var temp = new List<T>();
            foreach (var t in context.Set<U>().Select(e => e))
            {
                temp.Add(Mapper.ToDal(t) as T);
            }
            return temp;
        }

        public T GetById(int key)
        {
            return Mapper.ToDal(context.Set<U>().FirstOrDefault(e => e.Id == key)) as T;
        }

        public T GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                Expression<Func<U, T>> mapper = ExpressionMapper.ToDal((dynamic)context.Set<U>());
                return context.Set<U>().Select(mapper).FirstOrDefault(predicate);
            }
            throw new ArgumentNullException(nameof(predicate), "Predicate is null");
        }

        public IEnumerable<T> GetManyByPredicate(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                Expression<Func<U, T>> mapper = ExpressionMapper.ToDal((dynamic)context.Set<U>());
                return context.Set<U>().Select(mapper).Where(predicate);
            }
            throw new ArgumentNullException(nameof(predicate), "Predicate is null");
        }

        public void Create(T entity)
        {
            var e = context.Set<U>().Add(Mapper.ToOrm(entity) as U);
        }

        public void Delete(T e)
        {
            var ormEntity = context.Set<U>().Single(oe => oe.Id == e.Id);
            context.Set<U>().Remove(ormEntity);
        }

        public void Update(T entity)
        {
            var ormEntity = context.Set<U>().Find(entity.Id);
            context.Entry(ormEntity).CurrentValues.SetValues(Mapper.ToOrm(entity));
        }
    }
}