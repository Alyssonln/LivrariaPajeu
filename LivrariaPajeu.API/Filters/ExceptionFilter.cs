using LivrariaPajeu.Communication.Responses;
using LivratiaPajeu.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LivrariaPajeu.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is LivrariaPajeuException livrariaException)
        {
            HandleProjectException(context, livrariaException);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private static void HandleProjectException(ExceptionContext context, LivrariaPajeuException exception)
    {
        var response = new ResponseErrorJson(exception.GetErrors());

        context.Result = new ObjectResult(response)
        {
            StatusCode = exception.StatusCode
        };
    }

    private static void ThrowUnknownError(ExceptionContext context)
    {
        var response = new ResponseErrorJson("Ocorreu um erro inesperado no servidor.");

        context.Result = new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
