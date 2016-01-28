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
using SDPTExam.Web.UI;
using SDPTExam.DAL.Linq;
using SDPTExam.Web.UI.Controls;
namespace SDPTExam.Web.UI.Students
{
    public partial class EditProfile :BasePage
    {
        private StudentInfo s=null;
       private ExamDbDataContext dc;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(s==null)s = Student.GetStudentByID(UserID,out dc);
            if (Session["DepartmentID"] == null)
                Response.Redirect("~/login.aspx");
            if (!IsPostBack)
            {
                
                SetProfile();
                panelModify.Visible = true;
               
            }
        }

        private void SetProfile()
        {
            if (s == null)
            {
                Response.Write("s==null");
                return;
            }
            txtLoginName.Text = s.LoginName;
            txtRealName.Text = s.StuName;
            txtPassword.Text = s.Password;
            txtMajor.Text =BaseData.GetMajorByID(s.MajorID).MajorName;
            txtHomePhone.Text = s.HomePhoneNum;
            txtAddress.Text = s.HomeAddress;
            txtCellPhone.Text = s.CellPhone;
            txtGrade.Text = s.Grade;
            txtClass.Text = s.Class;
            txtDepartment.Text =BaseData.GetDepartmentByID(s.DepartmentID).DepartmentName;
            txtEmail.Text = s.Email;
            txtQQ.Text = s.QQNum;
            txtPersonalDesc.Text = s.PersonalDesc;
            txtStuNum.Text = s.StuNum;
            
        }

        private void SetTheProfile()
        {
            if (s != null)
            {

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (s == null)
            {
               lblMessage.Text="不能获取学生信息~";
               lblMessage.Visible = true;
                s = Student.GetStudentByID(UserID);
                if (s == null) Response.Redirect("~/Login.aspx");
               
            }
            string pwd123 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("123456", "SHA1");
            if (txtPassword.Text == "123456")
            {
                lblMessage.Text="初始密码必须修改为其他！";
                lblMessage.Visible = true;
                return;
            }
            else if (txtPassword.Text == "" && s.Password == pwd123)
            {
                lblMessage.Text = "必须修改初始密码为其他！";
                lblMessage.Visible = true;
                return;
            }

            bool hasAttachment = false;
            string attachmentPath = "";
            if (ViewState["hasAttachment"] != null)
                hasAttachment = (bool)ViewState["hasAttachment"];
            if (ViewState["attachmentPath"] != null)
                attachmentPath = ViewState["attachmentPath"].ToString();


            s.LoginName = txtLoginName.Text;
            s.StuName = txtRealName.Text;

            if (txtPassword.Text != "")
            {
                string pwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");

                s.Password = pwdEncode;
            }           
            //s.MajorID= txtMajor.Text  ;
            s.HomePhoneNum = txtHomePhone.Text;
            s.HomeAddress = txtAddress.Text;
            s.CellPhone = txtCellPhone.Text;
            s.Class = txtClass.Text;
            //  s.DepartmentID=;
            s.Email = txtEmail.Text;
            s.QQNum = txtQQ.Text;
            s.PersonalDesc = txtPersonalDesc.Text;
            s.StuNum = txtStuNum.Text;
            if (hasAttachment == true) s.ImagePath = attachmentPath;
            //else s.ImagePath = "no pic";
            
            s.IsFullProfile = true;
            s.Grade = txtGrade.Text;
            Student.UpdateStudent(s,dc);
            Session["FullProfile"] = true;

            ViewState["hasAttachment"] = null;
            ViewState["attachmentPath"] = null;

             this. ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功修改个人资料')</script>");
            Response.Redirect("ProfileManagement.aspx");
        }

    
        protected void SubmitFinished(object sender, EventArgs e)
        {
            FileUploader f = (FileUploader)sender;
            if (f == null) return;
            else
            {
                ViewState["hasAttachment"] = true;
                ViewState["attachmentPath"] = f.FullFilePath;
            }
        }
    }
}
