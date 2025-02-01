using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserRoles:BaseEntity
    {
        [Key]
        public int UserRole_Id { get; set; }
        public int User_Id { get; set; }
        public int Role_Id { get; set; }
    }
}
