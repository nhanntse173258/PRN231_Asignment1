using BOs.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace PRN_PE_Fall23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IJewelryRepo jewelryRepo;
        private readonly IAccountRepo accountRepo;
        private readonly ICategoryRepo categoryRepo;
        public CategoryController(
            IJewelryRepo jewelryRepo, 
            IAccountRepo accountRepo, 
            ICategoryRepo categoryRepo)
        {
            this.jewelryRepo = jewelryRepo;
            this.accountRepo = accountRepo;
            this.categoryRepo = categoryRepo;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("all-category")]
        public ActionResult<List<Category>> Get()
        {
            var result = categoryRepo.GetCategories();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("id")]
        public IActionResult GetById([FromRoute] string id)
        {
            var result = categoryRepo.GetCategory(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
