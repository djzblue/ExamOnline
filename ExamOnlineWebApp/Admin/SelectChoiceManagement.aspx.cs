using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDPTExam.BLL;
using SDPTExam.DAL.Model;

namespace SDPTExam.Web.UI.Admin
{
    public partial class SelectChoiceManagement : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
        {

         
                //再增加一题，判断题目是否完全相同。。 
                int answerNum = 0;

                foreach (ListItem l in chkListAnswer.Items)
                {
                    if (l.Selected) answerNum++;
                }
                //if (chkA.Checked) answerNum++;
                //if (chkB.Checked) answerNum++;
                //if (chkC.Checked) answerNum++;
                //if (chkD.Checked) answerNum++;

                if (answerNum == 0)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('请设置答案')</script>");
                    return;
                }
            if (imgBtnSave.ToolTip == "添加")
            {
                SelectChoiceInfo s = new SelectChoiceInfo();

               s.Title = txtTitle.Text;

                if (answerNum > 1) s.IsSingleSelect = false;
                else s.IsSingleSelect = true;

                //s.RightAnswer = strAnswers;

                s.ChoiceCount = 4; //可根据输入的答案决定选项个数。

                int choiceID = Choice.InsertSelectChoice(s);

                IList<TextBox> tlist = GetTextBoxIntoList();

                InsertItemsOfChoice(choiceID, tlist);

                //InsertChoiceItem(choiceID, txtAnswerA.Text, chkA.Checked);
                //InsertChoiceItem(choiceID, txtAnswerB.Text, chkB.Checked);
                //InsertChoiceItem(choiceID, txtAnswerC.Text, chkC.Checked);
                //InsertChoiceItem(choiceID, txtAnswerD.Text, chkD.Checked);


            }

            else
            {
                int choiceId =int.Parse(hideChoiceID.Value);

                 Choice.UpdateSelectChoice(choiceId,txtTitle.Text,answerNum==1);//更新选择题目

                 IList<TextBox> tlist = GetTextBoxIntoList();
                 int i = 0;
                 foreach (TextBox t in tlist)
                 {
                    
                     int cItemID = int.Parse(t.ToolTip); 
                     ChoiceItemInfo cItem = ChoiceItem.GetChoiceItemByID(cItemID);
                     //cItem.ChoiceItemID = cItemID;
                     cItem.Title = t.Text;
                     cItem.IsRight = chkListAnswer.Items[i].Selected;
                     ChoiceItem.UpdateChoiceItemByCopy(cItem);
                     i++;
                 }
            
            }



            ClearControl();
            this.ClientScript.RegisterStartupScript(this.GetType(), "Hint", "<script>alert('成功保存数据')</script>");



        }

        private void ClearControl()
        {
            txtTitle.Text = "";
            txtAnswerA.Text = "";
            txtAnswerB.Text = "";
            txtAnswerC.Text = "";
            txtAnswerD.Text = "";
            //chkA.Checked = chkB.Checked = chkC.Checked = chkD.Checked = false;
            foreach (ListItem l in chkListAnswer.Items)
            {
                l.Selected = false;
            }
        }
        private void InsertChoiceItem(int choiceID, string title, bool isRight)
        {
            ChoiceItemInfo item = new ChoiceItemInfo();
            item.Title = title;
            item.SelectChoiceID = choiceID;
            item.IsRight = isRight;
            ChoiceItem.InsertChoiceItem(item);

            
        }

        private void InsertItemsOfChoice(int choiceID, IList<TextBox> tlist)
        {
            IList<ChoiceItemInfo> items = new List<ChoiceItemInfo>();
            int i = 0;
            foreach (TextBox t in tlist)
            {
                ChoiceItemInfo item = new ChoiceItemInfo();
                item.Title =t.Text;
                item.SelectChoiceID = choiceID;
                item.IsRight = chkListAnswer.Items[i].Selected;
                items.Add(item);
                i++;
            }
            ChoiceItem.InsertChoiceItems(items);


        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            if (string.IsNullOrEmpty(b.CommandArgument)) return;
            hideChoiceID.Value = b.CommandArgument;

            imgBtnSave.ToolTip = "修改";
            SetControlValue(int.Parse(hideChoiceID.Value));
            panelAddnew.Visible = true;
            GridView1.Visible = false;


        }


        private void SetControlValue(int choiceID)
        {
            IList<TextBox> tlist = GetTextBoxIntoList();
           
            IList<ChoiceItemInfo> cList=ChoiceItem.GetChoiceItems(choiceID);

            for(int i=0;i<cList.Count;i++)
            {
                tlist[i].Text=cList[i].Title;
                tlist[i].ToolTip=cList[i].ChoiceItemID.ToString();
               chkListAnswer.Items[i].Selected= cList[i].IsRight;
            }            
            txtTitle.Text=Choice.GetSelectChoiceByID(choiceID).Title;

            //txtTotalMark.Text);
        }

        private IList<TextBox> GetTextBoxIntoList()
        {
            IList<TextBox> tlist = new List<TextBox>();
            tlist.Add(txtAnswerA);
            tlist.Add(txtAnswerB);
            tlist.Add(txtAnswerC);
            tlist.Add(txtAnswerD);
            return tlist;
        }

        protected void btnShowAddNew_Click(object sender, EventArgs e)
        {
            ClearControl();
            imgBtnSave.ToolTip = "添加";
            panelAddnew.Visible = true;
            GridView1.Visible = false;
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            panelAddnew.Visible =false;
            GridView1.Visible = true ;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DAL.Linq.ExamDbDataContext dc = DataAccess.CreateDBContext();
            dc.ChoiceItemInfo.DeleteAllOnSubmit(dc.ChoiceItemInfo.Where(a => true));
            dc.SelectChoiceInfo.DeleteAllOnSubmit(dc.SelectChoiceInfo.Where(a => true));
            dc.SubmitChanges();
        }
    }
}
