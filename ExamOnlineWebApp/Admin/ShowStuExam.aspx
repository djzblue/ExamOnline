<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowStuExam.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.ShowStuExam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">

*{padding:0;margin:0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
     
                    <a style="font-family: 楷体_GB2312; font-size: 15px;font-weight: bold;">
                        一.单选（每题<asp:Label ID="lblSingleEachMark" runat="server"></asp:Label>分）
                    </a>
                    <br />           
                    <asp:Repeater runat="server" ID="singleRep" 
            onitemdatabound="singleRep_ItemDataBound">
                        <ItemTemplate>
                           <br /> <a>
                               &nbsp; <%# singeCount++ %>
                                .<%# Eval("title") %>
                                <asp:HiddenField runat="server" Value='<%# Eval("ID") %>' ID="titleId" />
                            </a> <br />            
                            
                            <asp:RadioButtonList ID="rbListChoiceItem" runat="server" DataTextField="Title" DataValueField="ID">
                            </asp:RadioButtonList>
                                                       
                          
               <asp:Repeater ID="singItemRep" runat="server" >
               <ItemTemplate>
             <asp:RadioButton ID="RadioButton1" runat="server"  GroupName="single" /> <%# Eval("Seq")%>. <%# Eval("title") %><br />
               </ItemTemplate>
        </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater> 
        
                    <a style="font-family: 楷体_GB2312; font-size: 15px;font-weight: bold;">
                    <br />
                        二.多项选择题（每题<asp:Label ID="lblMutilEachMark" runat="server"></asp:Label>分）
                    </a>
                    <br />      
                    <asp:Repeater runat="server" ID="mutilRep" 
            onitemdatabound="mutilRep_ItemDataBound">
                        <ItemTemplate>
                           
                                &nbsp;<%# singeCount++ %>
                                .<%# Eval("title") %>
                                <asp:HiddenField runat="server" Value='<%# Eval("ID") %>' ID="titleId0" />
                            
                        <asp:CheckBoxList ID="chkListChoiceItem" runat="server"  DataTextField="Title" DataValueField="ID">
                        </asp:CheckBoxList>
<br />
                        </ItemTemplate>
                    </asp:Repeater>
                    <a style="font-family: 楷体_GB2312; font-size: 15px;font-weight: bold;">
                    <br />
                        三.判断题（每题<asp:Label ID="lblJudgeEachMark" runat="server"></asp:Label>分）
                    </a>
                    <br />
                    <table>
                    <asp:Repeater runat="server" ID="judgeRep" 
            onitemdatabound="judgeRep_ItemDataBound">
                        <ItemTemplate>
                            <tr><td>
                                &nbsp;<%# singeCount++ %>
                                .<%# Eval("title") %>
                                <asp:HiddenField runat="server" Value='<%# Eval("ID") %>' ID="titleId1" /></td>                           
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rbListJudges" runat="server" 
        RepeatDirection="Horizontal" RepeatLayout="Flow" DataValueField="setTrue">
        <asp:ListItem Value="1">对</asp:ListItem>
        <asp:ListItem Value="0">错</asp:ListItem>
    </asp:RadioButtonList></td>
           </tr>
                          
                        </ItemTemplate>
                    </asp:Repeater></table>
                    <a style="font-family: 楷体_GB2312; font-size: 15px;font-weight: bold;">
                    <br />
    </div> 
    

    </form>
</body>
</html>
