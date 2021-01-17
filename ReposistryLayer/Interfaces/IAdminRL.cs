using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposistryLayer.Interfaces
{
    public interface IAdminRL
    {
        bool AdminRegister(AdminRegistration registration);
        AdminRegistration AdminLogin(AdminLogin adminLogin);
    }
}
