using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_BDwAI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN musi mieć 13 znaków.")]
        public string ISBN { get; set; }

        [Required]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Ilość książek nie może być mniejsza niż 0.")]
        public int Quantity { get; set; } = 1;
    }
}
