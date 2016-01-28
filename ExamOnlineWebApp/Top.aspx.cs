using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;
using SDPTExam.DAL.Model;

namespace SDPTExam.Web.UI
{
    public partial class Top : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               if(UserRealName.Equals("Unknown",StringComparison.OrdinalIgnoreCase))
                   lblInfo.Text +="游客,您好！";
                else lblInfo.Text +=UserRealName + ",您好！";


               // string sysName="";

                //if (Session["SystemName"] != null)
                //    sysName = Session["SystemName"].ToString();

                //lblInfo.Text += "您已登录"+sysName+"管理平台";

   
                    if (Session["Role"] != null)
                    {

                    if(Session["Role"].ToString()=="admin")
                        lblInfo.Text += " 身份：部门管理员";
                    //else if(Session["Role"].ToString()=="superadmin")
                    //    lblInfo.Text += " 身份：超级管理员";
                    //else if (Session["Role"].ToString() == "schooladmin")
                    //    lblInfo.Text += " 身份：教务处管理员";
                    //else if (Session["Role"].ToString() == "majorManager")
                    //    lblInfo.Text += " 身份：专业负责人";
                    else if(Session["Role"].ToString() =="visitor")
                        lblInfo.Text += " 身份：匿名学生";
                    else if(Session["Role"].ToString() =="student")
                        lblInfo.Text += " 身份：学生";

                    else lblInfo.Text += " 身份：老师";



                    }

                    else lblInfo.Text += " 身份：老师";
               

            }
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
