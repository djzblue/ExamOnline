<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAs.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.LoginAs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        身份：<asp:DropDownList ID="ddlRole" runat="server">
            <asp:ListItem>学生</asp:ListItem>
            <asp:ListItem>教师</asp:ListItem>
        </asp:DropDownList>
        <br />
        姓名：<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
        <asp:Button ID="btnQuery" runat="server" onclick="btnQuery_Click" Text="查询" />
        <br />
        <asp:RadioButtonList ID="rblistName" runat="server" RepeatColumns="6" 
            RepeatDirection="Horizontal">
        </asp:RadioButtonList>
        <br />
        <br />
        <asp:Button ID="btnLoginAs" runat="server" Text="以该用户身份登录" Width="158px" 
            onclick="btnLoginAs_Click" />
    
    </div>
    </form>
</body>
</html>
