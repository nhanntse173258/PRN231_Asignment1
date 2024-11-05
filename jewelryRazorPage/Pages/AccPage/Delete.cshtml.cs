using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Entities;
using DAO;

namespace jewelryRazorPage.Pages.AccPage
{
    public class DeleteModel : PageModel
    {
        private readonly DAO.SilverJewelry2023DbContext _context;

        public DeleteModel(DAO.SilverJewelry2023DbContext context)
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

            var branchaccount = await _context.BranchAccounts.FirstOrDefaultAsync(m => m.AccountId == id);

            if (branchaccount == null)
            {
                return NotFound();
            }
            else
            {
                BranchAccount = branchaccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchaccount = await _context.BranchAccounts.FindAsync(id);
            if (branchaccount != null)
            {
                BranchAccount = branchaccount;
                _context.BranchAccounts.Remove(BranchAccount);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
