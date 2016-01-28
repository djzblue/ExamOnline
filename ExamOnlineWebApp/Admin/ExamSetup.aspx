<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamSetup.aspx.cs" Inherits="SDPTExam.Web.UI.Admin.ExamSetup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {           
            vertical-align: middle;
        }
        .style2
        {
            height: 14px;
        }
        .style3
        {
            vertical-align: middle;
            height: 14px;
        }
        .style4
        {
            width: 100%;
            height: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <table cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse; vertical-align:middle"
        width="70%">
        <tr>
            <td bgcolor="#eeeeee" style="text-align: right; " colspan="4" class="style4">
                <div class="title" align="left">
                    <h4 style="text-align: center">
                        试卷制定(<font color="red">随机出题</font>) &nbsp; &nbsp;<asp:Button ID="btnAdd" 
                            runat="server" onclick="btnAdd_Click" Text="新增" CausesValidation="False" />
                    </h4>
                </div>
            </td>
        </tr>
        <tr>
            <td bgcolor="#eeeeee" style="text-align: right;">
                所属课程：</td>
            <td class="style1" colspan="3">
                <asp:DropDownList ID="ddlCourses" runat="server" AutoPostBack="True" 
                    DataSourceID="sqlCourses" DataTextField="Title" DataValueField="CourseID" 
                    onselectedindexchanged="ddlCourses_SelectedIndexChanged" 
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="0">请选择课程</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlChapters" runat="server" DataSourceID="sqlChapters" 
                    DataTextField="Title" DataValueField="ChapterID">
                    <%--<asp:ListItem Value="0">所有章节</asp:ListItem>--%>
                </asp:DropDownList>
            </td>
     
        </tr>
        <tr>
            <td bgcolor="#eeeeee" style="text-align: right;">
                试卷标题：
            </td>
            <td class="style1" colspan="3">
                &nbsp;<div align="left">
                    <asp:TextBox ID="txtPaperName" runat="server" Width="231px" 
                        ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPaperName"
                        ErrorMessage="*！"></asp:RequiredFieldValidator>
                </div>
            </td>
     
        </tr>
        <tr>
            <td bgcolor="#eeeeee" style="text-align: right;" class="style2">
                试卷描述：</td>
            <td class="style3" colspan="3">
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="380px" 
                        Height="73px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                        ControlToValidate="txtDesc" Display="Dynamic"></asp:RequiredFieldValidator></td>
      
        </tr>
        <tr>
            <td bgcolor="#eeeeee" style="text-align: right;">
                <b>单选题</b>----数目：
            </td>
            <td class="style1">
              
                    <asp:TextBox ID="txtSingleNum" runat="server" Width="50px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSingleNum"
                        ValidationExpression="^[0-9]\d*$" ErrorMessage="整数" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="txtSingleNum" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td bgcolor="#eeeeee" style="text-align: right;">
                每题分值：
            </td>
            <td class="style1">               
                    <asp:TextBox ID="txtSingleFen" runat="server" Width="50px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSingleFen"
                        ValidationExpression="^[1-9]\d*$" ErrorMessage="正整数" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        ControlToValidate="txtSingleFen" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td bgcolor="#eeeeee" style="text-align: right;">
                <b>多选题</b>----数目：
            </td>
            <td class="style1">
            
                    <asp:TextBox ID="txtMultiNum" runat="server" Width="50px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMultiNum"
                        ValidationExpression="^[0-9]\d*$" ErrorMessage="整数" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                        ControlToValidate="txtMultiNum"></asp:RequiredFieldValidator>
            </td>
            <td bgcolor="#eeeeee" style="text-align: right;">
                每题分值：
            </td>
            <td class="style1">
              
                    <asp:TextBox ID="txtMultiFen" runat="server" Width="50px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMultiFen"
                        ValidationExpression="^[1-9]\d*$" ErrorMessage="正整数" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                        ControlToValidate="txtMultiFen"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td bgcolor="#eeeeee" style="text-align: right;">
                <b>判断题</b>----数目：
            </td>
            <td class="style1">
                     <asp:TextBox ID="txtJudgeNum" runat="server" Width="50px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtJudgeNum"
                        ValidationExpression="^[0-9]\d*$" ErrorMessage="整数" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                        ControlToValidate="txtJudgeNum"></asp:RequiredFieldValidator>
            </td>
            <td bgcolor="#eeeeee" style="text-align: right;">
                每题分值：
            </td>
            <td class="style1">              
                    <asp:TextBox ID="txtJudgeFen" runat="server" Width="50px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtJudgeFen"
                        ValidationExpression="^[1-9]\d*$" ErrorMessage="正整数" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                        ControlToValidate="txtJudgeFen"></asp:RequiredFieldValidator>
            </td>
        </tr>
      
        <tr>
            <td bgcolor="#eeeeee" style="text-align: right;">
                <b>总分：</b></td>
            <td class="style1">
                <asp:TextBox ID="txtTotalMark" runat="server" Width="50px">100</asp:TextBox>
                    <asp:RegularExpressionValidator ID="totalValidate" runat="server" ControlToValidate="txtTotalMark"
                        ValidationExpression="^[1-9]\d*$" ErrorMessage="正整数" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                        ControlToValidate="txtTotalMark"></asp:RequiredFieldValidator>
            </td>
            <td bgcolor="#eeeeee" style="text-align: right;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
      
        <tr>
            <td bgcolor="#eeeeee" style="text-align: right;">
                考试用时：</td>
            <td class="style1">
                <asp:TextBox ID="txtTotalTime" runat="server" Width="50px">120</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                        ControlToValidate="txtTotalTime"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="totalValidate0" runat="server" ControlToValidate="txtTotalTime"
                        ValidationExpression="^[1-9]\d*$" ErrorMessage="正整数" 
                        Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td bgcolor="#eeeeee" style="text-align: right;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
      
        <tr height="40">
            <td colspan="4" align="center">
                <asp:ImageButton ID="imgBtnConfirm" runat="server" ImageUrl="Images/Save.GIF"
                    OnClick="imgBtnConfirm_Click" style="height: 20px" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
              
                <br />
            </td>
        </tr>
    </table>
    
        <asp:SqlDataSource ID="sqlCourses" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SQLConnStr %>" 
            SelectCommand="SELECT * FROM [CourseInfo]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlChapters" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SQLConnStr %>" 
            SelectCommand="SELECT * FROM [ChapterInfo] WHERE ([CourseID] = @CourseID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlCourses" Name="CourseID" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    
      <br />  <br />
        <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
        <tr>
            <td valign="top" align="left" width="860">
                <h4>
                    >>试卷管理</h4>
                <hr />
              
                <br />
                <pe:ExtendedGridView ID="Egv" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="BasicExamID" 
                    DataSourceID="ObjectDataSource1" CheckBoxFieldHeaderWidth="3%" 
                    EnableModelValidation="True" IsHoldState="True" SerialText=""  >
                    <Columns>
                        <asp:BoundField DataField="BasicExamTitle" HeaderText="试卷标题" 
                            SortExpression="BasicExamTitle" />
                        <asp:BoundField DataField="AddedDate" HeaderText="添加时间" 
                            SortExpression="AddedDate" />
                        <asp:TemplateField HeaderText="考试用时" SortExpression="TimeUse">
             
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("TimeUse") %>'></asp:Label>分钟
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="启用" SortExpression="IsActive" Visible="False">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSetActive" runat="server" Text="设为当前考试" 
                                                Visible='<%# !(bool)Eval("IsActive") %>' CausesValidation="False" 
                                                CommandArgument='<%# Eval("BasicExamID") %>' onclick="btnSetActive_Click" />
                                <asp:Label ID="Label1" runat="server"  
                                                Visible='<%# Eval("IsActive") %>' Text="当前考试" ForeColor="Red"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="管理">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                    Text="修改" CommandArgument='<%# Eval("BasicExamID") %>' 
                                    onclick="LinkButton1_Click"></asp:LinkButton>
  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                    CommandName="Delete" Text="删除" OnClientClick="return confirm(&quot;这将删除试卷信息，请慎重考虑！确定删除吗？&quot;)"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </pe:ExtendedGridView>
                <br />
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                   SelectMethod="GetAllBasicExams" 
                    TypeName="SDPTExam.BLL.BasicExam" DeleteMethod="DeleteBasicExam">
                    <DeleteParameters>
                        <asp:Parameter Name="BasicExamID" Type="Int32" />
                    </DeleteParameters>
                </asp:ObjectDataSource>
                <br />
              
            </td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="hideExamID" runat="server" />
    </form>
</body>
</html>
