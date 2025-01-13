using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {       
        private readonly Lazy<IServiceBook> _book;
        private readonly Lazy<IAuthorService> _author;
        private readonly Lazy<IPublishingHouseService> _publishingHouse;
        private readonly Lazy<IUserService> _user;
        private readonly Lazy<IRoleService> _role;
        private readonly Lazy<IClaimService> _claim;
        public ServiceManager(IRepositoryManager manager,IMapper mapper)
        {
            _book = new Lazy<IServiceBook>(() => new ServiceBook(manager,mapper));
            _author=new Lazy<IAuthorService>(()=>  new AuthorService(manager, mapper));
            _publishingHouse=new Lazy<IPublishingHouseService>(()=>new PublishingHouseService(manager, mapper));
            _user = new Lazy<IUserService>(() => new UserService(manager,mapper));
            _role=new Lazy<IRoleService>(()=>new RoleService(manager,mapper));
            _claim=new Lazy<IClaimService>(()=>new ClaimService(manager,mapper));
        }
        public IServiceBook book => _book.Value;

        public IAuthorService author => _author.Value;

        public IPublishingHouseService publishingHouse => _publishingHouse.Value;
       
        public IUserService users =>_user.Value;

        public IRoleService role => _role.Value;

        public IClaimService claim => _claim.Value;
    }
}

