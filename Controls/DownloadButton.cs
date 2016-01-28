using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.Web.UI;
namespace ExamManagement.Controls
{
    /// <summary>
    /// 自定义下载按钮
    /// </summary>
    /// 
    [Themeable(true)]
    [ToolboxData("<{0}:DownloadButton ID=\"DButton\" runat=\"server\"></{0}:DownloadButton>")]
    public class DownloadButton:LinkButton
    {

        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue(false)]
        [Description("文件下载路径")]
        [Localizable(true)]
        public string DownloadFilePath
        {
            get
            {
                String s = (String)ViewState["DownloadFilePath"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["DownloadFilePath"]=value; 
            }
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            FileDownload(DownloadFilePath);


        }

        ///   <summary>   
        ///   文件下载   
        ///   </summary>   
        ///   <param   name="FullFileName"></param>   
        private void FileDownload(string FullFileName)
        {
            FileInfo DownloadFile = new FileInfo(FullFileName);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.FullName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
            HttpContext.Current.Response.WriteFile(DownloadFile.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }
    }
}
