using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DapperDemoAPI.Filters
{
    /*
     * 摘自：https://www.cnblogs.com/jlion/p/12394949.html
     * 过滤器的注入方式
     * 1.Action注册 位置：控制器的方法 操作：标注特性
     * 
     * 2.Controller注册 （背景：需要一个一个Action 标注特性注册，是不是很繁琐？）
     * ----微软给我们提供了简便的控制器标注注册方式----
     * 
     * 3.全局注册
     * services.AddControllersWithViews(option=> 
     * {
     *      option.Filters.Add<ExecptionFilter>();
     * });
     * 
     */

    /*
     * 这里的异常过滤器，使用了日志记录组件，也就是发生了依赖关系
     * Attribute 特性标注注册方式本身并不支持构造函数
     * 解决对服务有依赖关系的过滤器 正确打开方式：
     * TypeFilter 或者 ServiceFilter 
     * 两者的生命周期不同
     * 使用ServiceFilter时，需要结合进行过滤器的注入
     */

    /*
     * 在下一篇博客中，我们将介绍内置过滤器、过滤的使用、依赖注入、取消与截断等知识，谢谢！
     */

    /*
     * 摘自：https://www.cnblogs.com/viter/p/10107886.html
     * 过滤器，从我们开始开发 Asp.Net 应用程序开始，就一直伴随在我们左右；
     */

    //异常日志收集
    public class CustomerExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private IModelMetadataProvider _modelMetadataProvider;
        private ILogger<CustomerExceptionFilterAttribute> _logger;

        //构造注入日志组件
        public CustomerExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider,
            ILogger<CustomerExceptionFilterAttribute> logger)
        {
            _modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }

        //当系统发生未捕获异常时就会触发这个方法
        public void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            //日志收集
            _logger.LogError(context.Exception, context?.Exception?.Message ?? "异常");

            //控制器名字、方法
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            if (!context.ExceptionHandled)
            {
                if (this.IsAjaxRequest(context.HttpContext.Request))
                {
                    var userName = context.HttpContext.User.Identity.Name;
                    var userRole = context.HttpContext.User.IsInRole("roleName");
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
