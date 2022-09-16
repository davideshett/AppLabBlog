using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Models;

namespace api.Repo.Interface
{
    public interface IAuthorRepository
    {
         void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();

        Task<Author> AddAuthor(AuthorForCreation field);
         Task<IEnumerable<Author>> GetAuthors();
         Task<IEnumerable<object>> GetAuthorWithId(int id);
        Task<IEnumerable<object>> GetAuthorWithName(string name);
        Task<Author> UpdateAuthor(AuthorForUpdate model);
        Task<bool> DeleteAuthor(int id);
    }
}