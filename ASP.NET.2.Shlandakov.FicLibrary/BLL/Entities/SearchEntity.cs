﻿using System;

namespace BLL.Entities
{
    public class SearchEntity
    {
        public string Title { get; set; }
        public bool SearchSubtitles { get; set; }
        public string PublisherLogin { get; set; }
        public DateTime? PublicationDateStart { get; set; }
        public DateTime? PublicationDateFinish { get; set; }
        public double? MinRating { get; set; }
        public double? MaxRating { get; set; }
        public bool IncludeUnrated { get; set; }
    }
}