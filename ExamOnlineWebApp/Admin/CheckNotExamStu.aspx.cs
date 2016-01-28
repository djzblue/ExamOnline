using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SDPTExam.BLL;

namespace SDPTExam.Web.UI.Admin
{
    public partial class CheckNotExamStu : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                BindGrid(DeptMajorClassDropdownList1.ClassID);
            }
        }


        private void BindGrid(int p)
        {
            grvStuList.DataSource = RandomExam.GetNotExamStusByClassID(p);
            grvStuList.DataBind();
        }
        protected void grvStuMarkList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStuList.PageIndex = e.NewPageIndex;
            BindGrid(DeptMajorClassDropdownList1.ClassID);
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

            BindGrid(DeptMajorClassDropdownList1.ClassID);


            //Response.Write("hello");
        }
    }
}
