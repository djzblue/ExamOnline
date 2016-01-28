using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.Web.UI.Controls;
namespace SDPTExam.Web.UI.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            DeptMajorClassDropdownList dlist = sender as DeptMajorClassDropdownList;

            Response.Write(dlist.MajorID);

            Response.Write("级别：" + dlist.MinLevel);
        }
    }
}
