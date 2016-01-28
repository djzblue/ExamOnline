using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;
using SDPTExam.Web.UI;
namespace SDPTExam.Web.UI.Admin
{
    public partial class ImportExamData : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false || FileUpload1.PostedFile.ContentLength == 0)
                return;
            string fname = SaveFileToTemp("ChoicesData",FileUpload1);
            if (Utility.InsertChoiceItemsByExcleFile(fname) == false)
                Response.Write("导入失败，请确认格式正确，可参考页面右边样本文件");
            else this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功导入数据')</script>");

        }

        private string SaveFileToTemp(string fileName,FileUpload f)
        {
            if (f.FileName.EndsWith("xls"))
            {
                f.SaveAs(TempPath + "\\"+fileName+".xls");
                return TempPath + "\\"+fileName+".xls";
            }
            else if (f.FileName.EndsWith("xlsx"))
            {
                f.SaveAs(TempPath + "\\"+fileName+".xlsx");
                return TempPath + "\\"+fileName+".xlsx";
            }
            else
                return null;


        }

        protected void btnJudges_Click(object sender, EventArgs e)
        {
            if (fileJudges.HasFile == false || fileJudges.PostedFile.ContentLength == 0)
                return;
            string fname = SaveFileToTemp("JudgesData",fileJudges);
            if (Utility.InsertJudgesByExcleFile(fname) == false)
                Response.Write("导入失败，请确认格式正确，可参考页面右边样本文件");
            else this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功导入数据')</script>");
        }

        protected void btnQuestions_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false || FileUpload1.PostedFile.ContentLength == 0)
                return;
            string fname = SaveFileToTemp("QuestionData", FileUpload1);
            if (Utility.InsertQuestionsByExcleFile(fname) == false)
                Response.Write("导入失败，请确认格式正确，可参考页面右边样本文件");
            else this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功导入试题数据')</script>");

        }
    }
}
