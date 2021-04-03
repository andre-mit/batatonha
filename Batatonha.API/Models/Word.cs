using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Batatonha.API.Models
{
    public class Word
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public char FirstLetter { get; set; }
        public Category Category { get; set; }
    }
}
