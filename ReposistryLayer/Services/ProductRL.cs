using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using ReposistryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ReposistryLayer.Services
{
    public class ProductRL : IProductRL
    {

        public SqlConnection connection;
        private readonly IConfiguration configuration;
        
        public ProductRL(IConfiguration configuration)
        {
            this.configuration = configuration;
            connection = new SqlConnection(this.configuration.GetConnectionString("OreoContext"));
        }


        public List<Product> GetALLProducts()
        {
            List<Product> productList = new List<Product>();
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spGetAllProducts", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    this.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.ProductId = reader.GetInt32(0);
                            product.ProductName = reader.GetString(1);
                            product.ActualPrice = reader.GetDouble(2);
                            product.DiscountPrice = reader.GetDouble(3);
                            product.ProductQuantity = reader.GetInt32(4);
                            product.ProductImage = reader.GetString(5);
                            product.AddedToCart = reader.GetBoolean(6);
                            productList.Add(product);

                        }
                    }
                    
                }
                return productList;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
