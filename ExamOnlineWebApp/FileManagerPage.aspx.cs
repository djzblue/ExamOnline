using System;
using System.Collections;
using System.Collections.Generic;
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
using System.IO;
using FileManager;

namespace SDPTExam.Web.UI
{
    public partial class FileManagerPage : BasePage
    {
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["PersonalDirectory"] == null)
            {
                FileSystemManager.StrRootFolder = Server.MapPath("~/Files/Temp");
                

            }
            else
            {
                string dir = Session["PersonalDirectory"].ToString();
                string physicalPath = Server.MapPath(dir);
                if (Directory.Exists(physicalPath) == false) Directory.CreateDirectory(physicalPath);
                FileSystemManager.StrRootFolder = physicalPath;
                ShowVolume();
                
            }
            BindGrid();
        }
    }

    private void ShowVolume()
    {
        long currentSize = FileSystemManager.GetDirectoryOrFileLength(FileSystemManager.StrRootFolder);
        Session["CurrentSize"] = currentSize;
        lblSize.Text = "您的空间总容量为<font color='red'>10兆</font>,目前剩余：<font color='red'>";
        long leftSize = 10 * 1024 * 1024 - currentSize;
        double toMSize = (double)leftSize / 1024 / 1024;
        lblSize.Text += toMSize.ToString("0.00") +"兆</font>";
    }

    #region BindGrid()
    private void BindGrid()
    {
        List<FileSystemItem> list = FileSystemManager.GetItems();
        GridView1.DataSource = list;
        GridView1.DataBind();
        lblCurrentPath.Text = FileSystemManager.GetRootPath();
    }

    private void BindGrid(string path)
    {
        lblFeekBack.Text = "";
        List<FileSystemItem> list = FileSystemManager.GetItems(path);
        GridView1.DataSource = list;
        GridView1.DataBind();
        lblCurrentPath.Text = path;
       string newPath= path.Replace(FileSystemManager.StrRootFolder, "");
       if (newPath != "")
       {
           newPath = newPath.Replace("\\", "-->");
           newPath = "根目录" + newPath;
       }
       else newPath = "根目录";
       lblVirtualPath.Text = newPath;
        ShowVolume();
    }
    #endregion

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lb = (LinkButton)e.Row.Cells[1].FindControl("LinkButton1");
            if (lb.Text != "[根目录]" && lb.Text != "[上一级]")
            {
                if (Directory.Exists(lb.CommandArgument.ToString()))
                {
                    lb.Text = string.Format("<img src=\"images/file/folder.gif\" style=\"border:none; vertical-align:middle;\" /> {0}", lb.Text);
                }
                else
                {
                    string ext = lb.CommandArgument.ToString().Substring(lb.CommandArgument.LastIndexOf(".") + 1);
                    if (File.Exists(Server.MapPath(string.Format("images/file/{0}.gif", ext))))
                    {
                        lb.Text = string.Format("<img src=\"images/file/{0}.gif\" style=\"border:none; vertical-align:middle;\" /> {1}", ext, lb.Text);
                    }
                    else
                    {
                        lb.Text = string.Format("<img src=\"images/file/other.gif\" style=\"border:none; vertical-align:middle;\" /> {0}", lb.Text);
                    }
                }
            }
            else
            {
                e.Row.Cells[0].Controls.Clear();
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Directory.Exists(e.CommandArgument.ToString()))
        {
            BindGrid(e.CommandArgument.ToString());
        }
        else
        {//打开对应文件
            string fullFileName = e.CommandArgument.ToString();

          //  Response.Write(path);
                       
            if (File.Exists(fullFileName) == false)
            {
                lblFeekBack.Text = "找不到对应的文件，可能已经给删除或改名！";
                lblFeekBack.Visible = true;
                return;
            }
            FileDownload(fullFileName);
            //path = path.Replace(FileSystemManager.GetRootPath(), "~");
            //path = path.Replace("\\", "/");
            //Response.Redirect(path);
        }
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.Name, System.Text.Encoding.UTF8));
        Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
        Response.WriteFile(DownloadFile.FullName);
        Response.Flush();
        Response.End();

    }



    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        lblFeekBack.Text = "";
        bool isFoloderDeleteed = false;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)row.Cells[0].FindControl("CheckBox1");
                if (cb.Checked)
                {
                    LinkButton lb = (LinkButton)row.Cells[1].FindControl("LinkButton1");
                    if (Directory.Exists(lb.CommandArgument))
                    {
                        FileSystemManager.DeleteFolder(lb.CommandArgument);
                        isFoloderDeleteed = true;
                    }
                    else
                    {
                        FileSystemManager.DeleteFile(lb.CommandArgument);
                    }
                }
            }
        }
        BindGrid(lblCurrentPath.Text);

        if (isFoloderDeleteed)
        {
            Response.Write("<script>window.parent.navigate('login.aspx');</script>");
        }
    }

    /// <summary>
    /// 新建文件夹
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateFolder_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TextBox2.Text)) return;
        FileSystemManager.CreateFolder(TextBox2.Text, lblCurrentPath.Text);
        BindGrid(lblCurrentPath.Text);
    }

    /// <summary>
    /// 新建文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateFile_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TextBox4.Text)) return;
        FileSystemManager.CreateFile(TextBox4.Text, lblCurrentPath.Text);
        BindGrid(lblCurrentPath.Text);
    }

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            lblFeekBack.Text = "";
            string path = lblCurrentPath.Text + "\\";
            int fsize = FileUpload1.PostedFile.ContentLength;
            if(CheckIfEnoughSpace((long)fsize) == false)
                return;

            path += Path.GetFileName(FileUpload1.FileName);
            FileUpload1.PostedFile.SaveAs(path);
            BindGrid(lblCurrentPath.Text);
        }
    }

    private bool CheckIfEnoughSpace(long fsize)
    {
        if (Session["CurrentSize"] != null)
        {
            long csize = (long)Session["CurrentSize"];
            if (csize + fsize > 10 * 1024 * 1024) //上传的话将超过最大容量，不给上传。
            {
                lblFeekBack.Text = "对不起，您剩余空间不足！";
                lblFeekBack.Visible = true;
                return false;
            }
            

        }
        return true;
    }

    /// <summary>
    /// 剪切
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCut_Click(object sender, EventArgs e)
    {
        List<string> items = new List<string>();
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)row.Cells[0].FindControl("CheckBox1");
                if (cb.Checked)
                {
                    LinkButton lb = (LinkButton)row.Cells[1].FindControl("LinkButton1");
                    items.Add(lb.CommandArgument);
                }
            }
        }
        ViewState["clipboard"] = items;
        ViewState["action"] = "cut";
    }

    /// <summary>
    /// 粘贴
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPaste_Click(object sender, EventArgs e)
    {
        lblFeekBack.Text = "";
        if (ViewState["clipboard"] != null)
        {
            if (ViewState["action"].ToString() == "cut")
            {
                List<string> items = (List<string>)ViewState["clipboard"];
                foreach (string s in items)
                {
                    if (Directory.Exists(s))
                    {
                        Directory.Move(s, lblCurrentPath.Text + s.Substring(s.LastIndexOf("\\")));
                    }
                    else
                    {
                        File.Move(s, lblCurrentPath.Text + "\\" + Path.GetFileName(s));
                    }
                }
            }
            else
            {
                List<string> items = (List<string>)ViewState["clipboard"];
                foreach (string s in items)
                {   
                    long dsize = FileSystemManager.GetDirectoryOrFileLength(s);
                    if (CheckIfEnoughSpace(dsize) == false)
                            return;
                    if (Directory.Exists(s))
                    {

                        DirectoryInfo di = new DirectoryInfo(s);
                        FileSystemManager.CopyFolder(s, lblCurrentPath.Text + "\\" + di.Name);
                    }
                    else
                    {
                        
                        File.Copy(s, lblCurrentPath.Text + "\\复件 " + Path.GetFileName(s), true);
                    }
                }
            }
        }
        ViewState["clipboard"] = null;
        ViewState["action"] = null;
        BindGrid(lblCurrentPath.Text);
    }

    /// <summary>
    /// 复制
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        List<string> items = new List<string>();
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)row.Cells[0].FindControl("CheckBox1");
                if (cb.Checked)
                {
                    LinkButton lb = (LinkButton)row.Cells[1].FindControl("LinkButton1");
                    items.Add(lb.CommandArgument);
                }
            }
        }
        ViewState["clipboard"] = items;
        ViewState["action"] = "copy";
    }

    /// <summary>
    /// 重命名
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRename_Click(object sender, EventArgs e)
    {
        string src = "";
        string dest = "";
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)row.Cells[0].FindControl("CheckBox1");
                if (cb.Checked)
                {
                    LinkButton lb = (LinkButton)row.Cells[1].FindControl("LinkButton1");
                    src = lb.CommandArgument;
                }
            }
        }
        if (src.Length > 0)
        {
            dest = src.Substring(0, src.LastIndexOf('\\'));
            dest = dest + "\\" + TextBox3.Text;
            if (Directory.Exists(src))
            {
                FileSystemManager.MoveFolder(src, dest);
            }
            else
            {
                FileSystemManager.MoveFile(src, dest);
            }
            BindGrid(lblCurrentPath.Text);
        }
    }
}
    
}
