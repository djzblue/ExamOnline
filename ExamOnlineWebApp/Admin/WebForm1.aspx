<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.WebForm1" %>

<%@ Register src="../Controls/FileUploader.ascx" tagname="FileUploader" tagprefix="uc1" %>
<%@ Register src="../Controls/DeptMajorClassDropdownList.ascx" tagname="DeptMajorClassDropdownList" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:DeptMajorClassDropdownList ID="DeptMajorClassDropdownList1"  OnButtonClick="btnShow_Click"
            runat="server" />
    </div>
    </form>
</body>
</html>
