using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using SDPTExam.BLL;

namespace SDPTExam.Web.UI
{
    public class BasePage : System.Web.UI.Page
    {

        protected string TempPath
        {
            get
            {
                return Server.MapPath("~/Files/Temp");

            }
        }

        protected int DeptID
        {
            get
            {
                if (HttpContext.Current.Session["DepartmentID"] != null)
                {
                    return int.Parse(HttpContext.Current.Session["DepartmentID"].ToString());

                }
                else return 0;
            }
            //set { }
        }

        protected string DeptName
        {
            get
            {
                if (BaseData.GetDepartmentByID(DeptID) != null)
                    return BaseData.GetDepartmentByID(DeptID).DepartmentName;
                else return "未知系";
            }
            //set { }
        }



        /// <summary>
        /// 返回专业ID
        /// </summary>
        protected int MajorID
        {
            get
            {
                if (HttpContext.Current.Session["MajorID"] != null)
                {
                    return int.Parse(HttpContext.Current.Session["MajorID"].ToString());

                }
                else
                {
                    return 0;
                }
            }
        }


        /// <summary>
        /// 返回班级名称
        /// </summary>
        protected string ClassName
        {
            get
            {
                if (HttpContext.Current.Session["ClassName"] != null)
                {
                    return HttpContext.Current.Session["ClassName"].ToString();

                }
                else
                {
                    return "未分班";
                }
            }
        }


        /// <summary>
        /// 返回学生学号
        /// </summary>
        protected string StuNum
        {
            get
            {
                if (HttpContext.Current.Session["StuNum"] != null)
                {
                    return HttpContext.Current.Session["StuNum"].ToString();

                }
                else
                {
                    return null;
                }
            }
        }





        /// <summary>
        /// 获取用户的真实姓名
        /// </summary>
        protected string UserRealName
        {
            get
            {
                if (HttpContext.Current.Session["UserRealName"] != null)
                {
                    return HttpContext.Current.Session["UserRealName"].ToString();

                }
                else return "Unknown";

            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (HttpContext.Current.Session["DepartmentID"] == null)
                Response.Redirect("~/Login.aspx");

            //bool fixedPages = this.Request.Url.AbsoluteUri.Contains("Profile") || this.Request.Url.AbsoluteUri.Contains("Top");

            //if (HttpContext.Current.Session["FullProfile"] != null && (bool)HttpContext.Current.Session["FullProfile"] == false && fixedPages == false)
            //{
            //    if (IsStudent == true)
            //        Response.Redirect("~/Students/EditProfile.aspx");
            //    else
            //        Response.Redirect("~/Teachers/EditProfile.aspx");

            //}


        }
        /// <summary>
        /// 获取用户的id
        /// </summary>
        protected int UserID
        {
            get
            {
                if (HttpContext.Current.Session["UserID"] != null)
                {
                    return int.Parse(HttpContext.Current.Session["UserID"].ToString());

                }
                else return 0;

            }
        }

        protected bool IsStudent
        {
            get
            {
                if (HttpContext.Current.Session["IsStudent"] != null)
                {
                    return bool.Parse(HttpContext.Current.Session["IsStudent"].ToString());

                }
                else return true;

            }
        }

        /// <summary>
        /// 选题时间id
        /// </summary>
        protected int AppTimeLimitID
        {
            get
            {
                if (Utility.GetConfigValue(DeptID, "SubjectSelectTimitLimitID") == null)
                {
                    Utility.AddDepartmentConfig(DeptID, "");
                }
                string id = Utility.GetConfigValue(DeptID, "SubjectSelectTimitLimitID");
                if (id != null)
                {
                    return int.Parse(id);

                }
                else return 0;

            }
        }

        public string BaseUrl
        {
            get
            {
                string url = this.Request.ApplicationPath;
                if (url.EndsWith("/"))
                    return url;
                else
                    return url + "/";
            }
        }

        public string FullBaseUrl
        {
            get
            {
                return this.Request.Url.AbsoluteUri.Replace(
                   this.Request.Url.PathAndQuery, "") + this.BaseUrl;
            }
        }
    }
}
