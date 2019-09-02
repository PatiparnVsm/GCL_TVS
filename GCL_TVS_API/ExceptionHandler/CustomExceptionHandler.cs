using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            StatusError result = new StatusError();
            //result.ResponseCode = "99";
            //result.ResponseMsg = "system exception!";

            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, result);

            context.Result = new ResponseMessageResult(response);

            return base.HandleAsync(context, cancellationToken);
        }

        public virtual bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response =
                                 new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(Content);
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }
}