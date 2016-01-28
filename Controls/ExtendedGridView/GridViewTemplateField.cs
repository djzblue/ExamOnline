/*
*��    ����GridViewTemplateField
*��    �ߣ���ǿȪ
*�������ڣ�2007-07-31
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
    /// GridViewTemplateField
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:GridViewTemplateField runat=server></{0}:GridViewTemplateField>")]
    public class GridViewTemplateField : System.Web.UI.WebControls.TemplateField
    {
        /// <summary>
        /// ������Դ��ͼ��ԭΪ�������ǰһ��ͼ״̬
        /// </summary>
        /// <param name="savedState">һ����ʾҪ��ԭ�� System.Web.UI.WebControls.DataControlField ״̬�Ķ���</param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            base.ItemTemplate = ViewState["GridViewTemplateField"] as GridViewTemplate;
        }

        /// <summary>
        /// ��ȡ������������ʾ���ݰ󶨿ؼ��е����ģ��
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
