using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAuthors();
        Task<Author> GetAuthorById(int id);
        Task CreateAuthor(AuthorDto authorDto);
        Task UpdateAuthor(AuthorDto authorDto);
        Task DeleteAuthor(int id);

    }
}
