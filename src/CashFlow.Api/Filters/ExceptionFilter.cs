using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.Eventing.Reader;

namespace CashFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CashFlowException)
                HandleProjectException(context);
            else
                ThrowUnkowError(context);
        }

        private void HandleProjectException(ExceptionContext context)
        {
            var cashFlowException = (CashFlowException)context.Exception;
            var errorResponse = new ResponseErrorJson(cashFlowException.GetErrors());

            context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
            context.Result = new ObjectResult(errorResponse);
        }

        private void ThrowUnkowError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UnknownError);

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
