<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeptMajorClassDropdownList.ascx.cs" Inherits="SDPTExam.Web.UI.Controls.DeptMajorClassDropdownList" %>
<asp:DropDownList ID="ddlSelectDepts" runat="server" AppendDataBoundItems="True" 
                DataSourceID="objDepartments" DataTextField="DepartmentName" 
            DataValueField="DepartmentID" Visible="True" 
            onselectedindexchanged="ddlSelectDepts_SelectedIndexChanged" 
            AutoPostBack="True">
                <asp:ListItem Value="0">所有系</asp:ListItem>
            </asp:DropDownList>
         &nbsp;<asp:DropDownList ID="ddlSelectMajor" 
    runat="server" AppendDataBoundItems="True" 
              DataTextField="MajorName" 
    DataValueField="MajorID" AutoPostBack="True" 
    onselectedindexchanged="ddlSelectMajor_SelectedIndexChanged">
                <asp:ListItem Value="0">全部专业</asp:ListItem>
            </asp:DropDownList>
            
&nbsp;<asp:DropDownList ID="ddlClass" runat="server" AppendDataBoundItems="True" 
                 DataTextField="ClassName" 
    DataValueField="ClassID"     
    Visible="True" >
<asp:ListItem Value="0">全部班级</asp:ListItem>
</asp:DropDownList>
        <asp:ObjectDataSource ID="objDepartments" runat="server" 
            SelectMethod="GetDepartments" TypeName="SDPTExam.BLL.BaseData" 
            DeleteMethod="DeleteDepartment">
            <DeleteParameters>
                <asp:Parameter Name="departmentID" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        