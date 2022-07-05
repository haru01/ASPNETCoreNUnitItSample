using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Blogs.Domains;


namespace WebApi.Blogs.Controllers
{
    public class DomainExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundResult();
            }

            return base.OnExceptionAsync(context);

        }

    }
}

