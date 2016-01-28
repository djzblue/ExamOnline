<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="SDPTExam.Web.UI.Teachers.EditProfile" %>

<%@ Register src="~/Controls/FileUploader.ascx" tagname="FileUploader" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
       <style type="text/css">
 table{width:100%;border:1px solid #3399FF;border-width:1px 0 0 1px;margin:2px 0 2px 0;text-align:center;border-collapse:collapse;}
 td,th{border:1px solid #3399FF;border-width:0 1px 1px 0;margin:2px 0 2px 0;text-align:left;}
th{text-align:center;font-weight:600;font-size:12px;background-color:#F4F4F4;}
        .style1
        {
            text-align: right;
            width: 176px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
          <br />
        提示：必须先填写完整资料后，才能正常使用系统。<br />
         <br />
         <asp:Panel ID="panelModify" runat="server">
                <table style="width:80%;">            
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
                        <td class="style1">
                            头像：</td>
                        <td>
                            <asp:HyperLink ID="hlinkImage" runat="server" Target="_blank">
                                <asp:Image ID="imgPhoto" runat="server" Width="100" Height="100" ImageUrl="~/images/Null.gif"/></asp:HyperLink>
                             <uc1:FileUploader ID="FileUploader1" runat="server"  
                        OnSubmitFinished="SubmitFinished" FileType="img" />           
                        </td>
                    </tr>
            <tr>
                <td class="style1">
                    密码：</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtPassword" ErrorMessage="*" ValidationGroup="ss"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblPwd" runat="server" Visible="False"></asp:Label>
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
                        <tr>
                <td class="style1">
                    职称：</td>
                <td>
                    <asp:TextBox ID="txtProfessionTitle" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtProfessionTitle" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    职务：</td>
                <td>
                    <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtPosition" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    学历学位：</td>
                <td>
                    <asp:TextBox ID="txtDegree" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtDegree" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    毕业院校：</td>
                <td>
                    <asp:TextBox ID="txtGraduatefrom" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="txtGraduatefrom" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    专业：</td>
                <td>
                    <asp:TextBox ID="txtMajor" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                        ControlToValidate="txtMajor" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                    
            </tr>
                        <tr>
                <td class="style1">
                    研究领域</td>
                <td>
                    <asp:TextBox ID="txtResearchField" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="txtResearchField" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    是否在校：</td>
                <td>
                    <asp:CheckBox ID="chkInSchool" runat="server" Text="在校" />
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    手机（小灵通）：</td>
                <td>
                    <asp:TextBox ID="txtCellNum" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="txtCellNum" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    QQ：</td>
                <td>
                    <asp:TextBox ID="txtQQ" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                        ControlToValidate="txtQQ" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    Email：</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    办公电话：</td>
                <td>
                    <asp:TextBox ID="txtOfficePhone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                        ControlToValidate="txtOfficePhone" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    个人简介：</td>
                <td>
                    <asp:TextBox ID="txtProfileDesc" runat="server" Height="71px" 
                        TextMode="MultiLine" Width="186px"></asp:TextBox>
                            </td>
            </tr>
                
                    <tr>
                        <td class="style1">
                            <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                                Text="确认修改" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
        </table>
</asp:Panel>
    </div>
    
    </form>
</body>
</html>
