using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_BDwAI.Data;
using Projekt_BDwAI.Models;
using System.Security.Claims;
using Projekt_BDwAI.Areas.Identity.Data;


namespace Projekt_BDwAI.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class LoansController : Controller
    {
        private readonly Project_context _context;
        private readonly UserManager<User> _userManager;

        public LoansController(Project_context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Loans/MyLoans
        public async Task<IActionResult> MyLoans()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Unauthorized();

            var loans = await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.UserId == userId && l.ReturnDate == null)
                .ToListAsync();

            return View(loans);
        }

        // POST: Loans/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Unauthorized();

            var loan = await _context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

            if (loan == null)
                return NotFound();

            if (loan.ReturnDate != null)
                return BadRequest("Książka już została oddana.");

            loan.ReturnDate = DateTime.Now;
            if(loan.Book != null)
                loan.Book.Quantity++;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyLoans));
        }

        // POST: Loans/Create (loan book)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Unauthorized();

            var loan = new Loan
            {
                BookId = bookId,
                UserId = userId,
                LoanDate = DateTime.Now
            };

            var book = await _context.Books.FindAsync(bookId);

            if (book == null)
                return NotFound();

            if (book.Quantity <= 0)
                return BadRequest("Brak dostępnych egzemplarzy.");

            book.Quantity--;
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyLoans));
        }
    }
}
