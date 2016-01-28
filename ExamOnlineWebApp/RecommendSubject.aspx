<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecommendSubject.aspx.cs" Inherits="SDPTExam.Web.UI.RecommendSubject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="tableStyle.css" type="text/css" rel="Stylesheet" />
 <script src="js/OpenNewWindow.js" language="javascript" type="text/javascript" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblFeedback" runat="server" ForeColor="#FF3300"></asp:Label>
        <br />
        <asp:Button ID="btnMySubjects" runat="server" Text="我的试题列表" 
            CausesValidation="False" onclick="btnMySubjects_Click" />
&nbsp;
        <asp:Button ID="btnAddNewSubject" runat="server" Text="增加推荐试题" 
            CausesValidation="False" onclick="btnAddNewSubject_Click" />
        <pe:ExtendedGridView ID="grvSubjects" runat="server" 
            AutoGenerateColumns="False"  Visible="False" DataKeyNames="SubjectID" 
            onselectedindexchanged="grvSubjects_SelectedIndexChanged" 
            >
            <Columns>
                <asp:TemplateField HeaderText="试题名称" SortExpression="Title">
                    <ItemTemplate>
                                  <asp:HyperLink ID="HyperLink1" runat="server"  
                            NavigateUrl='<%# "javascript:void openwindow("+ "\"SubjectDetails.aspx?subjectID="+Eval("SubjectID").ToString()+"\",\"详细信息\",2)" %>' 
                            Text='<%# Eval("Title") %>'></asp:HyperLink>
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="试题简介" 
                    SortExpression="Description" />
                <asp:BoundField DataField="AuthorName" HeaderText="作者名称" 
                    SortExpression="AuthorName" />
                <asp:BoundField DataField="Reference" HeaderText="参考文献" 
                    SortExpression="Reference" />
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Visible='<%# Eval("IsFormal") %>'>已入库</asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="未入库" 
                            Visible='<%# !(bool)Eval("IsFormal") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                         <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False"  Visible='<%#IsStudent %>'
                            CommandName="Select" Enabled='<%# CheckSelectEnable((int)Eval("SubjectID")) %>' 
                            Text="拟选" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            </pe:ExtendedGridView>
        <br />
        <br />
    
        <asp:Panel ID="panelAddSubject" runat="server">
        
        <br />
        试题录入：
        <br />
         <table border="1" style="width:70%;">
   
        <tr>
            <td >
                试题标题：</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="301px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtTitle" ErrorMessage="试题标题不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td >
                试题答案：</td>
            <td>
                <asp:TextBox ID="txtBody" runat="server" Height="128px" TextMode="MultiLine" 
                    Width="387px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtBody" ErrorMessage="试题简介不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
     <tr>
            <td >
                参考文献：</td>
            <td>
                <asp:TextBox ID="txReference" runat="server" Height="128px" TextMode="MultiLine" 
                    Width="387px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txReference" ErrorMessage="参考文献不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
           <tr>
                <td >
                    所属科目：<br />
                </td>
                <td>            
                    <asp:DropDownList ID="ddlMajors" runat="server" 
                            DataSourceID="objMajors" DataTextField="MajorName" 
                            DataValueField="MajorID">
                        </asp:DropDownList>
                    &nbsp;</td>
            </tr>
             <tr>
                 <td>
                     选项：</td>
                 <td>
                     <asp:CheckBox ID="chkSelected" runat="server" Checked="True" Text="作为我的拟选试题" 
                         Visible="False" oncheckedchanged="chkSelected_CheckedChanged" />
                     <asp:CheckBox ID="chkToStudent" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chkToStudent_CheckedChanged" Text="推荐给学生" Visible="False" />
                     <asp:DropDownList ID="ddlStudents" runat="server" Visible="False" DataTextField="SName" DataValueField="StudentID">
                     </asp:DropDownList>
                     <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="学生列表为空！" 
                         Visible="False"></asp:Label>
                 </td>
             </tr>
        <tr>
            <td >
                <asp:Button ID="btnSend" runat="server" Text="确定" onclick="btnSend_Click" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="取消" CausesValidation="False" 
                    onclick="btnCancel_Click" />
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
        <br />
    <asp:ObjectDataSource ID="objMajors" runat="server" 
            SelectMethod="GetMajorsByDepartmentID" 
        TypeName="SDPTExam.BLL.BaseData" 
        OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="10003" Name="deptID" 
                    SessionField="DepartmentID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
