using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsShoppingCard.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage ="Minimum Length is 2 Characters")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required, MinLength(50, ErrorMessage ="You have to Enter at Least 50 Characters")]
        public string Description { get; set; }
        [Column(TypeName ="decimal(18, 2)")]
        public decimal Price { get; set; }
        [Display(Name ="Product Image")]
        public string Image {  get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [NotMapped]
        public IFormFile ImageUpload { get; set; }
    }
}
