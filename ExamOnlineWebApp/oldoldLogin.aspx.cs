using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;
using System.Web.Security;

namespace SDPTExam.Web.UI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
            lblMessage.Visible = false;

            if (Request.QueryString["LoginName"] != null && Request.QueryString["pwd"] != null)
            {
                string loginName = Request.QueryString["LoginName"];
                string pwd = Request.QueryString["pwd"];

                if (ValidUserByRole(loginName, pwd))
                    Response.Redirect("~/admin.html");


            }

        }


        protected void submitbtn_Click(object sender, ImageClickEventArgs e)
        {
            //此处设置相关登录代码，注意应该是管理员身份
            //  IUser user;

            //if (DropDownList1.SelectedValue == "student")
            //{
            //if (ValidateUser(new Student()) == true)
            //{

            //    Session["Role"] = "student";
            //    Response.Redirect("~/admin.html");
            //}
            //else if (ValidateUser(new Teacher()) == true)
            //{
            //    Session["Role"] = "teacher";
            //    Response.Redirect("~/admin.html");
            //}

            //else if (ValidateUser(new AdminUser()) == true)
            //{
            //    Session["Role"] = "admin";
            //    Response.Redirect("~/admin.html");
            //}
            //else lblMessage.Visible = true; 

            //if (ddlType.SelectedValue == "no")
            //{
            //    lblMessage.Text = "请选择登录系统的类别！";
            //    lblMessage.Visible = true;
            //    return;
            //}
            
            bool pass = false;

            if (ValidUserByRole(txtUserName.Text, txtPwd.Text))
                pass = true;
             if(pass)
                 Response.Redirect("~/admin.html");


            //Session["SystemType"] = ddlType.SelectedValue;
            //Session["SystemName"] = ddlType.SelectedItem.Text;

            //if (ddlType.SelectedValue == "1")
            //{
            //    if (Session["Role"].ToString() == "teacher")
            //        Session["UserID"] = 11425;
            //    else if (Session["Role"].ToString() == "student")
            //        Session["UserID"] = 17792;

            //    Session["DepartmentID"] = 10034;
            //   // Response.Redirect("http://enonling.org?loginName=" + txtUserName.Text + "&pwd=" + txtPwd.Text);
            //}
            //else if (ddlType.SelectedValue == "2")
            //{
            //    if (Session["Role"].ToString() == "teacher")
            //        Session["UserID"] = 11425;
            //    else if (Session["Role"].ToString() == "student")
            //        Session["UserID"] = 17792;

            //    Session["DepartmentID"] = 10035;
            //   // Response.Redirect("http://enonling.org?loginName=" + txtUserName.Text + "&pwd=" + txtPwd.Text);
            //}
            //else
            //{ 
            
            //}

         
        }

        private bool ValidUserByRole(string uName,string pwd)
        {
            bool returnValue = false;
            if (rblRoles.SelectedValue == "student")
            {
                if (ValidateUser(new Student(), uName, pwd) == true)
                {

                    Session["Role"] = "student";
                    returnValue = true;

                    //生成随机试卷的XML文件


                    
                }
            }
            else if (rblRoles.SelectedValue == "teacher")
            {
                if (ValidateUser(new Teacher(), uName, pwd) == true)
                {

                    returnValue = true;
                }
            }
            else
            {
                if (ValidateUser(new AdminUser(), uName, pwd) == true)
                {
                    // Session["Role"] = "admin";
                    returnValue = true;
                }
            }

            return returnValue;
        }

        private bool ValidateUser(IUser user, string uName, string pwd)
        {

            bool isValid = user.ValidateUser(uName,pwd);
            if (isValid == false)
            {

                lblMessage.Text = "用户名或者密码错误";
                lblMessage.Visible = true;
            }
            else
            {
                FormsAuthentication.SetAuthCookie(txtUserName.Text, false);
            }
            return isValid;
        }
    }
}
