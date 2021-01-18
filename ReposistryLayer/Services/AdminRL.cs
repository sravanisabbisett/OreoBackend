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
    public class AdminRL : IAdminRL
    {
        public SqlConnection connection;

        public readonly IConfiguration configuration;
        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
            connection = new SqlConnection(this.configuration.GetConnectionString("OreoContext"));
        }

        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        public bool AdminRegister(AdminRegistration registration)
        {
            try
            {
                using (this.connection)
                {
                    var password = Encryptdata(registration.Password);
                    SqlCommand command = new SqlCommand("spAdminRegister", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FullName", registration.FullName);
                    command.Parameters.AddWithValue("@Email", registration.Email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@MobileNumber", registration.MobileNumber);
                    command.Parameters.AddWithValue("@Role", "Admin");
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    if (result > 0)
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

        public AdminResponse AdminLogin(AdminLogin adminLogin)
        {
            AdminResponse adminRegistration = new AdminResponse();
            try
            {
                using (this.connection)
                {
                    var password = Encryptdata(adminLogin.Password);
                    SqlCommand command = new SqlCommand("spAdminLogin", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", adminLogin.Email);
                    command.Parameters.AddWithValue("@Password", password);
                    this.connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            adminRegistration.AdminId = dataReader.GetInt32(0);
                            adminRegistration.FullName = dataReader.GetString(1);
                            adminRegistration.Email = dataReader.GetString(2);
                            adminRegistration.Password = dataReader.GetString(3);
                            adminRegistration.MobileNumber = dataReader.GetString(4);
                            adminRegistration.Role = dataReader.GetString(5);
                        }
                        return adminRegistration;
                    }
                }
                return null;
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
