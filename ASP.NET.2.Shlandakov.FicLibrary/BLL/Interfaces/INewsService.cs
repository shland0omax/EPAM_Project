using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface INewsService : IService<NewsEntity>
    {
        IEnumerable<NewsEntity> GetLastNews(int count);
    }
}
