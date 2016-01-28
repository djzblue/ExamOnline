<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfileManagement.aspx.cs" Inherits="SDPTExam.Web.UI.Students.ProfileManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">

table{border:1px solid #3399FF;border-width:1px 0 0 1px;margin:2px 0 2px 0;text-align:center;border-collapse:collapse;}
 td,th{border:1px solid #3399FF;border-width:0 1px 1px 0;margin:2px 0 2px 0;text-align:left; height: 17px;}
th{text-align:center;font-weight:600;font-size:12px;background-color:#F4F4F4;}

        .style6
        {
            height: 26px;
            width: 221px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <br />个人档案<br />
     <asp:Panel ID="panelCurrentProile" runat="server">
        <br />
           <table style="width:60%;">
            <tr>
                <td style="width:27%">
                    真实姓名：</td>
                <td  style="width:47%">
                    <asp:Label ID="lblRealName" runat="server"></asp:Label>
                </td>
                <td rowspan="7">
                   &nbsp;&nbsp;<asp:Image ID="imgPhoto" runat="server" ToolTip="相片" Width="120px" 
                        ImageUrl="~/images/Null.gif" />
                </td>
            </tr>            
               <tr>
                   <td >
                       登录名：</td>
                   <td >
                       <asp:Label ID="lblLoginName" runat="server"></asp:Label>
                   </td>
               </tr>
   
            <tr>
                <td >
                    学号：</td>
                <td >
                    <asp:Label ID="lblStuNum" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td >
                    院系：</td>
                <td >
                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                            </td>
            </tr>
                        <tr>
                <td >
                    专业：</td>
                <td >
                    <asp:Label ID="lblMajor" runat="server"></asp:Label>
                            </td>
            </tr>
                        <tr>
                <td >
                    年级：</td>
                <td >
                    <asp:Label ID="lblGrade" runat="server"></asp:Label>
                            </td>
            </tr>
                        <tr>
                <td  >
                    班次：</td>
                <td>
                    <asp:Label ID="lblClass" runat="server"></asp:Label>
                            </td>
            </tr>
                        <tr>
                <td  >
                    家庭地址：</td>
                <td  colspan="2">
                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                            </td>
            </tr>
                        <tr>
                <td >
                    家庭电话：</td>
                <td  colspan="2">
                    <asp:Label ID="lblHomePhone" runat="server"></asp:Label>
                            </td>
                    
            </tr>
                       
                        <tr>
                <td >
                    手机（小灵通）：</td>
                <td  colspan="2">
                    <asp:Label ID="lblCellPhone" runat="server"></asp:Label>
                            </td>
            </tr>
            <tr>
                <td >
                    QQ：</td>
                <td  colspan="2">
                    <asp:Label ID="lblQQ" runat="server"></asp:Label>
                </td>
            </tr>
                        <tr>
                <td >
                    Email：</td>
                <td  colspan="2">
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
            </tr>
            <tr>
                <td >
                    个人简介：</td>
                <td  colspan="2">
                    <asp:Label ID="lblPersonalDesc" runat="server"></asp:Label>
                            </td>
            </tr>
        </table>
        </asp:Panel>
           <br />
           </div>
    </form>
</body>
</html>
