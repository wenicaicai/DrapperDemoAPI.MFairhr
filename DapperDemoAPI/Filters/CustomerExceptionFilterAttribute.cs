using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;

namespace DapperDemoAPI.Filters
{
    public class CustomerExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private IModelMetadataProvider _modelMetadataProvider;

        public CustomerExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            //控制器名字、方法
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            if (!context.ExceptionHandled)
            {
                if (this.IsAjaxRequest(context.HttpContext.Request))
                {
                    context.Result = new JsonResult(
                            new
                            {
                                Result = false,
                                ErrorMsg = context.Exception.Message
                            }
                        );
                }
                else
                {
                    var result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
                    result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
                    result.ViewData.TryAdd("Exception", context.Exception);
                    result.ViewData.TryAdd("ControllerName", controllerName);
                    result.ViewData.TryAdd("ActionName", actionName);
                    context.Result = result;
                }
                context.ExceptionHandled = true;
            }
        }

        private bool IsAjaxRequest(HttpRequest request)
        {
            var header = request.Headers["X-Request-With"];
            return header.Equals("XMLHttpRequest");
        }
    }


}
