<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUploader.ascx.cs" Inherits="SDPTExam.Web.UI.Controls.FileUploader" %>
<asp:Label ID="lblTitle" runat="server"></asp:Label>
<asp:FileUpload ID="filUpload" runat="server" />&nbsp;
<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="上传" CausesValidation="false" /><br />
<asp:Label ID="lblFeedback" SkinID="FeedbackOK" runat="server" 
    Font-Size="Medium" ForeColor="#33CC33"></asp:Label>
