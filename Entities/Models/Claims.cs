using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Claims:BaseEntity
    {
        [Key]
        public int Claims_Id { get; set; }
       public string? ClaimsName { get; set; }
        public string ClaimType { get; set; }
    }
}
