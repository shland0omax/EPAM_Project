using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO.Models;
using DAL.Interface;

namespace BLL.Services
{
    public class NewsService:Service<NewsEntity, DalNews>, INewsService
    {
        private readonly IUnitOfWork uow;

        public NewsService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.News)
        {
            uow = unitOfWork;
        }

        public IEnumerable<NewsEntity> GetLastNews(int count)
        {
            if (count < 1) throw new ArgumentException(nameof(count));
            return uow.News.GetAll().OrderBy(n => n.PublicationDate).Take(count).Select(Mapper.ToBll);
        }
    }
}