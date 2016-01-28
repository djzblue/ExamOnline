using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.Web.UI;
using SDPTExam.BLL;
using System.Xml.Linq;
using System.IO;

namespace SDPTExam.Web.UI.Admin
{
    public partial class ShowStuExam : AdminBasePage
    {
        protected int singeCount = 1;
        protected bool againLogin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int stuNum =int.Parse(Request.QueryString["stuNum"]);

               if (File.Exists(GetXMLFilePath(stuNum)) == false)
                {
                    return;
                }
               
               BindRep();

            }
        }



        protected void BindRep()
        {
            XElement xe = GetRootElement();


            var singleElements = from s in xe.Element("SingleChoices").Elements("SingleChoice")
                                 select s;// new { Title = s.Attribute("title").Value, ID = s.Attribute("choiceID").Value };
            var mutilElements = from s in xe.Element("MutilChoices").Elements("MutilChoice")
                                select new { Title = s.Attribute("title").Value, ID = s.Attribute("choiceID").Value }; ;
            var judgeElements = from s in xe.Element("Judges").Elements("JudgeItem")
                                select new { Title = s.Attribute("title").Value, ID = s.Attribute("judgeID").Value };

            singleRep.DataSource = from s in singleElements
                                   select new { Title = s.Attribute("title").Value, ID = s.Attribute("choiceID").Value };
            singleRep.DataBind();


            lblSingleEachMark.Text = xe.Element("SingleChoices").Attribute("eachMark").Value;

            lblMutilEachMark.Text = xe.Element("MutilChoices").Attribute("eachMark").Value;
            lblJudgeEachMark.Text = xe.Element("Judges").Attribute("eachMark").Value;

            mutilRep.DataSource = mutilElements;
            mutilRep.DataBind();

            judgeRep.DataSource = judgeElements;
            judgeRep.DataBind();



        }

        private XElement GetRootElement()
        {
            int stuNum = int.Parse(Request.QueryString["stuNum"]);
            return XElement.Load(GetXMLFilePath(stuNum));
        }

        private string GetXMLFilePath(int stuNum)
        {
       
            return Server.MapPath("~/Students/StuExamFiles/" + stuNum + ".xml");
        }


        protected void singleRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int stuNum = int.Parse(Request.QueryString["stuNum"]);
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
                    if (ds.Any(p => p.SetTrue == l.Value))
                        l.Selected = true;

                }
            }
        }


    }
}
