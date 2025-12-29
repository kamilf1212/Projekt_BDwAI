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
        [StringLength(100)]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [Required]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "Nieprawidłowy format ISBN")]
        public string ISBN { get; set; }

        [Required]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
