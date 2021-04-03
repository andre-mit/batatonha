using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Batatonha.API.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Word> Words { get; set; } = new List<Word>();
    }
}
