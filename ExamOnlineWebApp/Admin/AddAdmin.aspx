<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAdmin.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.AddAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加管理用户</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
        <br />
        <asp:Panel ID="panelAdminUser" runat="server">
        
        <asp:Button ID="Button2" runat="server" Text="查看现有管理员" />
        <br />
        <br />
        <pe:ExtendedGridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="objAdmins" DataKeyNames="AdminUserID" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" Width="392px" 
            >
            <Columns>
                <asp:BoundField DataField="LoginName" HeaderText="登陆名" 
                    SortExpression="LoginName" />
                <asp:CheckBoxField DataField="IsSuperAdmin" HeaderText="超级管理员" 
                    SortExpression="IsSuperAdmin" />
                <asp:TemplateField HeaderText="所在部门" SortExpression="DepartmentID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DepartmentID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# GetDepartmentNameByID((int)Eval("DepartmentID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="编辑" SelectText="编辑" ShowSelectButton="True" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" onclientclick="return confirm(&quot;确定删除该管理员吗？&quot;)" CausesValidation="False" 
                            CommandName="Delete" Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
        <asp:ObjectDataSource ID="objAdmins" runat="server" 
            SelectMethod="GetAdminUsers" TypeName="SDPTExam.BLL.AdminUser" 
            DeleteMethod="DeleteAdminUser">
            <DeleteParameters>
                <asp:Parameter Name="adminUserID" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        <br />
        <asp:Button ID="btnAddNewAdmin" runat="server" Text="新增管理员" 
            onclick="btnAddNewAdmin_Click" />
    
        <br />
    
        <br />
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="481px" 
            AutoGenerateRows="False" DataSourceID="objSingleAdmin" 
            oniteminserting="DetailsView1_ItemInserting" 
            onmodechanged="DetailsView1_ModeChanged" 
            onitemupdating="DetailsView1_ItemUpdating" 
            oniteminserted="DetailsView1_ItemInserted">
            <Fields>
                <asp:TemplateField HeaderText="用户名" SortExpression="LoginName">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rv1" runat="server" 
                            ControlToValidate="txtLoginName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="密码" SortExpression="Password">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPassword" runat="server" Height="16px" 
                            Text='<%# Bind("Password") %>' TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv2" runat="server" 
                            ControlToValidate="txtPassword" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="权限" SortExpression="IsSuperAdmin">
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkSuperAdmin" runat="server" 
                            Checked='<%# Bind("IsSuperAdmin") %>' Text="是否超级管理员" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" 
                            Checked='<%# Bind("IsSuperAdmin") %>' Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="所在部门" SortExpression="DepartmentID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlDepartment" runat="server" 
                            DataSourceID="objDepartments" DataTextField="DepartmentName" 
                            DataValueField="DepartmentID" SelectedValue='<%# Bind("DepartmentID") %>'>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objDepartments" runat="server" 
                            SelectMethod="GetDepartments" TypeName="SDPTExam.BLL.BaseData">
                        </asp:ObjectDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# GetDepartmentNameByID((int)Eval("DepartmentID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ShowInsertButton="True" />
            </Fields>
        </asp:DetailsView>
        <br />
        <asp:ObjectDataSource ID="objSingleAdmin" runat="server" 
            DataObjectTypeName="SDPTExam.DAL.Model.AdminUserInfo" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetAdminUserByID" 
            TypeName="SDPTExam.BLL.AdminUser" UpdateMethod="UpdateAdminUser" 
            InsertMethod="InsertAdminUser">
            <SelectParameters>
                <asp:ControlParameter ControlID="GridView1" Name="aID" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
    </asp:Panel>
    </div>
    </form>
</body>
</html>
