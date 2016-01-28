<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyMark.aspx.cs" Inherits="SDPTExam.Web.UI.Students.MyMark" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                            我的得分为： 
                            <asp:Label ID="lblMyMark" runat="server" ForeColor="Red"></asp:Label>
&nbsp;&nbsp;
                            <asp:Button ID="btnReExam" runat="server" 
                            CommandArgument='<%# Eval("RandomExamID") %>'
                            Text="重新考试" onclick="btnReExam_Click" 
                            onclientclick='return confirm("将删除你的考试信息，必须重新考试，否则没有成绩，确定吗？")' 
                                Enabled="False" />
    </div>
    </form>
</body>
</html>
