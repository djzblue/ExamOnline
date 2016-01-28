<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectChoiceManagement.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.SelectChoiceManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse"
                width="60%" frame="below">
                <tr>
                    <td bgcolor="#eeeeee" colspan="2">
                        <div class="title" align="left">
                            <h4 style="font-family: 楷体_GB2312">
                                >>选择题</h4>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align: right;" width="80px">
                        &nbsp;</td>
                    <td>
                        &nbsp;<div align="left">
                            <asp:DropDownList ID="ddlCourse" runat="server" Font-Size="9pt" Width="88px" 
                                Visible="False">
                            </asp:DropDownList>
                            <asp:Button ID="btnShowAddNew" runat="server" Text="添加新题" 
                                onclick="btnShowAddNew_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnShowList" runat="server" Text="题目列表" 
                                onclick="btnShowList_Click" />
                        </div>
                    </td>
                </tr>
                </table>
                
        <asp:Panel ID="panelAddnew" runat="server" Visible="False">
                
                <table cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse"
                width="60%" frame="below">
                <tr>
                    <td bgcolor="#eeeeee" style="text-align: right;" width="80px">
                        题目：
                    </td>
                    <td>
                        &nbsp;<div align="left">
                            <asp:TextBox ID="txtTitle" runat="server" Width="80%" TextMode="MultiLine" Height="70px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle"
                                ErrorMessage="不能为空！" ValidationGroup="choice"></asp:RequiredFieldValidator></div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align: right;">
                        选项A：
                    </td>
                    <td>
                        &nbsp;<div align="left">
                            <asp:TextBox ID="txtAnswerA" runat="server" Width="80%" TextMode="MultiLine" Height="70px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnswerA"
                                ErrorMessage="不能为空！" ValidationGroup="choice"></asp:RequiredFieldValidator></div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align: right;">
                        选项B：
                    </td>
                    <td>
                        &nbsp;<div align="left">
                            <asp:TextBox ID="txtAnswerB" runat="server" Width="80%" TextMode="MultiLine" Height="70px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAnswerB"
                                ErrorMessage="不能为空！" ValidationGroup="choice"></asp:RequiredFieldValidator></div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align: right;">
                        选项C：
                    </td>
                    <td>
                        &nbsp;<div align="left">
                            <asp:TextBox ID="txtAnswerC" runat="server" Width="80%" TextMode="MultiLine" Height="70px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAnswerC"
                                ErrorMessage="不能为空！" ValidationGroup="choice"></asp:RequiredFieldValidator></div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align: right;">
                        选项D：
                    </td>
                    <td>
                        &nbsp;<div align="left">
                            <asp:TextBox ID="txtAnswerD" runat="server" Width="80%" TextMode="MultiLine" Height="70px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAnswerD"
                                ErrorMessage="不能为空！" ValidationGroup="choice"></asp:RequiredFieldValidator></div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align: right;">
                        正确答案：
                    </td>
                    <td>
                        <asp:CheckBox ID="chkA" runat="server" Text="A" Visible="False" />
                        <asp:CheckBox ID="chkB" runat="server" Text="B" Visible="False"  />
                        <asp:CheckBox ID="chkC" runat="server" Text="C" Visible="False" />
                        <asp:CheckBox ID="chkD" runat="server" Text="D" Visible="False"  />
                        <asp:CheckBoxList ID="chkListAnswer" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>B</asp:ListItem>
                            <asp:ListItem>C</asp:ListItem>
                            <asp:ListItem>D</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label><br />
                        <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="Images/Save.GIF" 
                            OnClick="imgBtnSave_Click" style="height: 20px" ValidationGroup="choice" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                </tr>
            </table>
            </asp:Panel>
    </div>
    <pe:ExtendedGridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="SelectChoiceID" 
        DataSourceID="SqlDataSource1"   Width="40%">    
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="题目" 
                SortExpression="Title" />
            <asp:CheckBoxField DataField="IsSingleSelect" HeaderText="是否单项" 
                SortExpression="IsSingleSelect" />
                     <asp:TemplateField HeaderText="管理">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                    Text="修改" CommandArgument='<%# Eval("SelectChoiceID") %>' 
                                    onclick="LinkButton1_Click"></asp:LinkButton>
                         </ItemTemplate>
                        </asp:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnStr %>" 
        SelectCommand="SELECT * FROM [SelectChoiceInfo]">
    </asp:SqlDataSource>
    <asp:HiddenField ID="hideChoiceID" runat="server" />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="全部删除" onclientclick="return confirm(&quot;将删除所有选择题的内容，请慎重考虑！！！确定删除吗？&quot;)"  />
    </form>
</body>
</html>
