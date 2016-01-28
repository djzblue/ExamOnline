using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;
using SDPTExam.Web.UI;
using SDPTExam.DAL.Model;

namespace SDPTExam.Web.UI.Students
{
    public partial class MyMark :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChkExamState();
            }
        }

        private void ChkExamState()
        {
            //RandomExamInfo r = RandomExam.GetRandomExamByStuIDAndBasicExameID(UserID);
            //if (r == null)
            //    lblMyMark.Text = "尚未考试";
            //else if ((bool)r.HasFinished)
            //{

            //    lblMyMark.Text = r.TotalGetMark.ToString();
            //    if (r.TotalGetMark < 60)
            //    {
            //        btnReExam.Enabled = true;//若不及格，允许重新考试。
            //        btnReExam.CommandArgument = r.RandomExamID.ToString();
            //    }
            //}
            //else
            //{
            //    lblMyMark.Text = "正在考试中，请抓紧时间完成！";
            //}
        }

        protected void btnReExam_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int reaxmID = int.Parse(b.CommandArgument);

            RandomExam.DeleteExam(reaxmID);
           // BindGrid(DeptMajorClassDropdownList1.ClassID);
            btnReExam.Enabled = false;

            lblMyMark.Text = "请点击左边菜单（进入考试），重考一次！！";
        }
    }
}
