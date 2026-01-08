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
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Autor")]
        public int AuthorId { get; set; }
        [Display(Name = "Autor")]
        public Author? Author { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN musi mieć 13 znaków.")]
        public string ISBN { get; set; }

        [Required]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategoria")]
        public Category? Category { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Ilość książek nie może być mniejsza niż 0.")]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; } = 1;
    }
}
