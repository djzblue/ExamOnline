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
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;

namespace SDPTExam.Web.UI.Admin
{
    public partial class BaseDataManagement : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IsSuperAdmin"] == null)
                {
                    Response.End();
                  // Response.Redirect("~/Admin.html");
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  dtvDept.ChangeMode(DetailsViewMode.Edit);
            int deptID =int.Parse(grvDepartments.SelectedValue.ToString());
            DepartmentInfo d = BaseData.GetDepartmentByID(deptID);

            txtDeptName.Text = d.DepartmentName;
            txtDeptDesc.Text = d.Description;
            chkIsTeaching.Checked = d.IsTeachingDept;

            btnAddDept.Text = "修改";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
           // dtvDept.ChangeMode(DetailsViewMode.Insert);

            txtDeptName.Text = "";
            txtDeptDesc.Text = "";
            chkIsTeaching.Checked = false;
        }

        protected void btnAddMajor_Click(object sender, EventArgs e)
        {
           // dtvMajor.ChangeMode(DetailsViewMode.Insert);
            btnAddM.Text = "添加";
            txtMajorName.Text = "";
            txtMajorDesc.Text = "";

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int majorID=int.Parse(grvMajors.SelectedValue.ToString());
            MajorInfo m=BaseData.GetMajorByID(majorID);
            txtMajorName.Text = m.MajorName;
            txtMajorDesc.Text = m.Description;
            ddlDept.SelectedValue = m.DepartmentID.ToString();
            btnAddM.Text = "修改";
           // dtvMajor.ChangeMode(DetailsViewMode.Edit);
        }

        protected void dtvMajor_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {


            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            panelDept.Visible = true;
            panelMajor.Visible = false;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            panelMajor.Visible = true;
            panelDept.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //GridView2.DataBind();
        }



       
        protected void Button6_Click(object sender, EventArgs e)
        {
            try
            {
               // dtvDept.UpdateItem(false);
            }
            catch (HttpException he)
            {
                Response.Write(he.Message);
            }
        }


        protected void btnAddDept_Click(object sender, EventArgs e)
        {
            if (btnAddDept.Text == "修改")
            {

                ExamDbDataContext dc;
                int deptID = int.Parse(grvDepartments.SelectedValue.ToString());
                DepartmentInfo d = BaseData.GetDepartmentByID(deptID,out dc);
                d.DepartmentName = txtDeptName.Text;
                d.Description = txtDeptDesc.Text;
                d.IsTeachingDept = chkIsTeaching.Checked;
                dc.SubmitChanges();

                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功修改部门信息！')</script>");
                btnAddDept.Text = "添加";
            }
            else
            {
                DepartmentInfo dept = new DepartmentInfo();

                dept.DepartmentName = txtDeptName.Text;
                dept.Description = txtDeptDesc.Text;
                dept.IsTeachingDept = chkIsTeaching.Checked;
                int deptID = BaseData.InsertDepartment(dept);

                Utility.AddDepartmentConfig(deptID, txtDeptName.Text);

                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功添加部门！')</script>");
            }
           grvDepartments.DataBind();

           
        }

        protected void grvDepartments_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            int deptID = (int)e.Keys["DepartmentID"];


        }

        protected void btnAddM_Click(object sender, EventArgs e)
        {
            if (btnAddM.Text == "修改")
            {
                ExamDbDataContext dc;
                int mID = int.Parse(grvMajors.SelectedValue.ToString());
                MajorInfo oldM = BaseData.GetMajorByID(mID,out dc);
                oldM.DepartmentID = int.Parse(ddlDept.SelectedValue);
                oldM.MajorName = txtMajorName.Text;
                oldM.Description = txtMajorDesc.Text;
                dc.SubmitChanges();
                grvMajors.DataBind();
                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功修改专业！')</script>");
                
            }
            else
            {
                MajorInfo m = new MajorInfo();
                m.DepartmentID = int.Parse(ddlDept.SelectedValue);
                m.MajorName = txtMajorName.Text;
                m.Description = txtMajorDesc.Text;
                BaseData.InsertMajor(m);
                grvMajors.DataBind();
                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功添加专业！')</script>");
            }
        }

        protected void grvDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ValidateOperation(e);
        }

        private void ValidateOperation(GridViewDeleteEventArgs e)
        {
            if (ConfigurationManager.AppSettings["TheOnlySuperAdmin"] != null)
            {
                string uname = UserRealName.Trim().ToLower();
                string aname = ConfigurationManager.AppSettings["TheOnlySuperAdmin"].Trim().ToLower();
                if (uname != aname)
                {
                    lblMessage.Text = "对不起，当前配置不允许您进行删除操作，如有需要请联系站长。";
                    e.Cancel = true;
                }
            }
        }

        protected void grvMajors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ValidateOperation(e);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false || FileUpload1.PostedFile.ContentLength == 0)
                return;
            string fname = SaveFileToTemp(FileUpload1,"Majors.xls");
            if (BaseData.InsertMajorsByExcleFile(fname) == false)
                Response.Write("导入失败，请确认格式正确，可参考页面右边样本文件");
            else this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功导入数据')</script>");
        }

        private string SaveFileToTemp(FileUpload f,string fname)
        {
            if (f.FileName.EndsWith("xls"))
            {
                f.SaveAs(TempPath + "\\"+fname);
                return TempPath + "\\" + fname;
            }
            else if (f.FileName.EndsWith("xlsx"))
            {
                f.SaveAs(TempPath + "\\" + fname + "x");
                return TempPath + "\\" + fname+"x";
            }
            else
                return null;


        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            BaseData.CheckClassID();

            if (FileUpload2.HasFile == false || FileUpload2.PostedFile.ContentLength == 0)
                return;
            string fname = SaveFileToTemp(FileUpload2,"classes.xls");
            BaseData b=new BaseData();
            if (b.InsertClasssByExcleFile(fname) == false)
                Response.Write("导入失败，请确认格式正确，可参考页面右边样本文件");
            else this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功导入数据')</script>");

        }

        protected void Button6_Click1(object sender, EventArgs e)
        {
            RandomExam.CheckZeroRandomExam();
        }
    }


}
