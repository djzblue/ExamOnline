<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeAdminPwd.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.ChangeAdminPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .style1
        {
            width: 164px;
        }
    </style>
    
        <link href="../tableStyle.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
        <table>
            <tr>
                <td class="style1">
                    旧密码：</td>
                <td>
                    <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password" Width="248px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtOldPwd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    新密码：</td>
                <td>
                    <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" Width="248px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtNewPwd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    确认新密码：</td>
                <td>
                    <asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" Width="248px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtConfirm" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtNewPwd" ControlToValidate="txtConfirm" 
                        ErrorMessage="确认密码不一致"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnModify" runat="server" onclick="btnModify_Click" 
                        Text="确定修改" />
                </td>
            </tr>
        </table>
    
    
    </div>
    <asp:Label ID="lblFeedback" runat="server" ForeColor="#FF3300"></asp:Label>
    </form>
</body>
</html>
