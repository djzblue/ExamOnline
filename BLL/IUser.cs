using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPTExam.BLL
{
   public interface IUser
    {
        /// <summary>
        /// 验证用户的合法性
        /// </summary>
        /// <param name="uname">用户名</param>
        /// <param name="pwd">用户密码</param>
        /// <returns></returns>
        bool ValidateUser(string uname, string pwd);



        /// <summary>
        /// 退出登录
        /// </summary>
       // void LogOut();
    }
}
