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
using System.IO;
using SDPTExam.BLL;
using SDPTExam.DAL.Model;

namespace SDPTExam.Web.UI
{
    public partial class ShowNotices : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        

        protected string GetTypeName(int typeID)
        {
            string[] typeNames = { "专业", "系部", "学院" };
            return typeNames[typeID - 1];
        }



      


    }
}

