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
        public ServiceManager(IRepositoryManager manager,IMapper mapper)
        {
            _book = new Lazy<IServiceBook>(() => new ServiceBook(manager,mapper));
            _author=new Lazy<IAuthorService>(()=>  new AuthorService(manager, mapper));
            _publishingHouse=new Lazy<IPublishingHouseService>(()=>new PublishingHouseService(manager, mapper));
        }
        public IServiceBook book => _book.Value;

        public IAuthorService author => _author.Value;

        public IPublishingHouseService publishingHouse => _publishingHouse.Value;
    }
}

