 

using Jeremy.OA.Model;
using Jeremy.OA.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.IBLL
{
	
	public partial interface IActionInfoService : IBaseService<ActionInfo>
    {
        /// <summary>
        /// 批量删除用户对象
        /// </summary>
        /// <param name="list">用户信息列表ID集合</param>
        /// <returns></returns>
        bool DeleteEntities(List<int> list);
    }   
	
	public partial interface IBooksService : IBaseService<Books>
    {
       
    }   
	
	public partial interface IDepartmentService : IBaseService<Department>
    {
       
    }   
	
	public partial interface IKeyWordsRankService : IBaseService<KeyWordsRank>
    {
       
    }   
	
	public partial interface IR_UserInfo_ActionInfoService : IBaseService<R_UserInfo_ActionInfo>
    {
       
    }   
	
	public partial interface IRoleInfoService : IBaseService<RoleInfo>
    {
       
    }   
	
	public partial interface ISearchDetailsService : IBaseService<SearchDetails>
    {
       
    }   
	
	public partial interface IUserInfoService : IBaseService<UserInfo>
    {
       
    }



}