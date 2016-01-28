<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="SDPTExam.Web.UI.Teachers.StudentProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
.hello table{width:100%;border:1px solid #3399FF;border-width:1px 0 0 1px;margin:2px 0 2px 0;text-align:center;border-collapse:collapse;}
.hello td,th{border:1px solid #3399FF;border-width:0 1px 1px 0;margin:2px 0 2px 0;height:20px;text-align:left;}
.hello th{text-align:center;font-weight:600;font-size:12px;background-color:#F4F4F4;}
        </style>
          <script src="../js/OpenNewWindow.js" language="javascript" type="text/javascript" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    学生列表：
                
        <pe:ExtendedGridView  ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="objStudents" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            DataKeyNames="StudentID" Width="70%" CheckBoxFieldHeaderWidth="3%" 
            IsHoldState="True" SerialText="">
            <Columns>
                <asp:TemplateField HeaderText="学生姓名" SortExpression="SName">
<ItemTemplate>
                    <asp:HyperLink ID="hlinkName" runat="server"  NavigateUrl='<%# "javascript:void openwindow(" + "\"../Students/ProfileManagement.aspx?StudentID=" + Eval("StudentID").ToString() + "\",\"学生个人信息\",2)"%>' Text='<%# Bind("SName") %>'></asp:HyperLink>
                </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:BoundField DataField="TheClass" HeaderText="班级" 
                    SortExpression="TheClass" />
                <asp:BoundField DataField="ExamTitle" HeaderText="论文题目" 
                    SortExpression="ExamTitle" />
            </Columns>
         </pe:ExtendedGridView >
        
        <br />
       </div><div class="hello"> 
     <asp:ObjectDataSource ID="objStudents" runat="server" 
        SelectMethod="GetStudentsByTeacherID" TypeName="SDPTExam.BLL.Teacher" 
               OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter Name="tid" SessionField="UserID" Type="Int32" />
            <asp:Parameter DefaultValue="false" Name="hasToSelectedSubject" 
                Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
