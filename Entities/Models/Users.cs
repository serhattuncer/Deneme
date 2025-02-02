using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Users:BaseEntity
    {
        [Key]
        public int User_Id { get; set; }
        public string? UserName { get; set; }
        public string? NameSurName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled {  get; set; }
        public DateTimeOffset? LockoutEnd {  get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        
    }
}
