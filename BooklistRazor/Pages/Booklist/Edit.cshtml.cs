using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BooklistRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooklistRazor.Pages.Booklist
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        
        [BindProperty]
        public Book Book { get; set; }

        public EditModel(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Book.Update(Book);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}