using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SDPTExam.Web.UI
{
   public class AdminBasePage:BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (HttpContext.Current.Session["IsAdmin"] == null)
                Response.Redirect("~/Login.aspx");

        }
    }
}
