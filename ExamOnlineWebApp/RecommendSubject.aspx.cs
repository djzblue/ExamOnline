using System;
using System.Collections;
using System.Collections.Generic;
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

namespace SDPTExam.Web.UI
{
    public partial class RecommendSubject : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            
            chkSelected.Visible = IsStudent;
            chkToStudent.Visible = !IsStudent;
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["studentID"]) == false)
                {
                    int studentID = int.Parse(Request.QueryString["studentID"]);
                    if (studentID != 0)
                    {
                        ViewState["studentID"] = studentID;
                        chkToStudent.Checked = true;
                        chkToStudent_CheckedChanged(null, null);
                    }
                }
            }

         
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //IList<SubjectInfo> subjects = Subject.GetFormalSubjectsByTitle(txtTitle.Text, DeptID,MajorID);
            //if (subjects != null && subjects.Count > 0)
            //{
            //    lblFeedback.Text = "该课题标题已经存在，请输入其它课题！ : "+subjects[0].Title;
            //    return;
            //}

            SubjectInfo s = new SubjectInfo();
            s.Title = txtTitle.Text;
            s.Description = txtBody.Text;
            s.DepartmentID = DeptID;
            s.Reference = txReference.Text;
            s.IsChecked = false;
            s.IsFormal = false;
            s.AuthorID = UserID;
            s.AuthorIsStudent = IsStudent;
            if (IsStudent == false)
            {
                if (Utility.GetConfigValue(DeptID, "SubjectManagedByTeacher").Equals("true", StringComparison.OrdinalIgnoreCase))
                    s.IsFormal = true;

                s.AuthorName = UserRealName + " 老师";
            }
            else s.AuthorName = UserRealName + " 同学";
            s.MajorID =int.Parse(ddlMajors.SelectedValue);


           int subjectID= Subject.InsertSubject(s);

            ///如果导师为学生推荐该课题。
           if (IsStudent == false && chkToStudent.Checked == true)
           { 
               int stuID=int.Parse(ddlStudents.SelectedValue);
               RecommendSubjectToStudent(s, stuID);
           }


           bool isCheckPassed = false;
            //如果作者是学生，则自动将其推荐的课题作为其拟选课题。
           if (IsStudent == true && chkSelected.Checked == true)
           {
               //ChangeSelectedSubject(subjectID, ref isCheckPassed);
           
           }
           if (s.IsFormal == false)
           {
               if (IsStudent == true && chkSelected.Checked == true)
               {
                   if (isCheckPassed == true)
                   { 
                       lblFeedback.Text = "成功添加课题，将等待管理员或导师审核!但你先前所选课题已经过导师审核，无法更改选题,请与管理员联系！"; 
                   }
                   else lblFeedback.Text = "成功添加课题，并已将该课题作为您的拟选题目，将等待管理员或导师审核!";
               }
               else lblFeedback.Text = "成功添加课题，等待管理员审核入库。。。";

           }
           else lblFeedback.Text = "成功添加课题，该课题已入库！";
           BindGrid();
        }


        /// <summary>
        /// 为学生推荐课题
        /// </summary>
        /// <param name="subjectID"></param>
        /// <param name="studentID"></param>
        private void RecommendSubjectToStudent(SubjectInfo s, int studentID)
        {
                

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panelAddSubject.Visible = false;
        }

        protected void chkToStudent_CheckedChanged(object sender, EventArgs e)
        {
           // if(IsStudent==true) return;
           // IList<StudentToSelect> stus=Teacher.GetStudentsByTeacherID(UserID,false);
           // if (stus == null)
           // {
           //     lblError.Visible = true;
           //     chkToStudent.Checked = false;
           //     return;
           // }
           // ddlStudents.DataSource = stus;
           //ddlStudents.DataBind();
           //if (ViewState["studentID"] != null)
           //{
           //    string stuID = ViewState["studentID"].ToString();
           //    ddlStudents.SelectedValue = stuID;
           //}
           //else ddlStudents.Items[0].Selected = true;//默认选中第一项
           // ddlStudents.Visible = true;
        }

        protected void btnMySubjects_Click(object sender, EventArgs e)
        {
            BindGrid();
            lblFeedback.Text = "";
            panelAddSubject.Visible = false;
            
        }

        void BindGrid()
        {
            IList<SubjectInfo> subjects = Subject.GetSubjectsByAuthorID(UserID, IsStudent, DeptID);
            grvSubjects.DataSource=subjects;
            grvSubjects.DataBind();
            grvSubjects.Visible = true;

        }

        protected void btnAddNewSubject_Click(object sender, EventArgs e)
        {
            lblFeedback.Text = "";
            grvSubjects.Visible = false;
            panelAddSubject.Visible = true;
        }

        protected void grvSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(grvSubjects.SelectedValue.ToString());

            if (Subject.GetSubjectByID(id) != null)
            {
                if (Subject.GetSubjectByID(id).IsFormal == false)
                {
                    lblFeedback.Text = "你的课题还未入库，无法拟选";
                    return;
                }

            }         

               bool isCheckPassed = false;
          //  ChangeSelectedSubject(id, ref isCheckPassed);
           if (isCheckPassed ==true)
            {
                lblFeedback.Text = "你先前所选课题已经过导师审核，无法更改选题,请与管理员联系！";
                return;
            }

    
            //grvSubjects.DataBind();
            BindGrid();
        }




 
    }
}
