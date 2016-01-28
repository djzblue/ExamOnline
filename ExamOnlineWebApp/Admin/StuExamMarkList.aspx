<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StuExamMarkList.aspx.cs" EnableEventValidation = "false" Inherits="SDPTExam.Web.UI.Admin.StuExamMarkList" %>

<%@ Register src="../Controls/DeptMajorClassDropdownList.ascx" tagname="DeptMajorClassDropdownList" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body onload="javascript:myRefresh();">
    <form id="form1" runat="server">
    <div>
        学生成绩统计  
        <br />
        <br />  
        系别：<uc1:DeptMajorClassDropdownList ID="DeptMajorClassDropdownList1" OnButtonClick="btnShow_Click"
            runat="server"  />
        <asp:DropDownList ID="ddlState" runat="server" Visible="False">
            <asp:ListItem Value="1">完成考试</asp:ListItem>
            <asp:ListItem Value="0">正在考试</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlPass" runat="server" Visible="False">
            <asp:ListItem>及格</asp:ListItem>
            <asp:ListItem>不及格</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnShowStudents" runat="server" Text="显示" 
            onclick="btnShowStudents_Click" CausesValidation="False" />
        <asp:Button ID="btnExport" runat="server" onclick="btnExport_Click" 
            Text="导出所有成绩" />
        <br /> <br />
        <pe:ExtendedGridView ID="grvStuMarkList" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="RandomExamID" 
            Width="80%" AllowPaging="True" 
            onpageindexchanging="grvStuMarkList_PageIndexChanging" 
            onsorting="grvStuMarkList_Sorting" AllowSorting="True" 
      >
            <Columns>
                <asp:TemplateField HeaderText="学号" >
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#GetStuNumByStuID((int)Eval("StudentID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="姓名">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetStuNameByStuID((int)Eval("StudentID"))  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="班级" SortExpression="ClassID">
                      <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#GetClassNameByID((int)Eval("ClassID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SingleGetMark" HeaderText="单项" 
                    SortExpression="SingleGetMark" />
                <asp:BoundField DataField="MutilGetMark" HeaderText="多项" 
                    SortExpression="MutilGetMark" />
                <asp:BoundField DataField="JudgeGetMark" HeaderText="判断" 
                    SortExpression="JudgeGetMark" />
                <asp:BoundField DataField="TotalGetMark" HeaderText="总分" 
                    SortExpression="TotalGetMark" />
                <asp:TemplateField HeaderText="查看试卷" Visible="False">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl='<%# "ShowStuExam.aspx?stuNum="+GetStuNumByStuID((int)Eval("StudentID")) %>' 
                            Target="_blank">打开</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="考生状态" SortExpression="HasFinished">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="正在考试" 
                            Visible='<%# Eval("InExaming") %>'></asp:Label>
                        <asp:Label ID="Label5" runat="server" Visible='<%# Eval("HasFinished") %>' Text="完成考试" ForeColor="#009900"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="重考">
                    <ItemTemplate>
                        <asp:Button ID="btnReExam" runat="server" 
                            CommandArgument='<%# Eval("RandomExamID") %>'
                            Text="重新考试" onclick="btnReExam_Click" 
                            onclientclick='return confirm("将删除该学生的考试文件信息，该学生将重新考试，确定吗？")' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
        </pe:ExtendedGridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="GetRandomExamsByBasicExamID" TypeName="SDPTExam.BLL.RandomExam">
            <SelectParameters>
                <asp:ControlParameter ControlID="HiddenField1" DefaultValue="2" 
                    Name="basicExamID" PropertyName="Value" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <br />
    
    </div>
    </form>
    
      <script type="text/javascript" language="javascript" src="../js/KeepRefresh.js"/>
</body>
</html>
