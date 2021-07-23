using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(250)]
        public string  CategoryName { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
