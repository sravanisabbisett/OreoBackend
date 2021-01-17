using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposistryLayer.Interfaces
{
    public interface IProductRL
    {
        List<Product> GetALLProducts();
    }
}
