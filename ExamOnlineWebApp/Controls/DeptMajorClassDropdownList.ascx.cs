using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;

namespace SDPTExam.Web.UI.Controls
{
    public partial class DeptMajorClassDropdownList : System.Web.UI.UserControl
    {
       
        public event EventHandler ButtonClick;

        public int DepartmentID
        {
            get 
            {
               
                return int.Parse(ddlSelectDepts.SelectedValue);
            }
        }


        public int MajorID
        {
            get
            {
                if(ddlSelectDepts.SelectedValue!="0")
                return int.Parse(ddlSelectMajor.SelectedValue);
                else return 0;
            }
        }


        public int ClassID
        {
            get
            {
                if (ddlSelectDepts.SelectedValue == "0" || ddlSelectMajor.SelectedValue == "0")
                    return 0;
                else return int.Parse(ddlClass.SelectedValue);
            }
        }

        public string ClassName
        {
            get
            {
                if (ddlSelectDepts.SelectedValue == "0" || ddlSelectMajor.SelectedValue == "0")
                    return "未知班级";
                else return ddlClass.SelectedItem.Text;
            }
        }

        public int MinLevel
        {
            get 
            {
                if (ClassID != 0) return 2;
                else if (MajorID != 0) return 1;
                else return 0;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShowStudents_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null) ButtonClick(this, e);
        }


        protected void ddlSelectDepts_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList d = sender as DropDownList;
            ddlClass.Enabled = false;
            if (d.SelectedValue == "0")
            {
               
                ddlSelectMajor.Enabled = false;

            }
            else
            {
                int deptID = int.Parse(d.SelectedValue);

                FillDropdownList(ddlSelectMajor, BaseData.GetMajorsByDepartmentID(deptID), "全部专业");

                if (string.IsNullOrEmpty(ddlSelectMajor.SelectedValue) == false)
                {
                    int majorID = int.Parse(ddlSelectMajor.SelectedValue);

                    FillDropdownList(ddlClass, BaseData.GetClasssByMajorID(majorID), "全部班级");
                }


            }
        }

        protected void ddlSelectMajor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList d = sender as DropDownList;
            if (d.SelectedValue == "0")
            {
                ddlClass.Enabled = false;

            }
            else
            {
                int majorID = int.Parse(d.SelectedValue);

                FillDropdownList(ddlClass,BaseData.GetClasssByMajorID(majorID),"全部班级");
               // ddlClass.Visible = true;
               // ddlClass.Enabled = true;

            }
        }

        private void FillDropdownList(DropDownList d,object ds,string allName)
        {
            d.Items.Clear();
            d.AppendDataBoundItems = false;
            if (ds == null) return;    
       
            d.DataSourceID = "";
            d.DataSource =ds ;
            d.DataBind();
            
            d.Items.Add(new ListItem(allName, "0"));
            d.Enabled = true;
        }

    }
}