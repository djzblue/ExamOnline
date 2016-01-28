using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;
using SDPTExam.DAL.Model;


namespace SDPTExam.Web.UI.Admin
{
    public partial class JudgeManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
        {
            JudgeInfo j = new JudgeInfo();

            j.Title = txtTitle.Text;
            j.RightAnswer =bool.Parse(rblAnswer.SelectedValue);

            Judge.InsertJudge(j);

            txtTitle.Text = "";

            this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功保存数据')</script>");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //LinkButton b = sender as LinkButton;
            //if (string.IsNullOrEmpty(b.CommandArgument)) return;
            //hideChoiceID.Value = b.CommandArgument;

            //imgBtnSave.ToolTip = "修改";
            //SetControlValue(int.Parse(hideChoiceID.Value));
            //panelAddnew.Visible = true;
            //GridView1.Visible = false;


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DAL.Linq.ExamDbDataContext dc = DataAccess.CreateDBContext();
            dc.JudgeInfo.DeleteAllOnSubmit(dc.JudgeInfo.Where(a=>true));
            dc.SubmitChanges();

        }

 
    }
}
