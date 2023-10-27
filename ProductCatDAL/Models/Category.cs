using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatDAL.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "category name")]
        [MaxLength(255)]
        public string CategoryName { get; set; }

        public virtual ICollection<Product>? Products { get; set; } = new HashSet<Product>();
    }
}
