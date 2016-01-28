<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckNotExamStu.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.CheckNotExamStu" %>

<%@ Register src="../Controls/DeptMajorClassDropdownList.ascx" tagname="DeptMajorClassDropdownList" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">

th{text-align:center;font-weight:600;font-size:12px;background-color:#F4F4F4;}
        th{border:1px solid #3399FF;border-width:0 1px 1px 0;margin:2px 0 2px 0;text-align:left;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:DeptMajorClassDropdownList ID="DeptMajorClassDropdownList1" OnButtonClick="btnShow_Click"
            runat="server"  />
        <br />
        <br />
        缺考考生名单：<br />
        <br />

    
    </div>
        <pe:ExtendedGridView ID="grvStuList" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="StudentID" AllowPaging="True" Width="80%"        
        onpageindexchanging="grvStuMarkList_PageIndexChanging"  
                   >
        <Columns>
        <asp:BoundField DataField="StudentID" HeaderText="ID" 
                 />
            <asp:TemplateField HeaderText="姓名" SortExpression="StuName">
                <ItemTemplate>
                    <asp:HyperLink ID="hlinkName" runat="server"  NavigateUrl='<%# "javascript:void openwindow(" + "\"../Students/ProfileManagement.aspx?StudentID=" + Eval("StudentID").ToString() + "\",\"学生个人信息\",2)"%>' Text='<%# Bind("StuName") %>'></asp:HyperLink>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:BoundField DataField="StuNum" HeaderText="学号" 
                SortExpression="StuNum" />
            <asp:BoundField DataField="Class" HeaderText="班级" 
                SortExpression="Class" />
          </Columns>
        <EmptyDataTemplate>
            目前没有该系（该专业）的学生数据！
        </EmptyDataTemplate>
            <RowStyle HorizontalAlign="Center" />
    </pe:ExtendedGridView>
    </form>
</body>
</html>
