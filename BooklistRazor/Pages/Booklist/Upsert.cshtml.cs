using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooklistRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooklistRazor.Pages.Booklist
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        [BindProperty]
        public Book Book { get; set; }

        public UpsertModel(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id != null)
            {
                Book = await _db.Book.FindAsync(id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    await _db.Book.AddAsync(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }
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