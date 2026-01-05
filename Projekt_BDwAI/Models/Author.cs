using System.ComponentModel.DataAnnotations;

namespace Projekt_BDwAI.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię i nazwisko autora jest wymagane")]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Biography { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
