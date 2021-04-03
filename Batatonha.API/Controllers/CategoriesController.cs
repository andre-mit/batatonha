using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Batatonha.API.Contexts;
using Batatonha.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Batatonha.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> DetailsAsync(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x=>x.Id==id);
            return Ok(category);
        }

        [HttpPost("{category}")]
        public async Task<IActionResult> CreateAsync(string category)
        {
            await _context.Categories.AddAsync(new Category { Name = category });
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRangeAsync(List<string> categoryNames)
        {
            List<Category> categories = new List<Category>();
            categoryNames.ForEach(x =>
            {
                categories.Add(new Category { Name = x });
            });
            await _context.Categories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{category}/{letter}")]
        public async Task<ActionResult> GetWordsAsync([FromRoute]string category, [FromRoute] string letter)
        {
            var word = await _context.Words.Where(
                    x => x.Name
                        .ToLower().StartsWith(letter.ToLower()) &&
                    x.Category.Name.ToLower().Contains(category.ToLower()))
                .FirstOrDefaultAsync();
            return Ok(word);
        }
    }
}
