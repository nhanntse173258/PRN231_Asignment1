using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Net.Http.Json;
using BOs.Entities;

namespace jewelryRazorPage.Pages.JewelryPage
{
    public class CreateModel : PageModel
    {
        public static readonly Random random = new Random();
        private readonly HttpClient _httpClient;

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("./Login"); // Redirect if not logged in
            }
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5294/api/Category/all-category")
                {
                    Headers = { Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token) }
                };

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                    Categories = new SelectList(categories, "CategoryId", "CategoryName");
                }
                else
                {
                    // Handle error
                    Categories = new SelectList(new List<Category>(), "CategoryId", "CategoryName");
                    ModelState.AddModelError("", "Failed to load categories.");
                }
                return Page();
            }
            else if (userRole == "Staff")
            {
                return Unauthorized();
            }
            else
            {
                return Unauthorized();
            }
        }

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;
        public SelectList Categories { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("./Login"); // Redirect if not logged in
            }
            SilverJewelry.SilverJewelryId = GenerateJewelryId();
            SilverJewelry.CreatedDate = DateTime.Now;
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5294/api/Jewelry")
            {
                Content = JsonContent.Create(SilverJewelry)
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", "Failed to create jewelry.");
            return Page();
        }
        public static string GenerateJewelryId()
        {
            var prefixes = new[] { "SBD2", "SMD2", "SND2" };
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var prefix = prefixes[random.Next(prefixes.Length)];
            var middlePart = $"{letters[random.Next(letters.Length)]}{letters[random.Next(letters.Length)]}{random.Next(10000, 99999)}";
            var decimalPart = $"{random.Next(100, 999)}.000";

            return $"{prefix}{middlePart}.{decimalPart}";

        }
    }
}
