using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApi.Blogs.Context;
using WebApi.Blogs.Domains;

namespace WebApi.Blogs.Controllers;

[DomainExceptionFilter]
[ApiController]
public class BlogsController
{
    private readonly BlogsService blogService;

	public BlogsController(BlogContext _db)
	{
		this.blogService = new BlogsService(_db);
	}

    [HttpGet("blogs")]
    public async Task<IEnumerable<Blog>> SelectAllBlogs()
	{
		return await this.blogService.SelectAll();
	}

	[HttpGet("blogs/limit")]
	public async Task<IEnumerable<Blog>> SelectLimitBlogs(int limt)
	{
		return await this.blogService.SelectLimitBlogs(limt);
	}



	
	[HttpGet("blogs/{id}")]
	public async Task<Blog> FindBlogById(int id)
	{
		return  await this.blogService.FindById(id);
	}

}