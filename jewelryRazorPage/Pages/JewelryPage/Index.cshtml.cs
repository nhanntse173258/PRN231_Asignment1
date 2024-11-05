using BOs.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace jewelryRazorPage.Pages.JewelryPage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<SilverJewelryDto> SilverJewelry { get; set; } = new List<SilverJewelryDto>();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
               return RedirectToPage("/AuthPage/Login");
            }
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                using (var response = await _httpClient.GetAsync("http://localhost:5294/api/Jewelry/all-jewelry"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        SilverJewelry = JsonConvert.DeserializeObject<List<SilverJewelryDto>>(apiResponse) ?? new List<SilverJewelryDto>();
                    }
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
    }
}
