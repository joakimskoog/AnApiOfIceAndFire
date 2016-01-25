using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace AnApiOfIceAndFire.Infrastructure
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            context.Result = new ExceptionResponse(context.ExceptionContext.Request, HttpStatusCode.InternalServerError, 
                "Something went wrong, we're working on fixing it as soon as possible!");
            return base.HandleAsync(context, cancellationToken);
        }
    }

    public class ExceptionResponse : IHttpActionResult
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
        public HttpRequestMessage Request { get; }

        public ExceptionResponse(HttpRequestMessage request, HttpStatusCode statusCode, string message = "")
        {
            Request = request;
            StatusCode = statusCode;
            Message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(StatusCode)
            {
                RequestMessage = Request,
                Content = new StringContent(Message)
            };

            return Task.FromResult(response);
        }
    }
}