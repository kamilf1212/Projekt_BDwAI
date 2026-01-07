using Projekt_BDwAI.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Projekt_BDwAI.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        [Required]
        public string UserId { get; set; }
        public User? User { set; get; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }
    }
}
