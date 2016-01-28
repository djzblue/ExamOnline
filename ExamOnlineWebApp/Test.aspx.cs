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
using SDPTExam.Web.UI;
using SDPTExam.BLL;
namespace SDPTExam.Web.UI
{
    public partial class Test : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string pwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("123456", "MD5");

            //Response.Write("<br>MD5:  " + pwdEncode);


            // pwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("123456", "SHA1");

            // Response.Write("<br>SHA1：" + pwdEncode);

           // GetConfig();

           string tempPath= Utility.GetConfigValue(10001, "TempFilePath");
           Response.Write("TempFilePath=" + tempPath);
            
        }

        private void GetConfig()
        {
            //Utility.AddDepartmentConfig(10001);
        
        }

    }
}
