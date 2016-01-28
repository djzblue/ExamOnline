<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckLog.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.CheckLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看日志</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <pe:ExtendedGridView ID="Egv" runat="server" AutoGenerateColumns="False" 
            CheckBoxFieldHeaderWidth="3%" DataSourceID="objectLogs" IsHoldState="True" 
            SerialText="">
            <Columns>
                <asp:BoundField DataField="LogID" HeaderText="LogID" SortExpression="LogID" />
                <asp:BoundField DataField="LogTitle" HeaderText="LogTitle" 
                    SortExpression="LogTitle" />
                <asp:BoundField DataField="LogType" HeaderText="LogType" 
                    SortExpression="LogType" />
                <asp:BoundField DataField="AddedTime" HeaderText="AddedTime" 
                    SortExpression="AddedTime" />
                <asp:BoundField DataField="LogContent" HeaderText="LogContent" 
                    SortExpression="LogContent" />
            </Columns>
        </pe:ExtendedGridView>
    
    </div>
    <asp:ObjectDataSource ID="objectLogs" runat="server" SelectMethod="GetAllLogs" 
        TypeName="SDPTExam.BLL.BaseData">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="deptID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
