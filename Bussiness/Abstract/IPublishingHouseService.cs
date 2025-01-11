using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IPublishingHouseService
    {
        Task<List<PublishingHouse>> GetAllPublishingHouses();
        Task<PublishingHouse> GetPublishingHouseById(int id);
        Task CreatePublishingHouse(PublishingHouseDto PublishinghouseDto);
        Task UpdatePublishingHouse(PublishingHouseDto PublishinghouseDto);
        Task DeletePublishingHouse(int id);
    }
}
