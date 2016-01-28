using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace SDPTExam.Web.UI
{
    public partial class LeftPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();

                string menuSource = "";

                switch (role)
                {
                    case "student": menuSource = "Student.xml"; //学生
                        break;

                    case "teacher": menuSource = "Teacher.xml";  //普通教师
                        break;

                    case "admin": menuSource = "Admin.xml";  //部门管理员
                        break;

                    case "schooladmin": menuSource = "ForAdministration.xml"; //学校管理人员
                        break;

                    case "superadmin": menuSource = "SuperAdmin.xml"; //超级管理员
                        break;

                    case "majorManager": menuSource = "MajorManager.xml"; //专业负责人
                        break;

                    default: menuSource = "Student.xml";
                        break;
                }

                string menudir = "";

                //if (Session["SystemType"] != null)
                //{
                //    string sysType = Session["SystemType"].ToString();

                //    switch (sysType)
                //    {
                //        case "0": menudir = "MenuSource/Exam/";
                //            break;
                //        case "1": menudir = "MenuSource/practise/";
                //            break;
                //        case "2": menudir = "MenuSource/job/";
                //            break;
                //        default: menudir = "MenuSource/Exam/";
                //            break;
                //    }


                //}
                //else 

                menudir = "MenuSource/Exam/";

                menuSource = menudir + menuSource;

                XmlDataSource1.DataFile = menuSource;

                TreeView1.DataBind();
                TreeView1.Visible = true;
            }
            //if (Session["IsAdmin"] != null)
            //    hlinkOldEntry.Visible = true;
            else
            {
                Response.Write("非法访问！！！");
                Response.End();
            }
            SetVisibility();

        }

        private void SetVisibility()
        {
            if (Session["IsAdmin"] != null && Session["FullProfile"] != null)
            {
                //lbnLogout.Visible = false;
                //lnkbtnReturnAdmin.Visible = true;

            }
            else
            {
                // lbnLogout.Visible = true;
                //lnkbtnReturnAdmin.Visible = false;

            }
        }



        protected void lnkbtnReturnAdmin_Click(object sender, EventArgs e)
        {
            Session["UserRealName"] = "管理员";
            Session["UserID"] = 0;
            Session["IsStudent"] = false;
            Session["Role"] = "admin";
            Session["IsMajorManager"] = null;
            Session["PersonalDirectory"] = null;
            Session["FullProfile"] = null;
            FormsAuthentication.SetAuthCookie("管理员", false);
            Response.Write("<script>window.parent.navigate('../admin.html');</script>");
        }

        protected void lbnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            System.Web.Security.FormsAuthentication.SignOut();

            //  Server.Transfer("<script>window.parent.navigate('login.aspx');</script>");
            Response.Write("<script>window.parent.navigate('login.aspx');</script>");
        }
    }
}
