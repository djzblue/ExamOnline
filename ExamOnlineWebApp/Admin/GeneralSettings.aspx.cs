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
using System.Xml;
using SDPTExam.DAL.Linq;
namespace SDPTExam.Web.UI.Admin
{
    public partial class GeneralSettings : AdminBasePage
    {
       TimeLimitInfo t;
        ExamDbDataContext dc;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(t==null)t = BaseData.GetTimeLimitByID(AppTimeLimitID,out dc);
            if (!IsPostBack)
            {
              
                if (t != null)
                {
                   
                    dpkStart.Text = t.startTime.ToString();
                    dpkEnd.Text = t.endTime.ToString();
                }            

            }


        }


        /// <summary>
        /// 修改选题时间和犹豫期时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            lblError.Text = "";

            if (t == null)
            {
                t = new TimeLimitInfo();
                t.startTime = DateTime.Parse(dpkStart.Text);
                t.endTime = DateTime.Parse(dpkEnd.Text).AddDays(1).AddSeconds(-1);
                if (t.startTime > t.endTime || t.endTime <= DateTime.Now)
                {
                    lblError.Text = "<br>时间填写错误，截止时间应该在开始时间和当前时间之后,";
                  
                    return;
                }
                int tid = BaseData.InsertTimeLimit(t);
                Utility.ModifyConfig(DeptID, "SubjectSelectTimitLimitID", tid.ToString());
            }
            else
            {
                if (dc == null)
                {
                    lblError.Text = "不能获取datacontext";
                    return;
                }
                t.startTime = DateTime.Parse(dpkStart.Text);
                t.endTime = DateTime.Parse(dpkEnd.Text).AddDays(1).AddSeconds(-1);
                //Response.Write("<br>开始时间变为："+t.startTime.ToString());
                //Response.Write("<br>结束时间变为：" + t.endTime.ToString());
                //Response.Write("<br>准备更新...");
                BaseData.UpdateTimeLimit(t,dc);
            }

           this. ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功修改配置！')</script>");
        }


    }
}
