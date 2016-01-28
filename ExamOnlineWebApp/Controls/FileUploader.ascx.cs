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
using System.IO;

namespace SDPTExam.Web.UI.Controls
{
    public partial class FileUploader : System.Web.UI.UserControl
    {

      
        /// <summary>
        /// 文件类型id
        /// </summary>
        public int FiletypeID
        {
            get {
                object typeID = ViewState["FileTypeID"];

                return (typeID == null) ? 0 :(int)typeID; 
            }
            set
            {
                ViewState["FileTypeID"] = value;
               
            }
        }

        public bool HasSelectedFile
        {
            get { return filUpload.HasFile; }
        }

        private bool isExistBefore = false;
        /// <summary>
        /// 上传前文件是否已经存在
        /// </summary>
        public bool IsExistBefore
        {
            get {
                object exist = ViewState["IsExist"];
                return (exist==null)?false:(bool)exist;
            }
            set {
                ViewState["IsExist"] = value;
                isExistBefore = value;
            }
        }
     
        //上传文件的存放路径，这里是虚拟路径
        private string filepath = "";

        /// <summary>
        /// 文件存放目录，不包含文件名
        /// </summary>
        public string FilePath
        {
            get { return filepath; }
            set { this.filepath = value; }

        }

        //文件的名称，已通过当前时间改名
        private string fileName = "";

        /// <summary>
        /// 文件名，不包括后缀名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { this.fileName = value; }

        }

        //规定上传的文件类型
        private string fileType = "";
        /// <summary>
        /// 文件上传类型
        /// </summary>
        public string FileType
        {
            get { return fileType; }
            set { this.fileType = value; }

        }

        
        private string thetitle = "";
        /// <summary>
        /// 控件标题，表示该文件上传标题
        /// </summary>
        public string Title
        {
            get { return thetitle; }
            set { this.thetitle = value; lblTitle.Text = value; }

        }

        /// <summary>
        /// 文件的完整路径
        /// </summary>
        private string fullFilePath;
        public string FullFilePath
        {
            get { return fullFilePath; }
        }

        /// <summary>
        /// 规定是否可用
        /// </summary>
        public bool Enabled
        {
             get 
            {
                object e = ViewState["Enabled"];
                return (e == null) ? btnUpload.Enabled : (bool)e;
            }
            set 
            {
                ViewState["Enabled"] = value;
                btnUpload.Enabled = value;
            }
        }

        private bool _cancel = false;
        public bool Cancel
        {
            get { return _cancel; }
            set
            {
                _cancel = value;
            }
        }

        private int state = 0;
        /// <summary>
        /// 文件控件的状态值，表明上传文件后学生所处的状态值
        /// </summary>
        public int State
        {
            get
            {
                object v = ViewState["State"];
                return (v==null)?0:(int)v;
            }
            set
            {
                ViewState["State"] = value;
                state = value;
            }
        }

        private bool isStudent=true;
        /// <summary>
        /// 是否属于学生
        /// </summary>
        public bool IsStudent
        {
            get { return isStudent; }
            set { isStudent = value; }
        
        }


        public string Tips
        {
            get { return lblFeedback.Text; }
            set { lblFeedback.Text = value; }
        }


        /// <summary>
        /// 上传完成事件，提供事件订阅者自定义的处理程序
        /// </summary>
        public event System.EventHandler SubmitFinished;

        /// <summary>
        /// 准备上传事件，提供事件订阅者自定义的处理程序
        /// </summary>
        public event System.EventHandler Submiting;


        //控件持久化
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Page.RegisterRequiresControlState(this);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] ctlState = (object[])savedState;
            base.LoadControlState(ctlState[0]);
            this.FilePath = (string)ctlState[1];
            this.FileName = (string)ctlState[2];
            this.FileType = (string)ctlState[3];
        }

        protected override object SaveControlState()
        {
            object[] ctlState = new object[4];//这有何用？
            ctlState[0] = base.SaveControlState();
            ctlState[1] = this.FilePath;
            ctlState[2] = this.FileName;
            ctlState[3] = this.FileType;

            return ctlState;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.Page.User.Identity.IsAuthenticated)
            //   throw new SecurityException("匿名用户不允许上传文件");

         //   lblFeedback.Visible = false;
            if (IsExistBefore == true) 
            btnUpload.Attributes.Add("onclick", "return confirm( '该文件已经存在，确定要覆盖吗？');"); 

        }

        public void UploadManual()
        {
            btnUpload_Click(null, null);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Submiting != null)
            {
                this.Submiting(this, e);

            }
            if (Cancel == true) return;

            if (filUpload.HasFile && filUpload.PostedFile.ContentLength > 0)
            {
                //是否应该限定文件类型？

                try
                {
                    string fileContentType = filUpload.PostedFile.ContentType;
                    if (fileContentType == "application/x-msdownload")
                    {
                        throw new Exception("不允许上传可执行文件");
                    }

                    switch (this.FileType)
                    {
                        case "img":
                            if (fileContentType.IndexOf("image") == -1)
                            {
                                throw new Exception("上传失败，该文件必须是图片类型！！");
                            }
                            break;
                        case "flash":
                            if (fileContentType.IndexOf("flash") == -1)
                            {
                                throw new Exception("上传失败，该文件必须是Falsh！！");
                            }
                            break;
                        case "doc":
                            if (fileContentType.IndexOf("word") == -1)
                            {
                                throw new Exception("上传失败，该文件必须是Word文档！！");
                            }
                            break;
                        case "xls":
                            if (fileContentType.IndexOf("excel") == -1)
                            {
                                throw new Exception("上传失败，该文件必须是Excel文件！！");
                            }
                            break;
                        case "rar":
                            if (fileContentType.IndexOf("rar") == -1)
                            {
                                throw new Exception("上传失败，该文件必须是压缩文档！！");
                            }
                            break;

                        default: break;
                    }
             
            
                    // 如果目录还不存在，创建 /Uploads/<CurrentUserName>
                    //string dirUrl = (this.Page as MB.TheBeerHouse.UI.BasePage).BaseUrl +
                    //   "Uploads/" + this.Page.User.Identity.Name;

                    //如果当前用户不是网站用户，而是超级管理员，在文件放到默认上传目录
                    string dirUrl = "";

                    if (string.IsNullOrEmpty(FilePath) == false) dirUrl = FilePath;
                    else
                    {
                        dirUrl = "~/Files/Temp";//这里选择默认路径
                        FilePath = dirUrl;
                    }

               

                    string dirPath = Server.MapPath(dirUrl);

                    if (!Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);

                    // 保存文件，是否应该改文件名呢？
                    string fname = DateTime.Now.ToString("yyyymmddhhmmss");
                    //string originalName = Path.GetFileName(filUpload.PostedFile.FileName);
                    string extName = Path.GetExtension(filUpload.PostedFile.FileName);// originalName.Substring(originalName.LastIndexOf(".") + 1);
                    if (string.IsNullOrEmpty(FileName) == false) fname = FileName;
                    else fileName = fname;
                    string fileUrl = dirUrl + "/" + fname + extName; // Path.GetFileName(filUpload.PostedFile.FileName);
                    //this.FileName = fname + extName;
                    fullFilePath = fileUrl;
                    if (File.Exists(fullFilePath)) IsExistBefore = true;
                    filUpload.PostedFile.SaveAs(Server.MapPath(fileUrl));

                   // HttpContext.Current.Response.Write("文件的上传路径为：" + Server.MapPath(fileUrl));

                    lblFeedback.Visible = true;
                    lblFeedback.Text = "文件成功上传";//，其路径为: " + fileUrl;
                    if (SubmitFinished != null)
                    {
                        this.SubmitFinished(this, e);
                    }
                }
                catch (Exception ex)
                {
                    lblFeedback.Visible = true;
                    lblFeedback.Text = ex.Message;
                }




            }

            else
            {
                lblFeedback.Visible = true;
                lblFeedback.Text = "请选择文件";
            }

        }


    }
}