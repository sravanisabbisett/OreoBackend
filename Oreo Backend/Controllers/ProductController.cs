using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Oreo_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBL productBL;
        IConfiguration configuration;

        public ProductController(IProductBL productBL,IConfiguration configuration)
        {
            this.productBL = productBL;
            this.configuration = configuration;
        }


        [HttpGet("GetProducts")]
        [Authorize(Roles ="Admin,User")]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<Product> productList = this.productBL.GetAllProducts();
                if (productList != null)
                 {
                     return this.Ok(new { success = true, Message = "get product details successfully", data = productList });
                 }
                 else
                 {
                   return this.NotFound(new { success = false, Message = "get product details unsuccessfully" });
                 }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }
        

        [HttpPost("AddProduct")]
        [Authorize(Roles ="Admin")]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                if (this.productBL.AddProduct(product))
                {
                    return this.Ok(new { success = true, Message = "Product added successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new { success = false, Message = "product is not added " });
                }
            }
            catch (Exception exception)
            {
                if (exception != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { success = false, ErrorMessage = "Product not added sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = exception.Message });
                }

            }
        }
    }
}