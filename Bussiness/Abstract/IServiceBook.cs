using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IServiceBook
    {
        Task<List<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task CreateBook(BookDto bookDto);
        Task UpdateBook(BookDto bookDto);
        Task DeleteBook(int id);
    }
}
