using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public bool AdminRegister(AdminRegistration registration)
        {
            try
            {
                return this.adminRL.AdminRegister(registration);
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public AdminResponse AdminLogin(AdminLogin adminLogin)
        {
            try
            {
                return this.adminRL.AdminLogin(adminLogin);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
