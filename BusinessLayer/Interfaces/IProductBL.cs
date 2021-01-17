using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IProductBL
    {
        List<Product> GetAllProducts();
    }
}
