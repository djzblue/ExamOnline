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
using System.IO;
using System.Collections.Generic;

namespace SDPTExam.Web.UI.Admin
{
    public partial class StudentManagement : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Session["DepartmentID"] = 10002;
            if (!IsPostBack)
            {
                if (IsSuperAdmin())
                {
                    lnkbtnShowImport.Visible = true;
                    lblDept.Visible = true;
                    ddlSelectDepts.Visible = true;
                    //ddlMajors.Visible = false;

                }
                if (ConfigurationManager.AppSettings["CanBatchImportByDept"] != null)
                {
                    if (ConfigurationManager.AppSettings["CanBatchImportByDept"].ToString().ToLower() == "true" && Session["IsAdmin"] != null)
                        lnkbtnShowImport.Visible = true;
                }

                BindGrid();
            }
        }

        private bool IsSuperAdmin()
        {
            //return Session["IsSuperAdmin"] != null;
            return true;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelModify.Visible = false;
            GridView1.Visible = false;
            //DetailsView1.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false || FileUpload1.PostedFile.ContentLength == 0)
                return;
            string fname = SaveFileToTemp();
            lblFeedback.Visible = true;
            if (Student.InsertStudentsByExcleFile(fname) == false)
                lblFeedback.Text = "<br/>  导入失败，请确认格式正确，可参考页面右边样本文件";
            else lblFeedback.Text = "成功导入数据";
        }
        private string SaveFileToTemp()
        {
            if (FileUpload1.FileName.EndsWith("xls"))
            {
                FileUpload1.SaveAs(TempPath + "\\Students.xls");
                return TempPath + "\\Students.xls";
            }
            else if (FileUpload1.FileName.EndsWith("xlsx"))
            {
                FileUpload1.SaveAs(TempPath + "\\Students.xlsx");
                return TempPath + "\\Students.xlsx";
            }
            else
                return null;

        }

        protected void btnShowStudents_Click(object sender, EventArgs e)
        {

            lblFeedback.Visible = false;
            //DetailsView1.Visible = false;
            panelModify.Visible = false;
            BindGrid();
            GridView1.Visible = true;
        }

        private void BindGrid()
        {
            int deptID = 0;
            if (IsSuperAdmin())
            {

                deptID = int.Parse(ddlSelectDepts.SelectedValue);
            }

            else deptID = DeptID;

            int mId = int.Parse(ddlSelectMajor.SelectedValue);

            if (deptID == 0)
            {
                //ddlSelectMajor.Visible = false;
                GridView1.DataSource = Student.GetAllStudents();
            }
            else
            {

                GridView1.DataSource = Student.GetStudents(deptID, mId, null);
                
            }


            GridView1.DataBind();

        }

        protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            for (int i = 0; i < e.Values.Count; i++)
            {
                if (e.Values[i] != null) Response.Write("<br>" + e.Values[i].ToString());
            }
            //e.Cancel = true;

        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExamDbDataContext dc;
            if (e.CommandName == "modify")
            {
                int sid = int.Parse(e.CommandArgument.ToString());
                StudentInfo s = Student.GetStudentByID(sid, out dc);
               
                txtGrade.Text = s.Grade;
                txtHomeAddress.Text = s.HomeAddress;
                txtLoginName.Text = s.LoginName;
                txtRealName.Text = s.StuName;
                txtStuNum.Text = s.StuNum;
                //txtPassword.Text = s.Password;
                //txtPassword0.Text = s.Password;
                txtGrade.Text = s.Grade;
                //lblDeptAndMajor.Text = BaseData.GetDepartmentByID(s.DepartmentID).DepartmentName;
                //lblDeptAndMajor.Visible = true;
                ddlSex.SelectedValue = s.Sex.ToString().ToLower();

                Session["dc"] = dc;
                Session["stuInfo"] = s;
                btnSubmit.Text = "修改";
                //DetailsView1.Visible = false;
                GridView1.Visible = false;
                panelModify.Visible = true;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "确定添加")
            {
                StudentInfo s = new StudentInfo();
                if (GetInfoByControls(s) == false)
                    return;

                //s.ClassID = 0; ///先假设新增的班级id都为0
                Student.InsertStudent(s);
                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功添加学生信息')</script>");
            }
            else
            {
                if (Session["dc"] != null && Session["stuInfo"] != null)
                {
                    ExamDbDataContext dc = Session["dc"] as ExamDbDataContext;
                    StudentInfo oldstu = Session["stuInfo"] as StudentInfo;
                    if (GetInfoByControls(oldstu) == false)
                        return;
                    Student.UpdateStudent(oldstu, dc);
                    Session["dc"] = null;
                    Session["stuInfo"] = null;

                    this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功修改学生信息')</script>");
                }
                else { Response.Write("Session is null,can't modify"); return; }

            }
            BindGrid();
            GridView1.Visible = true;
            panelModify.Visible = false;
        }

        private bool GetInfoByControls(StudentInfo oldstu)
        {
            oldstu.StuName = txtRealName.Text;
            oldstu.StuNum = txtStuNum.Text;
            oldstu.LoginName = txtLoginName.Text;
            if (IsSuperAdmin() && btnSubmit.Text == "确定添加")
            {
                //lblDeptAndMajor.Text = ddlSelectDepts.SelectedItem.Text + " " + ddlSelectMajor.SelectedItem.Text;
                // lblDept.Visible = true;
                //int dID = int.Parse(ddlSelectDepts.SelectedValue);
                //int mID = int.Parse(ddlSelectMajor.SelectedValue);
                //if (dID == 0 || mID == 0) //没有选择系别专业
                //{
                //    lblError.Text = "请选择系别合专业!";
                //    lblError.Visible = true;
                //    return false;
                //}
                //else
                //{

               
                oldstu.DepartmentID = DeptMajorClassDropdownList1.DepartmentID;// dID;
                    oldstu.MajorID = DeptMajorClassDropdownList1.MajorID;
                    oldstu.ClassID = DeptMajorClassDropdownList1.ClassID;
                    oldstu.Class = DeptMajorClassDropdownList1.ClassName;
                    if (oldstu.DepartmentID == 0 || oldstu.MajorID == 0 || oldstu.ClassID == 0)
                    {
                        lblError.Text = "请依次选择系别专业班级!";
                        lblError.Visible = true;
                        return false;
                    }

                //}
            }
            else
            {
                oldstu.DepartmentID = DeptID;
                //oldstu.MajorID = int.Parse(ddlMajors.SelectedValue);
            }

            oldstu.Grade = txtGrade.Text;
            oldstu.HomeAddress = txtHomeAddress.Text;
           
            oldstu.Sex = bool.Parse(ddlSex.SelectedValue);
            oldstu.Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            return true;

        }


        protected string GetDeptNameByID(int deptID)
        {
            return BaseData.GetDepartmentByID(deptID).DepartmentName;
        }
        protected void btnAddNewStu_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblFeedback.Visible = false;
            btnSubmit.Text = "确定添加";
            GridView1.Visible = false;
            //  DetailsView1.Visible = false;
           
            txtGrade.Text = "";
            txtHomeAddress.Text = "";
            txtLoginName.Text = "";
            txtRealName.Text = "";
            txtStuNum.Text = "";
            panelModify.Visible = true;
            GridView1.Visible = false;


            //int deptID = 0;
            //if (IsSuperAdmin())
            //{

            //    deptID = int.Parse(ddlSelectDepts.SelectedValue);
            //}
            //else deptID = DeptID;
            //IList<MajorInfo> ms = BaseData.GetMajorsByDepartmentID(deptID);
            //if (ms == null)
            //{
            //    panelModify.Visible = false;
            //    lblError.Text = "请选择系别和专业!";
            //    lblError.Visible = true;
            //}
            //else
            //{
            //    ddlMajors.DataSource = ms;
            //    ddlMajors.DataBind();
            //}
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //DetailsView1.Visible = false;
            GridView1.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panelModify.Visible = false;
            GridView1.Visible = true;
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {

            Student.DeleteAllStudents();
            //Student.DeleteAllStudents(DeptID);
            //btnDeleteAll.Enabled = false;
        }

        protected void btnImportPhotos_Click(object sender, EventArgs e)
        {
            string extName = Path.GetExtension(filePhotos.PostedFile.FileName);

            if (filePhotos.HasFile && filePhotos.PostedFile.ContentLength > 0)
            {
                //是否应该限定文件类型？


                string fileContentType = filePhotos.PostedFile.ContentType;
                if (fileContentType == "application/x-msdownload")
                {
                    throw new Exception("不允许上传可执行文件");
                }


                if (extName.IndexOf("rar") == -1 && extName.IndexOf("zip") == -1)
                {
                    throw new Exception("上传失败，该文件不是压缩文件类型！！");
                }
            }
            else return;


            string tempPath = Server.MapPath(ConfigurationManager.AppSettings["TempPath"] + DeptID + "/");

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);


            string tempRarPath = tempPath + "StuPhotos.rar";
            filePhotos.SaveAs(tempRarPath);

            clsWinrar c = new clsWinrar();
            string sPath = Server.MapPath("~/Files/PublicFiles/Student/" + DeptID + "/Photos/");//这个是解压目录，可为各系的ID

            if (!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);

            c.unCompressRAR(sPath, tempPath, "StuPhotos.rar");
        }

        protected void ddlSelectDepts_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList d = sender as DropDownList;
            if (d.SelectedValue == "0")
            {
                ddlSelectMajor.Enabled = false;

            }
            else
            {
                int deptID = int.Parse(d.SelectedValue);
                ddlSelectMajor.Enabled = true;
                //ddlSelectMajor.Visible = true;
                ddlSelectMajor.AppendDataBoundItems = false;
                ddlSelectMajor.DataSourceID = "";
                ddlSelectMajor.DataSource = BaseData.GetMajorsByDepartmentID(deptID);
                ddlSelectMajor.DataBind();
                ddlSelectMajor.Items.Add(new ListItem("所有专业", "0"));

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();

        }

        protected void lnkbtnShowImport_Click(object sender, EventArgs e)
        {
            panelSuperAdmin.Visible = true;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (GridView1.DataKeys[e.RowIndex].Value != null)
            {
                int sID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

                Student.DeleteStudent(sID);
                BindGrid();
                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('已删除该学生')</script>");


            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('未能删除该学生')</script>");

            }
        }

        protected void lbtnHide_Click(object sender, EventArgs e)
        {
            panelSuperAdmin.Visible = false;
        }
    }
}
