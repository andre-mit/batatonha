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
    public class WordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{letter}")]
        public async Task<ActionResult> IndexAsync(char letter, [FromQuery] string categories)
        {
            List<Category> filtered = new List<Category>();
            letter = char.ToUpper(letter);

            var categoriesSplited = categories.Split(',');

            for (int i = 0; i < categoriesSplited.Length; i++) {
                categoriesSplited[i] = categoriesSplited[i].ToLower().Trim();
            }


            var categoriesAll = await _context.Categories
                .Include(x => x.Words)
                .Where(x => categoriesSplited
                    .Any(c => c == x.Name)
                    )
                .Select(x => new {
                    Category = new ViewModels.CategoryViewModels.ListViewModel {
                        Id = x.Id,
                        Category = x.Name,
                        Words = x.Words.Where(y => y.FirstLetter == letter)
                    .Select(x => new ViewModels.WordViewModels.ListViewModel {
                        Id = x.Id,
                        Word = x.Name
                    }).OrderBy(x=>x.Word).ToList()}
                }).OrderBy(x=> x.Category.Category)
                .ToListAsync();

            return Ok(categoriesAll);
        }

        [HttpPost("{categoryId}")]
        public async Task<ActionResult> CreateAsync(Guid categoryId, string word)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            category.Words.Add(new Word { Name = word, FirstLetter = word[0] });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
