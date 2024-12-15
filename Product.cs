using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BullyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; } 
        [Range(0,1000)]
        [Required]
        [Display(Name =("List price"))]
        public double Listprice { get; set; }
        [Range(0, 1000)]
        [Required]
        [Display(Name = ("price for 1-50"))]
        public double price { get; set; }
        [Range(0, 1000)]
        [Required]
        [Display(Name = ("price for 50+"))]
        public double price50 { get; set; }
        [Range(0, 1000)]
        [Required]
        [Display(Name = (" price for 100+"))]
       
        public double price100 { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
