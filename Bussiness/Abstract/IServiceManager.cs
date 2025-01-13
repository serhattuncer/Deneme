using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IServiceManager
    {
        IServiceBook book { get; }
        IAuthorService author { get; }
        IPublishingHouseService publishingHouse { get; }
        IUserService users { get; }
        IRoleService role { get; }
        IClaimService claim { get; }
       
    }
}
