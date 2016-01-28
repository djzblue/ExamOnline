/*
*��    ������չDatePicker�ؼ���ǰ׺Dpk
*��    �ߣ���ǿȪ
*�������ڣ�2006-11-16
*�޶����ڣ�
*�� �� �ˣ�
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
    /// ��չDatePicker�ؼ�
    /// </summary>
    [Themeable(true)]
    [ToolboxData("<{0}:DatePicker ID=\"Dpk\" runat=\"server\"></{0}:DatePicker>")]
    public class DatePicker : TextBox
    {
        private const string DatePicker_JS = "ExamManagement.Controls.DatePicker.DatePicker.js";
        private Image m_Image;

        /// <summary>
        /// ���캯��
        /// </summary>
        public DatePicker()
        {
            m_Image = new Image();
        }
        
        #region �ؼ��Զ�������

        
        /// <summary>
        /// �ؼ�����ʾͼƬ��·��
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue("")]
        [UrlProperty]
        [Description("�ؼ�����ʾͼƬ��·��")]
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
        /// �������
        /// </summary>
        [Description("�������")]
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
        /// ���ڸ�ʽ��trueΪ�����ڸ�ʽ
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
        /// ͼ��Style
        /// </summary>
        public CssStyleCollection ImageStyle
        {
            get { return m_Image.Style; }
        }

        /// <summary>
        /// ���ڸ�ʽ��
        /// �����ڸ�ʽ�� yyyy-MM-dd
        /// �����ڸ�ʽ��yyyy-MM-dd HH:mm:ss
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

        #region ��д�ؼ�����
        /// <summary>
        /// ���� Control.Load �¼�
        /// </summary>
        /// <param name="e">�����¼����ݵ� EventArgs ����</param>       
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // ���ؿؼ�ʹ�õ�JS
            Type t = this.GetType();
            string url = Page.ClientScript.GetWebResourceUrl(t, DatePicker_JS);
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(t, DatePicker_JS))
            {
                Page.ClientScript.RegisterClientScriptInclude(t, DatePicker_JS, url);
            }
        }
        
        /// <summary>
        /// ʹ��HtmlTextWriter�����HTML����
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
        /// ��������ַ���ת��Ϊ����ʱ������
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <returns>ת���ɹ��򷵻ض�Ӧ����ʱ�䣬���򷵻ص�ǰ����ʱ��</returns>
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
        /// �����ӿؼ�
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
