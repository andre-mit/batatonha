using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Batatonha.API.Models;

namespace Batatonha.API.ViewModels.WordViewModels
{
    public class ListViewModel
    {
        public Guid Id { get; set; }
        public string Word { get; set; }

        public static implicit operator ListViewModel(Word word)
        {
            return new ListViewModel {
                Id = word.Id,
                Word = word.Name
            };
        }
    }
}
