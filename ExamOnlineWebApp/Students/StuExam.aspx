<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StuExam.aspx.cs" Inherits="SDPTExam.Web.UI.Students.StuExam" %>

<%@ Register assembly="ExamManagement.Controls" namespace="ExamManagement.Controls.myControl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
<style type="text/css">
*{padding:0;margin:0;}

#timeDiv{
 position:fixed;
_position:absolute;
 z-index:1000;
 background:#eee;
 top:20px;
 left:600px;
 width:170px;
 height:30px;
    }

#content{background:#ccc;_height:100%;_overflow:auto; position:relative; padding-top:50px; }
    .style1
    {
        text-align: center;
    }
    .style2
    {
        color: #FF0000;
    }
    .style3
    {
        color: #3333CC;
    }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    
    <div style="margin-left:20px">
      
        

        <asp:Panel ID="panelStuInfo" runat="server" Visible="False" >
        <table border="1" cellpadding="0" cellspacing="0" style="vertical-align:middle">
            <tr>
                <td >
                    科目</td>      <td  class="style1">
                
                    学号</td>      
                   <td class="style1">
                
                       姓名</td>
                <td  class="style1">
                
                    班级</td>
            </tr>

            

            <tr>
                <td style="height:50px" class="style1">
                    <asp:Label ID="lblPaperName" runat="server" Text="考试科目"></asp:Label>  
           
                   
                </td>      <td  class="style1">
                
                    <asp:Label ID="lblStuNum" runat="server" Width="70px"></asp:Label>
                   
                   
                </td>      
                   <td class="style1">
                
                       <asp:Label ID="labUser" runat="server" Width="70px"></asp:Label>
                   
                   
                </td>
                                               
                                   <td  class="style1">
                
                    <asp:Label ID="lblClass" runat="server" Width="70px"></asp:Label>
                   
                   
                </td>
            </tr>

            

        </table></asp:Panel>
        <asp:Label ID="lblWelcome" runat="server" Visible="False"></asp:Label>
        <br />
        <br />
        &nbsp;
        <asp:Label ID="lblExamTitle" runat="server" Font-Bold="True" 
            Font-Size="X-Large" ForeColor="#0033CC"></asp:Label>
        <br />
        <br />
        <asp:Panel ID="panelMymark" runat="server" Visible="False">
            <div>
                            我的得分为： 
                            <asp:Label ID="lblMyMark" runat="server" ForeColor="Red"></asp:Label>
&nbsp;&nbsp;
                            <asp:Button ID="btnReExam" runat="server" 
                            CommandArgument='<%# Eval("RandomExamID") %>'
                            Text="重新考试" onclick="btnReExam_Click" 
                            onclientclick='return confirm("将删除你的考试信息，必须重新考试，否则没有成绩，确定吗？")' 
                                Enabled="False" />
    </div>

        </asp:Panel>
        <br />
        <asp:Button ID="btnStart" runat="server" onclick="btnStart_Click" 
            onclientclick="return confirm(&quot;即将开始计时，考试期间不可中断，是否继续？&quot;)" 
            Text="开始考试" />
        <br />
        <br class="style2" />
        <span class="style2">温馨提示：</span><span class="style3">点击“开始考试”后即开始计时，不要漏选，如留空白会提示不能交卷。如果考试过程中出现死机，窗口关闭等异常情况，请立即重新登录，

考试时间会相应减少。<br />
        交卷后在页面上方即时显示成绩，如不及格可点击"重新考试"。</span><br /><br />
   
        <asp:Button ID="btnSubmit" runat="server" Text="交卷"  
            onclick="btnSubmit_Click"  Enabled="False" 
            onclientclick="return confirm(&quot;将要提交试卷，确定吗？&quot;)" />
        <br />
        <span>
        <asp:Label 
            ID="lblTotalTimeHint" runat="server" ForeColor="Red"></asp:Label>
        <br />
        </span>
        <br />
     
         <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
        
               
        <div id="timeDiv"> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                 <ContentTemplate>
                    
                               
              剩余时间：<asp:Label ID="lblTime" 
                    runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" 
                    Enabled="False">
                </asp:Timer> 
                
   
            </ContentTemplate>
              </asp:UpdatePanel>
          </div>  
        <br />
        
        
        <asp:UpdatePanel ID="UpdatePanel2"  runat="server">
        <ContentTemplate> 
        <asp:Label ID="lblMessage" runat="server"></asp:Label>          
        <asp:Panel ID="panelWholePaper" runat="server" Visible="False">       
                    &nbsp;<asp:Button ID="btnSinglePanel" runat="server" Text="单选" 
                        onclick="btnSinglePanel_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnMutil" runat="server" Text="多选" onclick="btnMutil_Click" />
                    &nbsp; &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnJudge" runat="server" Text="判断" onclick="btnJudge_Click" />
                    <br /><asp:Panel ID="panelSingle" runat="server">
                        <br />
                    <a style="font-family: 楷体_GB2312; font-size: 15px;font-weight: bold;">
                        
                          一.单选（每题<asp:Label ID="lblSingleEachMark" runat="server"></asp:Label>分）
                    </a>
                    <asp:Repeater runat="server" ID="singleRep" 
            onitemdatabound="singleRep_ItemDataBound">
                        <ItemTemplate>
                           <br /> <a>
                               &nbsp; <%# singeCount++ %>
                                .<%# Eval("title") %><asp:HiddenField runat="server" Value='<%# Eval("ID") %>' ID="titleId" />
                            </a> <br />            
                            
                            <asp:RadioButtonList ID="rbListChoiceItem" runat="server" DataTextField="Title" DataValueField="ID">
                            </asp:RadioButtonList>
                                                       
                          
           <%--    <asp:Repeater ID="singItemRep" runat="server" >
               <ItemTemplate>
             <asp:RadioButton ID="RadioButton1" runat="server"  GroupName="single" /> <%# Eval("Seq")%>. <%# Eval("title") %><br />
               </ItemTemplate>
        </asp:Repeater>--%>
                        </ItemTemplate>
                    </asp:Repeater> 
        </asp:Panel>  
                   <asp:Panel ID="panelMutil" runat="server" Visible="False">  <a style="font-family: 楷体_GB2312; font-size: 15px;font-weight: bold;">
                          
                        <br />
                        二.多项选择题（每题<asp:Label ID="lblMutilEachMark" runat="server"></asp:Label>分）
                    </a>
                    <br />      
                    <asp:Repeater runat="server" ID="mutilRep" 
            onitemdatabound="mutilRep_ItemDataBound">
                        <ItemTemplate>
                           
                                &nbsp;<%# mutilCount++%>.<%# Eval("title") %><asp:HiddenField runat="server" Value='<%# Eval("ID") %>' ID="titleId" />
                            
                        <asp:CheckBoxList ID="chkListChoiceItem" runat="server"  DataTextField="Title" DataValueField="ID">
                        </asp:CheckBoxList>
<br />
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    </asp:Panel>  
                    
                            <asp:Panel ID="panelJudge" runat="server" Visible="False">
                       
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
                                &nbsp;<%# judgeCount++ %>.<%# Eval("title") %><asp:HiddenField runat="server" Value='<%# Eval("ID") %>' ID="titleId" /></td>                           
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rbListJudges" runat="server" 
        RepeatDirection="Horizontal" RepeatLayout="Flow" DataValueField="setTrue">
        <asp:ListItem Value="1">对</asp:ListItem>
        <asp:ListItem Value="0">错</asp:ListItem>
    </asp:RadioButtonList></td>
           </tr>
                          
                        </ItemTemplate>
                    </asp:Repeater></table>
                    
                    </asp:Panel> 
                    
                    <a style="font-family: 楷体_GB2312; font-size: 15px;font-weight: bold;">
                    <br />
                                 <asp:Button ID="btnSinglePanel0" runat="server" 
                        onclick="btnSinglePanel_Click" Text="单选" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnMutil0" runat="server" onclick="btnMutil_Click" Text="多选" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnJudge0" runat="server" onclick="btnJudge_Click" Text="判断" />
                    <br />
                    <br />
                    </asp:Panel>
         
        </ContentTemplate>
        </asp:UpdatePanel>
              <br/><hr/>
 <div style=" text-align:center">软件制作：电子与信息工程系计算机应用与软件教研室室<br />
 联系方式：24638831@qq.com 刁老师
 </div> 
    </div> 

    </form>
</body>
</html>
