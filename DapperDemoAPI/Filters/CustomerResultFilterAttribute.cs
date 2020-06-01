using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemoAPI.Filters
{
    public class CustomerResultFilterAttribute : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("OnActionExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("OnResultExecuting");
        }
    }
}
