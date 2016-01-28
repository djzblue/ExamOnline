/*
*��    ������չGridView�ؼ�
*��    �ߣ�ȫ�忪����Ա
*�������ڣ�2006-09-18
*�޶����ڣ�2006-11-06
*�� �� �ˣ�κ׳־
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ExamManagement.Controls
{

    /// <summary>
    /// ��չGridView�ؼ���ǰ׺Egv
    /// </summary>
    [Themeable(true)]
    [ToolboxBitmap(typeof(ExtendedGridView), "ExtendedGridView.bmp")]
    [ToolboxData("<{0}:ExtendedGridView ID=\"Egv\" runat=\"server\"></{0}:ExtendedGridView>")]
    public class ExtendedGridView : System.Web.UI.WebControls.GridView
    {

        private const string ExtendedGridView_JS = "ExamManagement.Controls.ExtendedGridView.ExtendedGridView.js";
        private const string CheckBoxColumHeaderTemplate = "<input type='checkbox' hidefocus='true' id='{0}' name='{0}' onclick='CheckAll(this)'>";
        private const string CheckBoxColumHeaderID = "{0}_HeaderButton";
        private string m_UniqueControlPageSize;
        private string m_UniqueControlPageIndex;
        private int m_RawPageIndex;


        #region �ؼ��Զ�������


        /// <summary>
        /// ����ƶ�������������ʾ��CSSЧ��
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue("")]
        [Description("����ƶ�������������ʾ��CSSЧ��")]
        [Localizable(true)]
        public string MouseOverCssClass
        {
            get
            {
                String s = (String)ViewState["MouseOverCssClass"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["MouseOverCssClass"] = value;
            }
        }

        /// <summary>
        /// ѡ�е�����������ʾ��CSSЧ��
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue("")]
        [Description("ѡ�е�����������ʾ��CSSЧ��")]
        [Localizable(true)]
        public string SelectedCssClass
        {
            get
            {
                String s = (String)ViewState["SelectedCssClass"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["SelectedCssClass"] = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�ؼ�Ĭ�ϵķ�ҳ������ʽ
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue(true)]
        [Description("�Ƿ���ʾ�ؼ�Ĭ�ϵķ�ҳ������ʽ")]
        [Localizable(true)]
        public bool ShowCustomPager
        {
            get
            {
                object o = ViewState["ShowCustomPager"];
                return (o == null) ? true : (bool)o;
            }

            set
            {
                ViewState["ShowCustomPager"] = value;
            }
        }

        /// <summary>
        /// ��ҳ��������ʾ����Ŀ����n
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue("��¼")]
        [Description("��ҳ��������ʾ����Ŀ����")]
        [Localizable(true)]
        public string ItemName
        {
            get
            {
                String s = (String)ViewState["ItemName"];
                return ((s == null) ? "��¼" : s);
            }

            set
            {
                ViewState["ItemName"] = value;
            }
        }

        /// <summary>
        /// ��ҳ��������ʾ����Ŀ��λ
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue("��")]
        [Description("��ҳ��������ʾ����Ŀ��λ")]
        [Localizable(true)]
        public string ItemUnit
        {
            get
            {
                String s = (String)ViewState["ItemUnit"];
                return ((s == null) ? "��" : s);
            }

            set
            {
                ViewState["ItemUnit"] = value;
            }
        }

        /// <summary>
        /// ��ʾ�����
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue(false)]
        [Description("�Ƿ񱣳ֵ�ǰ״̬")]
        [Localizable(true)]
        public bool IsHoldState
        {
            get
            {
                object o = ViewState["IsHoldState"];
                return (o == null) ? true : (bool)o;
            }

            set
            {
                ViewState["IsHoldState"] = value;
            }
        }

        /// <summary>
        /// ��ʾ�����
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue(false)]
        [Description("�Ƿ���ʾ�����")]
        [Localizable(true)]
        public bool AutoGenerateSerialColumn
        {
            get
            {
                object o = ViewState["AutoGenerateSerialColumn"];
                return (o == null) ? false : (bool)o;
            }

            set
            {
                ViewState["AutoGenerateSerialColumn"] = value;
            }
        }

        /// <summary>
        /// ����еı�������
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue("����")]
        [Description("����еı�������")]
        [Localizable(true)]
        public string SerialText
        {
            get
            {
                String s = (String)ViewState["SerialText"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["SerialText"] = value;
            }
        }

        /// <summary>
        /// ����е�����
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue(0)]
        [Description("����е�����")]
        [Localizable(true)]
        public int SerialColumnIndex
        {
            get
            {
                object o = ViewState["SerialColumnIndex"];
                return (o == null) ? 0 : (int)o;
            }

            set
            {
                ViewState["SerialColumnIndex"] = value;
            }
        }

        /// <summary>
        /// �Ƿ��Զ����ɸ�ѡ����
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue(false)]
        [Description("�Ƿ��Զ����ɸ�ѡ����")]
        [Localizable(true)]
        public bool AutoGenerateCheckBoxColumn
        {
            get
            {
                object o = ViewState["AutoGenerateCheckBoxColumn"];
                if (o == null)
                    return false;
                return (bool)o;
            }
            set { ViewState["AutoGenerateCheckBoxColumn"] = value; }
        }

        /// <summary>
        /// ��ѡ���п��
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [DefaultValue(20)]
        [Description("��ѡ���п��")]
        [Localizable(true)]
        public Unit CheckBoxFieldHeaderWidth
        {
            get
            {
                object o = ViewState["CheckBoxFieldHeaderWidth"];
                if (o == null)
                    return Unit.Percentage(3);
                return (Unit)o;
            }
            set { ViewState["CheckBoxFieldHeaderWidth"] = value; }
        }

        /// <summary>
        /// ��ѡ���е�����
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [Description("��ѡ���е�����")]
        [DefaultValue(0)]
        public int CheckBoxColumnIndex
        {
            get
            {
                object o = ViewState["CheckBoxColumnIndex"];
                if (o == null)
                    return 0;
                return (int)o;
            }
            set
            {
                ViewState["CheckBoxColumnIndex"] = (value < 0 ? 0 : value);
            }
        }

        /// <summary>
        /// ��˫��ʱ��ת��URL
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [Description("��˫��ʱ��ת��URL�����԰���{$Field}������󶨵������У����磺UserShow.aspx?UserID={$Field}")]
        [DefaultValue("")]
        public virtual string RowDblclickUrl
        {
            get
            {
                String s = (String)ViewState["RowDblclickUrl"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["RowDblclickUrl"] = value;
            }
        }

        /// <summary>
        /// ��˫��ʱ�󶨵�������URL
        /// </summary>
        [Bindable(true)]
        [Category("�Զ���")]
        [Description("��˫��ʱ�󶨵�������")]
        [DefaultValue("")]
        public virtual string RowDblclickBoundField
        {
            get
            {
                String s = (String)ViewState["RowDblclickBoundField"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["RowDblclickBoundField"] = value;
            }
        }

        /// <summary>
        /// �ܼ�¼��
        /// </summary>
        public int VirtualItemCount
        {
            get
            {
                object o = ViewState["VirtualItemCount"];
                return (o == null) ? 0 : (int)o;
            }
        }

        /// <summary>
        /// �õ���ѡ��ѡ����ID
        /// </summary>
        public StringBuilder SelectList
        {
            get
            {
                CheckBox Chk;
                string selectId = "";
                StringBuilder selectIdList = new StringBuilder("");

                for (int i = 0; i < this.Rows.Count; i++)
                {
                    Chk = (CheckBox)this.Rows[i].Cells[CheckBoxColumnIndex].FindControl("CheckBoxButton");
                    if (Chk.Checked)
                    {
                        selectId = this.DataKeys[i].Value.ToString();

                        if (selectIdList.Length == 0)
                        {
                            selectIdList.Append(selectId);
                        }
                        else
                        {
                            selectIdList.Append(",");
                            selectIdList.Append(selectId);
                        }
                    }
                }
                return selectIdList;
            }
        }

        /// <summary>
        /// ��дPageIndex���ԣ�������ֵ������Session��
        /// </summary>
        public override int PageIndex
        {
            set
            {
                base.PageIndex = value;
                if (!this.DesignMode)
                {
                    this.Context.Session[m_UniqueControlPageIndex] = value;
                }
            }
        }

        /// <summary>
        /// ��дPageSize���ԣ�������ֵ������Session��
        /// </summary>
        public override int PageSize
        {
            set
            {
                base.PageSize = value;
                if (!this.DesignMode)
                {
                    this.Context.Session[m_UniqueControlPageSize] = value;
                }
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
            string url = Page.ClientScript.GetWebResourceUrl(t, ExtendedGridView_JS);
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(t, ExtendedGridView_JS))
            {
                Page.ClientScript.RegisterClientScriptInclude(t, ExtendedGridView_JS, url);
            }

            // ��Session�ж�ȡ������״ֵ̬������ÿҳ��¼������ǰҳ
            if (IsHoldState)
            {
                m_UniqueControlPageSize = this.Page.GetType().Name + "_" + this.UniqueID + "_PageSize";
                m_UniqueControlPageIndex = this.Page.GetType().Name + "_" + this.UniqueID + "_PageIndex";
                if (this.Context.Session[m_UniqueControlPageSize] != null)
                {
                    this.PageSize = (int)this.Context.Session[m_UniqueControlPageSize];
                }
                if (this.Context.Session[m_UniqueControlPageIndex] != null)
                {
                    this.PageIndex = (int)this.Context.Session[m_UniqueControlPageIndex];
                    m_RawPageIndex = this.PageIndex;
                }
            }
          //  HttpContext.Current.Response.Write("<br>onload<br>");

        }

        /// <summary>
        /// ������Դ�е����ݰ󶨵��������ݰ󶨿ؼ���
        /// </summary>
        /// <param name="data">����Ҫ�󶨵��������ݰ󶨿ؼ���ֵ</param>
        protected override void PerformDataBinding(IEnumerable data)
        {
            base.PerformDataBinding(data);
            this.ViewState["VirtualItemCount"] = this.ViewState["_!ItemCount"];
            //HttpContext.Current.Response.Write("<br>performdatabinding<br>");
        }

        /// <summary>
        /// �������������ؼ���νṹ�����ֶμ�
        /// </summary>
        /// <param name="dataSource">����Դ</param>
        /// <param name="useDataSource">true ��ʾʹ�� dataSource ����ָ��������Դ������Ϊ false</param>
        /// <returns>�������ֶμ�</returns>
        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            ICollection columnList = base.CreateColumns(dataSource, useDataSource);
            if (!AutoGenerateCheckBoxColumn && !AutoGenerateSerialColumn)
            {
                return columnList;
            }

            // ������չ����
            ArrayList extendedColumnList = new ArrayList();
            if (AutoGenerateCheckBoxColumn)
            {
                extendedColumnList = AddCheckBoxColumn(columnList);
            }
            if (AutoGenerateSerialColumn)
            {
                extendedColumnList = AddSerialColumn(columnList);
            }
            return extendedColumnList;
        }

        /// <summary>
        /// ������ѡ��������
        /// </summary>
        /// <param name="columnList">����Դ</param>
        /// <returns>���ظ�ѡ��������</returns>
        private ArrayList AddCheckBoxColumn(ICollection columnList)
        {
            ArrayList list = new ArrayList(columnList);

            string checkBoxColumHeaderTemplate =
                "<input type='checkbox' hidefocus='true' id='{0}' name='{0}' onclick='CheckAll(this,\"" +
                this.RowStyle.CssClass + "\",\"" + this.SelectedCssClass + "\")'>";

            // �����Զ����CheckBox��
            InputCheckBoxField field = new InputCheckBoxField();
            string checkBoxID = String.Format(CheckBoxColumHeaderID, ClientID);
            field.HeaderText = String.Format(checkBoxColumHeaderTemplate, checkBoxID);
            field.HeaderStyle.Width = CheckBoxFieldHeaderWidth;
            field.ReadOnly = true;
            field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            // ����CheckBox�е����ֶμ���ָ��λ�ã����ָ��λ�ô����ֶμ�������������뵽���
            if (CheckBoxColumnIndex > list.Count)
            {
                list.Add(field);
                CheckBoxColumnIndex = list.Count - 1;
            }
            else
            {
                list.Insert(CheckBoxColumnIndex, field);
            }

            // ������չ������ֶμ�
            return list;
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <param name="columnList">����Դ</param>
        /// <returns>�������������</returns>
        private ArrayList AddSerialColumn(ICollection columnList)
        {
            ArrayList list = new ArrayList(columnList);

            // �����Զ���������
            TemplateField field = new TemplateField();
            field.HeaderText = this.SerialText;

            // ��������е����ֶμ���ָ��λ�ã����ָ��λ�ô����ֶμ�������������뵽���
            if (SerialColumnIndex > list.Count)
            {
                list.Add(field);
                SerialColumnIndex = list.Count - 1;
            }
            else
            {
                list.Insert(SerialColumnIndex, field);
            }

            // ������չ������ֶμ�
            return list;
        }

        /// <summary>
        /// ���� RowCreated �¼�
        /// </summary>
        /// <param name="e">GridViewRowEventArgs���������¼�����</param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(this.MouseOverCssClass))
                {
                    // ������Ƶ���ʱ�����ø�����ʽΪMouseOverCssClass��ָ����ֵ�� ������ԭ������ʽ
                    e.Row.Attributes.Add("onmouseover", "MouseOver(this,\"" + this.MouseOverCssClass + "\")");
                    // ���������ʱ��ԭ���е���ʽ
                    e.Row.Attributes.Add("onmouseout", "MouseOut(this)");
                }

                if (this.AutoGenerateSerialColumn)
                {
                    // Ϊ�Զ�������и�ֵ
                    e.Row.Cells[this.SerialColumnIndex].Text = Convert.ToString(e.Row.DataItemIndex + this.PageIndex * this.PageSize + 1);
                }
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                if (this.ShowCustomPager)
                {
                    // �����Զ���ķ�ҳ����
                    CreateCustomPagerRow(e);
                }
            }
            ////HttpContext.Current.Response.Write("<br>on row created<br>");
        }

        /// <summary>
        /// �����Զ����ҳ����
        /// </summary>
        /// <param name="e">GridViewRowEventArgs���������¼�����</param>
        private void CreateCustomPagerRow(GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Controls.Clear();

            #region ��ʾ�� �� 234 ƪ����

            e.Row.Cells[0].Controls.Add(new LiteralControl("��&nbsp;"));

            Label LblRowsCount = new Label();
            LblRowsCount.ID = "LblRowsCount";
            LblRowsCount.Text = VirtualItemCount.ToString();
            LblRowsCount.Font.Bold = true;

            e.Row.Cells[0].Controls.Add(LblRowsCount);

            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;" + this.ItemUnit + this.ItemName));
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            #endregion

            #region ��ʾ�� ��ҳ ��һҳ ��һҳ βҳ

            LinkButton LbtnFirst = new LinkButton();
            LbtnFirst.CommandName = "Page";
            LbtnFirst.CommandArgument = "First";
            LbtnFirst.Enabled = !(this.PageIndex == 0);
            LbtnFirst.Text = "��ҳ";
            e.Row.Cells[0].Controls.Add(LbtnFirst);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;"));

            LinkButton LbtnPrev = new LinkButton();
            LbtnPrev.CommandName = "Page";
            LbtnPrev.CommandArgument = "Prev";
            LbtnPrev.Enabled = !(this.PageIndex == 0);
            LbtnPrev.Text = "��һҳ";
            e.Row.Cells[0].Controls.Add(LbtnPrev);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;"));

            LinkButton LbtnNext = new LinkButton();
            LbtnNext.CommandName = "Page";
            LbtnNext.CommandArgument = "Next";
            LbtnNext.Enabled = !(this.PageIndex == this.PageCount - 1);
            LbtnNext.Text = "��һҳ";
            e.Row.Cells[0].Controls.Add(LbtnNext);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;"));

            LinkButton LbtnLast = new LinkButton();
            LbtnLast.CommandName = "Page";
            LbtnLast.CommandArgument = "Last";
            LbtnLast.Enabled = !(this.PageIndex == this.PageCount - 1);
            LbtnLast.Text = "βҳ";
            e.Row.Cells[0].Controls.Add(LbtnLast);

            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            if (IsHoldState)
            {
                LbtnFirst.Click += new EventHandler(LbtnFirst_Click);
                LbtnPrev.Click += new EventHandler(LbtnPrev_Click);
                LbtnNext.Click += new EventHandler(LbtnNext_Click);
                LbtnLast.Click += new EventHandler(LbtnLast_Click);
            }

            #endregion

            #region ��ʾ�� ҳ�Σ�23/45ҳ

            e.Row.Cells[0].Controls.Add(new LiteralControl("ҳ�Σ�"));

            Label LblCurrentPage = new Label();
            LblCurrentPage.Text = Convert.ToString(this.PageIndex + 1);
            LblCurrentPage.Font.Bold = true;
            LblCurrentPage.ForeColor = System.Drawing.Color.Red;
            e.Row.Cells[0].Controls.Add(LblCurrentPage);

            e.Row.Cells[0].Controls.Add(new LiteralControl("/"));

            Label LblTotalPages = new Label();
            LblTotalPages.Text = Convert.ToString(this.PageCount);
            LblTotalPages.Font.Bold = true;
            e.Row.Cells[0].Controls.Add(LblTotalPages);

            e.Row.Cells[0].Controls.Add(new LiteralControl("ҳ"));
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            #endregion

            #region ��ʾ�� 20 ƪ����/ҳ
            TextBox TxtMaxPerPage = new TextBox();
            TxtMaxPerPage.ApplyStyleSheetSkin(Page);
            TxtMaxPerPage.MaxLength = 3;
            TxtMaxPerPage.Width = 22;
            TxtMaxPerPage.Text = Convert.ToString(this.PageSize);
            TxtMaxPerPage.AutoPostBack = true;
            TxtMaxPerPage.TextChanged += new EventHandler(this.TxtMaxPerPage_TextChanged);
            e.Row.Cells[0].Controls.Add(TxtMaxPerPage);

            e.Row.Cells[0].Controls.Add(new LiteralControl(this.ItemUnit + this.ItemName + "/ҳ"));
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            #endregion

            #region ��ʾ�� ת����  ҳ
            e.Row.Cells[0].Controls.Add(new LiteralControl("ת����"));

            if (this.PageCount < 10)
            {
                DropDownList DropCurrentPage = new DropDownList();
                DropCurrentPage.ApplyStyleSheetSkin(Page);
                DropCurrentPage.AutoPostBack = true;
                DropCurrentPage.SelectedIndexChanged += new EventHandler(this.DropCurrentPage_SelectedIndexChanged);
                ArrayList dropValues = new ArrayList();
                for (int i = 1; i <= this.PageCount; i++)
                {
                    dropValues.Add(i);
                }
                DropCurrentPage.DataSource = dropValues;
                DropCurrentPage.DataBind();
                DropCurrentPage.SelectedIndex = this.PageIndex;
                e.Row.Cells[0].Controls.Add(DropCurrentPage);
            }
            else
            {
                TextBox TxtCurrentPage = new TextBox();
                TxtCurrentPage.ApplyStyleSheetSkin(Page);
                TxtCurrentPage.Width = 30;
                TxtCurrentPage.Text = Convert.ToString(this.PageIndex + 1);
                TxtCurrentPage.AutoPostBack = true;
                TxtCurrentPage.TextChanged += new EventHandler(this.TxtCurrentPage_TextChanged);
                e.Row.Cells[0].Controls.Add(TxtCurrentPage);
            }
            e.Row.Cells[0].Controls.Add(new LiteralControl("ҳ"));

            #endregion
        }

        /// <summary>
        /// ���� RowDataBound �¼�
        /// </summary>
        /// <param name="e">GridViewRowEventArgs���������¼�����</param>
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(RowDblclickUrl))
                {
                    if (!String.IsNullOrEmpty(RowDblclickBoundField))
                    {
                        // �����е�˫���¼������ÿͻ��˽ű�
                        //e.Row.Attributes.Add("ondblclick", "RowDblclick('" + StringHelper.ReplaceIgnoreCase(RowDblclickUrl, "{$Field}", DataBinder.Eval(e.Row.DataItem, RowDblclickBoundField).ToString()) + "');");
                    }
                    else
                    {
                        e.Row.Attributes.Add("ondblclick", "RowDblclick('" + RowDblclickUrl + "');");
                    }
                }
            }
            ////HttpContext.Current.Response.Write("<br>on row databound<br>");
        }

        /// <summary>
        /// ���� PreRender �¼�
        /// </summary>
        /// <param name="e">�����¼����ݵ� EventArgs</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // ���ɾ�����һҳ�����м�¼���·�ҳ��ʵ�����ݲ�һ�µ�����
            if (this.PageCount > 1 && this.PageIndex + 1 == this.PageCount && this.Rows.Count == 0)
            {
                if (base.Initialized)
                {
                    base.RequiresDataBinding = true;
                }
            }

            // ������浱ǰҳ�����ѯ�����ı�ʱ��ǰҳ�����ݵ�����
            if (this.PageIndex != m_RawPageIndex && this.Rows.Count == 0)
            {
                this.PageIndex = 0;
                if (base.Initialized)
                {
                    base.RequiresDataBinding = true;
                }
            }

            if (this.AllowPaging == true && this.BottomPagerRow != null)
            {
                this.BottomPagerRow.Visible = true;

                // ��������ǩ��ֵ
                if (this.ShowCustomPager)
                {
                    Label LblRowsCount = this.BottomPagerRow.Cells[0].FindControl("LblRowsCount") as Label;
                    if (LblRowsCount != null)
                    {
                        LblRowsCount.Text = VirtualItemCount.ToString();
                    }
                }
            }

            // Ϊÿ��ѡ������ӿͻ��˵�onclick�¼�
            if (this.AutoGenerateCheckBoxColumn)
            {
                string checkBoxID = String.Format(CheckBoxColumHeaderID, ClientID);
                foreach (GridViewRow rows in Rows)
                {
                    CheckBox cb = (CheckBox)rows.FindControl(InputCheckBoxField.CheckBoxID);
                    cb.Attributes["onclick"] = "CheckItem(this,\"" + checkBoxID + "\",\"" + this.RowStyle.CssClass + "\",\"" + this.SelectedCssClass + "\"," + Rows.Count + ")";
                }
            }
        }

        /// <summary>
        /// �����ؼ���νṹ
        /// </summary>
        protected override void PrepareControlHierarchy()
        {
            base.PrepareControlHierarchy();
            //GridViewRow blankRow = null;
            //for (int rownum = this.Rows.Count; rownum < this.PageSize; rownum++)
            //{
            //    blankRow = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
            //    for (int colnum = 0; colnum < this.Columns.Count; colnum++)
            //    {
            //        TableCell blankCell = new TableCell();
            //        blankCell.Controls.Add(new LiteralControl(""));
            //        blankRow.Controls.Add(blankCell);
            //    }
            //    if (this.Rows.Count > 0)
            //    {
            //        this.Controls[0].Controls.AddAt(this.Controls[0].Controls.Count - 1, blankRow);
            //    }
            //}
        }

        #endregion


        #region �Զ�����¼�������

        /// <summary>
        /// ÿҳ������ı����ĺ󴥷��¼�
        /// </summary>
        /// <param name="sender">�¼�Դ</param>
        /// <param name="e">�¼�</param>
        protected void TxtMaxPerPage_TextChanged(object sender, EventArgs e)
        {
            int pageSize;
            TextBox TxtPageSize = (TextBox)sender;
            if (Int32.TryParse(TxtPageSize.Text, out pageSize) == false)
            {
                pageSize = this.PageSize;
            }
            else
            {
                if (pageSize < 1) { pageSize = this.PageSize; }
            }
            this.PageSize = pageSize;
            this.PageIndex = 0;
            TxtPageSize.Text = pageSize.ToString();
            if (IsHoldState)
            {
                this.Context.Session[m_UniqueControlPageSize] = this.PageSize;
            }

           // HttpContext.Current.Response.Write("textmaxperPage change");
            if (string.IsNullOrEmpty(DataSourceID))
            {
                //this.OnLoad(null);
                //this.OnDataBinding(null);
                this.OnPageIndexChanging(new GridViewPageEventArgs(0));
            }
        }

        /// <summary>
        /// ��ǰҳ�ı����ĺ󴥷��¼�
        /// </summary>
        /// <param name="sender">�¼�Դ</param>
        /// <param name="e">�¼�</param>
        protected void TxtCurrentPage_TextChanged(object sender, EventArgs e)
        {
            int currentPage;
            TextBox TxtPage = (TextBox)sender;
            if (Int32.TryParse(TxtPage.Text, out currentPage) == false)
            {
                currentPage = 1;
            }
            else
            {
                if (currentPage < 1) { currentPage = 1; }
            }
            if (currentPage > this.PageCount) { currentPage = this.PageCount; }
            this.PageIndex = currentPage - 1;
            TxtPage.Text = currentPage.ToString();
            if (IsHoldState)
            {
                this.Context.Session[m_UniqueControlPageIndex] = this.PageIndex;
            }
           // this.OnLoad(null);
            if (string.IsNullOrEmpty(DataSourceID))
            {
                //this.OnLoad(null);
                //this.OnDataBinding(null);
                this.OnPageIndexChanging(new GridViewPageEventArgs(this.PageIndex));
            }
        }

        /// <summary>
        /// ����ʽ��ǰҳѡ����ĺ󴥷��¼�
        /// </summary>
        /// <param name="sender">�¼�Դ</param>
        /// <param name="e">�¼�</param>
        protected void DropCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DropCurrentPage = (DropDownList)sender;
            this.PageIndex = Convert.ToInt32(DropCurrentPage.SelectedValue) - 1;
            if (IsHoldState)
            {
                this.Context.Session[m_UniqueControlPageIndex] = this.PageIndex;
            }
//this.OnLoad(null);
            if (string.IsNullOrEmpty(DataSourceID))
            {
                //this.OnLoad(null);
                //this.OnDataBinding(null);
                this.OnPageIndexChanging(new GridViewPageEventArgs(this.PageIndex));
            }
        }

        /// <summary>
        /// �����ҳ���Ӵ����¼�
        /// </summary>
        /// <param name="sender">�¼�Դ</param>
        /// <param name="e">�¼�</param>
        protected void LbtnFirst_Click(object sender, EventArgs e)
        {
            this.Context.Session[m_UniqueControlPageIndex] = 0;
        }

        /// <summary>
        /// �����һҳ���Ӵ����¼�
        /// </summary>
        /// <param name="sender">�¼�Դ</param>
        /// <param name="e">�¼�</param>
        protected void LbtnPrev_Click(object sender, EventArgs e)
        {
            this.Context.Session[m_UniqueControlPageIndex] = this.PageIndex - 1;
        }

        /// <summary>
        /// �����һҳ���Ӵ����¼�
        /// </summary>
        /// <param name="sender">�¼�Դ</param>
        /// <param name="e">�¼�</param>
        protected void LbtnNext_Click(object sender, EventArgs e)
        {
            this.Context.Session[m_UniqueControlPageIndex] = this.PageIndex + 1;
        }

        /// <summary>
        /// ���βҳ���Ӵ����¼�
        /// </summary>
        /// <param name="sender">�¼�Դ</param>
        /// <param name="e">�¼�</param>
        protected void LbtnLast_Click(object sender, EventArgs e)
        {
            this.Context.Session[m_UniqueControlPageIndex] = this.PageCount - 1;
        }

        #endregion


    }
}
