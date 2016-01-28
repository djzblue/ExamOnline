/*
*描    述：扩展DatePicker控件，前缀Dpk
*作    者：曾强泉
*创建日期：2006-11-16
*修订日期：
*修 订 人：
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamManagement.Controls
{
    /// <summary>
    /// 扩展DatePicker控件
    /// </summary>
    [Themeable(true)]
    [ToolboxData("<{0}:DatePicker ID=\"Dpk\" runat=\"server\"></{0}:DatePicker>")]
    public class DatePicker : TextBox
    {
        private const string DatePicker_JS = "ExamManagement.Controls.DatePicker.DatePicker.js";
        private Image m_Image;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DatePicker()
        {
            m_Image = new Image();
        }
        
        #region 控件自定义属性

        
        /// <summary>
        /// 控件中显示图片的路径
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue("")]
        [UrlProperty]
        [Description("控件中显示图片的路径")]
        [Localizable(true)]
        public string DateImage
        {
            get
            {
                EnsureChildControls();
                return m_Image.ImageUrl;
            }

            set
            {
                EnsureChildControls();
                m_Image.ImageUrl = value;
            }
        }

        /// <summary>
        /// 日期输出
        /// </summary>
        [Description("日期输出")]
        public DateTime Date
        {
            get
            {
                DateTime output;
                if (!DateTime.TryParse(this.Text, out output))
                {
                    output = DateTime.Now;
                }
                return output;
            } 
        }

        /// <summary>
        /// 日期格式，true为长日期格式
        /// </summary>
        [DefaultValue("false")]
        public bool IsLongDate
        {
            set {
                ViewState["IsLongDate"] = value;
            }
            
            get {
                object o = ViewState["IsLongDate"];
                return (o == null) ? false : (bool)o;
            }
        }

        /// <summary>
        /// 图像Style
        /// </summary>
        public CssStyleCollection ImageStyle
        {
            get { return m_Image.Style; }
        }

        /// <summary>
        /// 日期格式　
        /// 短日期格式： yyyy-MM-dd
        /// 长日期格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        [DefaultValue("yyyy-MM-dd")]
        public string DateFormat
        {
            set
            {
                ViewState["DateFormat"] = value;
            }

            get
            {
                object o = ViewState["DateFormat"];
                return (o == null) ? "" : (string)o;
            }
        }
        #endregion

        #region 重写控件方法
        /// <summary>
        /// 处理 Control.Load 事件
        /// </summary>
        /// <param name="e">包含事件数据的 EventArgs 对象</param>       
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 加载控件使用的JS
            Type t = this.GetType();
            string url = Page.ClientScript.GetWebResourceUrl(t, DatePicker_JS);
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(t, DatePicker_JS))
            {
                Page.ClientScript.RegisterClientScriptInclude(t, DatePicker_JS, url);
            }
        }
        
        /// <summary>
        /// 使用HtmlTextWriter类呈现HTML代码
        /// </summary>
        /// <param name="writer">HtmlTextWriter</param>
        protected override void Render(HtmlTextWriter writer) 
        {
            string dateTextFormat = "yyyy-MM-dd HH:mm:ss";
            if (!string.IsNullOrEmpty(DateFormat))
            {
                dateTextFormat = DateFormat;
                m_Image.Attributes.Add("onclick", "return showCalendar('" + this.ClientID + "', '"+ DateFormat +"');");
            }
            else
            {
                if (IsLongDate)
                {
                    dateTextFormat = "yyyy-MM-dd HH:mm:ss";
                    m_Image.Attributes.Add("onclick", "return showCalendar('" + this.ClientID + "', 'yyyy-MM-dd HH:mm:ss');");
                }
                else
                {
                    dateTextFormat = "yyyy-MM-dd";
                    m_Image.Attributes.Add("onclick", "return showCalendar('" + this.ClientID + "', 'yyyy-MM-dd');");
                }
            }
            m_Image.Style.Add("cursor", "pointer");
            
            if (!string.IsNullOrEmpty(this.Text))
            {       
                this.Text = CDate(this.Text).ToString(dateTextFormat);
            }
            base.Render(writer);
            m_Image.RenderControl(writer);
        }


        /// <summary>
        /// 将输入的字符串转换为日期时间类型
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>转换成功则返回对应日期时间，否则返回当前日期时间</returns>
        public  DateTime CDate(string input)
        {
            DateTime output;
            if (!DateTime.TryParse(input, out output))
            {
                output = DateTime.Now;
            }
            return output;
        }

        /// <summary>
        /// 创建子控件
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            m_Image.ApplyStyleSheetSkin(Page); 
            this.Controls.Add(m_Image);      
       }
        #endregion

    }
}
