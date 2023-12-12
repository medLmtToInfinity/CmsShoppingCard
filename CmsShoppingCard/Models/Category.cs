using System.ComponentModel.DataAnnotations;

namespace CmsShoppingCard.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2 Characters")]
        [RegularExpression(@"^[a-zA-Z-]+$", ErrorMessage = "Only Letters are allowed")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }
    }
}