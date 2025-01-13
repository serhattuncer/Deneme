using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IRepositoryBook book { get; }
        IRepositoryAuthor author { get; }
        IRepositoryPublishingHouse Publishinghouse { get; }
        IRepositoryUsers User { get; }
        IRepositoryRoles Role { get; }
        IRepositoryClaims Claim { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}
