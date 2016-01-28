/*
*描    述：扩展GridView控件使用的选择列
*作    者：全体开发人员
*创建日期：2006-09-18
*修订日期：
*修 订 人：
*/
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamManagement.Controls
{
    internal sealed class InputCheckBoxField : CheckBoxField
    {
        public const string CheckBoxID = "CheckBoxButton";

        public InputCheckBoxField()
        {
        }

        //CheckBoxField.InitializeDataCell 方法 ：将指定的 DataControlFieldCell 对象初始化为指定的行状态
        //InitializeDataCell 方法是一种 Helper 方法，用于初始化 CheckBoxField 对象中的单元格
        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);

            // Add a checkbox anyway, if not done already
            if (cell.Controls.Count == 0)
            {
                CheckBox chk = new CheckBox();
                
                chk.ID = InputCheckBoxField.CheckBoxID;
                cell.Controls.Add(chk);
            }
        }
    }
}
