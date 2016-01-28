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

namespace SDPTExam.Web.UI.Students
{
    public partial class ProfileManagement :BasePage
    {
        private static StudentInfo s;  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DepartmentID"] == null)
                Response.Redirect("~/login.aspx");
            if (!IsPostBack)
            {
                if (Request.QueryString["StudentID"] != null)
                {
                    try
                    {
                        int sID = int.Parse(Request.QueryString["StudentID"]);
                        s = Student.GetStudentByID(sID);
                    }
                    catch
                    {

                    }


                }
                else
                {
                    s = Student.GetStudentByID(UserID);
                }
                ShowProfile();
            }
        }


    

        private void ShowProfile()
        {
            if (s == null)
            {
                Response.Write("s==null");
                return;
            }
            lblLoginName.Text = ReturnValidString(s.LoginName);
            lblRealName.Text = ReturnValidString(s.StuName);
           // lblPassword.Text = ReturnValidString(s.Password);
            lblMajor.Text = BaseData.GetMajorByID(s.MajorID).MajorName;
            lblHomePhone.Text = ReturnValidString(s.HomePhoneNum);
            lblAddress.Text = ReturnValidString(s.HomeAddress);
            lblCellPhone.Text = ReturnValidString(s.CellPhone);
            lblClass.Text = ReturnValidString(s.Class);
            lblDepartment.Text = BaseData.GetDepartmentByID(s.DepartmentID).DepartmentName;
            lblEmail.Text = ReturnValidString(s.Email);
            lblQQ.Text = ReturnValidString(s.QQNum);
            lblGrade.Text = ReturnValidString(s.Grade);
            lblPersonalDesc.Text = ReturnValidString(s.PersonalDesc);
            lblStuNum.Text = ReturnValidString(s.StuNum);
            if (string.IsNullOrEmpty(s.ImagePath) == false) imgPhoto.ImageUrl = s.ImagePath;
        }

        private string ReturnValidString(string s)
        {
            if (string.IsNullOrEmpty(s)) return "未填写！";
            else return s;
        }


   

     
    }
}
