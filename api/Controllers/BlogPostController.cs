using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Repo.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BlogPostController: ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepositor;
        private readonly IMapper mapper;

        public BlogPostController(IBlogPostRepository blogPostRepositor, IMapper mapper)
        {
            this.blogPostRepositor = blogPostRepositor;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogPst([FromBody] BlogPostForCreation field)
        {
            var dataFromRepo = await blogPostRepositor.AddBlogPost(field);
            if(dataFromRepo == null)
            {
                return BadRequest(new {
                    Message = "Error",
                    StatusCode = 401,
                    IsSuccessful = false
                });
            }

            return Ok(new {
                Message = "Success",
                StatusCode = 201,
                IsSuccessful = true,
                Data = dataFromRepo
            });
        }

        [HttpGet]
        public async Task<IActionResult> getBlogPosts()
        {
            var dataFromRepo = await blogPostRepositor.GetBlogPosts();
             if(dataFromRepo == null)
            {
                return BadRequest(new {
                    Message = "Error",
                    StatusCode = 401,
                    IsSuccessful = false
                });
            }

            return Ok(new {
                Message = "Success",
                StatusCode = 201,
                IsSuccessful = true,
                Data = dataFromRepo
            });
        }



    }
}