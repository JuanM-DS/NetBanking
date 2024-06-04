using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetBanking.Core.Exceptions;
using System.Net;


namespace NetBanking.Infrastructure.Filters
{
    public class GlobalExceptions : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if(filterContext.Exception.GetType() == typeof(BusinessLogicException))
            {
                var exception = filterContext.Exception as Exception;

                var response = new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    title = HttpStatusCode.BadRequest,
                    massage = exception.Message
                };

                var json = new
                {
                    errors = new[] { response },
                };

                filterContext.Result = new BadRequestObjectResult(json);
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
