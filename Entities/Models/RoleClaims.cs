using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class RoleClaims:BaseEntity
    {
        [Key]
        public int RoleClaims_Id { get; set; }
        [ForeignKey("Claims_Id")]
        public int Claims_Id {  get; set; }
        [ForeignKey("Role_Id")]
        public int Role_Id { get; set; }
    }
}
