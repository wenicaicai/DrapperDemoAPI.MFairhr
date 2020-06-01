using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace DapperDemoAPI.Filters
{
    //操作日志
    //参数验证
    //权限控制
    public class CustomerActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("OnActionExecuting");
        }
    }
}
