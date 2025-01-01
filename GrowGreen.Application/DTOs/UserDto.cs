using GrowGreen.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowGreen.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public String Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

}
