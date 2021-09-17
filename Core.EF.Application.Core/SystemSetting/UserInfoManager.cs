using Core.EF.Application.Commom;
using Core.EF.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Options;
using Core.EF.Application.Web.Commom;

namespace Core.EF.Application.Core
{
    public class UserInfoManager
    {
        private readonly AppSettings _appSettings;
        public UserInfoManager (IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
        }

        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NormalResult<GetListDataResult<UserInfo>> GetUserInfoList(GetListDataArgs args)
        {
            GetListDataResult<UserInfo> result = new GetListDataResult<UserInfo>();
            using (EFCoreContex db = EFCoreContex.CreateContext())
            {
                IQueryable<UserInfo> quaryable = db.UserInfo
                    .Where(x => x.Removed == false)
                    .AsNoTracking();

                if (args.Parameters.IsNullOrEmpty("UserCode") == false)
                {
                    string userCode = args.Parameters.GetValue<string>("UserCode");
                    quaryable = quaryable.Where(x => x.UserCode == userCode);
                }

                result.PagingInfo = new ResultPagingInfo(args.PagingInfo);
                int totalCount = quaryable.Count();
                result.PagingInfo.UpdateTotalCount(totalCount);

                if (string.IsNullOrEmpty(args.OrderBy) == false)
                {
                    quaryable = quaryable.OrderBy(args.OrderBy);
                }

                result.Data = quaryable
                    .Skip((result.PagingInfo.CurrentPage - 1) * result.PagingInfo.PageSize)
                    .Take(result.PagingInfo.PageSize)
                    .ToList();
            }

            return new NormalResult<GetListDataResult<UserInfo>>()
            {
                Data = result
            };
        }

        public List<UserInfo> GetAllUserList()
        {
            GetListDataResult<UserInfo> result = new GetListDataResult<UserInfo>();
            using (EFCoreContex db = EFCoreContex.CreateContext())
            {
                var list = db.UserInfo
                    .Where(x => x.Removed == false)
                    .AsNoTracking()
                    .ToList();

                return list;
            }
        }
    }
}
