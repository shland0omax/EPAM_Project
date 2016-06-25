using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FicLibraryMvcPL.Models.BllModels;

namespace FicLibraryMvcPL.Models
{
    public class PagedDataViewModel<T> where T: class
    {
        public IEnumerable<T> Elements { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}