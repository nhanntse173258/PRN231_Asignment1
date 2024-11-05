using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Entities;
using DAO;
using Newtonsoft.Json;

namespace jewelryRazorPage.Pages.CatePage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            List<Category> result = new List<Category>();
            using (HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5294/api/Category/all-category"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
            }

            Category = result;
        }
    }
}
