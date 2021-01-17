using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Oreo_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        IConfiguration configuration;

        public AdminController(IAdminBL adminBL,IConfiguration configuration)
        {
            this.adminBL = adminBL;
            this.configuration = configuration;
        }

        [HttpPost("AdminRegister")]
        public IActionResult AdminRegister(AdminRegistration adminRegistration)
        {
            try
            {
                if (this.adminBL.AdminRegister(adminRegistration))
                {
                    return this.Ok(new { success = true, Message = "Admin record added successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new { success = false, Message = "Admin record is not added " });
                }
            }
            catch (Exception exception)
            {
                if (exception != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { success = false, ErrorMessage = "Cannot insert duplicate Email values." });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = exception.Message });
                }

            }
        }

        [HttpPost("AdminLogin")]
        public ActionResult UserLogin(AdminLogin login)
        {
            try
            {
                var result = this.adminBL.AdminLogin(login);
                if (result != null)
                {
                    //string token = GenrateJWTToken(result.Email, result.UserId);
                    return this.Ok(new
                    {
                        success = true,
                        Message = "Admin login successfully",
                        Data = result,
                        //Token = token
                    });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "Admin login unsuccessfully" });
                }
            }
            catch (Exception e)
            {

                return this.BadRequest(new { success = false, Message = e.Message });

            }
        }
    }
}