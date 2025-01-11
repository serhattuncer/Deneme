using AutoMapper;
using Entities.ModelDto;
using Entities.Models;
using Repositories.Contracts;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class PublishingHouseService : IPublishingHouseService
    {

        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public PublishingHouseService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;

        }

        public async Task CreatePublishingHouse(PublishingHouseDto publishingHouseDto)
        {
            var data = _mapper.Map<PublishingHouse>(publishingHouseDto);
            await _manager.Publishinghouse.Create(data);
            await _manager.SaveChangesAsync();

        }

        public async Task DeletePublishingHouse(int id)
        {
            var data = _manager.Publishinghouse.GetById(id).Result;
            data.IsDeleted = true;
            _manager.Publishinghouse.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task<List<PublishingHouse>> GetAllPublishingHouses()
        {
            return await _manager.Publishinghouse.GetAll();
        }

        public async Task<PublishingHouse> GetPublishingHouseById(int id)
        {
            return await _manager.Publishinghouse.GetById(id);
        }

        public async Task UpdatePublishingHouse(PublishingHouseDto publishingHouseDto)
        {
            var data = _mapper.Map<PublishingHouse>(publishingHouseDto);
            _manager.Publishinghouse.Update(data);
            await _manager.SaveChangesAsync();
        }
    }
}
