<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="SDPTExam.Web.UI.Students.EditProfile" %>

<%@ Register src="~/Controls/FileUploader.ascx" tagname="FileUploader" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
    table{border:1px solid #3399FF;border-width:1px 0 0 1px;margin:2px 0 2px 0;text-align:center;border-collapse:collapse;}
 td,th{border:1px solid #3399FF;border-width:0 1px 1px 0;margin:2px 0 2px 0;text-align:left;}
th{text-align:center;font-weight:600;font-size:12px;background-color:#F4F4F4;}
        .style1
        {
            color: #00F;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        <br />
        
        <asp:Panel ID="panelModify" runat="server">
    
        <table style="width:87%;">
            <tr>
                <td >
                    登录名：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtLoginName" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >
                    真实姓名：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtRealName" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>                    
            <tr>
                <td >
                    学号：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtStuNum" runat="server" ReadOnly="True" 
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td >
                    院系：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtDepartment" runat="server" ReadOnly="True" 
                        Enabled="False"></asp:TextBox>
                            </td>
            </tr>
                        <tr>
                <td >
                    专业：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtMajor" runat="server" ReadOnly="True" 
                        Enabled="False"></asp:TextBox>
                            </td>
            </tr>
                        <tr>
                <td >
                    年级：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtGrade" runat="server" Enabled="False"></asp:TextBox>
                            </td>
            </tr>
                        <tr>
                <td >
                    班次：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtClass" runat="server" Enabled="False"></asp:TextBox>
                            </td>
            </tr>
                <tr>
                <td >
                    密码：</td>
                <td >
                   <asp:TextBox Width="200px"  ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    &nbsp;默认为123456，请修改！</td>
            </tr>
                <tr>
                <td >
                    确认密码：</td>
                <td >
                   <asp:TextBox Width="200px"  ID="txtConfirmPwd" runat="server" 
                        TextMode="Password"></asp:TextBox>
                    &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtPassword" ControlToValidate="txtConfirmPwd" 
                        ErrorMessage="密码不一致"></asp:CompareValidator>
                    </td>
            </tr>
                        <tr>
                            <td class="style1" colspan="2">
                               <br />
                                <br /> 以下信息非常重要,请如实填写,以保证有效。如有虚假,责任自负!
                            </td>
            </tr>
                        <tr>
                <td >
                    家庭地址：</td>
                <td>
                   <asp:TextBox Width="300px"  ID="txtAddress" runat="server"></asp:TextBox>
                            <span class="style1">*<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator5" runat="server" ErrorMessage="必须填写" 
                        ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                    </span></td>
            </tr>
                        <tr>
                <td >
                    家庭电话：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtHomePhone" runat="server"></asp:TextBox>
                            <span class="style1">*<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator4" runat="server" ErrorMessage="必须填写" 
                        ControlToValidate="txtHomePhone"></asp:RequiredFieldValidator>
                    &nbsp;<span class="style1"><asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator3" runat="server" ErrorMessage="必须是数字！" 
                        ValidationExpression="\d+" ControlToValidate="txtHomePhone"></asp:RegularExpressionValidator>
                    </span>
                    </span></td>
                    
            </tr>
                       
                        <tr>
                <td >
                    手机（小灵通）：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtCellPhone" runat="server"></asp:TextBox>
                            <span class="style1">*<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator3" runat="server" ErrorMessage="必须填写" 
                        ControlToValidate="txtCellPhone"></asp:RequiredFieldValidator>
                    &nbsp;<span class="style1"><asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator4" runat="server" ErrorMessage="必须是数字！" 
                        ValidationExpression="\d+" ControlToValidate="txtCellPhone"></asp:RegularExpressionValidator>
                    </span>
                    </span></td>
            </tr>
            <tr>
                <td >
                    QQ：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtQQ" runat="server"></asp:TextBox>
                    <span class="style1">*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                        runat="server" ErrorMessage="必须填写" ControlToValidate="txtQQ"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                        runat="server" ErrorMessage="必须是数字！" ValidationExpression="\d+" 
                        ControlToValidate="txtQQ"></asp:RegularExpressionValidator>
                    </span></td>
            </tr>
                        <tr>
                <td >
                    Email：</td>
                <td>
                   <asp:TextBox Width="200px"  ID="txtEmail" runat="server"></asp:TextBox>
                            <span class="style1">*</span><asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="必须填写" 
                        ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ErrorMessage="格式不对！" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                            </td>
            </tr>
            <tr>
                <td >
                    个人简介：</td>
                <td>
                   <asp:TextBox Width="249px"  ID="txtPersonalDesc" runat="server" Height="104px" 
                        TextMode="MultiLine"></asp:TextBox>
                            （选填）</td>
            </tr>
                        <tr>
                <td >
                    头像：</td>
                <td>
                    <uc1:FileUploader ID="FileUploader1" runat="server"  
                        OnSubmitFinished="SubmitFinished" FileType="img" Enabled="False" />
                            如需要自行上传图片请联系管理员。</td>
            </tr>
                        <tr>
                <td  colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="确认修改" onclick="Button1_Click" />
                            </td>
                
            </tr>
        </table>
    </asp:Panel>
    </div>
    <p>
        &nbsp;</p>
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
