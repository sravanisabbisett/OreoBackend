using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class ProductBL:IProductBL
    {
        private readonly IProductRL productRL;

        public ProductBL(IProductRL productRL)
        {
            this.productRL = productRL;
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return this.productRL.GetALLProducts();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool AddProduct(Product product)
        {
            try
            {
                return this.productRL.AddProduct(product);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public string Image(IFormFile file, int productId)
        {
            try
            {
                return this.productRL.Image(file, productId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
