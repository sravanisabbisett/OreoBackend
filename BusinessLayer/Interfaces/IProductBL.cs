using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IProductBL
    {
        List<Product> GetAllProducts();
        bool AddProduct(Product product);
        string Image(IFormFile file, int productId);
    }
}
