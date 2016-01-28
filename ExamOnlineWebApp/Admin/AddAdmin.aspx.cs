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
    public partial class AddAdmin : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IsSuperAdmin"] == null)
                {
                    Response.End();
                    Response.Redirect("~/Admin.html");
                }
                else if (ConfigurationManager.AppSettings["TheOnlySuperAdmin"] != null)
                {
                    string uname = UserRealName.Trim().ToLower();
                    string aname = ConfigurationManager.AppSettings["TheOnlySuperAdmin"].Trim().ToLower();
                    if (uname!=aname)
                    {
                        lblMessage.Text = "对不起，您登录的是试用账号，不允许进行添加管理员操作，如有需要请联系站长。";
                        //Response.Write("用户名："+UserRealName);
                        //Response.Write("<br>配置信息：" + ConfigurationManager.AppSettings["TheOnlySuperAdmin"]);
                        panelAdminUser.Visible = false;
                    }
                }
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DetailsView1.Visible = true;
            DetailsView1.ChangeMode(DetailsViewMode.Edit);
        }

        protected void btnAddNewAdmin_Click(object sender, EventArgs e)
        {
            DetailsView1.Visible = true;
            DetailsView1.ChangeMode(DetailsViewMode.Insert);
        }

        protected string GetDepartmentNameByID(int id)
        {
           DepartmentInfo depInfo= BaseData.GetDepartmentByID(id);
           if (depInfo != null)
               return depInfo.DepartmentName;
           else return "未知部门";
        
        }

        protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {

            DropDownList dlist = DetailsView1.FindControl("ddlDepartment") as DropDownList;
            TextBox txtName = DetailsView1.FindControl("txtLoginName") as TextBox;
            TextBox txtPwd = DetailsView1.FindControl("txtPassword") as TextBox;
            CheckBox chkSuper = DetailsView1.FindControl("chkSuperAdmin") as CheckBox;

            if (dlist == null || txtName == null || txtPwd == null || chkSuper == null)
            {
                Response.Write("出错，不能获取相关控件");
                return;
            }
            int did=int.Parse(dlist.SelectedValue);
          
            AdminUserInfo a = new AdminUserInfo();
            a.LoginName = txtName.Text;
            a.Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPwd.Text, "SHA1"); 
            a.DepartmentID = did;
            a.IsSuperAdmin = chkSuper.Checked;
            AdminUser.InsertAdminUser(a);
           this. ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功添加管理员信息')</script>");
            DetailsView1.Visible = false;
            GridView1.DataBind();
            e.Cancel = true;
        }

        protected void DetailsView1_ModeChanged(object sender, EventArgs e)
        {
            if (DetailsView1.CurrentMode == DetailsViewMode.ReadOnly) DetailsView1.Visible = false;
            else DetailsView1.Visible = true;
        }

        protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            DropDownList dlist = DetailsView1.FindControl("ddlDepartment") as DropDownList;
            TextBox txtName = DetailsView1.FindControl("txtLoginName") as TextBox;
            TextBox txtPwd = DetailsView1.FindControl("txtPassword") as TextBox;
            CheckBox chkSuper = DetailsView1.FindControl("chkSuperAdmin") as CheckBox;

            if (dlist == null || txtName == null || txtPwd == null || chkSuper == null)
            {
                Response.Write("出错，不能获取相关控件");
                return;
            }
            int did = int.Parse(dlist.SelectedValue);
            int adminID = 0;
           if(GridView1.SelectedValue!=null)
              adminID=int.Parse(GridView1.SelectedValue.ToString());
           if (adminID == 0)
           {
               e.Cancel = true;
               return;
           }

           ExamDbDataContext dc;
           AdminUserInfo a = AdminUser.GetAdminUserByID(adminID,out dc);
            a.LoginName = txtName.Text;
            a.Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPwd.Text, "SHA1");
            a.DepartmentID = did;
            a.IsSuperAdmin = chkSuper.Checked;
            AdminUser.UpdateAdminUser(a,dc);
            this. ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功修改管理员信息')</script>");
            DetailsView1.Visible = false;
            GridView1.DataBind();
            e.Cancel = true;
        }

        protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            
            GridView1.DataBind();
        }
    }
}
