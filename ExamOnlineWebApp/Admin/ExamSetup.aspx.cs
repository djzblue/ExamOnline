using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;
using SDPTExam.DAL.Model;
using SDPTExam.DAL.Linq;
using System.Xml;

namespace SDPTExam.Web.UI.Admin
{
    public partial class ExamSetup : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgBtnConfirm_Click(object sender, ImageClickEventArgs e)
        {
            int courseID=int.Parse(ddlCourses.SelectedValue);

            if (courseID == 0)
            {
                lblMessage.Text = "请选择课程！！";
                return;
            }
            
            int singNum = int.Parse(txtSingleNum.Text);

            int singMark = int.Parse(txtSingleFen.Text);

            int mutilNum = int.Parse(txtMultiNum.Text);

            int mutilMark = int.Parse(txtMultiFen.Text);

            int judgeNum = int.Parse(txtJudgeNum.Text);

            int judgeMark = int.Parse(txtJudgeFen.Text);

            int totalMark=int.Parse(txtTotalMark.Text);
            int  sumMark= singMark * singNum + mutilMark * mutilNum + judgeMark * judgeNum;

            if (totalMark != sumMark)
                lblMessage.Text = "试卷总分不匹配，请重新计算！";
            else
            { 
             //插入试卷。
                BasicExamInfo exam = null;

                ExamDbDataContext dc=new ExamDbDataContext();

                if (string.IsNullOrEmpty(hideExamID.Value))
                {
                    exam = new BasicExamInfo();
                    exam.AddedDate = DateTime.Now;
                }
                else exam = BasicExam.GetBasicExamByID(int.Parse(hideExamID.Value), out dc);

                exam.BasicExamTitle = txtPaperName.Text;
                exam.BasicExamDesc = txtDesc.Text;
                exam.JudgeNum = judgeNum;
                exam.JudgeMark = judgeMark;
                exam.MutilChoiceNum = mutilNum;
                exam.MutilChoiceMark = mutilMark;
                exam.SingChoiceNum = singNum;
                exam.SingChoiceMark = singMark;
                exam.CourseID = courseID;

                

                if (ddlChapters.Items.Count == 0)
                    exam.ChapterID = 0;
                else exam.ChapterID = int.Parse(ddlChapters.SelectedValue);

                exam.TimeUse = int.Parse(txtTotalTime.Text);
                
                
                
                 if(string.IsNullOrEmpty(hideExamID.Value))
                BasicExam.InsertBasicExam(exam);
                 else dc.SubmitChanges();
                this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功保存试卷')</script>");

                ClearTextBoxValue();

                hideExamID.Value="";
            
            }
            Egv.DataBind();

        }

        private void ClearTextBoxValue()
        {
            //foreach (Control c in this.Controls)
            //{
            //    if (c is TextBox)
            //    {
            //        TextBox t = c as TextBox;
            //        t.Text = "";
            //    }
            //}

            txtSingleNum.Text = "";

            txtSingleFen.Text = "";

            txtMultiNum.Text = "";

            txtMultiFen.Text = "";
            txtJudgeNum.Text = "";

            txtJudgeFen.Text = "";
            txtPaperName.Text = "";
            txtDesc.Text = "";
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnSetActive_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (string.IsNullOrEmpty(b.CommandArgument)) return;
            int examID = int.Parse(b.CommandArgument);
            BasicExam.SetActiveExam(examID);

            XmlDocument xmldoc = new XmlDocument();
            string configPath = Server.MapPath("~/AppSettings.config");//转换成实际的物理路径
            try
            {
                xmldoc.Load(configPath);
                Utility.ModifyConfig(xmldoc, "ActiveBasicExamID", examID.ToString());
                xmldoc.Save(configPath);
            }
            catch
            {
                //Response.Write("<script>alert('读文件时错误,请检查 Web.config 文件是否存在!')</script>");
                return;
            }

            Egv.DataBind();

        }

 

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            if (string.IsNullOrEmpty(b.CommandArgument)) return;
            hideExamID.Value=b.CommandArgument;
            SetControlValue();

        }


        private void SetControlValue()
        {

            if (string.IsNullOrEmpty(hideExamID.Value)) return;
            int examID = int.Parse(hideExamID.Value);
            BasicExamInfo exam = BasicExam.GetBasicExamByID(examID);

            txtSingleNum.Text=exam.SingChoiceNum.ToString();

            txtSingleFen.Text=exam.SingChoiceMark.ToString();

            txtMultiNum.Text=exam.MutilChoiceNum.ToString();

            txtMultiFen.Text=exam.MutilChoiceMark.ToString();

            txtJudgeNum.Text=exam.JudgeNum.ToString();

            txtJudgeFen.Text=exam.JudgeMark.ToString();
             txtPaperName.Text =exam.BasicExamTitle;
            txtDesc.Text =exam.BasicExamDesc ; 
            
            ddlCourses.SelectedValue = exam.CourseID.ToString();

            if (exam.ChapterID == 0) BindChapters();

            ddlChapters.SelectedValue = exam.ChapterID.ToString();
           

            txtTotalTime.Text = exam.TimeUse.ToString();
          
            //txtTotalMark.Text);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            hideExamID.Value = "";

            ClearTextBoxValue();


        }

        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChapters();          

        }

        private void BindChapters()
        {
            ddlChapters.Items.Clear();
            ddlChapters.AppendDataBoundItems = true;
            ddlChapters.Items.Add(new ListItem("所有章节", "0"));
            ddlChapters.DataBind();
        }


    }
}
