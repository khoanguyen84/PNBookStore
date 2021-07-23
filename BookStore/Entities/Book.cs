using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [MaxLength(500)]
        public string BookName { get; set; }
        [Required]
        [MaxLength(300)]
        public string Authors { get; set; }
        [Required]
        public int PublishYear { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public bool  IsDeleted { get; set; }
        [Required]
        [MaxLength(300)]
        public string Photo { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
