<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeDetails.aspx.cs" Inherits="SDPTExam.Web.UI.NoticeDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="panelNoticeDetails" runat="server">
        
          <div id="noticeTitle" ><asp:Label ID="lblTitle" runat="server"></asp:Label><br />
           <div style="font-size:12px">发布：<asp:Label ID="lblAddedBy" runat="server" Text="管理员" ForeColor="HotTrack"></asp:Label>
    &nbsp;&nbsp;
               添加时间：<asp:Label ID="lblAddedDate" runat="server" ForeColor="HotTrack"></asp:Label>
    &nbsp;&nbsp;&nbsp; 类别：<asp:Label ID="lblType" runat="server"></asp:Label>
    </div>
    </div>
  
             <div id="noticeBody">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblBody" runat="server" ></asp:Label>
        <br /><br />
        附件下载：<asp:Label ID="lblNoAttachment" runat="server" Visible="False">没有附件！</asp:Label>
        <asp:LinkButton ID="lbtnAttachment" runat="server" onclick="lbtnAttachment_Click" 
                     Visible="False">下载</asp:LinkButton>
        <br />  <asp:Label ID="lblFeekBack" runat="server" Font-Size="Large" ForeColor="Red" Visible="False"></asp:Label>
        </div>
    </asp:Panel>
    
    </div>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
    </form>
</body>
</html>
