using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;
using SDPTExam.DAL.Model;
using SDPTExam.Web.UI;
using System.Web.Configuration;

namespace SDPTExam.Web.UI.Admin
{


    public partial class QueryShow : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
           if(!IsPostBack) BindGrid(0);
        }

        string GetPencent(int a, int sum)
        {
            double result = 100*(double)a / (double)sum;

           result= Math.Round(result, 2);

           return result.ToString() + "%";            
        
        }



        void BindGrid(int classID)
        {
            
           // Response.Write("classID="+classID+"<br/>");
            string basicExamID = WebConfigurationManager.AppSettings["ActiveBasicExamID"];
            if (basicExamID == null)
            {
                Response.Write("无法获取basicExamID");
                return;
            }
            int examID = int.Parse(basicExamID);
          

           // int total = count;

            //int pass = 0;

            //int excellent = 0;


            IQueryable<ExamResult> rr = QueryTool.GetAllQueryResults(examID, classID);//将方法移到业务层，使用缓存

            //var rc = from s in rr
            //         where s.ClassID == classID
            //         select s;
            //var rm = from s in rr
            //         where s.MajorID == DeptMajorClassDropdownList1.MajorID
            //         select s;
            //var rd = from s in rr
            //         where s.DeptID == DeptMajorClassDropdownList1.DepartmentID
            //         select s;

            //var ra = from s in rr
            //         select s;

            //if (classID != 0)
            //{
            //    grvResultList.DataSource = rc;
            //}
            //else
            //{
            //    if (DeptMajorClassDropdownList1.MajorID != 0)
            //        grvResultList.DataSource = rm;//rr.Where(p => p.MajorID == DeptMajorClassDropdownList1.MajorID);
            //    else if (DeptMajorClassDropdownList1.DepartmentID != 0)
            //        grvResultList.DataSource =rd;// rr.Where(p => p.DeptID == DeptMajorClassDropdownList1.DepartmentID);
            //    else grvResultList.DataSource = ra;

            //}

            if (rr.Count() == 0) return;

            grvResultList.DataSource = rr;
            grvResultList.DataBind();

   //foreach (var ra in rr)
            //{
            //    int totalCount = ra.Count();

            //   foreach(RandomExamInfo s in ra)
            //   {
               
            //   }
            //}
           
            //foreach (RandomExamInfo r in results)//暂时未区分班级
            //{
                
            //    if (r.TotalGetMark >= 80)
            //    {
            //        excellent++;
            //        pass++;
            //    }
            //    else if (r.TotalGetMark >= 60)
            //    {
            //        pass++;
            //    }          
               
            //}

           //double agvMark= results.Average(p => p.TotalGetMark);

           // double jigelv = 100 * (double)pass/ (double)total;

           // double youlianglv = 100 * (double)excellent / (double)total;

           // lblExcellent.Text = youlianglv.ToString() + "%";

           // lblPass.Text = jigelv.ToString() + "%";

           // lblAvg.Text = agvMark + "分";
        

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

                BindGrid(DeptMajorClassDropdownList1.ClassID);
          
           
        }


        protected string GetClassNameByID(int id)
        {
           return BaseData.GetClassByID(id).ClassName;
        }

        protected void grvResultList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvResultList.PageIndex = e.NewPageIndex;
            BindGrid(DeptMajorClassDropdownList1.ClassID);
        }
    }
}
