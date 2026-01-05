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
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN musi mieć 13 znaków.")]
        public int ISBN { get; set; }

        [Required]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
