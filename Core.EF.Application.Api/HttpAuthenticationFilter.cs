using Core.EF.Application.Commom;
using Core.EF.Application.Dto;
using Core.EF.Application.Web.Commom;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.EF.Application.Api
{
    public class HttpAuthenticationFilter : AuthenticationFilter
    {
        public HttpAuthenticationFilter(CachingService cachingService, ILoggerFactory loggerFactory, IHostingEnvironment env) : base(cachingService, loggerFactory, env)
        {
        }

        public override void OnActionExecutingFinished(ActionExecutingContext context)
        {
            //不做登录验证，默认使用admin账号
            //UserInfoDto user = new UserInfoDto()
            //{
            //    Id = Guid.Parse("4D79F993CA454A5EBCAA438D49BC0988"),
            //    UserCode = "admin",
            //    UserName = "超级管理员"
            //};

            base.OnActionExecutingFinished(context);
        }
    }
}
