using System;
using System.Collections;
using System.Configuration;
using System.Web.Configuration;
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
using System.Collections.Generic;
namespace SDPTExam.Web.UI.Admin
{
    public partial class TeacherManagement : AdminBasePage
    {
        SDPTExam.BLL.Teacher t = new SDPTExam.BLL.Teacher();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IsSuperAdmin"] != null)
                {
                    panelImport.Visible = true;
                    lblDept.Visible = true;
                    ddlSelectDepts.Visible = true;

                    //btnAddNew.Visible = false;
                }
                else
                {
                    lblInDept.Text = DeptName;
                    if (ConfigurationManager.AppSettings["CanBatchImportByDept"] != null)
                    {
                        if (ConfigurationManager.AppSettings["CanBatchImportByDept"].ToString().ToLower() == "true" && Session["IsAdmin"] != null)
                            panelImport.Visible = true;
                    }
                }
                BindGrid();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false || FileUpload1.PostedFile.ContentLength == 0)
                return;
            string fname = SaveFileToTemp();
            if (Teacher.InsertTeachersByExcleFile(fname) == false)
                Response.Write("导入失败，请确认格式正确，可参考页面右边样本文件");
            else this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功导入数据')</script>");

        }

        private string SaveFileToTemp()
        {
            if (FileUpload1.FileName.EndsWith("xls"))
            {
                FileUpload1.SaveAs(TempPath + "\\Teachers.xls");
                return TempPath + "\\Teachers.xls";
            }
            else if (FileUpload1.FileName.EndsWith("xlsx"))
            {
                FileUpload1.SaveAs(TempPath + "\\Teachers.xlsx");
                return TempPath + "\\Teachers.xlsx";
            }
            else
                return null;


        }

        protected void Button2_Click(object sender, EventArgs e)
        {


            BindGrid();
            GridView1.Visible = true;
            panelModify.Visible = false;
        }



        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int teacherID = int.Parse(GridView1.SelectedValue.ToString());
            Session["teacherId"] = teacherID;
            SetTheProfile(teacherID);
            btnSubmit.Text = "修改";
            panelModify.Visible = true;
            GridView1.Visible = false;
        }

        private void SetTheProfile(int teacherID)
        {
            TeacherInfo t = Teacher.GetTeacherByID(teacherID);
            if (t == null)
            {
                t = new TeacherInfo();
            }
            txtLoginName.Text = t.LoginName;
            txtRealName.Text = t.TeacherName;
            txtPassword.Text = t.Password;
            txtConfirmPwd.Text = t.Password;
            
            ddlSex.SelectedValue = t.Sex.ToString().ToLower();
            chkManager.Checked = t.IsMajorManager;

            if (teacherID == 0) return;

            lblInDept.Text =BaseData.GetDepartmentByID(t.DepartmentID).DepartmentName;
            int deptID = t.DepartmentID;
            ddlMajors.DataSource = BaseData.GetMajorsByDepartmentID(deptID);
            ddlMajors.DataBind();        


            if (t.MajorID != null && t.MajorID != 0) ddlMajors.SelectedValue = ((int)t.MajorID).ToString();
            else
            {
                ddlMajors.Items.Add(new ListItem("请选择专业", "0"));
                ddlMajors.SelectedValue = "0";
            }
            

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "添加")
            {
                //这里插入新教师 
                TeacherInfo newTeacher = new TeacherInfo();

                GetInfoByControls(newTeacher);
                if (Session["IsSuperAdmin"] != null)
                {
                    int deptID = int.Parse(ddlSelectDepts.SelectedValue);
                    if (deptID == 0)
                    {
                        lblError.Text = "请选择系别！";
                        lblError.Visible = true;
                        return;
                    }
                    else newTeacher.DepartmentID = deptID;
                }
                else newTeacher.DepartmentID = DeptID;


                if (Teacher.InsertTeacher(newTeacher) == true)
                    this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功添加教师信息')</script>");
                else
                    this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('登陆名已经存在，请换一个名称')</script>");
            }
            else
            {
                int tID = 0;
                if (Session["teacherId"] != null)
                {
                    tID = int.Parse(Session["teacherId"].ToString());
                }
                //不能获取教师id
                else
                {
                    return;
                }

                ExamDbDataContext dc;
                TeacherInfo t = Teacher.GetTeacherByID(tID, out dc);

                if (t == null)
                {
                    Response.Write("updating fail,t is null");
                    return;
                }
                GetInfoByControls(t);
                Teacher.UpdateTeacher(t, dc);

                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功修改教师信息')</script>");
                Session["teacherId"] = null;
            }
            BindGrid();
            GridView1.Visible = true;
            panelModify.Visible = false;

        }

        private void GetInfoByControls(TeacherInfo t)
        {
            t.LoginName = txtLoginName.Text;
            t.TeacherName = txtRealName.Text;

            if (string.IsNullOrEmpty(txtPassword.Text) == false)
            {
                string pwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");

                t.Password = pwdEncode;
            }
            // t.Major = txtMajor.Text;
            t.Sex = bool.Parse(ddlSex.SelectedValue);
            if (ddlMajors.SelectedValue != "0")
                t.MajorID = int.Parse(ddlMajors.SelectedValue);
            t.IsMajorManager = chkManager.Checked;
            //t.ProfessionalTitle = txtProfessionTitle.Text;
            //t.Position = txtPosition.Text;
            //t.OfficePhoneNum = txtOfficePhone.Text;
            //t.Email = txtEmail.Text;
            //t.MotherSchool = txtGraduatefrom.Text;
            //t.EduLeve = txtDegree.Text;
            //t.CellPhoneNum = txtCellNum.Text;
            //t.ResearchField = txtResearchField.Text;
            //t.QQNum = txtQQ.Text;
            //t.IsInSchool = chkInSchool.Checked;
            //t.PersonalDesc = txtProfileDesc.Text;
            //t.IsFullProfile = true;
        }


        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            GridView1.Visible = false;
            panelModify.Visible = true;
            btnSubmit.Text = "添加";
            int deptID = 0;
            if (Session["IsSuperAdmin"] != null)
            {

                deptID = int.Parse(ddlSelectDepts.SelectedValue);
                lblInDept.Text = ddlSelectDepts.SelectedItem.Text;
            }
            else deptID = DeptID;

            ddlMajors.DataSource = BaseData.GetMajorsByDepartmentID(deptID);
            ddlMajors.DataBind();

            lblError.Visible = false;

            SetTheProfile(0);
        }

        private void BindGrid()
        {
            int deptID = 0;
            if (ddlSelectDepts.Visible == true)
            {

                deptID = int.Parse(ddlSelectDepts.SelectedValue);
            }
            else deptID = DeptID;

            if (deptID == 0)
            {
                GridView1.DataSource = Teacher.GetAllTeachers();
            }
            else
            {
                GridView1.DataSource = Teacher.GetAllTeachersByDepartmentID(deptID);
                // objTeachers.SelectParameters["deptID"].DefaultValue = deptID.ToString();

            }
            GridView1.DataBind();





        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (GridView1.DataKeys[e.RowIndex].Value != null)
            {
                int sID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

                Teacher.DeleteTeacher(sID);
                BindGrid();
                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('已删除该教师')</script>");


            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('未能删除该教师')</script>");

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

                ExamDbDataContext dc ;
           IList<TeacherInfo> ts = Teacher.GetAllTeachers(out dc);
           dc.TeacherInfo.DeleteAllOnSubmit<TeacherInfo>(ts);
           dc.SubmitChanges();
        }



    }
}
