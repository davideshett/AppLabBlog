using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto
{
    public class BlogPostForCreation
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}