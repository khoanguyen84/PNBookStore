using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Cart
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public BookItem Book { get; set; }
    }
}
