using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IProductRL
    {
        List<Product> GetALLProducts();
        bool AddProduct(Product product);
        string Image(IFormFile file,int productId);
    }
}
