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

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyLoans));
        }

        // POST: Create
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

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyLoans));
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.Id == id);
        }

        // FORM
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form(Loan loan)
        {
            return View("Wynik", loan);
        }

        public IActionResult Wynik(Loan loan)
        {
            return View(loan);
        }
    }
}
