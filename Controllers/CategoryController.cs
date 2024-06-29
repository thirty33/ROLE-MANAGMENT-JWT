using ApiPeople.Models;
using ApiPeople.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeople.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService CategoryService;

        public CategoryController(ICategoryService service)
        {
            CategoryService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(CategoryService.Get());
        }


        [HttpPost]
        public IActionResult Post([FromBody] Category Category)
        {
            CategoryService.Save(Category);
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Category Category)
        {
            CategoryService.Update(id, Category);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            CategoryService.Delete(id);
            return Ok();
        }
    }
}
