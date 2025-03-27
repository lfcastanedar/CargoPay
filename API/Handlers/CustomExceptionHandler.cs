using Common.Exceptions;
using Common.Resources;
using Microsoft.AspNetCore.Mvc.Filters;
using Infraestructure.Core.DTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Handlers;

public class CustomExceptionHandler : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        HttpResponseException responseException = new HttpResponseException();
        ResponseDto response = new ResponseDto();
        
        
        context.Result = new ObjectResult(responseException.Value)
        {
            StatusCode = responseException.Status,
            Value = response
        };
        
        if (context.Exception is UnauthorizedAccessException)
        {
            responseException.Status = StatusCodes.Status401Unauthorized;
            response.Message = "3213";
            context.ExceptionHandled = true;
            Log.Error("dwq");
        }
        
        
        Log.Fatal(context.Exception, GeneralMessage.Error500);
        if (responseException.Status == StatusCodes.Status500InternalServerError)
            context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = GeneralMessage.Error500;
    }
}