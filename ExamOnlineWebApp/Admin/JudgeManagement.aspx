<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JudgeManagement.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.JudgeManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse"
                width="60%">
                <tr><td bgcolor="#eeeeee" colspan="2">
           
                            <h4 style="font-family: 楷体_GB2312">
                                >>判断题</h4>
                      
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
                        </div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align: right;">
                        题目：
                    </td>
                    <td>
                        &nbsp;<div align="left">
                            <asp:TextBox ID="txtTitle" runat="server" Width="80%" TextMode="MultiLine" 
                                Height="97px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle"
                                ErrorMessage="不能为空！" ValidationGroup="judge"></asp:RequiredFieldValidator></div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align: right;">
                        答案
                    </td>
                    <td>
                        &nbsp;<div align="left">
                            <asp:RadioButtonList ID="rblAnswer" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="true">正确</asp:ListItem>
                                <asp:ListItem Value="false">错误</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label><br />
                        <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="Images/Save.GIF" 
                            OnClick="imgBtnSave_Click" ValidationGroup="judge" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                </tr>
            </table>
    </div>
    <pe:ExtendedGridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ObjectDataSource1" Width="40%" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="题目" 
                SortExpression="Title" />
            <asp:CheckBoxField DataField="RightAnswer" HeaderText="答案" 
                SortExpression="RightAnswer" />
                                     <asp:TemplateField HeaderText="管理">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                    Text="修改" CommandArgument='<%# Eval("JudgeID") %>' 
                                    onclick="LinkButton1_Click"></asp:LinkButton>
                         </ItemTemplate>
                        </asp:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllJudges" 
        TypeName="SDPTExam.BLL.Judge"></asp:ObjectDataSource>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="全部删除"  onclientclick="return confirm(&quot;将删除所有判断题的内容，请慎重考虑！！！确定删除吗？&quot;)"/>
    </form>
</body>
</html>
