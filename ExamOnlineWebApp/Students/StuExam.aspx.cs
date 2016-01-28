using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data;
using SDPTExam.BLL;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using SDPTExam.DAL.Model;
using SDPTExam.DAL.Linq;
using System.Drawing;

namespace SDPTExam.Web.UI.Students
{
    public partial class StuExam : BasePage
    {
        protected int singeCount = 1;

        protected int mutilCount = 1;

        protected int judgeCount = 1;

        protected int RemainSeconds = 7200;

        public int BasicExamID
        {
            get 
            {
                string basicExamID = Request.QueryString["examID"];
                return int.Parse(basicExamID);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
                   


            if (!Page.IsPostBack)
            {
                // ClientTimer_Test.TimerEnabled = true;   
       
                //this.ClientTimer_Test.onTimeOut += new ExamManagement.Controls.myControl.onTimeOutEventHandler(this.ClientTimer1_onTimeOut);

                if (Session["Role"] == null || Session["Role"].ToString() != "student" && Session["Role"].ToString()!="visitor") Response.Redirect("~/Login.aspx");

                if (Request.QueryString["examID"] == null)//这里需要再加上判断examid为整数
                {
                    Response.Write("参数错误！！！！");
                    Response.End();
                    return;
                }
                
                lblExamTitle.Text=BasicExam.GetBasicExamByID(BasicExamID).BasicExamTitle;

                labUser.Text = UserRealName;

                lblStuNum.Text = StuNum;

                lblClass.Text = ClassName;
                                
               // StartExam();
                    
            }
        }

        private void StartExam()
        {
            //if (Utility.CheckIfFinished(UserID))
            //{
            //    NotAllowToExam();
            //    return;
            //}

            if (Utility.CheckIfFinished(StuNum,BasicExamID))
            {
                NotAllowToExam();
                return;
            }
            string showEndtime="";   
            XElement xroot = null;
            // WebConfigurationManager.AppSettings["ActiveBasicExamID"];

            //这里不需要设置courseID,，//只要设置basicExamID即可，可以从网址中获取！！


            //if (basicExamID == null) return;
            int examID =  BasicExamID;
            lblPaperName.Text = BasicExam.GetBasicExamByID(examID).BasicExamTitle;

            if (File.Exists(Utility.GetStuExamFilePath(StuNum,examID)) == false) //如果考生xml文件不存在，则创建。
            {
                StudentInfo stu=null;
                int randomExamID = 0;
                if(Session["Role"].ToString()=="visitor")
                {
                    stu=new StudentInfo();
                    lblWelcome.Text = "游客，欢迎您！！";
                    stu.StuNum=StuNum;
                    stu.StudentID=0;
                    stu.ClassID = 0;
                }
                else
                {
                    stu=Student.GetStudentByID(UserID);

                    lblWelcome.Text = stu.StuName + "同学，欢迎您！！";
                }
                lblWelcome.Visible = true;
                randomExamID = RandomExam.CreatRandomExamXMLFile(stu, examID);
                if (randomExamID == 0) return;

                ExamDbDataContext dc = DataAccess.CreateDBContext();
                RandomExamInfo r = RandomExam.GetRandomExamByID(randomExamID, out dc);

                r.StartTime = DateTime.Now;

                r.EndTime = DateTime.Now.AddMinutes((double)r.TimeUse);

                showEndtime = r.EndTime.ToString();
                dc.SubmitChanges();

                xroot = GetRootElement();

                xroot.SetAttributeValue("endTime", showEndtime);
                xroot.SetAttributeValue("loginNum", 1);

                this.index = (int)r.TimeUse;

               
            }
            else  //如果考生xml文件已经存在，则直接读取。
            {
                ViewState["againLogin"] = true;
                xroot = GetRootElement();
                showEndtime = xroot.Attribute("endTime").Value;//从xml文件中获取结束时间
                string loginNum = xroot.Attribute("loginNum").Value;

                int l = int.Parse(loginNum);

                l++;

                xroot.SetAttributeValue("loginNum", l);

                TimeSpan tLeft = DateTime.Parse(showEndtime) - DateTime.Now; //如果考试时间已过，不允许考试！！

                //如果已经交卷了呢？？

                this.index = tLeft.Minutes+tLeft.Hours*60;

                if (index <= 0)
                {
                  //  NotAllowToExam(); //这里应该对RandomExam重新设置,还需要帮学生交卷计分

                    RandomExam.SetTotalMark(xroot, 0, 0, 0, true);

                   // EndExam("由于考试时间已到，系统自动交卷，考试结束！！");
                   // RandomExam.SetRandomExamFinished(int.Parse(xroot.Attribute("randomExamID").Value));
                    return;
                }

            }

            xroot.Save(Utility.GetStuExamFilePath(StuNum,BasicExamID));

                panelWholePaper.Visible = true;
                lblTotalTimeHint.Text = "考试总时间为：" + this.index.ToString() + "分钟，结束时间为：";
                if (ViewState["againLogin"] != null && (bool)ViewState["againLogin"])
                    lblTotalTimeHint.Text = "考试总时间为：" + this.index.ToString() + "分钟（因为您已登录过）,结束时间为：";
                lblTotalTimeHint.Text += showEndtime+"，到时会自动交卷。请注意时间。";
                lblTime.Text = this.index+ "分钟";
                Timer1.Enabled = true;
                btnSubmit.Enabled = true;

                lblMessage.Visible = false;//或者给其他提示信息

            BindRep();
           // 

            //ClientTimer_Test.TimerEnabled = true;   
       
            //ClientTimer_Test.TimeOutLength = index;

        }

        private void NotAllowToExam()
        {
            lblMessage.Text = @"你已经完成考试，无法再考！！";//<a href='#' title='close the window' onclick='window.close()'>【关闭窗口】</a>";
            panelWholePaper.Visible = false;
        }

        private void ClientTimer1_onTimeOut()
        {
            //this.ClientTimer_Test.TimerEnabled = false;
            
            ////Label1.Text = DateTime.Now.ToString() + "到时间了";

            //EndExam();


            Response.Write("时间到咯！！<br/>");

        }



        protected void BindRep()
        {
            XElement xe = GetRootElement();

            
            var singleElements = from s in xe.Element("SingleChoices").Elements("SingleChoice")
                                 select s;// new { Title = s.Attribute("title").Value, ID = s.Attribute("choiceID").Value };


            if (xe.Element("MutilChoices") == null)
            {
                panelMutil.Visible = false;
                btnMutil.Visible = false;
                btnMutil0.Visible = false;
            }
            else
            {
                var mutilElements = from s in xe.Element("MutilChoices").Elements("MutilChoice")
                                    select new { Title = s.Attribute("title").Value, ID = s.Attribute("choiceID").Value };
                if (mutilElements != null)
                {
                    lblMutilEachMark.Text = xe.Element("MutilChoices").Attribute("eachMark").Value;
                    mutilRep.DataSource = mutilElements;
                    mutilRep.DataBind();
                }
            }

            var judgeElements = from s in xe.Element("Judges").Elements("JudgeItem")
                                select new { Title = s.Attribute("title").Value, ID = s.Attribute("judgeID").Value };

            //foreach (XElement e in singleElements)
            //{
            //    Response.Write(e.Name);

            //}
            //foreach (XAttribute xa in xe.Element("SingleChoices").Element("SingleChoice").Attributes("title"))
            //    Response.Write(xa.Name + ": " + xa.Value + "<br/>");

            //Items=s.Elements("ChoiceItem").Attributes("Title")

            singleRep.DataSource = from s in singleElements
                                   select new { Title = s.Attribute("title").Value, ID = s.Attribute("choiceID").Value };
            singleRep.DataBind();


            lblSingleEachMark.Text = xe.Element("SingleChoices").Attribute("eachMark").Value;

            
            lblJudgeEachMark.Text = xe.Element("Judges").Attribute("eachMark").Value;

            judgeRep.DataSource = judgeElements;
            judgeRep.DataBind();



        }


        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        private XElement GetRootElement()        
        {
            string basicExamID = Request.QueryString["examID"];
 
            return XElement.Load(Utility.GetStuExamFilePath(StuNum,int.Parse(basicExamID)));
        }

        private string GetXMLFilePath(string stuNum)
        {
            if (Session["StuNum"] == null) Response.Redirect("~/Login.aspx");

                       return Server.MapPath("~/Students/StuExamFiles/"+stuNum+".xml");
        }


        private void GetParperAll()
        {


        }
        protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            // NewMethod();
        }

        //private void NewMethod()
        //{
        //    string Label = labSingle.Text;//单选分数
        //    string paperid = Session["PaperID"].ToString();
        //    string UserId = Session["userID"].ToString();
        //   // DBHelp db = new DBHelp();
        //    foreach (RepeaterItem item in singleRep.Items)
        //    {
        //        HiddenField titleId = item.FindControl("titleId") as HiddenField;
        //        string id = (string)titleId.Value;
        //        string str = "";
        //        if (((RadioButton)item.FindControl("rbA")).Checked)
        //        {
        //            str = "A";
        //        }
        //        else if (((RadioButton)item.FindControl("rbB")).Checked)
        //        {
        //            str = "B";
        //        }
        //        else if (((RadioButton)item.FindControl("rbC")).Checked)
        //        {
        //            str = "C";
        //        }
        //        else if (((RadioButton)item.FindControl("rbD")).Checked)
        //        {
        //            str = "D";
        //        }
        //        string single = "insert into UserAnswer(UserID,PaperID,Type,TitleID,Mark,UserAnswer,ExamTime) values('" + UserId + "','" + paperid + "','单选题','" + id + "','" + Label + "','" + str + "','" + DateTime.Now.ToString() + "')";
        //       // db.Insert(single);

        //    }


        //    string labeM = Label3.Text;//多选分数
        //    foreach (RepeaterItem item in Repeater2.Items)
        //    {
        //        HiddenField titleId = item.FindControl("titleId") as HiddenField;
        //        string id = (string)titleId.Value;
        //        string str = "";
        //        if (((CheckBox)item.FindControl("CheckBox1")).Checked)
        //        {
        //            str += "A";
        //        }
        //        if (((CheckBox)item.FindControl("CheckBox2")).Checked)
        //        {
        //            str += "B";
        //        }
        //        if (((CheckBox)item.FindControl("CheckBox3")).Checked)
        //        {
        //            str += "C";
        //        }
        //        if (((CheckBox)item.FindControl("CheckBox4")).Checked)
        //        {
        //            str += "D";
        //        }
        //        string Multi = "insert into UserAnswer(UserID,PaperID,Type,TitleID,Mark,UserAnswer,ExamTime) values('" + UserId + "','" + paperid + "','多选题','" + id + "','" + labeM + "','" + str + "','" + DateTime.Now.ToString() + "')";
        //      //  db.Insert(Multi);


        //    }

        //    string labeJ = Label4.Text;//判断分数
        //    foreach (RepeaterItem item in Repeater3.Items)
        //    {
        //        HiddenField titleId = item.FindControl("titleId") as HiddenField;
        //        string id = (string)titleId.Value;

        //        string str = Convert.ToString(false);
        //        if (((RadioButton)item.FindControl("rbA")).Checked)
        //        {
        //            str = Convert.ToString(true);
        //        }
        //        else if (((RadioButton)item.FindControl("rbB")).Checked)
        //        {
        //            str = Convert.ToString(false);
        //        }
        //        string Judge = "insert into UserAnswer(UserID,PaperID,Type,TitleID,Mark,UserAnswer,ExamTime) values('" + UserId + "','" + paperid + "','判断题','" + id + "','" + labeJ + "','" + str + "','" + DateTime.Now.ToString() + "')";
        //      //  db.Insert(Judge);
        //    }

        //    // Session["Test"] = "eeee";

        //   // Response.Write("<script language=javascript>alert('试卷提交成功!');window.close();</script>");

        //}

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            // NewMethod();            

            this.index--;
            
            this.lblTime.Text = this.index + "分钟" ;

            if (index <= 0)
            {
                EndExam("时间到！系统自动交卷，考试结束！！");
            }

            else if(index%3==0) SaveStuXMLFile(false);//每隔3分钟，保存一次试卷


           



        }


        /// <summary>
        /// 定义在线考试总时间变量index,
        /// 并设置读写属性
        /// </summary>
        private int index
        {
            get
            {
                object o = ViewState["index"];
                return (o == null) ? 120 : (int)o;
            }
            set
            {
                ViewState["index"] = value;
            }
        }

        protected void singleRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                XElement xe = GetRootElement();

                //HiddenField h = singleRep.FindControl("titleId") as HiddenField;
                //string singleID = "2";
                //if (h != null && string.IsNullOrEmpty(h.Value) == false)
                //    singleID = h.Value;

                //var choiceElements = singleElements.Where(p => p.Attribute("choiceID").Value == singleID).Single<XElement>().Elements("ChoiceItem");

                //              rbListChoiceItem

                string choiceID = (string)DataBinder.Eval(e.Item.DataItem, "ID");
                var ds = from s in xe.Element("SingleChoices").Elements("SingleChoice").Elements("ChoiceItem")
                         where s.Parent.Attribute("choiceID").Value == choiceID && s != null
                         select new { Title = s.Attribute("title").Value, ID = s.Attribute("choiceItemID").Value, IsSelect = s.Attribute("isSelected").Value };
                RadioButtonList rbList = e.Item.FindControl("rbListChoiceItem") as RadioButtonList;
                
                if (rbList != null)
                {
                    rbList.DataSource = ds;
                    rbList.DataBind();

                }


                foreach (ListItem l in rbList.Items)
                {
                    if (ds.Single(p => p.ID == l.Value).IsSelect == "true")
                        l.Selected = true;
                    else l.Selected = false;
                }

                //Repeater rSingleItem = e.Item.FindControl("singItemRep") as Repeater;

                //if (rSingleItem != null)
                //{


                //    rSingleItem.DataSource = ds;
                //    rSingleItem.DataBind();
                //}
                //else
                //{
                //    Response.Write("找不到singItemRep");
                //}

            }
        }

        protected void mutilRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                XElement xe = GetRootElement();


                string choiceID = (string)DataBinder.Eval(e.Item.DataItem, "ID");
                var ds = from s in xe.Element("MutilChoices").Elements("MutilChoice").Elements("ChoiceItem")
                         where s.Parent.Attribute("choiceID").Value == choiceID && s != null
                         select new { Title = s.Attribute("title").Value, ID = s.Attribute("choiceItemID").Value, IsSelect = s.Attribute("isSelected").Value };
                CheckBoxList chkList = e.Item.FindControl("chkListChoiceItem") as CheckBoxList;

                if (chkList != null)
                {
                    chkList.DataSource = ds;
                    chkList.DataBind();

                }

                foreach (ListItem l in chkList.Items)
                {
                    if (ds.Single(p => p.ID == l.Value).IsSelect == "true")
                        l.Selected = true;

                }
            }
        }


        bool IsSingleFinished()//(Repeater r,ListControl rList)
        {
            bool isFinished=false;

            bool result = true;
            foreach (RepeaterItem ri in singleRep.Items)
            { 
                 RadioButtonList  rList = ri.FindControl("rbListChoiceItem") as RadioButtonList;
                 foreach (ListItem li in rList.Items)
                     isFinished = isFinished || li.Selected;

                 if (isFinished == false)
                 {
                     result = false; ; //如果存在一题没做，返回判断
                     rList.Font.Bold = true;//这里设置了加黑

                     rList.ForeColor = Color.Red;
                 }

                 isFinished = false;//重置
            
            }
            return result;
          
        }

        bool IsMutilFinished()
        {
           
            bool isFinished = false;
            bool result = true;
            foreach (RepeaterItem ri in mutilRep.Items)
            {
                CheckBoxList rList = ri.FindControl("chkListChoiceItem") as CheckBoxList;
                
                foreach (ListItem li in rList.Items)
                    isFinished = isFinished || li.Selected;

                if (isFinished == false)
                {
                    result = false; ; //如果存在一题没做，返回判断
                    rList.Font.Bold = true;
                    rList.ForeColor = Color.Red;
                }


                isFinished = false;//重置

            }
            return result;
        }

        bool IsJudgeFinished()
        {
           
            foreach (RepeaterItem ri in judgeRep.Items)
            {    
                RadioButtonList rList = ri.FindControl("rbListJudges") as RadioButtonList;

                if (rList.SelectedIndex < 0)
                {
                    rList.Font.Bold = true;
                    rList.ForeColor = Color.Red;
                    return false;
                }

            }
            return true;
         

          
        }
        /// <summary>
        /// 交卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsMutilFinished() && IsSingleFinished() && IsJudgeFinished())
            {
                EndExam("你已经交卷成功，考试结束！！");
                ChkExamState();
            }
            else
            {
                if (IsSingleFinished() == false)
                    this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('单选题未完成')</script>");
                else if (IsMutilFinished() == false)
                    this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('多选题未完成')</script>");
                else
                    this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('判断题未完成')</script>");
            }
            
        }

        private void EndExam(string s)
        {
            lblMessage.Text = s+@"<a href='#' title='close the window' onclick='window.close()'>【关闭窗口】</a>";
            lblMessage.Visible = true;
            btnSubmit.Enabled = false;
            panelWholePaper.Visible = false;
            Timer1.Enabled = false;
            string basicExamID = Request.QueryString["examID"];
            if (Utility.CheckIfFinished(StuNum,int.Parse(basicExamID)))
            {
                lblWelcome.Visible = false;
                NotAllowToExam();
                return;
            }
           else SaveStuXMLFile(true);
            
            //ClientTimer_Test.TimerEnabled = false;
        }

        private void SaveStuXMLFile(bool isFinished)
        {
            XElement xe = GetRootElement();

           // xe.SetAttributeValue("timeLeft", index);

            int singleRightNum = SetChoicesAnswer(xe, singleRep, true, isFinished);

            int mutilRightNum = SetChoicesAnswer(xe, mutilRep, false, isFinished);

            int judgeNum = SetJudgesAnswer(xe, judgeRep, isFinished);

            if (isFinished)//若未交卷，只是保存xml选项结点，不保存分数，不操作数据库
            {
               RandomExam.SetTotalMark(xe, singleRightNum, mutilRightNum, judgeNum,false);           
            }

            xe.Save(Utility.GetStuExamFilePath(StuNum, BasicExamID));
        }




        /// <summary>
        /// 设置判断题
        /// </summary>
        /// <param name="xe"></param>
        /// <param name="judgeRep"></param>
        /// <param name="isFinished"></param>
        /// <returns></returns>
        private int SetJudgesAnswer(XElement xe, Repeater judgeRep, bool isFinished)
        {
            int rightNum = 0;///该判断题答对的题数
           
            XElement judgeElement=xe.Element("Judges");
            foreach (RepeaterItem ri in judgeRep.Items)
            {

                HiddenField h = ri.FindControl("titleId") as HiddenField;

                if (h == null) continue;//若不能获取id，则忽略该题

                string judgeID = h.Value;

                XElement judgeItemElement = Utility.GetJudgeElementByID(judgeElement, judgeID);

                bool isRight = true;

               RadioButtonList  rList = ri.FindControl("rbListJudges") as RadioButtonList;

               if (rList.SelectedIndex >= 0)
               {
                   judgeItemElement.SetAttributeValue("setTrue", rList.SelectedValue);
                   if (isFinished) isRight = RandomExam.CheckJudgeAnswerIsRight(int.Parse(judgeID), rList.SelectedValue == "1" ? true : false);
                   else continue;

               }
               else continue;
               if (isRight && isFinished) rightNum++;      

            }

            return rightNum;
        }

        private int SetChoicesAnswer(XElement xe,Repeater rep, bool isSingle,bool isFinished)
        {
            int rightNum = 0;///该选择题答对的题数
            foreach (RepeaterItem ri in rep.Items)
            {               

                HiddenField h = ri.FindControl("titleId") as HiddenField;

                if (h == null) continue;//若不能获取id，则忽略该题

                string choiceID = h.Value;

                string elementName = "SingleChoice";

                if (isSingle == false) elementName = "MutilChoice";

                XElement choiceElement = Utility.GetChoiceElementByChoiceID(xe, choiceID,elementName );

                ListControl rList = null;

                if (isSingle) rList = ri.FindControl("rbListChoiceItem") as RadioButtonList;
                else rList = ri.FindControl("chkListChoiceItem") as CheckBoxList;


                bool isRight = true;
                foreach (ListItem i in rList.Items)
                {
                    XElement itemElement = Utility.GetChoiceItemElementByID(choiceElement, i.Value);

                    itemElement.SetAttributeValue("isSelected", i.Selected);

                    if(isFinished)isRight=isRight&&RandomExam.CheckChoiceItemAnswerIsRight(int.Parse(i.Value), i.Selected);//必须所有选项都正确
                   
                }
                if (isFinished && isRight) rightNum++;               

            }

            return rightNum;
        }

        protected void judgeRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                XElement xe = GetRootElement();

                string choiceID = (string)DataBinder.Eval(e.Item.DataItem, "ID");
                var ds = from s in xe.Element("Judges").Elements("JudgeItem")
                         where s.Attribute("judgeID").Value == choiceID && s != null
                         select new { Title = s.Attribute("title").Value, ID = s.Attribute("judgeID").Value, SetTrue = s.Attribute("setTrue").Value };

                RadioButtonList rbList = e.Item.FindControl("rbListJudges") as RadioButtonList;

    
                foreach (ListItem l in rbList.Items)
                {
                    if (ds.Any(p=>p.SetTrue==l.Value))
                        l.Selected = true;

                }
            }
        }


        protected void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;            
            
            StartExam();          
        }

        protected void btnSinglePanel_Click(object sender, EventArgs e)
        {
            XElement xe = GetRootElement();         

            if (panelJudge.Visible)
            {
                SetJudgesAnswer(xe, judgeRep, false);
                panelJudge.Visible = false;
            }
            if (panelMutil.Visible)
            {
                SetChoicesAnswer(xe, mutilRep, false, false);
                panelMutil.Visible = false;
            }
            panelSingle.Visible = true;

            xe.Save(Utility.GetStuExamFilePath(StuNum, BasicExamID));
        }

        protected void btnMutil_Click(object sender, EventArgs e)
        {
            XElement xe = GetRootElement();

            if (panelJudge.Visible)
            {
                SetJudgesAnswer(xe, judgeRep, false);
                panelJudge.Visible = false;
            }
            if (panelSingle.Visible)
            {  
                SetChoicesAnswer(xe, singleRep, true, false);
                panelSingle.Visible = false;
            }
            panelMutil.Visible = true;
            xe.Save(Utility.GetStuExamFilePath(StuNum, BasicExamID));

        }

        protected void btnJudge_Click(object sender, EventArgs e)
        {
            XElement xe = GetRootElement();

            if (panelSingle.Visible)
            {            
                SetChoicesAnswer(xe, singleRep, true, false);
                panelSingle.Visible = false;
            }
            if (panelMutil.Visible)
            {
                SetChoicesAnswer(xe, mutilRep, false, false);
                panelMutil.Visible = false;
            }
            panelJudge.Visible = true;
            xe.Save(Utility.GetStuExamFilePath(StuNum, BasicExamID));
        }



        protected void btnReExam_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int reaxmID = int.Parse(b.CommandArgument);

            RandomExam.DeleteExam(reaxmID);
            // BindGrid(DeptMajorClassDropdownList1.ClassID);
            btnReExam.Enabled = false;

            lblMyMark.Text = "请点击开始考试，重考一次！！";

            btnStart.Enabled = true;
        }



        private void ChkExamState()
        {
            string basicExamID = Request.QueryString["examID"];
            RandomExamInfo r = RandomExam.GetRandomExamByStuNumAndBasicExameID(StuNum,int.Parse(basicExamID));
            if (r == null)
                lblMyMark.Text = "尚未考试";
            else if ((bool)r.HasFinished)
            {

                lblMyMark.Text = r.TotalGetMark.ToString();
                if (r.TotalGetMark < 60)
                {
                    btnReExam.Enabled = true;//若不及格，允许重新考试。
                    btnReExam.CommandArgument = r.RandomExamID.ToString();
                }
            }
            else
            {
                lblMyMark.Text = "正在考试中，请抓紧时间完成！";
            }
            panelMymark.Visible = true;
        }




    }
}
