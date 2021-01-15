using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class UserLogin
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
