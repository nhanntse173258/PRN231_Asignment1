using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace jewelryRazorPage.Pages.AuthPage
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        
        [BindProperty]
        public string LoginError { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var loginRequest = new { Email, Password };
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5294/api/auth/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                HttpContext.Session.SetString("JWTToken", result.Token); // Store token in session
                HttpContext.Session.SetString("UserRole", result.Role);
                if (result.Role != null && result.Role == "Admin")
                {
                    return RedirectToPage("/JewelryPage/Index");
                }
                else if(result.Role != null && result.Role == "Staff")
                {
                    return RedirectToPage("/JewelryPage/Search");
                }
            }
            LoginError = "You are not allowed to access this function!";
            return Page();
        }

        private class TokenResponse
        {
            public string Token { get; set; }
            public string Role { get; set; }
        }
    }
}
