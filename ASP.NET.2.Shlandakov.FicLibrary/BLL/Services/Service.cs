using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interfaces;
using DAL.DTO;
using DAL.Interface;
using BLL.Mappers;

namespace BLL.Services
{
    public class Service<T, U>: IService<T>
        where T: class, IBllEntity
        where U: class, IDalEntity
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<U> repository;

        public Service(IUnitOfWork unitOfWork, IRepository<U> repository)
        {
            uow = unitOfWork;
            this.repository = repository;
        } 
        public IEnumerable<T> GetAllEntities()
        {
            return repository.GetAll().Select(entity => (T) Mapper.ToBll(entity));
        }

        public T GetEntityById(int key)
        {
            return Mapper.ToBll(repository.GetById(key)) as T;
        }

        public void CreateEntity(T bllEntity)
        {
            repository.Create(Mapper.ToDal(bllEntity) as U);
            uow.Commit();
        }

        public void DeleteEntity(T bllEntity)
        {
            repository.Delete(Mapper.ToDal(bllEntity) as U);
            uow.Commit();
        }

        public void UpdateEntity(T bllEntity)
        {
            repository.Update(Mapper.ToDal(bllEntity) as U);
            uow.Commit();
        }
    }
}