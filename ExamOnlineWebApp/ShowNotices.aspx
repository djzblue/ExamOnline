<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowNotices.aspx.cs" Inherits="SDPTExam.Web.UI.ShowNotices" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>通知列表</title>
     <script src="../js/OpenNewWindow.js" language="javascript" type="text/javascript" ></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <pe:ExtendedGridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="NoticeID" DataSourceID="ObjectDataSource1" Width="581px" AllowPaging="True" 
            PageSize="7" >
            <Columns>
                <asp:TemplateField HeaderText="标题" SortExpression="Title">
                    <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server"                            
                               NavigateUrl='<%# "javascript:void openwindow(" + "\"NoticeDetails.aspx?NoticeID=" + Eval("NoticeID").ToString() + "\",\"通知详细信息\",2)"%>' Text='<%# Eval("Title") %>'></asp:HyperLink>
                      
                    </ItemTemplate>
                    <ItemStyle Width="40%" />
                </asp:TemplateField>
                <asp:BoundField DataField="AddedTime" HeaderText="发布时间" 
                    SortExpression="AddedTime" />
                <asp:TemplateField HeaderText="类别" SortExpression="Type">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# GetTypeName((int)Eval("Type")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="查看" ShowHeader="False">
                    <ItemTemplate>                        
                            <asp:HyperLink ID="HyperLink1" runat="server" 
                           
                               NavigateUrl='<%# "javascript:void openwindow(" + "\"NoticeDetails.aspx?NoticeID=" + Eval("NoticeID").ToString() + "\",\"通知详细信息\",2)"%>' Text="详细内容"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
        <br />
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetNotices" 
            TypeName="SDPTExam.BLL.Message">
            <SelectParameters>
                <asp:SessionParameter Name="deptID" SessionField="DepartmentID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br /><br />
     <br />
                        
  </div>
        
        <br />
        <br />
    
  
    </form>
</body>
</html>
