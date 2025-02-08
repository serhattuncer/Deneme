using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserRoles:BaseEntity
    {
        [Key]
        public int UserRole_Id { get; set; }
        [ForeignKey("User_Id")]
        public int User_Id { get; set; }
        [ForeignKey("Role_Id")]
        public int Role_Id { get; set; }
    }
}
