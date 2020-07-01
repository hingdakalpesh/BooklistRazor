using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooklistRazor.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BooklistRazor.Pages.Booklist
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        public IndexModel(ApplicationDBContext db)
        {
            _db = db;
        }
        public IEnumerable<Book> books { get; set; }
        public async Task OnGet()
        {
            books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}