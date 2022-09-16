using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto;
using api.Models;
using api.Repo.Interface;
using Microsoft.EntityFrameworkCore;

namespace api.Repo
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext dataContext;

        public AuthorRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public void Add<T>(T entity) where T : class
        {
            dataContext.Add(entity);
        }

        public async  Task<Author> AddAuthor(AuthorForCreation field)
        {
            var Data = new Author{
                Name = field.Name
            };

            await dataContext.Authors.AddAsync(Data);
            await SaveAll();
            return Data;
        }

        public void Delete<T>(T entity) where T : class
        {
            dataContext.Remove(entity);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
           var dataFromRepo = await dataContext.Authors.FirstOrDefaultAsync(a=> a.Id== id);
            if(dataFromRepo != null)
            {
                dataContext.Authors.Remove(dataFromRepo);
                await SaveAll();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            var dataFromRepo = await dataContext.Authors.ToListAsync();
            return dataFromRepo;
        }

        public async Task<IEnumerable<object>> GetAuthorWithId(int id)
        {
            var dataFromRepo = await dataContext.Authors.Where(x=> x.Id == id).ToListAsync();
            return dataFromRepo;
        }

        public async Task<IEnumerable<object>> GetAuthorWithName(string name)
        {
             var dataFromRepo = await dataContext.Authors.Where(x=> x.Name == name).ToListAsync();
            return dataFromRepo;
        }

        public async Task<bool> SaveAll()
        {
             return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Author> UpdateAuthor(AuthorForUpdate model)
        {
            var data = await dataContext.Authors
                .FirstOrDefaultAsync(i => i.Id == model.Id);

            if (data == null)
            {
                return null;
            }
            
            data.Name = model.Name;
            await SaveAll();

            return data;
        }
    }
}