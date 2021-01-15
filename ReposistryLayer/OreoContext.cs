using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposistryLayer
{
    public class OreoContext:DbContext
    {
        public OreoContext(DbContextOptions options)
            : base(options)
        {
        }
        DbSet<UserRegistration> userRegistration { get; set; }
        DbSet<UserLogin> userLogin { get; set; }
    }
}
