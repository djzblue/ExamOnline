<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaseDataManagement.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.BaseDataManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>学校部门，专业信息</title>
    <style type="text/css">
        .style1
        {
            vertical-align:top;
            width: 126px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button4" runat="server" Text="部门管理" onclick="Button4_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button5" runat="server" Text="专业管理" onclick="Button5_Click" />
        
    &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnClass" runat="server" Text="班级管理" />
        
    <asp:Panel ID="panelDept" runat="server" Visible="False">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        部门列表：<pe:ExtendedGridView ID="grvDepartments" runat="server" 
            AutoGenerateColumns="False" DataSourceID="objDepartments" Width="477px" DataKeyNames="DepartmentID" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onrowdeleted="grvDepartments_RowDeleted" onrowdeleting="grvDepartments_RowDeleting"            
            >
            <Columns>
                <asp:BoundField DataField="DepartmentID" HeaderText="部门ID" />
                <asp:BoundField DataField="DepartmentName" HeaderText="部门名称" 
                    SortExpression="DepartmentName" />
                <asp:BoundField DataField="Description" HeaderText="部门简介" 
                    SortExpression="Description" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Select" Text="修改"></asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton2" runat="server"  onclientclick="return confirm(&quot;确定删除该部门吗？这将删除所有与该部门相关的信息。&quot;)" CommandName="Delete">删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
    
        
            <asp:Button ID="btnAddD" runat="server" onclick="Button2_Click" 
            Text="添加部门" />
        <br />
    
        
            <table style="width:100%;">
                <tr>
                    <td class="style1">
                        部门名称：</td>
                    <td>
                        <asp:TextBox ID="txtDeptName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        部门简介：</td>
                    <td>
                        <asp:TextBox ID="txtDeptDesc" runat="server" TextMode="MultiLine" Height="72px" 
                            Width="219px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        是否教学部门：</td>
                    <td>
                        <asp:CheckBox ID="chkIsTeaching" runat="server" Checked="True" Text="是" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Button ID="btnAddDept" runat="server" onclick="btnAddDept_Click" 
                            Text="添加" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
        </table>   
        </asp:Panel>
        
        <asp:Button ID="Button6" runat="server" onclick="Button6_Click1" Text="补计分" />
        
      <asp:Panel ID="panelMajor" runat="server" Visible="False">
        
          按部门查询：<asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="objDepartments" DataTextField="DepartmentName" 
                DataValueField="DepartmentID">
            </asp:DropDownList>
            <asp:Button ID="Button3" runat="server" Text="查询" onclick="Button3_Click" />
            &nbsp;<br />
            <br />
        <pe:ExtendedGridView ID="grvMajors" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="MajorID" DataSourceID="objMajors" 
            onselectedindexchanged="GridView2_SelectedIndexChanged" Width="599px" 
              onrowdeleting="grvMajors_RowDeleting">
            <Columns>
                <asp:BoundField DataField="MajorID" HeaderText="MajorID" 
                    SortExpression="MajorID" />
                <asp:BoundField DataField="MajorName" HeaderText="专业名称" 
                    SortExpression="MajorName" />
                <asp:BoundField DataField="Description" HeaderText="专业简介" 
                    SortExpression="Description" />
                <asp:CommandField SelectText="修改" ShowSelectButton="True" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Delete" 
                            onclientclick="return confirm(&quot;确定删除该专业吗？这将删除所有与该专业相关的信息。&quot;)" Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
    
        
            <br />
       
        <br />
      
          
            <asp:Button ID="btnAddNewMajor" runat="server" onclick="btnAddMajor_Click" 
                style="height: 26px; width: 78px" Text="添加专业" />
            <br />
            <table>
                <tr>
                    <td>
                        专业名称：</td>
                    <td>
                        <asp:TextBox ID="txtMajorName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        专业简介：</td>
                    <td>
                        <asp:TextBox ID="txtMajorDesc" runat="server" TextMode="MultiLine" 
                            Height="74px" Width="228px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        所属系别：</td>
                    <td>
                        <asp:DropDownList ID="ddlDept" runat="server" DataSourceID="objDepartments" 
                            DataTextField="DepartmentName" DataValueField="DepartmentID" 
                           >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAddM" runat="server" onclick="btnAddM_Click" Text="添加" />
                    </td>
                    
                </tr>
            </table>
          <br />
          <asp:Panel ID="panelImport" runat="server" >
              <br />
              <asp:FileUpload ID="FileUpload1" runat="server" />
              &nbsp;
              <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="批量导入专业" />
              &nbsp;
              <asp:HyperLink ID="HyperLink1" runat="server" 
                  NavigateUrl="~/SampleFiles/Teachers.xls">参考格式</asp:HyperLink>
              <br />
                            <br />
              <asp:FileUpload ID="FileUpload2" runat="server" />
              &nbsp;
              <asp:Button ID="Button2" runat="server" onclick="Button2_Click1" 
                  Text="批量导入班级" />
              &nbsp;
              <asp:HyperLink ID="HyperLink2" runat="server" 
                  NavigateUrl="~/SampleFiles/Teachers.xls">参考格式</asp:HyperLink>
              <br />
              <br />
          </asp:Panel>
        <br />
      </asp:Panel>
    
        <br />
        <asp:ObjectDataSource ID="objMajors" runat="server" 
            SelectMethod="GetMajorsByDepartmentID" TypeName="SDPTExam.BLL.BaseData" 
            DeleteMethod="DeleteMajor">
            <DeleteParameters>
                <asp:Parameter Name="majorID" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="deptID" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objSingleMajor" runat="server" 
            DataObjectTypeName="SDPTExam.DAL.Model.MajorInfo" 
            InsertMethod="InsertMajor" SelectMethod="GetMajorByID" 
            TypeName="SDPTExam.BLL.BaseData" UpdateMethod="UpdateMajor" OldValuesParameterFormatString="original_{0}" 
           >
            <SelectParameters>
                <asp:ControlParameter ControlID="grvMajors" Name="mID" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objDepartments" runat="server" 
            SelectMethod="GetDepartments" TypeName="SDPTExam.BLL.BaseData" 
            DeleteMethod="DeleteDepartment">
            <DeleteParameters>
                <asp:Parameter Name="departmentID" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objSingleDepartment" runat="server" 
            DataObjectTypeName="SDPTExam.DAL.Model.DepartmentInfo" InsertMethod="InsertDepartment" 
            SelectMethod="GetDepartmentByID" TypeName="SDPTExam.BLL.BaseData" 
            UpdateMethod="UpdateDepartment" 
            OldValuesParameterFormatString="original_{0}" 
            >
            <SelectParameters>
                <asp:ControlParameter ControlID="grvDepartments" Name="dID" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
