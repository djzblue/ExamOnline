<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oldoldLogin.aspx.cs" Inherits="SDPTExam.Web.UI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">

.TxtUserNameCssClass {
	    border-width: 0px;
            PADDING-LEFT: 25px; BACKGROUND: url('Images/user_login_name.gif') no-repeat; 
            WIDTH: 165px;     LINE-HEIGHT: 20px;     HEIGHT: 21px;
        }
INPUT {
	FONT-FAMILY: "????"
}
INPUT {
	FONT-SIZE: 12px; FONT-FAMILY: "????"
}
    </style>
</head>
<body style="font-size:12px">
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" width="1002">
<!-- fwtable fwsrc="images/enonline2(login).png" fwpage="页面 1" fwbase="images/enonline2(login).jpg" fwstyle="Dreamweaver" fwdocid = "266956181" fwnested="0" -->
  <tr>
   <td><img src="spacer.gif" width="3" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="216" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="19" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="4" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="378" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="91" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="57" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="4" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="11" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="218" height="1" border="0" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="1" border="0" alt="" /></td>
  </tr>

  <tr>
   <td><img name="images/enonline2login_r1_c1" src="images/enonline2(login)_r1_c1.jpg" width="3" height="28" border="0" id="images/enonline2login_r1_c1" alt="" /></td>
   <td rowspan="2" colspan="10"><img name="images/enonline2login_r1_c2" src="images/enonline2(login)_r1_c2.jpg" width="999" height="111" border="0" id="images/enonline2login_r1_c2" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="28" border="0" alt="" /></td>
  </tr>
  <tr>
   <td rowspan="9"><img name="images/enonline2login_r2_c1" src="images/enonline2(login)_r2_c1.jpg" width="3" height="587" border="0" id="images/enonline2login_r2_c1" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="83" border="0" alt="" /></td>
  </tr>
  <tr>
   <td rowspan="8"><img name="images/enonline2login_r3_c2" src="images/enonline2(login)_r3_c2.jpg" width="216" height="504" border="0" id="images/enonline2login_r3_c2" alt="" /></td>
   <td colspan="8"><img name="images/enonline2login_r3_c3" src="images/enonline2(login)_r3_c3.jpg" width="565" height="76" border="0" id="images/enonline2login_r3_c3" alt="" /></td>
   <td rowspan="8"><img name="images/enonline2login_r3_c11" src="images/enonline2(login)_r3_c11.jpg" width="218" height="504" border="0" id="images/enonline2login_r3_c11" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="76" border="0" alt="" /></td>
  </tr>
  <tr>
   <td colspan="8"><img name="images/enonline2login_r4_c3" src="images/enonline2(login)_r4_c3.jpg" width="565" height="10" border="0" id="images/enonline2login_r4_c3" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="10" border="0" alt="" /></td>
  </tr>
  <tr>
   <td rowspan="6"><img name="images/enonline2login_r5_c3" src="images/enonline2(login)_r5_c3.jpg" width="19" height="418" border="0" id="images/enonline2login_r5_c3" alt="" /></td>
   <td colspan="6"><img name="images/enonline2login_r5_c4" src="images/enonline2(login)_r5_c4.jpg" width="535" height="47" border="0" id="images/enonline2login_r5_c4" alt="" /></td>
   <td rowspan="6"><img name="images/enonline2login_r5_c10" src="images/enonline2(login)_r5_c10.jpg" width="11" height="418" border="0" id="images/enonline2login_r5_c10" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="47" border="0" alt="" /></td>
  </tr>
  <tr>
   <td rowspan="3"><img name="images/enonline2login_r6_c4" src="images/enonline2(login)_r6_c4.jpg" width="4" height="186" border="0" id="images/enonline2login_r6_c4" alt="" /></td>
   <td colspan="3"><img name="images/enonline2login_r6_c5" src="images/enonline2(login)_r6_c5.jpg" width="526" height="37" border="0" id="images/enonline2login_r6_c5" alt="" /></td>
   <td rowspan="3"><img name="images/enonline2login_r6_c8" src="images/enonline2(login)_r6_c8.jpg" width="4" height="186" border="0" id="images/enonline2login_r6_c8" alt="" /></td>
   <td rowspan="3"><img name="images/enonline2login_r6_c9" src="images/enonline2(login)_r6_c9.jpg" width="1" height="186" border="0" id="images/enonline2login_r6_c9" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="37" border="0" alt="" /></td>
  </tr>
  <tr>
  <td rowspan="2"  bgcolor="#D3D7DC">
   <table width="100%">
   <tr><td align="right">用户名：</td><td>
      <asp:TextBox ID="txtUserName" CssClass="txtusernamecssclass" runat="server" 
           Width="181px"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
           ErrorMessage="*" ControlToValidate="txtUserName"></asp:RequiredFieldValidator></td></tr>
    <tr><td align="right">密码：</td><td>
         <asp:TextBox ID="txtPwd" CssClass="txtpasswordcssclass" runat="server" 
             TextMode="Password" Width="180px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
             ErrorMessage="*" ControlToValidate="txtPwd"></asp:RequiredFieldValidator></td></tr>
     <tr><td align="right">身份：</td><td> <asp:RadioButtonList ID="rblRoles" runat="server"  ToolTip="提示：'教师'包括校内指导教师、校外指导教师及各教研室管理人员，'部门'包括系主任，校级管理员，网站管理员"
                     RepeatDirection="Horizontal">
                          <asp:ListItem Value="student" Selected="True">学生</asp:ListItem>
                     <asp:ListItem Value="teacher">教师</asp:ListItem>
                     <asp:ListItem Value="admin">部门</asp:ListItem>
                 </asp:RadioButtonList>
         </td></tr>
     </table>
   &nbsp;<asp:Label ID="lblMessage" runat="server" ForeColor="Red"
            Text="用户名或密码错误!" Visible="False"></asp:Label>   
      </td>
   <td><asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="images/enonline2(login)_r7_c6.jpg" OnClick="submitbtn_Click" 
            ToolTip="请点击进入后台管理界面" /></td>
   <td rowspan="2"><img name="images/enonline2login_r7_c7" src="images/enonline2(login)_r7_c7.jpg" width="57" height="149" border="0" id="images/enonline2login_r7_c7" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="91" border="0" alt="" /></td>
  </tr>
  <tr>
   <td><img name="images/enonline2login_r8_c6" src="images/enonline2(login)_r8_c6.jpg" width="91" height="58" border="0" id="images/enonline2login_r8_c6" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="58" border="0" alt="" /></td>
  </tr>
  <tr>
   <td colspan="6"><img name="images/enonline2login_r9_c4" src="images/enonline2(login)_r9_c4.jpg" width="535" height="22" border="0" id="images/enonline2login_r9_c4" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="22" border="0" alt="" /></td>
  </tr>
  <tr>
   <td colspan="6"><img name="images/enonline2login_r10_c4" src="images/enonline2(login)_r10_c4.jpg" width="535" height="163" border="0" id="images/enonline2login_r10_c4" alt="" /></td>
   <td><img src="spacer.gif" width="1" height="163" border="0" alt="" /></td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
