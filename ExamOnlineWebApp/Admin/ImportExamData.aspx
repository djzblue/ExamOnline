<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportExamData.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.ImportExamData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
    

<asp:Panel ID="panelImport" runat="server" >
        
        <br />批量导入选择题信息：
        
        <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;
        <asp:Button ID="btnChoices" runat="server" Text="批量导入" 
            onclick="Button1_Click" />
&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/SampleFiles/Choices.xls">参考格式</asp:HyperLink>
    
        <br />
        <br />
        <br />
        批量导入判断题信息： 
        <asp:FileUpload ID="fileJudges" runat="server" />
        &nbsp;
        <asp:Button ID="btnJudges" runat="server" Text="批量导入" 
            onclick="btnJudges_Click" />
        &nbsp;
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="~/SampleFiles/Judges.xls">参考格式</asp:HyperLink>
    
        <br /></asp:Panel>
    
    </div>
    <br />
    <br />
    批量导入试题：<asp:FileUpload ID="FileUpload2" runat="server" />
&nbsp;
        <asp:Button ID="btnQuestions" runat="server" Text="批量导入" 
            onclick="btnQuestions_Click" />
    </form>
</body>
</html>
