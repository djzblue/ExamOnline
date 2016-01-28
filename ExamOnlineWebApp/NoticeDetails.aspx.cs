using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SDPTExam.DAL.Model;
using SDPTExam.BLL;

namespace SDPTExam.Web.UI
{
    public partial class NoticeDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["NoticeID"] != null)
                {
                    try
                    {
                        int tid = int.Parse(Request.QueryString["NoticeID"]);
                        ShowNotice(tid);
                    }
                    catch
                    {
                        ShowError("链接ID必须是数字!");
                        
                    }


                }
                else
                {
                    ShowError("对不起，地址无效，不能直接访问本网页！");
                }
            }
        }


        protected void ShowNotice(int noticeID)
        {

            
            NoticeInfo n = Message.GetNoticeByID(noticeID);

            if (n == null)
            {
                //无法获取通知实体，
                ShowError("对不起，无法获取通知信息，链接ID无效");

                return;
            }
            lblAddedDate.Text = n.AddedTime.ToString();
            lblTitle.Text = n.Title;
            lblBody.Text = n.Body;
            lblType.Text = GetTypeName(n.Type);
            lblNoAttachment.Visible = !n.HasAttachment;
            lbtnAttachment.Visible = n.HasAttachment;
            lbtnAttachment.CommandArgument = n.AttachmentPath;
            lblFeekBack.Visible = false;
            
        }

        private void ShowError(string s)
        {
            lblError.Text = s;
            lblError.Visible = true;
            panelNoticeDetails.Visible = false;
        }

        protected void lbtnAttachment_Click(object sender, EventArgs e)
        {
            LinkButton l = sender as LinkButton;
            if (l == null) return;
            string fullFileName = Server.MapPath(l.CommandArgument);
            if (File.Exists(fullFileName) == false)
            {
                lblFeekBack.Text = "找不到对应的文件，可能已经给删除或改名！";
                lblFeekBack.Visible = true;
                return;
            }

            //lblFeekBack.Text = "文件全名：" + fullFileName;
            //lblFeekBack.Visible = true;
            FileDownload(fullFileName);
        }

        protected string GetTypeName(int typeID)
        {
            string[] typeNames = { "专业", "系部", "学院" };
            return typeNames[typeID - 1];
        }


        private long GetFileSize()
        {
            //if (_articleID != 0)
            //{
            //    Article download = Article.GetArticleByID(_articleID);
            //    string fileurl = download.AttachmentPath;
            //    fileurl = fileurl.Replace(this.FullBaseUrl, "");
            //    fileurl = "~/" + Session["DomainName"].ToString() + "/" + fileurl;

            //    string fullFileName = Server.MapPath(fileurl);


            //    FileInfo DownloadFile = new FileInfo(fullFileName);
            //    return DownloadFile.Length;
            //}
            //else return 0;
            return 0;
        }

        ///   <summary>   
        ///   文件下载   
        ///   </summary>   
        ///   <param   name="FullFileName"></param>   
        private void FileDownload(string FullFileName)
        {
            FileInfo DownloadFile = new FileInfo(FullFileName);
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.FullName, System.Text.Encoding.UTF8));
            Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
            Response.WriteFile(DownloadFile.FullName);
            Response.Flush();
            Response.End();

        }


    }
}
