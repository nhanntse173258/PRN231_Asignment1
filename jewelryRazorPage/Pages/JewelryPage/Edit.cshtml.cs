using BOs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace jewelryRazorPage.Pages.JewelryPage
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;
        public SelectList Categories { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/AuthPage/Login");
            }

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            //add authen header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // Fetch the jewelry details from the API
            var jewel = await _httpClient.GetAsync($"http://localhost:5294/api/jewelry/{id}");
            if (jewel.IsSuccessStatusCode)
            {
                SilverJewelry = await jewel.Content.ReadFromJsonAsync<SilverJewelry>();
                if (SilverJewelry == null)
                {
                    return NotFound();
                }
            }
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5294/api/Category/all-category")
            {
                Headers = { Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token) }
            };

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                Categories = new SelectList(categories, "CategoryId", "CategoryName", SilverJewelry.CategoryId);
            }
            else
            {
                // Handle error
                Categories = new SelectList(new List<Category>(), "CategoryId", "CategoryName", SilverJewelry.CategoryId);
                ModelState.AddModelError("", "Failed to load categories.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/AuthPage/Login");
            }

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // Send the updated jewelry details to the API
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5294/api/jewelry/{SilverJewelry.SilverJewelryId}", SilverJewelry);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", "Error updating the jewelry.");
            return Page();
        }
    }
}
