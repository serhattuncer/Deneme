using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserClaims:BaseEntity
    {
        [Key]
        public int UserClaims_Id {  get; set; }
        public int Claims_Id { get; set; }
        public int User_Id { get; set; }
    }
}
