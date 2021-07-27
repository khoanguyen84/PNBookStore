using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Book
{
    public class ListBook
    {
        public Entities.Category Category { get; set; }
        public Pagination Pagination { get; set; }
    }
}
