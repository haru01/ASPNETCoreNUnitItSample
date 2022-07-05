using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApi.Blogs.Context;
using WebApi.Blogs.Domains;

namespace WebApi.Tests.Blogs.Controllers;


public class CustomWebApplicationTake2Factory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // DB を SQL Server からインメモリーにする
            var descriptor = services.SingleOrDefault(
                x => x.ServiceType == typeof(DbContextOptions<BlogContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            services.AddDbContext<BlogContext>(options =>
            {
                options.UseInMemoryDatabase("Testing");
            });
        });
    }
}


public class BlogsControllerTake2Test : IClassFixture<CustomWebApplicationTake2Factory<Program>>
{

    private readonly CustomWebApplicationTake2Factory<Program> _factory;

    public BlogsControllerTake2Test(CustomWebApplicationTake2Factory<Program> factory)
    {
        _factory = factory;
    }



    [Fact]
    public async Task _ブログが一覧取得できること()
    {
        // Arrange
        Blog[] blogs = new Blog[]
        {
            new Blog()
            {
                BlogId = 10,
                Url = "http://test10.com",
                Posts = new List<Post>
                {
                    new Post() {
                        PostId = 100, Title = "Title-10-100", Content = "C-10-100", BlogId = 10
                    },
                }
            },

            new Blog()
            {
                BlogId = 20,
                Url = "htttp://test20.com",
            }
        };
        SetUpbBlogs(blogs);

        // Act
        var response = await client().GetAsync($"/blogs");

        // Assert
        var json = await response.Content.ReadAsStringAsync();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(ToJson(blogs), json);
        //Assert.Equal(blogs, results);
    }


    [Fact]
    public async Task _ブログが一件取得できること()
    {
        // Arrange
        Blog[] blogs = new Blog[]
        {
            new Blog()
            {
                BlogId = 77,
                Url = "http://test77.com",
                Posts = new List<Post>
                {
                    new Post() {
                        PostId = 100, Title = "Title-77-100", Content = "C-77-100", BlogId = 77
                    },
                }
            },
            new Blog()
            {
                BlogId = 78,
                Url = "http://test78.com",
            },

        };
        SetUpbBlogs(blogs);

        // Act
        var response = await client().GetAsync($"/blogs/77");
        var json = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(ToJson(blogs[0]), json);
    }

    public String ToJson(Object obj)
    {
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        var options = new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        return JsonConvert.SerializeObject(obj, options);
    }




    [Fact]
    public async Task _存在しないブログが指定された場合は404を返すこと()
    {

        // Act
        var response = await client().GetAsync($"/blogs/9999999"); // not found

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    private void SetUpbBlogs(params Blog[] blogs)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<BlogContext>();
            db.Database.EnsureDeleted();
            db.Blogs.AddRange(
                blogs
            );
            db.SaveChanges();
        }
    }

    public HttpClient client()
    {
        return _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
        });
    }

}

