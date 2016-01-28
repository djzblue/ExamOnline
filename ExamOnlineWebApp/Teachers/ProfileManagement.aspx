<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfileManagement.aspx.cs" Inherits="SDPTExam.Web.UI.Teachers.ProfileManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
   <style type="text/css">
 table{width:70%;border:1px solid #3399FF;border-width:1px 0 0 1px;margin:2px 0 2px 0;text-align:center;border-collapse:collapse;}
 td,th{border:1px solid #3399FF;border-width:0 1px 1px 0;margin:2px 0 2px 0;height:20px;text-align:left;}
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
    
        <br />
      
        <asp:Panel ID="panelCurrentProile" runat="server">
          <table>
            <tr>
                <td class="style1">
                    登录名：</td>
                <td>
                    <asp:Label ID="lblLoginName" runat="server"></asp:Label>
                </td>
                 <td rowspan="7">
                   &nbsp;&nbsp;<asp:Image ID="imgPhoto" runat="server" ToolTip="相片" Width="120px" 
                         ImageUrl="~/images/Null.gif" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    真实姓名：</td>
                <td>
                    <asp:Label ID="lblRealName" runat="server"></asp:Label>
                </td>
            </tr>           
            <tr>
                <td class="style1">
                    职称：</td>
                <td>
                    <asp:Label ID="lblProfessionTitle" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    职务：</td>
                <td>
                    <asp:Label ID="lblPosition" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    学历学位：</td>
                <td>
                    <asp:Label ID="lblDegree" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    毕业院校：</td>
                <td>
                    <asp:Label ID="lblGraduatefrom" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    专业：</td>
                <td>
                    <asp:Label ID="lblMajor" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    研究领域</td>
                <td colspan="2">
                    <asp:Label ID="lblResearchField" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    是否在校：</td>
                <td colspan="2">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="在校" />
                </td>
                    
            </tr>
                        <tr>
                <td class="style1">
                    手机（小灵通）：</td>
                <td colspan="2">
                    <asp:Label ID="lblCellNum" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    QQ：</td>
                <td colspan="2">
                    <asp:Label ID="lblQQ" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    Email：</td>
                <td colspan="2">
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    办公电话：</td>
                <td colspan="2">
                    <asp:Label ID="lblOfficePhone" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    个人简介：</td>
                <td colspan="2">
                    <asp:Label ID="lblProfileDesc" runat="server" 
                        TextMode="MultiLine" Width="186px"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    头像：</td>
                <td colspan="2">
                    <asp:Label ID="lblimg" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td class="style1">
                    &nbsp;</td>
                <td colspan="2">
                    <asp:Button ID="btnShowModify" runat="server" CausesValidation="False" 
                        Height="26px" onclick="btnShowModify_Click" Text="修改个人资料" />
                            </td>
            </tr>
        </table>
        </asp:Panel>
        <br />
       <br />
   
        <br />
    
    
    </div>
    </form>
</body>
</html>
