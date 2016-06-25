using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FicLibraryMvcPL.Models
{
    public enum SortParameter
    {
        Rating, PublicationDate
    }
    public class SearchViewModel
    {
        public string Title { get; set; }
        [Display(Name = "осуществлять поиск по подзаголовкам")]
        public bool SearchSubtitles { get; set; }
        public string PublisherLogin { get; set; }
        public DateTime? PublicationDateStart { get; set; }
        public DateTime? PublicationDateFinish { get; set; }
        public double? MinRating { get; set; }
        public double? MaxRating { get; set; }
        [Display(Name = "включать публикации без оценок")]
        public bool IncludeUnrated { get; set; }
        public SortParameter SortParameter { get; set; }
    }
}