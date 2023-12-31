﻿using System.ComponentModel.DataAnnotations;

namespace CmsShoppingCard.Models
{
    public class Page
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        public string Content { get; set; }
        public int Sorting { get; set; }
    }
}
