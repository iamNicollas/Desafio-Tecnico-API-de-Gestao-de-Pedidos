using System.Net;
using System.Text.Json;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continua o fluxo da requisição.
                await _next(context);
            }
            catch (Exception ex)
            {
                // Exceção não tratada chegará aqui.
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse();

            switch (exception)
            {
                case DomainException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    response.StatusCode = context.Response.StatusCode;
                    response.Message = exception.Message;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    response.StatusCode = context.Response.StatusCode;
                    response.Message = "Ocorreu um erro interno na aplicação.";
                    break;
            }

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }

        private class ErrorResponse
        {
            public int StatusCode { get; set; }

            public string Message { get; set; } = string.Empty;
        }
    }
}
