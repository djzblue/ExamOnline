using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.DAL.Model;
using SDPTExam.DAL.Linq;
using SDPTExam.BLL;
using System.Web.Security;

namespace SDPTExam.Web.UI.Admin
{
    public partial class LoginAs : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            ExamDbDataContext dc=DataAccess.CreateDBContext();
            
            if (ddlRole.SelectedValue == "学生")
            {
                ViewState["SelectedRole"] = "student";
                var stus = dc.StudentInfo.Where(p => p.StuName.Contains(txtUserName.Text) && p.DepartmentID == DeptID);
                if (stus != null)
                {
                    rblistName.DataSource = stus.ToList();
                    rblistName.DataTextField = "StuName";
                    rblistName.DataValueField = "StudentID";
                    rblistName.DataBind();
                }
            }
            else
            {
                ViewState["SelectedRole"] = "teacher";
                var teachers = dc.TeacherInfo.Where(p => p.TeacherName.Contains(txtUserName.Text)&&p.DepartmentID==DeptID);
                if (teachers != null)
                {
                    rblistName.DataSource = teachers.ToList();
                    rblistName.DataTextField = "TeacherName";
                    rblistName.DataValueField = "TeacherID";
                    rblistName.DataBind();

                }
                }

        }

        protected void btnLoginAs_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedRole"] == null) return;
            if (rblistName.SelectedItem != null)
            {
                int aid = int.Parse(rblistName.SelectedValue);

                if (ViewState["SelectedRole"].ToString() == "teacher")
                {
                   TeacherInfo t= Teacher.GetTeacherByID(aid);

                   if (t == null) return;
                    FormsAuthentication.SetAuthCookie(t.TeacherName, false);
                   Session["UserRealName"] = t.TeacherName;
                   Session["UserID"] = t.TeacherID;
                   Session["IsStudent"] = false;
             
                   if (t.IsMajorManager == true)
                   {
                       Session["Role"] = "majorManager";
                       Session["IsMajorManager"] = true;

                   }
                   else Session["Role"] = "teacher";

                   string basePath = Utility.GetConfigValue(t.DepartmentID, "UploadedFilePath");


                   Session["PersonalDirectory"] = basePath + "/PersonalFiles/Teacher/" + t.TeacherName + "_" + (t.TeacherNum == null ? "N" : t.TeacherNum);

                

                   Response.Write("<script>window.parent.navigate('../admin.html');</script>");
                }

                else 
                {
                    StudentInfo s = Student.GetStudentByID(aid);
                    if (s == null) return;
                    FormsAuthentication.SetAuthCookie(s.StuName, false);
                    Session["UserRealName"] = s.StuName;
                    Session["UserID"] = s.StudentID;
                    Session["IsStudent"] = true;
                    Session["MajorID"] = s.MajorID;
                    Session["Role"] = "student";
                    string basePath = Utility.GetConfigValue(s.DepartmentID, "UploadedFilePath");
                    Session["PersonalDirectory"] = basePath + "/PersonalFiles/Student/" + s.StuNum + "_" + s.StuName;
                
                    Session["FullProfile"] = s.IsFullProfile;
                 //   Response.Redirect("~/admin.html");

                    Response.Write("<script>window.parent.navigate('../admin.html');</script>");
                }

            }

        }
    }
}
