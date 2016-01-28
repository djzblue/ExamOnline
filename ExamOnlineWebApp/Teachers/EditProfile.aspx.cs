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
using SDPTExam.Web.UI;
using SDPTExam.Web.UI.Controls;
namespace SDPTExam.Web.UI.Teachers
{
    public partial class EditProfile : BasePage
    {
        private TeacherInfo t=null;
        ExamDbDataContext dc;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(t==null) t = Teacher.GetTeacherByID(UserID,out dc);
            if (!IsPostBack)
            {
               
                SetTheProfile();
            }
        }


        private void SetTheProfile()
        {
            if (t == null)
            {
                Response.Write("t==null");
                return;
            }
            lblPwd.Text = t.Password;
            txtLoginName.Text = t.LoginName;
            txtRealName.Text = t.TeacherName;
            //txtPassword.Attributes.Add("value", t.Password);// Text = t.Password;
         
            txtProfessionTitle.Text = t.ProfessionalTitle;

            txtOfficePhone.Text = t.OfficePhoneNum;
            txtResearchField.Text = t.ResearchField;
            txtEmail.Text = t.Email;
      

            txtCellNum.Text = t.CellPhoneNum;
            txtQQ.Text = t.QQNum;
         
            txtProfileDesc.Text = t.PersonalDesc;
            
            FileUploader1.FileName = DeptID+"_"+ t.TeacherName + "_" + t.TeacherID;
            if (string.IsNullOrEmpty(t.ImaPath)==false)
            {
                imgPhoto.ImageUrl = t.ImaPath;
                hlinkImage.NavigateUrl = t.ImaPath;
            }
            //else imgPhoto.ImageUrl = "~/images/nu";
        }


       protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (t == null)
            {
                Response.Write("updating fail,t is null");
                return;
            } 
           string pwd123=System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("123456", "SHA1");
            if (txtPassword.Text == "123456"||lblPwd.Text!=pwd123&&string.IsNullOrEmpty(txtPassword.Text))
            {
                Response.Write("初始密码必须修改为其他！");
                return;
            }
            bool hasAttachment = false;
            string attachmentPath = "";
            if (ViewState["hasAttachment"] != null)
                hasAttachment = (bool)ViewState["hasAttachment"];
            if (ViewState["attachmentPath"] != null)
                attachmentPath = ViewState["attachmentPath"].ToString();

            if (hasAttachment == true)
                t.ImaPath = attachmentPath;


            t.LoginName = txtLoginName.Text;
            t.TeacherName = txtRealName.Text;
            if (string.IsNullOrEmpty(txtPassword.Text) == false) 
            {
                string pwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");

                t.Password = pwdEncode;
            }
          
            t.ProfessionalTitle = txtProfessionTitle.Text;
     
            t.OfficePhoneNum = txtOfficePhone.Text;
            t.Email = txtEmail.Text;

            t.CellPhoneNum = txtCellNum.Text;
            t.ResearchField = txtResearchField.Text;
            t.QQNum = txtQQ.Text;

            t.PersonalDesc = txtProfileDesc.Text;

            Teacher.UpdateTeacher(t,dc);
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
               imgPhoto.ImageUrl = f.FullFilePath;
               hlinkImage.NavigateUrl = f.FullFilePath;
               t.ImaPath = f.FullFilePath;
               Teacher.UpdateTeacher(t, dc);
               this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('头像更新成功~！')</script>");
           }
       }
    }
}
