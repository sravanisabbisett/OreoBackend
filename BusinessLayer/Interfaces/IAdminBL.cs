using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAdminBL
    {
        bool AdminRegister(AdminRegistration registration);
        AdminRegistration AdminLogin(AdminLogin adminLogin);
    }
}
