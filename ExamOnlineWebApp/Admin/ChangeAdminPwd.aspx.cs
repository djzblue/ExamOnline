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
using SDPTExam.DAL.Model;
using SDPTExam.DAL.Linq;
namespace SDPTExam.Web.UI.Admin
{
    public partial class ChangeAdminPwd : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnModify_Click(object sender, EventArgs e)
        {

            ExamDbDataContext dc;
            int aID=0;
            if (Session["AdminUserID"] != null)
            {
                aID =int.Parse(Session["AdminUserID"].ToString());
            }
            AdminUserInfo aInfo = AdminUser.GetAdminUserByID(aID,out dc);
            if (aInfo == null)
            {
                lblFeedback.Text = "获取管理员信息失败，不能修改密码！";
                return;
            }
            string pwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtOldPwd.Text, "SHA1");
            if (aInfo.Password == pwdEncode)
            {
                string newPwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPwd.Text, "SHA1");
            
                aInfo.Password =newPwdEncode ;

                AdminUser.UpdateAdminUser(aInfo, dc);

                lblFeedback.Text = "成功修改密码，请记住新密码！";
            }
            else
            {
                lblFeedback.Text = "旧密码输入错误，请重新输入！";
            }
            
        }
    }
}
