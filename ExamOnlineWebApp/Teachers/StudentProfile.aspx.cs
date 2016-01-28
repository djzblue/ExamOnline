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

namespace SDPTExam.Web.UI.Teachers
{
    public partial class StudentProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

     

        private void ShowProfile(StudentInfo s)
        {
            if (s == null)
            {
                Response.Write("对不起，暂未分配学生给您！");
                return;
            }            
            //lblRealName.Text = ReturnValidString(s.StuName);
            //lblMajor.Text = BaseData.GetMajorByID(s.MajorID).MajorName;
            //lblHomePhone.Text = ReturnValidString(s.HomePhoneNum);
            //lblAddress.Text = ReturnValidString(s.HomeAddress);
            //lblCellPhone.Text = ReturnValidString(s.CellPhone);
            //lblClass.Text = ReturnValidString(s.Class);
            //lblDepartment.Text =BaseData.GetDepartmentByID(s.DepartmentID).DepartmentName;
            //lblEmail.Text = ReturnValidString(s.Email);
            //lblQQ.Text = ReturnValidString(s.QQNum);
            //lblGrade.Text = ReturnValidString(s.Grade);
            //lblPersonalDesc.Text = ReturnValidString(s.PersonalDesc);
            //lblStuNum.Text = ReturnValidString(s.StuNum);
            //imgPhoto.ImageUrl = s.ImagePath;
            //panelCurrentProile.Visible = true;
        }

        private string ReturnValidString(string s)
        {
            if (string.IsNullOrEmpty(s)) return "未填写！";
            else return s;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sid = int.Parse(GridView1.SelectedValue.ToString());
            StudentInfo student = Student.GetStudentByID(sid);
            ShowProfile(student);
        }

    }
}
