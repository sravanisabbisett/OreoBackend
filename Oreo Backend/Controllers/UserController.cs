﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Oreo_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        IConfiguration configuration;

        public UserController(IUserBL userBL,IConfiguration configuration)
        {
            this.userBL = userBL;
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult RegisterUser(UserRegistration user)
        {
            try
            {
                if (this.userBL.userRegister(user))
                {
                    return this.Ok(new { Success = true, Message = "user record added successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { Success = false, Message = "user record is not added " });
                }
            }
            catch (Exception exception)
            {
                if (exception != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { Success = false, ErrorMessage = "Cannot insert duplicate Email values." });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = exception.Message });
                }

            }
        }

        [HttpPost("Login")]
        public ActionResult UserLogin(UserLogin login)
        {
            try
            {
                var result = this.userBL.Login(login);
                if (result != null)
                {
                    string token = GenrateJWTToken(result.Email, result.UserId,result.Role);
                    return this.Ok(new
                    {
                        success = true,
                        Message = "User login successfully",
                        Data = result,
                        Token = token
                    });
                }
                else
                {
                    return this.NotFound(new { Success = false, Message = "User login unsuccessfully" });
                }
            }
            catch (Exception e)
            {

                return this.BadRequest(new { Success = false, Message = e.Message });

            }
        }


        private string GenrateJWTToken(string email, long id,string Role)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            string userId = Convert.ToString(id);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),
                            new Claim(ClaimTypes.Role, Role),
                            new Claim("id",userId),

                        };
            var tokenOptionOne = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }
    }
}