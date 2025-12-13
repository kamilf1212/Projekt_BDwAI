using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_BDwAI.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        [Column(TypeName = "nvarchar(13)")]
        public string ISBN { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
