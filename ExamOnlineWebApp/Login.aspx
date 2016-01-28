<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SDPTExam.Web.Root.LoginPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>登录页面</title>
   
    <link href="images/default.css" type=text/css rel=stylesheet>
<link href="images/xtree.css" type=text/css rel=stylesheet>
<link href="images/user_login.css" type=text/css rel=stylesheet>
<meta http-equiv=content-type content="text/html; charset=gb2312">
<meta content="mshtml 6.00.6000.16674" name=generator></head>
<body id=userlogin_body>

<div style="text-align:center"><img src="images/sdpt.jpg" border="0" width="567" alt="" /></div>
    <form id="form1" runat="server">    
   
   <div id=user_login>
<dl>
  <dd id=user_top>
  <ul>
    <li class=user_top_l></li>
    <li class=user_top_c></li>
    <li class=user_top_r></li></ul>
  <dd id=user_main>
  <ul>
    <li class=user_main_l></li>
    <li class=user_main_c>
    <div class=user_main_box>
    <ul>
      <li class=user_main_text>用户名：</li>
      <li class=user_main_input>
      <asp:TextBox ID="txtUserName" CssClass="txtusernamecssclass" runat="server" Width="140px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入用户名" ControlToValidate="txtUserName"></asp:RequiredFieldValidator></td>
      
       </li></ul>
    <ul>
      <li class=user_main_text>密码：</li>
      <li class=user_main_input>
         <asp:TextBox ID="txtPwd" CssClass="txtpasswordcssclass" runat="server" TextMode="Password" Width="140px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入密码" ControlToValidate="txtPwd"></asp:RequiredFieldValidator></td>
      
      </li></ul>
    <ul>
      <li class=user_main_text>选择身份：</li>
      <li class=user_main_input> 
          <asp:RadioButtonList ID="rblRoles" runat="server" 
                     RepeatDirection="Horizontal">
                          <asp:ListItem Value="student" Selected="True">学生</asp:ListItem>
                     <asp:ListItem Value="admin">教师</asp:ListItem>                    
                 </asp:RadioButtonList>
          <asp:Button ID="btnVisitorLogin" runat="server" Text="游客登录" 
              CausesValidation="False" onclick="btnVisitorLogin_Click" />

      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

      </li></ul>
       <ul>
      <li class=user_main_input>
       <asp:Label ID="lblMessage" runat="server" ForeColor="Red"
            Text="用户名或密码错误!" Visible="False"></asp:Label>   
      </li>
</ul>
  
      </div></li>
    <li class=user_main_r>
    <asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/images/user_botton.gif" OnClick="submitbtn_Click" 
            ToolTip="请点击进入后台管理界面" />
     </li></ul>
</dl></div>    
       </form>
</body>
</html>

