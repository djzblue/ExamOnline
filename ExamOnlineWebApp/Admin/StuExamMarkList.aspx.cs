using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;
using SDPTExam.DAL.Model;
using System.Web.Configuration;
using System.IO;
using System.Text;


namespace SDPTExam.Web.UI.Admin
{
    public partial class StuExamMarkList : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HiddenField1.Value = WebConfigurationManager.AppSettings["ActiveBasicExamID"];
                BindGrid(DeptMajorClassDropdownList1.ClassID);
            }
        }

        protected string GetStuNumByStuID(int stuID)
        {

            StudentInfo s = Student.GetStudentByID(stuID);
            if (s != null)
                return s.StuNum;
            else return "未知";
      
        }


        protected string GetStuNameByStuID(int stuID)
        {
            StudentInfo s = Student.GetStudentByID(stuID);
            if (s != null)
                return s.StuName;
            else return "未知";
        }

        protected void btnReExam_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int reaxmID = int.Parse(b.CommandArgument);

            RandomExam.DeleteExam(reaxmID);
            BindGrid(DeptMajorClassDropdownList1.ClassID);
       
        }

        protected string GetClassNameByID(int id)           
        {
            if (id == 0) return "匿名游客";
            return BaseData.GetClassByID(id).ClassName;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
       
           BindGrid(DeptMajorClassDropdownList1.ClassID);
       

            //Response.Write("hello");
        }

        private void BindGrid(int p)
        {
           IQueryable<RandomExamInfo> rExams= RandomExam.GetRandomExamsByBasicExamID(int.Parse(HiddenField1.Value));

           var results = from s in rExams
                         where s.ClassID == p
                         select s;


           IQueryable<RandomExamInfo> rs;
           if (p != 0) rs= results;
           else
           {
               if (DeptMajorClassDropdownList1.MajorID != 0)
                   rs = RandomExam.GetRandomExamsByMajorID(DeptMajorClassDropdownList1.MajorID);

               else if (DeptMajorClassDropdownList1.DepartmentID != 0)
                   rs = RandomExam.GetRandomExamsByDeptID(DeptMajorClassDropdownList1.DepartmentID);
               else rs = rExams;
           }
           if (ViewState["SortDirection"] == null) ViewState["SortDirection"] = "Ascending"; 
           if (ViewState["SortExpression"] != null && rs != null)
           {
               string sortExpr = ViewState["SortExpression"].ToString();
               string sortDir= ViewState["SortDirection"].ToString();              
             
               switch (sortExpr)
               {
                   case "TotalGetMark":
                       rs = OrderBy(rs, q => q.TotalGetMark, sortDir);  // rs.OrderByDescending(q => q.TotalGetMark);
                       break;
                   case "ClassID":
                       rs =OrderBy(rs,q => q.ClassID,sortDir);
                       break;
                   case "SingleGetMark":
                       rs = OrderBy(rs,q => q.SingleGetMark,sortDir);
                       break;
                   case "MutilGetMark":
                       rs = OrderBy(rs,q => q.MutilGetMark,sortDir);
                       break;
                   case "JudgeGetMark":
                       rs = OrderBy(rs,q => q.JudgeGetMark,sortDir);
                       break;
                   case "HasFinished":
                       rs = OrderBy(rs,q => (bool)q.HasFinished?1:0,sortDir);
                       break;

                   default: break;
               }
           }
       

           grvStuMarkList.DataSource = rs;
            grvStuMarkList.DataBind();
        }

        private static IQueryable<RandomExamInfo> OrderBy(IQueryable<RandomExamInfo> rs,Expression<Func<RandomExamInfo,int>> exp,string sortDir)
        {
            if (sortDir == "Ascending")
                rs = rs.OrderBy(exp);
            else rs = rs.OrderByDescending(exp);
            return rs;
        }

        protected void grvStuMarkList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStuMarkList.PageIndex = e.NewPageIndex;
            BindGrid(DeptMajorClassDropdownList1.ClassID);
      // ObjectDataSource1.sel
        }

        protected void grvStuMarkList_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["SortExpression"] = e.SortExpression;

          //// Response.Write(e.SortDirection.ToString());
          // if (e.SortDirection==SortDirection.Ascending)
          //     e.SortDirection = SortDirection.Descending;
          // else e.SortDirection = SortDirection.Ascending;
            if (ViewState["SortDirection"] == null) ViewState["SortDirection"] = "Ascending"; 
            if (ViewState["SortDirection"].ToString() == "Ascending")
            {
                ViewState["SortDirection"] = "Descending";

            }
            else
            {
                ViewState["SortDirection"] = "Ascending";

            } 

            BindGrid(DeptMajorClassDropdownList1.ClassID);
        }

        protected void btnShowStudents_Click(object sender, EventArgs e)
        {
            BindGrid(DeptMajorClassDropdownList1.ClassID);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            HttpContext curContext = System.Web.HttpContext.Current;
            StringWriter strWriter = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(strWriter);
            curContext.Response.AppendHeader("Content-Disposition", "StudentMark.xls");

            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            curContext.Response.Charset = "UTF-8";

            //GridView GV = new GridView();//一个无分页的GridView
            //GV.DataSourceID = "SqlDataSource1";
            //GV.AllowPaging = false;
            //GV.DataBind();
            grvStuMarkList.AllowSorting = false;
            grvStuMarkList.AllowPaging = false;
            grvStuMarkList.GridLines = GridLines.Both;
            grvStuMarkList.Columns[5].Visible = false;
            //grvStuMarkList.Columns[2].Visible = false;
            grvStuMarkList.Columns[3].Visible = false;
            grvStuMarkList.Columns[4].Visible = false;
            //grvStuMarkList.Columns[6].Visible = false;
            grvStuMarkList.Columns[7].Visible = false;
            grvStuMarkList.Columns[8].Visible = false;
            grvStuMarkList.Columns[9].Visible = false;
            BindGrid(0);
            grvStuMarkList.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();

            grvStuMarkList.AllowPaging = true;
            grvStuMarkList.AllowSorting = true;
            grvStuMarkList.GridLines = GridLines.None;
            BindGrid(0);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //输出Excel文件用
        }

    }
}
