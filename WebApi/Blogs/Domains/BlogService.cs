using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Blogs.Context;

namespace WebApi.Blogs.Domains
{
	public class BlogsService
	{
        private BlogContext _db;
          
		public BlogsService(BlogContext context)
		{
            _db = context;
        }

        // Query系
        internal async Task<IEnumerable<Blog>> SelectAll()
        {   
            return await _db.Blogs
                    .Include(blog => blog.Posts)
                    .ToListAsync();
        }

        internal async Task<IEnumerable<Blog>> SelectLimitBlogs(int limit)
        {
            return await _db.Blogs.Take(limit).ToListAsync();
        }

        internal async Task<Blog> FindById(int id)
        {
            var result = await _db.Blogs.Include(blog => blog.Posts)
                                        .FirstOrDefaultAsync(blog => blog.BlogId == id);
            if (result == null)
            {
                throw new NotFoundException();
            }
            return result;
        }
    }
}

