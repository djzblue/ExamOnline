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
namespace SDPTExam.Web.UI.Teachers
{
    public partial class ProfileManagement : BasePage
    {
        private static TeacherInfo t;     
          
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                t = Teacher.GetTeacherByID(UserID);
               //Response.Write("userid=" + UserID);
                ShowProfile();
               
            }
        }

        private void ShowProfile()
        {
            if (t == null)
            {
                Response.Write("t==null");
                return;
            }
            lblLoginName.Text = t.LoginName;
            lblRealName.Text = t.TeacherName;
//            lblPassword.Text = t.Password;
         
            lblProfessionTitle.Text = ReturnValidString(t.ProfessionalTitle);

            lblOfficePhone.Text = ReturnValidString(t.OfficePhoneNum);
            lblEmail.Text = ReturnValidString(t.Email);

            lblCellNum.Text = ReturnValidString(t.CellPhoneNum);
            lblResearchField.Text = ReturnValidString(t.ResearchField);
            lblQQ.Text = ReturnValidString(t.QQNum);
           if(t.ImaPath!=null)imgPhoto.ImageUrl = t.ImaPath;

            lblProfileDesc.Text = ReturnValidString(t.PersonalDesc);
        }


        

        private string ReturnValidString(string s)
        {
            if (string.IsNullOrEmpty(s)) return "未填写！";
            else return s;
        }

  

        protected void btnShowModify_Click(object sender, EventArgs e)
        {
            //SetTheProfile();
            //panelModify.Visible = true;
            Response.Redirect("EditProfile.aspx");
        }

       
    }
}
