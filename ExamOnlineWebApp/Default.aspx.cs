using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SDPTExam.BLL;

namespace SDPTExam.Web.UI
{
    public partial class _Default : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["Role"] != null)
            //{
            //    if (Session["Role"].ToString() == "student")
            //    {
            //        Response.Redirect("~/Students/ProfileManagement.aspx");
            //    }
            //    else if (Session["Role"].ToString() == "teacher")
            //        Response.Redirect("~/Teachers/ProfileManagement.aspx");
            //    else
            //        Response.Redirect("~/Admin/BaseDataManagement.aspx");
            //  }
        }

    }
}
