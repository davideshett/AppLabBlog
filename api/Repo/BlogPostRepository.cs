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
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly DataContext context;

        public BlogPostRepository(DataContext context)
        {
            this.context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public async Task<BlogPost> AddBlogPost(BlogPostForCreation field)
        {
            var data = new BlogPost
            {
                Title = field.Title,
                Body = field.Body
            };

            await context.BlogPosts.AddAsync(data);
            await SaveAll();
            return data;
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<bool> DeleteBlogPost(int id)
        {
            var dataFromRepo = await context.BlogPosts.FirstOrDefaultAsync(a=> a.Id== id);
            if(dataFromRepo != null)
            {
                context.BlogPosts.Remove(dataFromRepo);
                await SaveAll();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPosts()
        {
            var dataFromRepo = await context.BlogPosts.ToListAsync();
            return dataFromRepo;
        }

        public async Task<IEnumerable<object>> GetBlogPostWithId(int id)
        {
            var dataFromRepo = await context.BlogPosts.Where(x=> x.Id == id).ToListAsync();
            return dataFromRepo;
        }

        public async Task<IEnumerable<object>> GetBlogPostWithTitle(string title)
        {
             var dataFromRepo = await context.BlogPosts.Where(x=> x.Title == title).ToListAsync();
            return dataFromRepo;
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<BlogPost> UpdateBlogPost(BlogPostForUpdate model)
        {
             var data = await context.BlogPosts
                .FirstOrDefaultAsync(i => i.Id == model.Id);

            if (data == null)
            {
                return null;
            }
            
            data.Title = model.Title;
            data.Body = model.Body;
            data.Timestamp = model.Timestamp;
            await SaveAll();

            return data;
        }
    }
}