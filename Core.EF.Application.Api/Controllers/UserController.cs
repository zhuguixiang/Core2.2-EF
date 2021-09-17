using AutoMapper;
using Core.EF.Application.Commom;
using Core.EF.Application.Core;
using Core.EF.Application.Dto;
using Core.EF.Application.Models;
using Core.EF.Application.Web.Commom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.EF.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserInfoManager _userInfoManager;
        public UserController(UserInfoManager userInfoManager)
        {
            _userInfoManager = userInfoManager;
        }

        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost("GetUserInfoList")]
        public NormalResult<GetListDataResult<UserInfoDto>> GetUserInfoList(GetListDataArgs args)
        {
            NormalResult<GetListDataResult<UserInfo>> userInfoList = _userInfoManager.GetUserInfoList(args);

            if (userInfoList.Successful)
            {
                GetListDataResult<UserInfoDto> result = new GetListDataResult<UserInfoDto>();
                result.PagingInfo = userInfoList.Data.PagingInfo;
                result.Data = Mapper.Map<List<UserInfo>, List<UserInfoDto>>(userInfoList.Data.Data);

                return new NormalResult<GetListDataResult<UserInfoDto>>()
                {
                    Data = result
                };
            }
            else
            {
                return new NormalResult<GetListDataResult<UserInfoDto>>(userInfoList.Message);
            }
        }

        [HttpPost("GetAllUserList")]
        public NormalResult<List<UserInfoDto>> GetAllUserList()
        {
            List<UserInfo> userInfoList = _userInfoManager.GetAllUserList();

            if (userInfoList.Any())
            {
                //redis
                CachingService.Instance.Set<List<UserInfo>>("AllUserList", userInfoList);
                var data = CachingService.Instance.Get<List<UserInfo>>("AllUserList");

                var list = Mapper.Map<List<UserInfo>, List<UserInfoDto>>(userInfoList);

                return new NormalResult<List<UserInfoDto>>()
                {
                    Data = list
                };
            }
            else
            {
                return new NormalResult<List<UserInfoDto>>();
            }
        }
    }
}