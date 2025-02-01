using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class UsersDto
    {
        public int User_Id { get; set; }
        public string? UserName { get; set; }
        public string? NameSurName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
       
       
    }
}
