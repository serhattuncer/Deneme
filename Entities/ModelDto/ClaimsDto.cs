using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class ClaimsDto
    {
        public int Claims_Id { get; set; }
        public string? ClaimsName { get; set; }
        public string ClaimType { get; set; }
    }
}
