using System.ComponentModel.DataAnnotations;

namespace Projekt_BDwAI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Kategoria")]
        public string Name { get; set; }

        [StringLength(1000)]
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
