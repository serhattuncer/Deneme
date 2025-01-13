﻿using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IRepositoryBook> _book;
        private readonly Lazy<IRepositoryAuthor> _author;
        private readonly Lazy<IRepositoryPublishingHouse> _publishingHouse;
        private readonly Lazy<IRepositoryUsers> _users;
        private readonly Lazy<IRepositoryRoles> _roles;
        private readonly Lazy<IRepositoryClaims> _claims;
        public IRepositoryBook book => _book.Value;
        public IRepositoryAuthor author => _author.Value;
        public IRepositoryPublishingHouse Publishinghouse => _publishingHouse.Value;
        public IRepositoryUsers User => _users.Value;
        public IRepositoryRoles Role => _roles.Value;
        public IRepositoryClaims Claim => _claims.Value;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
            _book = new Lazy<IRepositoryBook>(() => new RepositoryBook(_context));
            _author= new Lazy<IRepositoryAuthor>(() => new RepositoryAuthor(_context));
            _publishingHouse= new Lazy<IRepositoryPublishingHouse>(() => new RepositoryPublishingHouse(_context));
            _users= new Lazy<IRepositoryUsers>(() => new RepositoryUsers(_context));
            _roles= new Lazy<IRepositoryRoles>(() => new RepositoryRoles(_context));
            _claims= new Lazy<IRepositoryClaims>(() => new RepositoryClaims(_context));
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }






       
    }
}
