<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="SDPTExam.Web.UI.Top" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<style>
a:link,a:visited,a:hover
{
    color:#FFF;
     text-decoration:none;
    }

    .style1
    {
        width: 82%;
    }

    .style2
    {
       vertical-align:top;
    }


    .style3
    {
        vertical-align: top;
        width: 8px;
    }


</style>
   
</head>
<body style="font-size:12px;margin:0px;">
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%"><!-- fwtable fwsrc="images/enonline2.png" fwpage="页面 1" fwbase="images/enonline2.jpg" fwstyle="Dreamweaver" fwdocid = "266956181" fwnested="0" -->

  <tr>
   <td colspan="3"><img name="images/enonline2_r2_c1" src="images/sdpt.jpg" border="0" alt="" /></td>
  </tr>
  <tr>
   <td background="images/enonline2_r2_c7.jpg" class="style1">
       <asp:Label ID="lblInfo" runat="server" ForeColor="#FFFFCC"></asp:Label>
      </td>
   <td style="background:url(images/enonline2_r2_c9.jpg);" align="left">&nbsp;<asp:LinkButton ID="lbnLogout" runat="server" OnClick="lbnLogout_Click" ToolTip="以管理员身份重新登录">安全退出</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp; </td>
   <td><img src="spacer.gif" width="1" height="37" border="0" alt="" /></td>
  </tr>
</table>

    </div>
    </form>
</body>
</html>
