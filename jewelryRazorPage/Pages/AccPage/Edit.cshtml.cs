using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs.Entities;
using DAO;

namespace jewelryRazorPage.Pages.AccPage
{
    public class EditModel : PageModel
    {
        private readonly DAO.SilverJewelry2023DbContext _context;

        public EditModel(DAO.SilverJewelry2023DbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BranchAccount BranchAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchaccount =  await _context.BranchAccounts.FirstOrDefaultAsync(m => m.AccountId == id);
            if (branchaccount == null)
            {
                return NotFound();
            }
            BranchAccount = branchaccount;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BranchAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchAccountExists(BranchAccount.AccountId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BranchAccountExists(int id)
        {
            return _context.BranchAccounts.Any(e => e.AccountId == id);
        }
    }
}
