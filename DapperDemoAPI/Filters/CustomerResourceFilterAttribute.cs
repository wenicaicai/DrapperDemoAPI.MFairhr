using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemoAPI.Filters
{
    //资源过滤
    public class CustomerResourceFilterAttribute : Attribute, IResourceFilter
    {

        private static ConcurrentDictionary<string, object> CACHE_DICT = new ConcurrentDictionary<string, object>();
        private static string _cacheKey;
        //最后执行调用
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (!CACHE_DICT.ContainsKey(_cacheKey))
            {
                if (context.Result != null)
                {
                    CACHE_DICT.TryAdd(_cacheKey, context.Result);
                }
            }
        }

        //Controller创建之前进行调用
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _cacheKey = context.HttpContext.Request.Path.ToString();
            if (CACHE_DICT.TryGetValue(_cacheKey, out object result))
            {
                var actionResult = result as ActionResult;
                if (actionResult != null)
                {
                    context.Result = actionResult;
                }
            }
        }
    }
}
