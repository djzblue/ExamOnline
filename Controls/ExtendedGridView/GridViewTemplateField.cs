/*
*描    述：GridViewTemplateField
*作    者：曾强泉
*创建日期：2007-07-31
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
    /// GridViewTemplateField
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:GridViewTemplateField runat=server></{0}:GridViewTemplateField>")]
    public class GridViewTemplateField : System.Web.UI.WebControls.TemplateField
    {
        /// <summary>
        /// 将数据源视图还原为保存过的前一视图状态
        /// </summary>
        /// <param name="savedState">一个表示要还原的 System.Web.UI.WebControls.DataControlField 状态的对象</param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            base.ItemTemplate = ViewState["GridViewTemplateField"] as GridViewTemplate;
        }

        /// <summary>
        /// 获取或设置用于显示数据绑定控件中的项的模板
        /// </summary>
        public override ITemplate ItemTemplate
        {
            get
            {
                if (ViewState["GridViewTemplateField"] != null)
                {
                    return ViewState["GridViewTemplateField"] as GridViewTemplate;
                }
                return base.ItemTemplate;
            }
            set
            {
                base.ItemTemplate = value;
                ViewState["GridViewTemplateField"] = value;
            }
        }
    }
}
