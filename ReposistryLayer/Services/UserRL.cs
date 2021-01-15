using CommonLayer.Models;
using ReposistryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ReposistryLayer.Services
{
    public class UserRL : IUserRL
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=oreo;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public bool Register(UserRegistration register)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("registerUser", this.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Fullname", register.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", register.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", register.Password);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", register.MobileNumber);
                    this.connection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
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
