/*
*��    ����GridViewTemplate
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
        /// <param name="controlId">�ؼ�ID</param>
        /// <param name="controlType">�ؼ���������</param>
        /// <param name="dataItemType">���ݳ�Ա����</param>
        /// <param name="dataField">���ݳ�Ա����</param>
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
        /// <param name="controlId">�ؼ�ID</param>
        /// <param name="controlType">�ؼ���������</param>
        public GridViewTemplate(string controlId, string controlType) : this(controlId, controlType, null, "") { }

        /// <summary>
        /// GridViewTemplate
        /// </summary>
        /// <param name="controlId">�ؼ�ID</param>
        public GridViewTemplate(string controlId) : this(controlId, "TextBox", null, "") { }

        /// <summary>
        /// ������ʵ��ʱ�������ӿؼ���ģ�������� System.Web.UI.Control ����Ȼ��������ģ���ж�����Щ�ӿؼ�
        /// </summary>
        /// <param name="container">Ҫ��������ģ���еĿؼ�ʵ���� System.Web.UI.Control ����</param>
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
