using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Batatonha.API.ViewModels.CategoryViewModels
{
    public class ListViewModel
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public List<WordViewModels.ListViewModel> Words { get; set; }
    }
}
