<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherManagement.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.TeacherManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">

        .style1
        {
            text-align: right;
            width: 176px;
        }
        </style>
         <link href="../tableStyle.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:Label ID="lblDept" runat="server" Text="系别：" Visible="False"></asp:Label>
        <asp:DropDownList ID="ddlSelectDepts" runat="server" AppendDataBoundItems="True" 
                DataSourceID="objDepartments" DataTextField="DepartmentName" 
            DataValueField="DepartmentID" Visible="False" 
            AutoPostBack="True">
                <asp:ListItem Value="0">所有系</asp:ListItem>
            </asp:DropDownList>
        
       
        &nbsp;
        
       
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="显示" 
            CausesValidation="False" />
    
        &nbsp;&nbsp;<asp:Button ID="btnAddNew" runat="server" CausesValidation="False" 
            onclick="btnAddNew_Click" Text="新增" />
    
        &nbsp;
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Button" />
    
        <br />
    
    </div>
    <pe:ExtendedGridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="TeacherID" 
        AllowPaging="True" 
       
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
       
        onpageindexchanging="GridView1_PageIndexChanging" 
        onrowdeleting="GridView1_RowDeleting">
        <Columns>
         <asp:BoundField DataField="TeacherID" HeaderText="ID" 
                SortExpression="TeacherID" />
            <asp:BoundField DataField="TeacherName" HeaderText="姓名" 
                SortExpression="TeacherName" />
            <asp:BoundField DataField="ProfessionalTitle" HeaderText="职称" 
                SortExpression="ProfessionalTitle" />
            <asp:BoundField DataField="ResearchField" HeaderText="研究领域" 
                SortExpression="ResearchField" />
            <asp:BoundField DataField="LoginCount" HeaderText="登录次数" />
            <asp:CommandField SelectText="修改" ShowSelectButton="True" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandName="Delete" 
                        onclientclick="return confirm(&quot;这将删除所有与该教师相关的信息，其指导的学生将恢复为初始状态。请慎重考虑！确定删除吗？&quot;)" Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
         <asp:Panel ID="panelModify" runat="server" Visible="False">
                <table style="width:70%;">            
            <tr>
                <td class="style1">
                    登录名：</td>
                <td>
                    <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtLoginName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>            <tr>
                <td class="style1">
                    真实姓名：</td>
                <td>
                    <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtRealName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                 <tr>
                <td class="style1" >
                    性别：</td>
                <td>
                    <asp:DropDownList ID="ddlSex" runat="server">
                        <asp:ListItem Selected="True" Value="false">男</asp:ListItem>
                        <asp:ListItem Value="true">女</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    密码：</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
                            <tr>
                <td class="style1">
                    确认密码：</td>
                <td>
                    <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtPassword" ControlToValidate="txtConfirmPwd" 
                        ErrorMessage="密码不一致"></asp:CompareValidator>
                </td>
            </tr>
            <tr><td class="style1">所属专业：</td>
            <td>
                <asp:DropDownList ID="ddlMajors" runat="server" 
                  DataTextField="MajorName" 
                    DataValueField="MajorID">
                    <asp:ListItem Value="0">请选择专业</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Label ID="lblInDept" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            
            <tr><td  class="style1">专业负责人：</td><td>
                <asp:CheckBox ID="chkManager" runat="server" />
                </td></tr>
                        <tr>
                <td class="style1">
                    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                        Text="确认修改" />
                            </td>
                <td>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                            </td>
            </tr>
        </table>
</asp:Panel>


    <br />


<asp:Panel ID="panelImport" runat="server" Visible="False">
        
        <br />
        
        <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;
        <asp:Button ID="Button1" runat="server" Text="批量导入" onclick="Button1_Click" />
&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/SampleFiles/Teachers.xls">参考格式</asp:HyperLink>
    
        <br /></asp:Panel>
        <asp:ObjectDataSource ID="objDepartments" runat="server" 
            SelectMethod="GetDepartments" TypeName="SDPTExam.BLL.BaseData" 
            DeleteMethod="DeleteDepartment">
            <DeleteParameters>
                <asp:Parameter Name="departmentID" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
