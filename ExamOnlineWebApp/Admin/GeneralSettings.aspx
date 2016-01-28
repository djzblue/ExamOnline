<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralSettings.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.GeneralSettings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        考试时间范围：<br />
        <br />
        开始时间：<pe:DatePicker ID="dpkStart" runat="server" 
            DateImage="~/images/showdate.gif"></pe:DatePicker><br />
        结束时间：<pe:DatePicker ID="dpkEnd" runat="server" 
            DateImage="~/images/showdate.gif"></pe:DatePicker><br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="确定" onclick="btnSubmit_Click" />
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
