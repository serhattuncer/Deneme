using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Roles:BaseEntity
    {
        [Key]
        public int Role_Id { get; set; }
        public string? Name { get; set; }
        
    }
}
