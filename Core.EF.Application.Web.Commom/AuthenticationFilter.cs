using Core.EF.Application.Commom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace Core.EF.Application.Web.Commom
{
    public class AuthenticationFilter : IActionFilter
    {
        protected readonly CachingService _cache;
        protected readonly ILoggerFactory _loggerFactory;
        protected readonly IHostingEnvironment _env;

        protected UserContext _userContext;

        JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };

        public AuthenticationFilter(CachingService cachingService, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _cache = cachingService;
            _loggerFactory = loggerFactory;
            _env = env;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //是否允许第三方远程调用接口
            object[] objAllowedAnonymousArray =
                (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(
                    typeof(AllowAnonymousAttribute), false);
            if (objAllowedAnonymousArray.Length > 0)
                return;

            //不做登录验证，默认使用admin账号
            //_userContext = new UserContext();
            //_userContext.Token = Guid.NewGuid().ToString();
            //_userContext.UserId = Guid.Parse("4D79F993CA454A5EBCAA438D49BC0988");
            //_userContext.LoginTime = DateTime.Now;

            //if (_userContext == null)
            //{
            //    NormalResult normalResult = new NormalResult(false);
            //    normalResult.Reason = 7001;
            //    normalResult.Message = "用户尚未登录。";

            //    context.Result = new ContentResult
            //    {
            //        Content = JsonConvert.SerializeObject(normalResult, Formatting.Indented, _jsonSerializerSettings),
            //        StatusCode = StatusCodes.Status200OK,
            //    };

            //    return;
            //}

            OnActionExecutingFinished(context);
        }


        public virtual void OnActionExecutingFinished(ActionExecutingContext context)
        {

        }
    }


}
