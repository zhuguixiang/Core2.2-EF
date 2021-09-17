using Core.EF.Application.Commom;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EF.Application.Web.Commom
{
    /// <summary>
    /// 全局异常过滤器
    /// https://blog.csdn.net/sD7O95O/article/details/78096113
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        readonly IHostingEnvironment _env;

        JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };

        public HttpGlobalExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext filterContext)
        {
            //var requestParameters = JsonHelper.Serializer(context.HttpContext.Items.Values);
            var requestParameters = JsonConvert.SerializeObject(filterContext.HttpContext.Items.Values);
            LogService.Instance.Error("全局异常处理：服务器内部错误", filterContext.Exception, requestParameters);

            NormalResult normalResult = new NormalResult("服务器内部错误：" + ExceptionHelper.GetMessage(filterContext.Exception));

            filterContext.Result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(normalResult, Formatting.Indented, _jsonSerializerSettings),
                StatusCode = StatusCodes.Status200OK
            };

            filterContext.ExceptionHandled = true;//异常已处理
        }
    }
}
