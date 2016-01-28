using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;
using System.Web;
using System.Configuration;
using System.ComponentModel;

namespace SDPTExam.BLL
{
    [DataObject(true)]
   public class AdminUser:BLLBase,IUser
    {

   
        #region 实现IUser接口
        /// <summary>
        /// 验证管理员的合法性
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ValidateUser(string uname, string pwd)
        {
           string pwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1");
            
             ExamDbDataContext dc = DataAccess.CreateDBContext();
            var admins = from a in dc.AdminUserInfo
                         where a.LoginName == uname && a.Password == pwdEncode
                         select a;
            if (admins.Count<AdminUserInfo>() > 0)
            {
                AdminUserInfo ad=ToSingle<AdminUserInfo>(admins);//.First<AdminUserInfo>();
                if (ad == null) return false;
                HttpContext.Current.Session["DepartmentID"] =ad.DepartmentID;
                HttpContext.Current.Session["UserRealName"] = ad.LoginName;
                HttpContext.Current.Session["IsAdmin"] = true;
                HttpContext.Current.Session["UserID"] = 0;
                   HttpContext.Current.Session["AdminUserID"]= ad.AdminUserID;
                HttpContext.Current.Session["IsStudent"] = false;
                if (ad.IsSuperAdmin == true)
                {
                    HttpContext.Current.Session["IsSuperAdmin"] = true;
                    HttpContext.Current.Session["Role"] = "superadmin";
                }
                else
                {
                    HttpContext.Current.Session["IsSuperAdmin"] = null;

                    if (ad.IsFromAdministration == true)
                        HttpContext.Current.Session["Role"] = "schooladmin";
                    else HttpContext.Current.Session["Role"] = "admin";
                }

                ad.LoginCount = ad.LoginCount + 1;

                LogInfo l = new LogInfo();
                l.LogTitle = "登录";
                l.LogContent = ad.LoginName + "管理员登录成功！";
                l.LogType = "登录";
                l.AddedTime = DateTime.Now;
                dc.LogInfo.InsertOnSubmit(l);
                dc.SubmitChanges();
                return true;
            }
            else return false;
        }

        #endregion

       /// <summary>
       /// 返回管理员列表
       /// </summary>
       /// <returns></returns>
        public static IList<AdminUserInfo> GetAdminUsers()
        {
             ExamDbDataContext dc = DataAccess.CreateDBContext(); var admins = from a in dc.AdminUserInfo
                         select a;
            return ToList<AdminUserInfo>(admins);
        }

       /// <summary>
       /// 根据id返回对应的adminuser
       /// </summary>
       /// <param name="aID"></param>
       /// <returns></returns>
        public static AdminUserInfo GetAdminUserByID(int aID)
        {
             ExamDbDataContext dc = DataAccess.CreateDBContext();
            var admins = from a in dc.AdminUserInfo
                         where a.AdminUserID==aID
                         select a;
            
            return ToSingle<AdminUserInfo>(admins);
        }


        /// <summary>
        /// 根据id返回对应的adminuser
        /// </summary>
        /// <param name="aID"></param>
        /// <returns></returns>
        public static AdminUserInfo GetAdminUserByID(int aID,out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var admins = from a in dc.AdminUserInfo
                         where a.AdminUserID == aID
                         select a;
            d = dc;
            return ToSingle<AdminUserInfo>(admins);
        }

       /// <summary>
       /// 添加一个管理员用户
       /// </summary>
       /// <param name="a"></param>
        public static void InsertAdminUser(AdminUserInfo a)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            dc.AdminUserInfo.InsertOnSubmit(a);
            dc.SubmitChanges();
           
        }

        /// <summary>
        /// 更新管理员用户信息
        /// </summary>
        /// <param name="a"></param>
        public static void UpdateAdminUser(AdminUserInfo a)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<AdminUserInfo>(dc.AdminUserInfo, a);
            dc.SubmitChanges();
        }


        /// <summary>
        /// 更新管理员用户信息
        /// </summary>
        /// <param name="a"></param>
        public static void UpdateAdminUser(AdminUserInfo a, ExamDbDataContext dc)
        {
            AttachInfo<AdminUserInfo>(dc.AdminUserInfo, a);
            dc.SubmitChanges();
        }
       /// <summary>
       /// 删除一个管理员用户
       /// </summary>
       /// <param name="a"></param>
        public static void DeleteAdminUser(AdminUserInfo a,ExamDbDataContext dc)
        {
            AttachInfo<AdminUserInfo>(dc.AdminUserInfo,a);
            dc.AdminUserInfo.DeleteOnSubmit(a);
            dc.SubmitChanges();        
        }

        /// <summary>
        /// 删除一个管理员用户,根据id
        /// </summary>
        /// <param name="a"></param>
        public static void DeleteAdminUser(int adminUserID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
           
            AdminUserInfo a = dc.AdminUserInfo.Single(p => p.AdminUserID == adminUserID);
               dc.AdminUserInfo.DeleteOnSubmit(a);
            dc.SubmitChanges();
        }

    }
}
