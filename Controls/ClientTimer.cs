using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamManagement.Controls
{
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace myControl
{
    /**//// <summary>
    /// 客户端计时器clientTimer控件
    /// 在线考试系统中卷面计时所用，你可以自由修改
    /// 丛兴滋（cncxz）    2005-12-3
    /// </summary>
    [Description("客户端计时器clientTimer")]
    [Designer(typeof(clientTimerDesigner))]
    [ToolboxData("<{0}:clientTimer runat=server></{0}:clientTimer>")]
    public class clientTimer: System.Web.UI.WebControls.PlaceHolder
    {

        public onTimeOutEventHandler onTimeOut;        //超时事件
        private LinkButton myLB;
        private Label myLabel;

        #region "公共属性"

        [Browsable(true),Category("计时相关"),DefaultValue(TimeOutUnitsType.Minute),Description("计时单位，有秒、分钟、小时三种，默认为分钟。")]
        public TimeOutUnitsType TimeOutUnits
        {
            get
            {
                object obj=ViewState["TimeOutUnits"];
                return (obj==null)?TimeOutUnitsType.Minute:(TimeOutUnitsType)obj;
            }
            set
            {
                ViewState["TimeOutUnits"]=value;
            }
        }


        [Browsable(true),Category("计时相关"),DefaultValue(30),Description("计时超时时间（单位与TimeOutUnits属性一致）。")]
        public int TimeOutLength
        {
            get
            {
                object obj=ViewState["TimeOutLength"];
                return (obj==null)?30:int.Parse(obj.ToString());
            }
            set
            {
                ViewState["TimeOutLength"]=value;
            }
        }
        

        [Browsable(true),Category("计时相关"),DefaultValue(0),Description("已用去的时间（单位与TimeOutUnits属性一致）。")]
        public int PassTimeLength
        {
            get
            {
                object obj=ViewState["PassTimeLength"];
                return (obj==null)?0:int.Parse(obj.ToString());
            }
            set
            {
                ViewState["PassTimeLength"]=value;
            }
        }


        [Browsable(true),Category("行为"),DefaultValue(false),Description("是否以倒计时的方式显示友好界面，是则显示还剩多少时间，否则显示用了多少时间。")]
        public bool CountDown
        {
            get
            {
                object obj=ViewState["CountDown"];
                return (obj==null)?false:(bool)obj;
            }
            set
            {
                ViewState["CountDown"]=value;
            }
        }

        [Browsable(true),Category("行为"),DefaultValue(true),Description("是否启用计时器")]
        public bool TimerEnabled
        {
            get
            {
                object obj=ViewState["TimerEnabled"];
                return (obj==null)?true:(bool)obj;
            }
            set
            {
                ViewState["TimerEnabled"]=value;
            }
        }
        
        #endregion


        public clientTimer()
        {
            myLB=new LinkButton();            
            myLB.Click+=new EventHandler(myLB_Click);
            myLabel=new Label();
        }

        private void myLB_Click(object sender, System.EventArgs e){
            if(onTimeOut!=null){
                onTimeOut();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if(this.TimerEnabled)
            {
                myLB.ID=this.ClientID+"_LB_TimeOut";                
                myLB.Text="";

                myLabel.ID=this.ClientID+"_Label_Msg";
                myLabel.Text="";

                this.Controls.Add(myLB);
                this.Controls.Add(myLabel);
            }
            base.OnLoad(e);
            
        }

        protected override void Render(HtmlTextWriter writer) 
        {
            if(this.TimerEnabled)
            {
                switch(this.TimeOutUnits)
                {
                    case TimeOutUnitsType.Second:
                        writer.Write(this.strJS(1000," 秒"));
                        break;
                    case TimeOutUnitsType.Minute:
                        writer.Write(this.strJS(60000," 分钟"));
                        break;
                    case TimeOutUnitsType.Hour:
                        writer.Write(this.strJS(3600000," 小时"));
                        break;
                }
            }
            base.Render(writer);
        }

        private string  strJS(int intCycLength,string strUnits){
            
            string strFunction=this.ClientID+"_Timer";
            string strTimeOut=this.ClientID+"_TimeOut";
            string strPassTime=this.ClientID+"_PassTime";

            string scriptString ="\n";
            scriptString += @"<script language=""JavaScript"">"+"\n";
            scriptString += @"    <!--"+"\n";
            scriptString += "var "+strTimeOut+"="+this.TimeOutLength.ToString()+"; \n";
            scriptString += "var "+strPassTime+"="+this.PassTimeLength.ToString()+";\n var ua = navigator.userAgent.toLowerCase();\n ";

            scriptString += @"if(ua.indexOf(""msie"") > -1)window.attachEvent(""onload"", "+strFunction+");" +"\n";

            scriptString += @"else window.addEventListener(""load"", " + strFunction + ",false);" + "\n";
            scriptString +="function "+strFunction+"() {\n";
            scriptString += "    if("+strPassTime+"<"+strTimeOut+"){\n";
            scriptString += @"        //未超时"+"\n";
            scriptString += "        "+strPassTime+"+=1;\n";
            if(this.CountDown)
            {
                scriptString += "        var myNum="+strTimeOut+"-"+strPassTime+";\n";
                scriptString += @"        document.getElementById("""+this.myLabel.ClientID+@""").innerText=""剩余时间：""+myNum+"""+strUnits+@""";"+"\n";
            }
            else
            {
                scriptString += @"        document.getElementById("""+this.myLabel.ClientID+@""").innerText=""已用时间：""+"+strPassTime+@"+"""+strUnits+@""";"+"\n";
            }            
            scriptString += "    }else{\n";
            scriptString += @"        //时间到"+"\n";
            scriptString += @"        document.getElementById("""+this.myLB.ClientID+@""").click();"+"\n";
            scriptString += "    }\n";
            scriptString += @"    window.setTimeout("""+strFunction+@"()"","+intCycLength.ToString()+@");"+"\n";

            scriptString += "}\n";            

            scriptString += @"//-->"+"\n";
            scriptString += @"</script>"+"\n";
            


            return scriptString;
        }
    }



    /**//// <summary>
    /// 计时单位的类型。
    /// </summary>
    public enum TimeOutUnitsType:byte
    {
        /**//// <summary>
        /// 秒。
        /// </summary>
        Second,
        /**//// <summary>
        /// 分钟。
        /// </summary>
        Minute,
        /**//// <summary>
        /// 小时。
        /// </summary>
        Hour
    }

    public delegate void onTimeOutEventHandler();



    public class clientTimerDesigner :System.Web.UI.Design.ControlDesigner
    {
        private clientTimer CT;
        public clientTimerDesigner(){

        }
        public override string GetDesignTimeHtml()
        {
            CT=(clientTimer)Component;
            string str = "";
            str += @"<span style=""height:20px;padding:2px 10px 2px 10px;border-left:1px solid #fafafa;border-top:1px solid #fafafa;border-bottom:1px solid #d0d0d0;border-right:1px solid #d0d0d0;FILTER: progid:DXImageTransform.Microsoft.Gradient(startColorStr='#f5f5f5', endColorStr='#e5e5e5', gradientType='0');"">";
            str += CT.ID + @"</span>";
            return str;
        }
    }

    

}

}
