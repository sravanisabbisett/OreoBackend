using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public double ActualPrice { get; set; }
        public double DiscountPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductImage { get; set; }
        public bool AddedToCart { get; set; }
    }
}
