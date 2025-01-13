using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class RoleClaimsDto
    {
        public int RoleClaims_Id { get; set; }
        public int Claims_Id { get; set; }
        public int Role_Id { get; set; }
    }
}
