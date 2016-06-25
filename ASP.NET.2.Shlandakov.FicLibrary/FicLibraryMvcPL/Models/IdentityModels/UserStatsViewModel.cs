using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FicLibraryMvcPL.Models.IdentityModels
{
    public class UserStatsViewModel
    {
        public int DaysInService { get; set; }
        public int? TotalPublications { get; set; }
        public int TotalComments { get; set; }
        public double? AverageRating { get; set; }
    }
}