using System.ComponentModel.DataAnnotations;

namespace Projekt_BDwAI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
