using System.Web.Http;
using Microsoft.EntityFrameworkCore;
using WebApi.Blogs.Context;
using WebApi.Blogs.Controllers;
using WebApi.Blogs.Domains;

var builder = WebApplication.CreateBuilder(args);


// Create
Console.WriteLine("Inserting a new blog");


builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlite($"Data Source=devBlog.db"));
//builder.Services.AddDbContext<BloggingContext>(opt =>
//    opt.UseInMemoryDatabase("BloggingContext"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();


app.Run();

public partial class Program
{
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string? ToString()
    {
        return base.ToString();
    }
}