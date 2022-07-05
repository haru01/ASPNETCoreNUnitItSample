using System;
namespace WebApi.Blogs.Domains
{
	public class DomainException : SystemException
	{
	}

	public class NotFoundException : DomainException {
	}

	public class NotImpl : NotImplementedException
    {

    }

}

