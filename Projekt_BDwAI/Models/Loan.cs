using Projekt_BDwAI.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Projekt_BDwAI.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Książka")]
        public int BookId { get; set; }
        [Display(Name = "Książka")]
        public Book? Book { get; set; }

        [Required]
        [Display(Name = "Użytkownik")]
        public string UserId { get; set; }
        [Display(Name = "Użytkownik")]
        public User? User { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data wypożyczenia")]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Data zwrotu")]
        public DateTime? ReturnDate { get; set; }
    }
}
