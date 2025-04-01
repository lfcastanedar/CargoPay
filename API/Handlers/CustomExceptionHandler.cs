using Domain.DTO;
using Domain.Exceptions;
using Domain.Resources;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace API.Handlers;

public class CustomExceptionHandler : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        HttpResponseException responseException = new HttpResponseException();
        ResponseDto response = new ResponseDto();
        
        
        if (context.Exception is BusinessException)
        {
            responseException.Status = StatusCodes.Status400BadRequest;
            response.Message = context.Exception.Message;
            context.ExceptionHandled = true;
            Log.Error(context.Exception, response.Message);
        }
        else if (context.Exception is UnauthorizedAccessException)
        {
            responseException.Status = StatusCodes.Status401Unauthorized;
            response.Message = GeneralMessages.Error401;
            context.ExceptionHandled = true;
            Log.Error(GeneralMessages.Error401);
        }
        else 
        {
            responseException.Status = StatusCodes.Status500InternalServerError;
            response.Result = JsonConvert.SerializeObject(context.Exception);
            response.Message = GeneralMessages.Error500;
            context.ExceptionHandled = true;
            Log.Fatal(context.Exception, GeneralMessages.Error500);

        }
        
        context.Result = new ObjectResult(responseException.Value)
        {
            StatusCode = responseException.Status,
            Value = response
        };
        
        if (responseException.Status == StatusCodes.Status500InternalServerError)
            context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = GeneralMessages.Error500;
    }
}