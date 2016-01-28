/*
*描    述：GridViewTemplate
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
using System.Data;

namespace ExamManagement.Controls
{
    /// <summary>
    /// GridViewTemplate
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:GridViewTemplate runat=server></{0}:GridViewTemplate>")]
    [Serializable]
    public class GridViewTemplate : ITemplate
    {
        private string m_ControlId = null;
        private string m_DataField = null;
        private string m_ControlType;
        private Type m_DataItemType;

        /// <summary>
        /// GridViewTemplate
        /// </summary>
        /// <param name="controlId">控件ID</param>
        /// <param name="controlType">控件类型名称</param>
        /// <param name="dataItemType">数据成员类型</param>
        /// <param name="dataField">数据成员名称</param>
        public GridViewTemplate(string controlId, string controlType,Type dataItemType, string dataField)
        {
            this.m_ControlId = controlId;
            this.m_ControlType = controlType;
            this.m_DataItemType =dataItemType;
            this.m_DataField = dataField;
        }

        /// <summary>
        /// GridViewTemplate
        /// </summary>
        /// <param name="controlId">控件ID</param>
        /// <param name="controlType">控件类型名称</param>
        public GridViewTemplate(string controlId, string controlType) : this(controlId, controlType, null, "") { }

        /// <summary>
        /// GridViewTemplate
        /// </summary>
        /// <param name="controlId">控件ID</param>
        public GridViewTemplate(string controlId) : this(controlId, "TextBox", null, "") { }

        /// <summary>
        /// 当由类实现时，定义子控件和模板所属的 System.Web.UI.Control 对象。然后在内联模板中定义这些子控件
        /// </summary>
        /// <param name="container">要包含内联模板中的控件实例的 System.Web.UI.Control 对象</param>
        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (m_ControlType)
            {
                case ("Label"):
                    Label label = new Label();
                    label.ID = m_ControlId;
                    label.DataBinding += new EventHandler(control_DataBinding);
                    //label.Width = Unit.Pixel(50);
                    //label.Height = Unit.Pixel(12);
                    container.Controls.Add(label);
                    break;
                case ("TextBox"):
                    TextBox textBox = new TextBox();
                    textBox.ID = m_ControlId;
                    textBox.Width = Unit.Pixel(50);
                    textBox.Height = Unit.Pixel(12);
                    textBox.DataBinding += new EventHandler(control_DataBinding);
                    container.Controls.Add(textBox);
                    break;
                case ("CheckBox"):
                    CheckBox checkBox = new CheckBox();
                    checkBox.ID = m_ControlId;
                    checkBox.Checked = true;
                    //checkBox.CausesValidation = false;
                    //checkBox.AutoPostBack = true;
                    //checkBox.CheckedChanged += new EventHandler(OnCheckedChanged);
                    container.Controls.Add(checkBox);
                    break;
                default:
                    break;
            }
        }

        //private void OnCheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox checkBox = sender as CheckBox;
        //    GridViewRow row = checkBox.NamingContainer as GridViewRow;
        //    CheckBox ChkIsValid = (CheckBox)row.FindControl("ChkIsValid");
        //    Label LblProperty = (Label)row.FindControl("LblProperty");
        //    TextBox txtChildStocks = (TextBox)row.FindControl("TxtChildStocks");
        //    if (!ChkIsValid.Checked)
        //    {
        //        LblProperty.Enabled = false;
        //        txtChildStocks.Text = "qq";
        //    }
        //    else
        //    {
        //        LblProperty.Enabled = true;
        //        txtChildStocks.Text = "mm";

        //    }
        //    //throw new Exception("The method or operation is not implemented.");
        //}

        private void control_DataBinding(object sender, EventArgs e)
        {
            GridViewRow container;
            switch (m_ControlType)
            {
                case ("Label"):
                    Label label = (Label)sender;
                    container = (GridViewRow)label.NamingContainer;
                    if (!string.IsNullOrEmpty(m_DataField))
                    {
                        if (m_DataItemType != null)
                        {
                            label.Text = m_DataItemType.InvokeMember(m_DataField, System.Reflection.BindingFlags.GetProperty, null, container.DataItem, null).ToString();
                        }
                        else
                        {
                            label.Text = m_DataField;
                        }
                    }
                    break;
                case ("TextBox"):
                    TextBox textBox = (TextBox)sender;
                    container = (GridViewRow)textBox.NamingContainer;
                    if (!string.IsNullOrEmpty(m_DataField))
                    {
                        if (m_DataItemType != null)
                        {
                            textBox.Text = m_DataItemType.InvokeMember(m_DataField, System.Reflection.BindingFlags.GetProperty, null, container.DataItem, null).ToString();
                        }
                        else
                        {
                            textBox.Text = m_DataField;
                        }  
                    }
                    break;
                case ("CheckBox"):
                    CheckBox checkBox = (CheckBox)sender;
                    container = (GridViewRow)checkBox.NamingContainer;
                    //if (!string.IsNullOrEmpty(m_DataField))
                    //{
                    //    if (m_DataItemType != null)
                    //    {
                    //        checkBox.Checked = (bool)m_DataItemType.InvokeMember(m_DataField, System.Reflection.BindingFlags.GetProperty, null, container.DataItem, null);
                    //    }
                    //    else
                    //    {
                    //        checkBox.Checked = m_DataField;
                    //    }
                    //}
                    break;
                default:
                    break;
            }
        }
    }
}
