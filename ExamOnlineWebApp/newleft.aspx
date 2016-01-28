<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newleft.aspx.cs" Inherits="SDPTExam.Web.UI.NewLeftPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="newHead1" runat="server">
    <title>无标题页</title>
</head>
<body style="font-size:12px; background:url(images/leftbg.jpg)">
    <form id="newleftform1" runat="server">
    <div>
        <div>
            
            <br />
              请选择自测内容：<br />
            <br />
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="sqlChapters">
            <ItemTemplate>
             <a href='<%#"Students/StuExam.aspx?examID="+Eval("BasicExamID") %>' target="mainFrame"><%# Eval("BasicExamTitle")%></a>
             <br /><br />
            
            </ItemTemplate>
            </asp:Repeater>
            <br />
            <asp:LinkButton ID="newlbnLogout" runat="server" OnClick="lbnLogout_Click" 
                ToolTip="以管理员身份重新登录">安全退出</asp:LinkButton>
                <br />
            <br />
            <asp:SqlDataSource ID="sqlChapters" runat="server" 
                ConnectionString="<%$ ConnectionStrings:SQLConnStr %>" 
                SelectCommand="SELECT * FROM [BasicExamInfo] WHERE ([CourseID] = @CourseID)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="3" Name="CourseID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
                
            <br />

    </div>
  
    </form>
</body>
</html>
