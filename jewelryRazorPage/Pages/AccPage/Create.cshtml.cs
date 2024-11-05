﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Entities;
using DAO;

namespace jewelryRazorPage.Pages.AccPage
{
    public class CreateModel : PageModel
    {
        private readonly DAO.SilverJewelry2023DbContext _context;

        public CreateModel(DAO.SilverJewelry2023DbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BranchAccount BranchAccount { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BranchAccounts.Add(BranchAccount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
