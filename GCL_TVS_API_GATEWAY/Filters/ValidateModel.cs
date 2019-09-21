using GCL_TVS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GCL_TVS_API_GATEWAY.Filters
{
    public class ValidateViewModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ResponseStatus response = new ResponseStatus();
            response.status = new Status();

            if (actionContext.ActionArguments.Any(kv => kv.Value == null))
            {
                response.status.code = "10";
                response.status.message = "Arguments cannot be null";
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,response);
            }

            if (actionContext.ModelState.IsValid == false)
            {                
                response.status.code = "10";
                response.status.message = actionContext.ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,response);
            }
        }
    }
}