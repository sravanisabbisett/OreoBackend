using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAdminRL
    {
        bool AdminRegister(AdminRegistration registration);
        AdminResponse AdminLogin(AdminLogin adminLogin);
    }
}
