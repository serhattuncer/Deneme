using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class RoleClaims:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int Claims_Id {  get; set; }
        public int Role_Id { get; set; }
    }
}
