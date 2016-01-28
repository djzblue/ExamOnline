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
using SDPTExam.BLL;

namespace SDPTExam.Web.Root
{
    public partial class LoginPage : System.Web.UI.Page
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
            lblMessage.Visible = false;

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


            if (rblRoles.SelectedValue == "student")
            {
                if (ValidateUser(new Student()) == true)
                {

                    Session["Role"] = "student";
                    Response.Redirect("~/stuexam.html");
                }
            }
            else if (rblRoles.SelectedValue == "teacher")
            {
                if (ValidateUser(new Teacher()) == true)
                {
                    Session["Role"] = "teacher";
                    Response.Redirect("~/admin.html");
                }
            }
            else
            {
                if (ValidateUser(new AdminUser()) == true)
                {
                    //Session["Role"] = "admin";
                    Response.Redirect("~/admin.html");
                }
            }

        }

        private bool ValidateUser(IUser user)
        {
            
            bool isValid = user.ValidateUser(txtUserName.Text, txtPwd.Text);
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

        protected void btnVisitorLogin_Click(object sender, EventArgs e)
        {
            //if (Session["StuNum"] == null)//如果是首次登陆
            //{
                Session["StuNum"] = Guid.NewGuid().ToString();

                Session["Role"] = "visitor";
                Session["DepartmentID"] = 0;               
            //}

            Response.Redirect("~/stuexam.html");
            

        }

    }
}
