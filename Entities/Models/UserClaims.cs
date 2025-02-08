using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserClaims:BaseEntity
    {
        [Key]
        public int UserClaims_Id {  get; set; }
        [ForeignKey("Claims_Id")]
        public int Claims_Id { get; set; }
        [ForeignKey("User_Id")]
        public int User_Id { get; set; }
      
    }
}
