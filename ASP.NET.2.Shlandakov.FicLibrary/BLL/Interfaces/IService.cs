using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BLL.Interfaces;

namespace BLL.Interfaces
{
    public interface IService<T> where T: class, IBllEntity
    {
        IEnumerable<T> GetAllEntities();
        T GetEntityById(int key);
        void CreateEntity(T bllEntity);
        void DeleteEntity(T bllEntity);
        void UpdateEntity(T bllEntity);
    }
}