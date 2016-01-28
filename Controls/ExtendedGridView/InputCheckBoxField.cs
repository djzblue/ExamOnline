/*
*��    ������չGridView�ؼ�ʹ�õ�ѡ����
*��    �ߣ�ȫ�忪����Ա
*�������ڣ�2006-09-18
*�޶����ڣ�
*�� �� �ˣ�
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

        //CheckBoxField.InitializeDataCell ���� ����ָ���� DataControlFieldCell �����ʼ��Ϊָ������״̬
        //InitializeDataCell ������һ�� Helper ���������ڳ�ʼ�� CheckBoxField �����еĵ�Ԫ��
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
