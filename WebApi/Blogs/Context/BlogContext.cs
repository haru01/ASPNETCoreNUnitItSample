using Microsoft.EntityFrameworkCore;
using WebApi.Blogs.Domains;

namespace WebApi.Blogs.Context;

public class BlogContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }


    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
                    .HasMany(b => b.Posts)
                    .WithOne(p => p.Blog)
                    .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Blog>()
                   .HasData(
                       new Blog { BlogId = 1, Url = @"https://devblogs.microsoft.com/dotnet", },
                       new Blog { BlogId = 2, Url = @"https://mytravelblog.com/" });

        modelBuilder.Entity<Post>()
        .HasData(
        new Post
        {
            PostId = 1,
            BlogId = 1,
            Title = "What's new",
            Content = "Lorem ipsum dolor sit amet",
        },
        new Post
        {
            PostId = 2,
            BlogId = 2,
            Title = "Around the World in Eighty Days",
            Content = "consectetur adipiscing elit",
        },
        new Post
        {
            PostId = 3,
            BlogId = 2,
            Title = "Glamping *is* the way",
            Content = "sed do eiusmod tempor incididunt",
        },
        new Post
        {
            PostId = 4,
            BlogId = 2,
            Title = "Travel in the time of pandemic",
            Content = "ut labore et dolore magna aliqua",
        });
    }

}


