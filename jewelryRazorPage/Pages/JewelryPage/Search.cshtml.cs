using BOs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Common;

namespace jewelryRazorPage.Pages.JewelryPage
{
    public class SearchModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public SearchModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public string SearchTerm { get; set; }

        public List<SilverJewelry> JewelryList { get; set; } = new List<SilverJewelry>();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/AuthPage/Login");
            }

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin" && userRole != "Staff")
            {
                return Unauthorized();
            }

            // If the user is authorized, simply return the page.
            return Page();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                ModelState.AddModelError("", "Please enter a search term.");
                return Page();
            }

            var token = HttpContext.Session.GetString("JWTToken");
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"http://localhost:5294/api/jewelry/search?term={SearchTerm}");

            if (response.IsSuccessStatusCode)
            {
                JewelryList = await response.Content.ReadFromJsonAsync<List<SilverJewelry>>();
                return Page();
            }

            ModelState.AddModelError("", "Error retrieving jewelry data.");
            return Page();
        }
    }
}
