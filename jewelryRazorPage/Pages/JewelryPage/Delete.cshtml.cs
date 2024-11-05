using BOs.Dtos;
using BOs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace jewelryRazorPage.Pages.JewelryPage
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public DeleteModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public SilverJewelryDto SilverJewelry { get; set; } = new SilverJewelryDto();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/AuthPage/Login");
            }
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }
                var response = await _httpClient.GetAsync($"http://localhost:5294/api/jewelry/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    SilverJewelry = JsonConvert.DeserializeObject<SilverJewelryDto>(apiResponse) ?? new SilverJewelryDto();
                    return Page();
                }
                TempData["ErrorMessage"] = "Jewelry item not found.";
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
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id)){ return NotFound(); }
            var token = HttpContext.Session.GetString("JWTToken");
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"http://localhost:5294/api/jewelry/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Jewelry item deleted successfully."; // Optional success message
                return RedirectToPage("./Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                TempData["ErrorMessage"] = "Jewelry item not found."; // Optional error message for not found
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to delete the jewelry item. Please try again later."; // General error message
            }
            return RedirectToPage("./Index");
        }
    }
}
