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

namespace jewelryRazorPage.Pages.AccPage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<BranchAccount> BranchAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            List<BranchAccount> result = new List<BranchAccount>();
            using (HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5294/api/Jewelry/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<BranchAccount>>(apiResponse);
            }

            BranchAccount = result;
        }
    }
}
