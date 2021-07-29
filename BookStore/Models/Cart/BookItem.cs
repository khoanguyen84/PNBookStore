using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Cart
{
    public class BookItem
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Authors { get; set; }
        public int PublishYear { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
    }
}
