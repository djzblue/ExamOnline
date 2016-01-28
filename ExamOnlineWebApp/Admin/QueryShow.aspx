<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryShow.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.QueryShow" %>

<%@ Register src="../Controls/DeptMajorClassDropdownList.ascx" tagname="DeptMajorClassDropdownList" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 219px;
        }
        .style2
        {
            width: 172px;
        }
        .style3
        {
            width: 145px;
        }
    </style>
    
       
</head>
<body>
    <form id="form1" runat="server">
    <uc1:DeptMajorClassDropdownList ID="DeptMajorClassDropdownList1"  OnButtonClick="btnShow_Click"
        runat="server" Visible="False"/>
    <br />
        <pe:ExtendedGridView ID="grvResultList" runat="server" 
        AutoGenerateColumns="False" AllowPaging="True" 
        onpageindexchanging="grvResultList_PageIndexChanging" 
        CheckBoxFieldHeaderWidth="3%" IsHoldState="True" SerialText="" 
        >
            <Columns>
                <asp:TemplateField HeaderText="班级">
                       <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#GetClassNameByID((int)Eval("ClassID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AvgMark" HeaderText="平均分" />
                <asp:TemplateField HeaderText="及格率">
    
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# Eval("Jigelv")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="优良率">
                              <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Youlianglv") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            

                <asp:BoundField HeaderText="应考人数" DataField="ShouldExamNum" />
                <asp:BoundField HeaderText="实考人数" DataField="ActualExamNum" />
            

                <asp:TemplateField HeaderText="导出">
                    <ItemTemplate>
                        <asp:Button ID="btnExport" runat="server" Text="导出班级成绩" />
                    </ItemTemplate>
                </asp:TemplateField>
            

            </Columns>
            <RowStyle HorizontalAlign="Center" />
    </pe:ExtendedGridView>
    <div>
        <br />
        <asp:Panel ID="Panel1" runat="server" Visible="False">
       
        <table style="width:60%;">
            <tr>
                <td class="style1">
                    班级名称</td>
                <td class="style2">
                    及格率</td>
                <td class="style3">
                    优良率</td>
            </tr>
            <tr>
                <td class="style1">
                    所有学生</td>
                <td class="style2">
                    <asp:Label ID="lblPass" runat="server"></asp:Label>
                    <asp:Label ID="lblAvg" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="lblExcellent" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
        </table> </asp:Panel>
    </div>
        <br />
    </form>
</body>
</html>
