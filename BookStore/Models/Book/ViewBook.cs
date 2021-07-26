using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Book
{
    public class ViewBook
    {
        public int BookId { get; set; }
        [Required(ErrorMessage ="The book name is required")]
        [MaxLength(500)]
        public string BookName { get; set; }
        [Required(ErrorMessage = "The authors is required")]
        [MaxLength(300)]
        public string Authors { get; set; }
        [Required(ErrorMessage = "The publish year is required")]
        [Range(minimum: 1900, maximum: 2021)]
        public int PublishYear { get; set; }
        [Required(ErrorMessage = "The price is required")]
        public int Price { get; set; }
        [Required(ErrorMessage = "The quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "The description is required")]
        [MaxLength(1000)]
        public string Description { get; set; }
        public string ExistPhoto { get; set; }
        public int CategoryId { get; set; }
        public Entities.Category Category { get; set; }
    }
}
