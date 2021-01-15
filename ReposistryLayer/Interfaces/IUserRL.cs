using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposistryLayer.Interfaces
{
    public interface IUserRL
    {
        bool Register(UserRegistration register);
    }
}
