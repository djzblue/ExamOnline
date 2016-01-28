<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OldEntry.aspx.cs" Inherits="SDPTExam.Web.UI.OldEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        说明：旧版系统仅为单一院系提供毕业设计管理的功能，是外语系在2008年~2009年期间实际运行使用的系统，里面包括大量真实数据。<br />
        <br />
        管理员用户：admin；导师测试账号:tutor; 学生测试账号:student<br />
        密码：0<br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="http://testExam.enonline.org/" Target="_blank">点击进入</asp:HyperLink>
    
    </div>
    </form>
</body>
</html>
