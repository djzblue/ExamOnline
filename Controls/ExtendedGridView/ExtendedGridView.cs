/*
*描    述：扩展GridView控件
*作    者：全体开发人员
*创建日期：2006-09-18
*修订日期：2006-11-06
*修 订 人：魏壮志
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
    /// 扩展GridView控件，前缀Egv
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


        #region 控件自定义属性


        /// <summary>
        /// 鼠标移动到数据行上显示的CSS效果
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue("")]
        [Description("鼠标移动到数据行上显示的CSS效果")]
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
        /// 选中的数据行上显示的CSS效果
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue("")]
        [Description("选中的数据行上显示的CSS效果")]
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
        /// 是否显示控件默认的分页导航方式
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue(true)]
        [Description("是否显示控件默认的分页导航方式")]
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
        /// 分页导航处显示的项目名称n
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue("记录")]
        [Description("分页导航处显示的项目名称")]
        [Localizable(true)]
        public string ItemName
        {
            get
            {
                String s = (String)ViewState["ItemName"];
                return ((s == null) ? "记录" : s);
            }

            set
            {
                ViewState["ItemName"] = value;
            }
        }

        /// <summary>
        /// 分页导航处显示的项目单位
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue("条")]
        [Description("分页导航处显示的项目单位")]
        [Localizable(true)]
        public string ItemUnit
        {
            get
            {
                String s = (String)ViewState["ItemUnit"];
                return ((s == null) ? "条" : s);
            }

            set
            {
                ViewState["ItemUnit"] = value;
            }
        }

        /// <summary>
        /// 显示序号列
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue(false)]
        [Description("是否保持当前状态")]
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
        /// 显示序号列
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue(false)]
        [Description("是否显示序号列")]
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
        /// 序号列的标题文字
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue("名次")]
        [Description("序号列的标题文字")]
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
        /// 序号列的索引
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue(0)]
        [Description("序号列的索引")]
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
        /// 是否自动生成复选框列
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue(false)]
        [Description("是否自动生成复选框列")]
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
        /// 复选框列宽度
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue(20)]
        [Description("复选框列宽度")]
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
        /// 复选框列的索引
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [Description("复选框列的索引")]
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
        /// 行双击时跳转的URL
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [Description("行双击时跳转的URL，可以包含{$Field}来代替绑定的数据列，比如：UserShow.aspx?UserID={$Field}")]
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
        /// 行双击时绑定的数据列URL
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [Description("行双击时绑定的数据列")]
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
        /// 总记录数
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
        /// 得到复选框选定的ID
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
        /// 覆写PageIndex属性，并属性值保存在Session中
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
        /// 覆写PageSize属性，并属性值保存在Session中
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
            string url = Page.ClientScript.GetWebResourceUrl(t, ExtendedGridView_JS);
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(t, ExtendedGridView_JS))
            {
                Page.ClientScript.RegisterClientScriptInclude(t, ExtendedGridView_JS, url);
            }

            // 从Session中读取保留的状态值，包括每页记录数，当前页
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
        /// 将数据源中的数据绑定到复合数据绑定控件。
        /// </summary>
        /// <param name="data">包含要绑定到复合数据绑定控件的值</param>
        protected override void PerformDataBinding(IEnumerable data)
        {
            base.PerformDataBinding(data);
            this.ViewState["VirtualItemCount"] = this.ViewState["_!ItemCount"];
            //HttpContext.Current.Response.Write("<br>performdatabinding<br>");
        }

        /// <summary>
        /// 创建用来构建控件层次结构的列字段集
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="useDataSource">true 表示使用 dataSource 参数指定的数据源；否则为 false</param>
        /// <returns>返回列字段集</returns>
        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            ICollection columnList = base.CreateColumns(dataSource, useDataSource);
            if (!AutoGenerateCheckBoxColumn && !AutoGenerateSerialColumn)
            {
                return columnList;
            }

            // 创建扩展的列
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
        /// 构建复选框列数组
        /// </summary>
        /// <param name="columnList">数据源</param>
        /// <returns>返回复选框列数组</returns>
        private ArrayList AddCheckBoxColumn(ICollection columnList)
        {
            ArrayList list = new ArrayList(columnList);

            string checkBoxColumHeaderTemplate =
                "<input type='checkbox' hidefocus='true' id='{0}' name='{0}' onclick='CheckAll(this,\"" +
                this.RowStyle.CssClass + "\",\"" + this.SelectedCssClass + "\")'>";

            // 创建自定义的CheckBox列
            InputCheckBoxField field = new InputCheckBoxField();
            string checkBoxID = String.Format(CheckBoxColumHeaderID, ClientID);
            field.HeaderText = String.Format(checkBoxColumHeaderTemplate, checkBoxID);
            field.HeaderStyle.Width = CheckBoxFieldHeaderWidth;
            field.ReadOnly = true;
            field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            // 插入CheckBox列到列字段集的指定位置，如果指定位置大于字段集的列数，则插入到最后
            if (CheckBoxColumnIndex > list.Count)
            {
                list.Add(field);
                CheckBoxColumnIndex = list.Count - 1;
            }
            else
            {
                list.Insert(CheckBoxColumnIndex, field);
            }

            // 返回扩展后的列字段集
            return list;
        }

        /// <summary>
        /// 构建序号列数组
        /// </summary>
        /// <param name="columnList">数据源</param>
        /// <returns>返回序号列数组</returns>
        private ArrayList AddSerialColumn(ICollection columnList)
        {
            ArrayList list = new ArrayList(columnList);

            // 创建自定义的序号列
            TemplateField field = new TemplateField();
            field.HeaderText = this.SerialText;

            // 插入序号列到列字段集的指定位置，如果指定位置大于字段集的列数，则插入到最后
            if (SerialColumnIndex > list.Count)
            {
                list.Add(field);
                SerialColumnIndex = list.Count - 1;
            }
            else
            {
                list.Insert(SerialColumnIndex, field);
            }

            // 返回扩展后的列字段集
            return list;
        }

        /// <summary>
        /// 引发 RowCreated 事件
        /// </summary>
        /// <param name="e">GridViewRowEventArgs，它包含事件数据</param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(this.MouseOverCssClass))
                {
                    // 当鼠标移到的时候设置该行样式为MouseOverCssClass中指定的值， 并保存原来的样式
                    e.Row.Attributes.Add("onmouseover", "MouseOver(this,\"" + this.MouseOverCssClass + "\")");
                    // 当鼠标移走时还原该行的样式
                    e.Row.Attributes.Add("onmouseout", "MouseOut(this)");
                }

                if (this.AutoGenerateSerialColumn)
                {
                    // 为自定义序号列赋值
                    e.Row.Cells[this.SerialColumnIndex].Text = Convert.ToString(e.Row.DataItemIndex + this.PageIndex * this.PageSize + 1);
                }
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                if (this.ShowCustomPager)
                {
                    // 创建自定义的分页导航
                    CreateCustomPagerRow(e);
                }
            }
            ////HttpContext.Current.Response.Write("<br>on row created<br>");
        }

        /// <summary>
        /// 创建自定义分页导航
        /// </summary>
        /// <param name="e">GridViewRowEventArgs，它包含事件数据</param>
        private void CreateCustomPagerRow(GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Controls.Clear();

            #region 显示： 共 234 篇文章

            e.Row.Cells[0].Controls.Add(new LiteralControl("共&nbsp;"));

            Label LblRowsCount = new Label();
            LblRowsCount.ID = "LblRowsCount";
            LblRowsCount.Text = VirtualItemCount.ToString();
            LblRowsCount.Font.Bold = true;

            e.Row.Cells[0].Controls.Add(LblRowsCount);

            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;" + this.ItemUnit + this.ItemName));
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            #endregion

            #region 显示： 首页 上一页 下一页 尾页

            LinkButton LbtnFirst = new LinkButton();
            LbtnFirst.CommandName = "Page";
            LbtnFirst.CommandArgument = "First";
            LbtnFirst.Enabled = !(this.PageIndex == 0);
            LbtnFirst.Text = "首页";
            e.Row.Cells[0].Controls.Add(LbtnFirst);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;"));

            LinkButton LbtnPrev = new LinkButton();
            LbtnPrev.CommandName = "Page";
            LbtnPrev.CommandArgument = "Prev";
            LbtnPrev.Enabled = !(this.PageIndex == 0);
            LbtnPrev.Text = "上一页";
            e.Row.Cells[0].Controls.Add(LbtnPrev);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;"));

            LinkButton LbtnNext = new LinkButton();
            LbtnNext.CommandName = "Page";
            LbtnNext.CommandArgument = "Next";
            LbtnNext.Enabled = !(this.PageIndex == this.PageCount - 1);
            LbtnNext.Text = "下一页";
            e.Row.Cells[0].Controls.Add(LbtnNext);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;"));

            LinkButton LbtnLast = new LinkButton();
            LbtnLast.CommandName = "Page";
            LbtnLast.CommandArgument = "Last";
            LbtnLast.Enabled = !(this.PageIndex == this.PageCount - 1);
            LbtnLast.Text = "尾页";
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

            #region 显示： 页次：23/45页

            e.Row.Cells[0].Controls.Add(new LiteralControl("页次："));

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

            e.Row.Cells[0].Controls.Add(new LiteralControl("页"));
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            #endregion

            #region 显示： 20 篇文章/页
            TextBox TxtMaxPerPage = new TextBox();
            TxtMaxPerPage.ApplyStyleSheetSkin(Page);
            TxtMaxPerPage.MaxLength = 3;
            TxtMaxPerPage.Width = 22;
            TxtMaxPerPage.Text = Convert.ToString(this.PageSize);
            TxtMaxPerPage.AutoPostBack = true;
            TxtMaxPerPage.TextChanged += new EventHandler(this.TxtMaxPerPage_TextChanged);
            e.Row.Cells[0].Controls.Add(TxtMaxPerPage);

            e.Row.Cells[0].Controls.Add(new LiteralControl(this.ItemUnit + this.ItemName + "/页"));
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            #endregion

            #region 显示： 转到第  页
            e.Row.Cells[0].Controls.Add(new LiteralControl("转到第"));

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
            e.Row.Cells[0].Controls.Add(new LiteralControl("页"));

            #endregion
        }

        /// <summary>
        /// 引发 RowDataBound 事件
        /// </summary>
        /// <param name="e">GridViewRowEventArgs，它包含事件数据</param>
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(RowDblclickUrl))
                {
                    if (!String.IsNullOrEmpty(RowDblclickBoundField))
                    {
                        // 增加行的双击事件，调用客户端脚本
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
        /// 引发 PreRender 事件
        /// </summary>
        /// <param name="e">包含事件数据的 EventArgs</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // 解决删除最后一页的所有记录导致分页与实际数据不一致的问题
            if (this.PageCount > 1 && this.PageIndex + 1 == this.PageCount && this.Rows.Count == 0)
            {
                if (base.Initialized)
                {
                    base.RequiresDataBinding = true;
                }
            }

            // 解决缓存当前页后，因查询条件改变时当前页无数据的问题
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

                // 给总数标签赋值
                if (this.ShowCustomPager)
                {
                    Label LblRowsCount = this.BottomPagerRow.Cells[0].FindControl("LblRowsCount") as Label;
                    if (LblRowsCount != null)
                    {
                        LblRowsCount.Text = VirtualItemCount.ToString();
                    }
                }
            }

            // 为每个选择框增加客户端的onclick事件
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
        /// 建立控件层次结构
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


        #region 自定义的事件处理方法

        /// <summary>
        /// 每页最大数文本更改后触发事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件</param>
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
        /// 当前页文本更改后触发事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件</param>
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
        /// 下拉式当前页选择更改后触发事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件</param>
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
        /// 点击首页链接触发事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件</param>
        protected void LbtnFirst_Click(object sender, EventArgs e)
        {
            this.Context.Session[m_UniqueControlPageIndex] = 0;
        }

        /// <summary>
        /// 点击上一页链接触发事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件</param>
        protected void LbtnPrev_Click(object sender, EventArgs e)
        {
            this.Context.Session[m_UniqueControlPageIndex] = this.PageIndex - 1;
        }

        /// <summary>
        /// 点击下一页链接触发事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件</param>
        protected void LbtnNext_Click(object sender, EventArgs e)
        {
            this.Context.Session[m_UniqueControlPageIndex] = this.PageIndex + 1;
        }

        /// <summary>
        /// 点击尾页链接触发事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件</param>
        protected void LbtnLast_Click(object sender, EventArgs e)
        {
            this.Context.Session[m_UniqueControlPageIndex] = this.PageCount - 1;
        }

        #endregion


    }
}
