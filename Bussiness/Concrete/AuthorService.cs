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
    public class AuthorService : IAuthorService
    {

        private IRepositoryManager _manager;
        private readonly IMapper _mapper;


        public AuthorService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task CreateAuthor(AuthorDto authorDto)
        {
            try
            {
                var data = _mapper.Map<Author>(authorDto);
                await _manager.author.Create(data);
                await _manager.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task DeleteAuthor(int id)
        {
            var data = _manager.author.GetById(id).Result;
            data.IsDeleted = true;
            _manager.author.Delete(data);
            await _manager.SaveChangesAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _manager.author.GetById(id);
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _manager.author.GetAll();
        }

        public async Task UpdateAuthor(AuthorDto authorDto)
        {
            var data = _mapper.Map<Author>(authorDto);
            _manager.author.Update(data);
            await _manager.SaveChangesAsync();
        }
    }
}
