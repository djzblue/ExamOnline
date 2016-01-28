<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseManagement.aspx.cs" Inherits="SDPTThesis.Web.UI.Admin.CourseManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <pe:ExtendedGridView ID="Egv" runat="server" AutoGenerateColumns="False" 
            CheckBoxFieldHeaderWidth="3%" DataKeyNames="CourseID" DataSourceID="sqlCourses" 
            EnableModelValidation="True" IsHoldState="True" SerialText="">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="课程名" SortExpression="Title" />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowSelectButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </pe:ExtendedGridView>
        <asp:SqlDataSource ID="sqlCourses" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SQLConnStr %>" 
            DeleteCommand="DELETE FROM [CourseInfo] WHERE [CourseID] = @CourseID" 
            InsertCommand="INSERT INTO [CourseInfo] ([Title], [MajorID], [DepartmentID], [CourseDesc], [CreditHour], [ClassHour], [ChargeTeacherID]) VALUES (@Title, @MajorID, @DepartmentID, @CourseDesc, @CreditHour, @ClassHour, @ChargeTeacherID)" 
            SelectCommand="SELECT * FROM [CourseInfo]" 
            UpdateCommand="UPDATE [CourseInfo] SET [Title] = @Title, [MajorID] = @MajorID, [DepartmentID] = @DepartmentID, [CourseDesc] = @CourseDesc, [CreditHour] = @CreditHour, [ClassHour] = @ClassHour, [ChargeTeacherID] = @ChargeTeacherID WHERE [CourseID] = @CourseID">
            <DeleteParameters>
                <asp:Parameter Name="CourseID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="MajorID" Type="Int32" />
                <asp:Parameter Name="DepartmentID" Type="Int32" />
                <asp:Parameter Name="CourseDesc" Type="String" />
                <asp:Parameter Name="CreditHour" Type="Int32" />
                <asp:Parameter Name="ClassHour" Type="String" />
                <asp:Parameter Name="ChargeTeacherID" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="MajorID" Type="Int32" />
                <asp:Parameter Name="DepartmentID" Type="Int32" />
                <asp:Parameter Name="CourseDesc" Type="String" />
                <asp:Parameter Name="CreditHour" Type="Int32" />
                <asp:Parameter Name="ClassHour" Type="String" />
                <asp:Parameter Name="ChargeTeacherID" Type="Int32" />
                <asp:Parameter Name="CourseID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
    </div>
    <asp:SqlDataSource ID="sqlSingleCourse" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnStr %>" 
        DeleteCommand="DELETE FROM [CourseInfo] WHERE [CourseID] = @CourseID" 
        InsertCommand="INSERT INTO [CourseInfo] ([Title], [MajorID], [DepartmentID], [CourseDesc], [CreditHour], [ClassHour], [ChargeTeacherID]) VALUES (@Title, @MajorID, @DepartmentID, @CourseDesc, @CreditHour, @ClassHour, @ChargeTeacherID)" 
        SelectCommand="SELECT * FROM [CourseInfo]" 
        UpdateCommand="UPDATE [CourseInfo] SET [Title] = @Title, [MajorID] = @MajorID, [DepartmentID] = @DepartmentID, [CourseDesc] = @CourseDesc, [CreditHour] = @CreditHour, [ClassHour] = @ClassHour, [ChargeTeacherID] = @ChargeTeacherID WHERE [CourseID] = @CourseID">
        <DeleteParameters>
            <asp:Parameter Name="CourseID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="MajorID" Type="Int32" />
            <asp:Parameter Name="DepartmentID" Type="Int32" />
            <asp:Parameter Name="CourseDesc" Type="String" />
            <asp:Parameter Name="CreditHour" Type="Int32" />
            <asp:Parameter Name="ClassHour" Type="String" />
            <asp:Parameter Name="ChargeTeacherID" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="MajorID" Type="Int32" />
            <asp:Parameter Name="DepartmentID" Type="Int32" />
            <asp:Parameter Name="CourseDesc" Type="String" />
            <asp:Parameter Name="CreditHour" Type="Int32" />
            <asp:Parameter Name="ClassHour" Type="String" />
            <asp:Parameter Name="ChargeTeacherID" Type="Int32" />
            <asp:Parameter Name="CourseID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Button ID="Button1" runat="server" Text="添加课程" />
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="CourseID" 
        DataSourceID="sqlSingleCourse" DefaultMode="Insert" 
        EnableModelValidation="True">
        <EditItemTemplate>
            CourseID:
            <asp:Label ID="CourseIDLabel1" runat="server" Text='<%# Eval("CourseID") %>' />
            <br />
            Title:
            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
            <br />
            MajorID:
            <asp:TextBox ID="MajorIDTextBox" runat="server" Text='<%# Bind("MajorID") %>' />
            <br />
            DepartmentID:
            <asp:TextBox ID="DepartmentIDTextBox" runat="server" 
                Text='<%# Bind("DepartmentID") %>' />
            <br />
            CourseDesc:
            <asp:TextBox ID="CourseDescTextBox" runat="server" 
                Text='<%# Bind("CourseDesc") %>' />
            <br />
            CreditHour:
            <asp:TextBox ID="CreditHourTextBox" runat="server" 
                Text='<%# Bind("CreditHour") %>' />
            <br />
            ClassHour:
            <asp:TextBox ID="ClassHourTextBox" runat="server" 
                Text='<%# Bind("ClassHour") %>' />
            <br />
            ChargeTeacherID:
            <asp:TextBox ID="ChargeTeacherIDTextBox" runat="server" 
                Text='<%# Bind("ChargeTeacherID") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="更新" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="取消" />
        </EditItemTemplate>
        <InsertItemTemplate>
            Title:
            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
            <br />
            MajorID:
            <asp:TextBox ID="MajorIDTextBox" runat="server" Text='<%# Bind("MajorID") %>' />
            <br />
            DepartmentID:
            <asp:TextBox ID="DepartmentIDTextBox" runat="server" 
                Text='<%# Bind("DepartmentID") %>' />
            <br />
            CourseDesc:
            <asp:TextBox ID="CourseDescTextBox" runat="server" 
                Text='<%# Bind("CourseDesc") %>' />
            <br />
            CreditHour:
            <asp:TextBox ID="CreditHourTextBox" runat="server" 
                Text='<%# Bind("CreditHour") %>' />
            <br />
            ClassHour:
            <asp:TextBox ID="ClassHourTextBox" runat="server" 
                Text='<%# Bind("ClassHour") %>' />
            <br />
            ChargeTeacherID:
            <asp:TextBox ID="ChargeTeacherIDTextBox" runat="server" 
                Text='<%# Bind("ChargeTeacherID") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="插入" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="取消" />
        </InsertItemTemplate>
        <ItemTemplate>
            CourseID:
            <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' />
            <br />
            Title:
            <asp:Label ID="TitleLabel" runat="server" Text='<%# Bind("Title") %>' />
            <br />
            MajorID:
            <asp:Label ID="MajorIDLabel" runat="server" Text='<%# Bind("MajorID") %>' />
            <br />
            DepartmentID:
            <asp:Label ID="DepartmentIDLabel" runat="server" 
                Text='<%# Bind("DepartmentID") %>' />
            <br />
            CourseDesc:
            <asp:Label ID="CourseDescLabel" runat="server" 
                Text='<%# Bind("CourseDesc") %>' />
            <br />
            CreditHour:
            <asp:Label ID="CreditHourLabel" runat="server" 
                Text='<%# Bind("CreditHour") %>' />
            <br />
            ClassHour:
            <asp:Label ID="ClassHourLabel" runat="server" Text='<%# Bind("ClassHour") %>' />
            <br />
            ChargeTeacherID:
            <asp:Label ID="ChargeTeacherIDLabel" runat="server" 
                Text='<%# Bind("ChargeTeacherID") %>' />
            <br />
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                CommandName="Edit" Text="编辑" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" 
                CommandName="Delete" Text="删除" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
                CommandName="New" Text="新建" />
        </ItemTemplate>
    </asp:FormView>
    <br />
    <asp:Button ID="Button2" runat="server" Text="添加章节" />
    <br />
    <asp:SqlDataSource ID="sqlChapters" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnStr %>" 
        SelectCommand="SELECT * FROM [ChapterInfo]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlChapter" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnStr %>" 
        DeleteCommand="DELETE FROM [ChapterInfo] WHERE [ChapterID] = @ChapterID" 
        InsertCommand="INSERT INTO [ChapterInfo] ([Title], [CourseID], [ChapterDesc]) VALUES (@Title, @CourseID, @ChapterDesc)" 
        SelectCommand="SELECT * FROM [ChapterInfo]" 
        UpdateCommand="UPDATE [ChapterInfo] SET [Title] = @Title, [CourseID] = @CourseID, [ChapterDesc] = @ChapterDesc WHERE [ChapterID] = @ChapterID">
        <DeleteParameters>
            <asp:Parameter Name="ChapterID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="CourseID" Type="Int32" />
            <asp:Parameter Name="ChapterDesc" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="CourseID" Type="Int32" />
            <asp:Parameter Name="ChapterDesc" Type="String" />
            <asp:Parameter Name="ChapterID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <pe:ExtendedGridView ID="Egv0" runat="server" AutoGenerateColumns="False" 
        CheckBoxFieldHeaderWidth="3%" DataKeyNames="ChapterID" 
        DataSourceID="sqlChapters" EnableModelValidation="True" IsHoldState="True" 
        SerialText="">
        <Columns>
            <asp:BoundField DataField="ChapterID" HeaderText="ChapterID" 
                InsertVisible="False" ReadOnly="True" SortExpression="ChapterID" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="CourseID" HeaderText="CourseID" 
                SortExpression="CourseID" />
            <asp:BoundField DataField="ChapterDesc" HeaderText="ChapterDesc" 
                SortExpression="ChapterDesc" />
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:FormView ID="FormView2" runat="server" DataKeyNames="ChapterID" 
        DataSourceID="sqlChapter" DefaultMode="Insert" EnableModelValidation="True">
        <EditItemTemplate>
            ChapterID:
            <asp:Label ID="ChapterIDLabel1" runat="server" 
                Text='<%# Eval("ChapterID") %>' />
            <br />
            Title:
            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
            <br />
            CourseID:
            <asp:TextBox ID="CourseIDTextBox" runat="server" 
                Text='<%# Bind("CourseID") %>' />
            <br />
            ChapterDesc:
            <asp:TextBox ID="ChapterDescTextBox" runat="server" 
                Text='<%# Bind("ChapterDesc") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="更新" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="取消" />
        </EditItemTemplate>
        <InsertItemTemplate>
            章节名:
            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
            <br />
            所属课程:
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sqlCourses" 
                DataTextField="Title" DataValueField="CourseID" 
                SelectedValue='<%# Bind("CourseID") %>'>
            </asp:DropDownList>
            <br />
            章节介绍:
            <asp:TextBox ID="ChapterDescTextBox" runat="server" Height="50px" 
                Text='<%# Bind("ChapterDesc") %>' TextMode="MultiLine" Width="175px" />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="插入" />
&nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" 
                CommandName="Cancel" Text="取消" />
        </InsertItemTemplate>
        <ItemTemplate>
            ChapterID:
            <asp:Label ID="ChapterIDLabel" runat="server" Text='<%# Eval("ChapterID") %>' />
            <br />
            Title:
            <asp:Label ID="TitleLabel" runat="server" Text='<%# Bind("Title") %>' />
            <br />
            CourseID:
            <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Bind("CourseID") %>' />
            <br />
            ChapterDesc:
            <asp:Label ID="ChapterDescLabel" runat="server" 
                Text='<%# Bind("ChapterDesc") %>' />
            <br />
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                CommandName="Edit" Text="编辑" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" 
                CommandName="Delete" Text="删除" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
                CommandName="New" Text="新建" />
        </ItemTemplate>
    </asp:FormView>
    <br />
    <br />
    </form>
</body>
</html>
