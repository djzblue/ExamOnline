/*
*描    述：绑定布尔型字段
*作    者：魏壮志
*创建日期：2007-04-26
*修订日期：
*修 订 人：
*/
using System;
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
    /// 绑定布尔型字段
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BoolField runat=server></{0}:BoolField>")]
    public class BoolField : BoundField
    {

        /// <summary>
        /// 绑定值为 true 时显示的文本
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue("√")]
        [Description(" 绑定值为 true 时显示的文本")]
        [Localizable(true)]
        public string TrueText
        {
            get
            {
                String s = (String)ViewState["TrueText"];
                return ((s == null) ? "√" : s);
            }

            set
            {
                ViewState["TrueText"] = value;
            }
        }

       //new public int get_TrueText()
       // {
       //     return 9;
       // }
        /// <summary>
        /// 绑定值为 false 时显示的文本
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue("×")]
        [Description("绑定值为 false 时显示的文本")]
        [Localizable(true)]
        public string FalseText
        {
            get
            {
                String s = (String)ViewState["FalseText"];
                return ((s == null) ? "×" : s);
            }

            set
            {
                ViewState["FalseText"] = value;
            }
        }


        /// <summary>
        /// 绑定值为 true 时显示文本的颜色
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(WebColorConverter))]
        [Description("绑定值为 true 时显示文本的颜色")]
        [Localizable(true)]
        public virtual Color TrueTextColor
        {
            get
            {
                object obj = ViewState["TrueTextColor"];
                return obj == null ? Color.Empty : (Color)obj;
            }
            set
            {
                ViewState["TrueTextColor"] = value;
            }
        }


        /// <summary>
        /// 绑定值为 false 时显示文本的颜色
        /// </summary>
        [Bindable(true)]
        [Category("自定义")]
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(WebColorConverter))]
        [Description("绑定值为 false 时显示文本的颜色")]
        [Localizable(true)]
        public virtual Color FalseTextColor
        {
            get
            {
                object obj = ViewState["FalseTextColor"];
                return obj == null ? Color.Empty : (Color)obj;
            }
            set
            {
                ViewState["FalseTextColor"] = value;
            }
        }


        /// <summary>
        /// 格式化字段值
        /// </summary>
        /// <param name="dataValue">字段值</param>
        /// <param name="encode">是否对该值进行编码</param>
        /// <returns>已格式化的字段值</returns>
        protected override string FormatDataValue(object dataValue, bool encode)
        {
            if (dataValue is bool)
            {
                if ((bool)dataValue)
                {
                    if (this.TrueTextColor == Color.Empty)
                    {
                        return this.TrueText;
                    }
                    else
                    {
                        return "<span style=\"color:#" + this.TrueTextColor.ToArgb().ToString("x").Substring(2) + "\">" + this.TrueText + "</span>";
                    }
                }
                else
                {
                    if (this.FalseTextColor == Color.Empty)
                    {
                        return this.FalseText;
                    }
                    else
                    {
                        return "<span style=\"color:#" + this.FalseTextColor.ToArgb().ToString("x").Substring(2) + "\">" + this.FalseText + "</span>";
                    }
                }
            }
            return base.FormatDataValue(dataValue, encode);
        }

    }
}
