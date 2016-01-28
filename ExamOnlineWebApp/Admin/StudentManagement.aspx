<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentManagement.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.StudentManagement" %>

<%@ Register src="../Controls/DeptMajorClassDropdownList.ascx" tagname="DeptMajorClassDropdownList" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
        <style type="text/css">
    table{border:1px solid #3399FF;border-width:1px 0 0 1px;margin:2px 0 2px 0;text-align:left;border-collapse:collapse;}
 td,th{border:1px solid #3399FF;border-width:0 1px 1px 0;margin:2px 0 2px 0;text-align:left;}
th{text-align:center;font-weight:600;font-size:12px;background-color:#F4F4F4;}
        </style>
        
         <script src="../js/OpenNewWindow.js" language="javascript" type="text/javascript" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   
    
        <asp:Button ID="btnDeleteAll" runat="server" onclick="btnDeleteAll_Click" 
            Text="删除所有学生" ToolTip="慎用此功能"  
            onclientclick="return confirm(&quot;将删除该系所有学生的所有信息，确定删除吗？&quot;)" 
            style="margin-bottom: 0px" Enabled="false"  />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <br />
        <asp:Label ID="lblDept" runat="server" Text="系别：" Visible="False"></asp:Label>
        <asp:DropDownList ID="ddlSelectDepts" runat="server" AppendDataBoundItems="True" 
                DataSourceID="objDepartments" DataTextField="DepartmentName" 
            DataValueField="DepartmentID" Visible="False" 
            onselectedindexchanged="ddlSelectDepts_SelectedIndexChanged" 
            AutoPostBack="True">
                <asp:ListItem Value="0">所有系</asp:ListItem>
            </asp:DropDownList>
         &nbsp;专业：<asp:DropDownList ID="ddlSelectMajor" runat="server" AppendDataBoundItems="True" 
                DataSourceID="objMajors" DataTextField="MajorName" DataValueField="MajorID">
                <asp:ListItem Value="0">所有专业</asp:ListItem>
            </asp:DropDownList>
&nbsp;<asp:Button ID="btnShowStudents" runat="server" Text="显示" 
            onclick="btnShowStudents_Click" CausesValidation="False" />
    
    &nbsp;
    
    <asp:Button ID="btnAddNewStu" runat="server" Text="新增" 
            onclick="btnAddNewStu_Click" CausesValidation="False" />
        <br />
        <pe:ExtendedGridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
            DataKeyNames="StudentID" AllowPaging="True" Width="80%" 
                   onrowcommand="GridView1_RowCommand" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onrowdeleting="GridView1_RowDeleting">
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
            <asp:TemplateField HeaderText="编辑" ShowHeader="False">
                <ItemTemplate>                    
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="modify" 
                        CommandArgument='<%# Eval("StudentID") %>' CausesValidation="False">修改</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandName="Delete" 
                        onclientclick="return confirm(&quot;将删除该学生的所有信息，确定删除吗？&quot;)" Text="删除"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="27%" />
            </asp:TemplateField>
          </Columns>
        <EmptyDataTemplate>
            目前没有该系（该专业）的学生数据！
        </EmptyDataTemplate>
            <RowStyle HorizontalAlign="Center" />
    </pe:ExtendedGridView>
        <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" 
                             Visible="False"></asp:Label>
                     <br />
        <br />
          <asp:Panel ID="panelModify" runat="server" Visible="false">
             <table style="width:87%;">
            <tr>
                <td >
                    登录名：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtLoginName" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtLoginName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td >
                    真实姓名：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtRealName" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtRealName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>            <tr>
                <td >
                    密码：</td>
                <td >
                   <asp:TextBox Width="200px"  ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtPassword" ErrorMessage="*"></asp:RequiredFieldValidator>
                     </td>
            </tr>
                 <tr>
                     <td>
                         确认密码：</td>
                     <td>
                         <asp:TextBox ID="txtPassword0" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                         <asp:CompareValidator ID="CompareValidator1" runat="server" 
                             ControlToCompare="txtPassword" ControlToValidate="txtPassword0" 
                             ErrorMessage="密码不一致"></asp:CompareValidator>
                     </td>
                 </tr>
            <tr>
                <td >
                    性别：</td>
                <td>
                    <asp:DropDownList ID="ddlSex" runat="server">
                        <asp:ListItem Selected="True" Value="false">男</asp:ListItem>
                        <asp:ListItem Value="true">女</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
                        <tr>
                <td >
                    学号：</td>
                <td>
                    <asp:TextBox ID="txtStuNum" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtStuNum" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
            </tr>
                               <tr>
                <td >
                    班级：</td>
                <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
                   <uc1:DeptMajorClassDropdownList ID="DeptMajorClassDropdownList1" 
                        runat="server" />  </ContentTemplate>   </asp:UpdatePanel>
                                   </td>
            </tr>
                        
            <tr>
                <td >
                    年级：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtGrade" runat="server" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtGrade" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
            </tr>
                  <tr>
                <td >
                    家庭地址</td>
                <td>
                    <asp:TextBox ID="txtHomeAddress" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="txtHomeAddress" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
            </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" Text="添加" />
                         &nbsp;
                         <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                             onclick="btnCancel_Click" Text="取消" />
                         &nbsp;</td>
                 </tr>
            </table>
            </asp:Panel>
        <br />
    </div>
    <br />
    <br />
    <asp:LinkButton ID="lnkbtnShowImport" runat="server" Visible="False" 
        onclick="lnkbtnShowImport_Click" CausesValidation="False">批量导入数据</asp:LinkButton>
     <asp:Panel ID="panelSuperAdmin" runat="server" Visible="False">
         <br />
         <asp:LinkButton ID="lbtnHide" runat="server" onclick="lbtnHide_Click">隐藏</asp:LinkButton>
         <br />
         <br />
        <asp:Label ID="lblFeedback" runat="server" ForeColor="#FF3300"></asp:Label>
         <br />
         <asp:Label ID="lblHint" runat="server" ForeColor="Red"></asp:Label>
        <br />
    
        <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;
        <asp:Button ID="Button1" runat="server" Text="批量导入" onclick="Button1_Click" />
&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/SampleFiles/Students.xls">参考格式</asp:HyperLink>
    
        <br />
        
      
    <br />
    上传学生相片 ：<asp:FileUpload ID="filePhotos" runat="server" />
&nbsp;<asp:Button ID="btnImportPhotos" runat="server" 
        onclick="btnImportPhotos_Click" Text="批量导入" />
        
          </asp:Panel>
    <br />
    <asp:ObjectDataSource ID="objSingleStudent" runat="server" 
        SelectMethod="GetStudentByID" TypeName="SDPTExam.BLL.Student" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="id" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objMajors" runat="server" 
            SelectMethod="GetMajorsByDepartmentID" 
        TypeName="SDPTExam.BLL.BaseData" 
        OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:SessionParameter Name="deptID" SessionField="DepartmentID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objDepartments" runat="server" 
            SelectMethod="GetDepartments" TypeName="SDPTExam.BLL.BaseData" 
            DeleteMethod="DeleteDepartment">
            <DeleteParameters>
                <asp:Parameter Name="departmentID" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        <br />
    </form>
</body>
</html>
