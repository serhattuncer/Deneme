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
    public class ServiceBook : IServiceBook
    {

        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ServiceBook(IRepositoryManager manager, IMapper mapper)
        {

            _manager = manager;
            _mapper = mapper;
        }

        public async Task CreateBook(BookDto bookDto)
        {
            try
            {
                var data = _mapper.Map<Book>(bookDto);
                await _manager.book.Create(data);
                await _manager.SaveChangesAsync();
            }
            catch (Exception e)
            {

                new Exception("message:" + e);
            }


        }

        public async Task DeleteBook(int id)
        {
            var data=_manager.book.GetById(id).Result;
            data.IsDeleted = true;
            _manager.book.Delete(data);
            await _manager.SaveChangesAsync();
        }

       

        public async Task<Book> GetBookById(int id)
        {
            return await _manager.book.GetById(id);
        }

        public async Task<List<Book>> GetBooks()
        {
            return await _manager.book.GetAll();
        }

        public async Task UpdateBook(BookDto bookDto)
        {
            var data = _mapper.Map<Book>(bookDto);
            _manager.book.Update(data);
            await _manager.SaveChangesAsync();
        }

       
        
    }
}
