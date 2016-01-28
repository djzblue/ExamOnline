using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;
using System.Xml.Linq;
using System.ComponentModel;
using System.Web;
using System.IO;

namespace SDPTExam.BLL
{
    [DataObject(true)]
    /// <summary>
    /// 随机生成的试卷
    /// </summary>
   public class RandomExam : BLLBase
    {


        ///生成学生随机试卷的xml文件,返回随机试卷的ID

        public static int CreatRandomExamXMLFile(StudentInfo stu, int basicExamID)
       {
           ExamDbDataContext dc = DataAccess.CreateDBContext();
            
            BasicExamInfo exam = dc.BasicExamInfo.Single(p => p.BasicExamID == basicExamID);

            if (exam == null) return 0;
           IList<int> singleChoices = GetSingleChoicesIDs(dc, exam);

           IList<int> mutilChoices = GetMutilChoicesIDs(dc, exam);

           IList<int> judges = GetJudgesIDs(dc, exam);

           
            //将随机生成的选择题id和判断题id分别插入随机试卷实体中
            RandomExamInfo re = new RandomExamInfo();

            re.SingleChoiceIDs = getIDs(singleChoices);
            
            if (mutilChoices == null)
               re.MutilChoiceIDs = "0";
           else re.MutilChoiceIDs = getIDs(mutilChoices);
           

            re.JudgeIDs = getIDs(judges);

            re.BasicExamID = basicExamID;

            re.ExamInfoFilePath =Utility.GetRelativeExamFilePath(stu.StuNum,basicExamID);//需要相对路径，待修改！！

            re.StudentID = stu.StudentID;

            re.ClassID = (int)stu.ClassID;

            re.InExaming = true;

            re.LoginTimes = 1;

            re.HasFinished = false;

            re.StartTime = DateTime.Now;

            re.TimeUse = exam.TimeUse;

            re.IsVisitor = (stu.StudentID == 0);

            re.StuNum = stu.StuNum;


            int randomExamID = RandomExam.InsertRandomExam(re);
            //XDocument xdoc=new XDocument(
            XElement xroot = new XElement("StudentExam");

            xroot.SetAttributeValue("stuNum", stu.StuNum);

            xroot.SetAttributeValue("basicExamID", basicExamID);

            xroot.SetAttributeValue("randomExamID", randomExamID);

            xroot.SetAttributeValue("getMark",0);

            //xroot.SetAttributeValue("timeLeft", re);

            xroot.SetAttributeValue("endTime", "");
            xroot.SetAttributeValue("loginNum", "1");

            //随机获取指定数量的单项选择题,假设要5条单项题


            //if (singleChoices == null || mutilChoices == null || judges == null) return 0;

            //if (singleChoices.Count == 0 || mutilChoices.Count == 0 || judges.Count == 0) return 0;      

            


            XElement singlesElement= GetChoices(dc, exam, singleChoices,"SingleChoice");

            xroot.Add(singlesElement);

            XElement mutilsElement = GetChoices(dc, exam, mutilChoices, "MutilChoice");

            if (mutilChoices != null) xroot.Add(mutilsElement);

            XElement judgesElement = GetJudgesELement(dc, exam, judges);

            xroot.Add(judgesElement);

            xroot.Save(Utility.GetStuExamFilePath(stu.StuNum,basicExamID));

            return randomExamID;
        
        }



        /// <summary>
        /// 看看选择的项是否正确
        /// </summary>
        /// <param name="choiceItemID"></param>
        /// <param name="isSelected"></param>
        /// <returns></returns>
        public static bool CheckChoiceItemAnswerIsRight(int choiceItemID, bool isSelected)
        {
            ExamDbDataContext dc = new ExamDbDataContext();
           ChoiceItemInfo ci= dc.ChoiceItemInfo.Single(p => p.ChoiceItemID == choiceItemID);

           if (ci.IsRight == isSelected) return true;
           else return false;


        }


        /// <summary>
        /// 看看判断题是否正确
        /// </summary>
        /// <param name="choiceItemID"></param>
        /// <param name="isSelected"></param>
        /// <returns></returns>
        public static bool CheckJudgeAnswerIsRight(int judgeID, bool setTrue)
        {
            ExamDbDataContext dc = new ExamDbDataContext();
           JudgeInfo ci = dc.JudgeInfo.Single(p => p.JudgeID==judgeID);

            if (ci.RightAnswer == setTrue) return true;
            else return false;


        }

        public static bool CheckZeroRandomExam()
        {
            ExamDbDataContext dc = new ExamDbDataContext();

            var exams = dc.RandomExamInfo.Where(p => (DateTime)p.EndTime < DateTime.Now && p.HasFinished == false && p.TotalGetMark == 0);

            foreach (var r in exams)
            {
                r.HasFinished = true;
                r.InExaming = false;
                //if(File.Exists(r.ExamInfoFilePath))
                //SetTotalMark(XElement.Load(r.ExamInfoFilePath), 0, 0, 0, true);
                
            }
            dc.SubmitChanges();
           // singleChoicesElement.Attribute("getMark").Value;
            return true;
        }

        public static void setXMLMark(XElement xe)
        {

            XElement singleChoicesElement = xe.Element("SingleChoices");

            XElement mutilChoicesElement = xe.Element("MutilChoices");

            XElement judgesElement = xe.Element("Judges");

          //  bool isRight=true;

            foreach (XElement sx in singleChoicesElement.Elements("ChoiceItem"))
            { 
               // isRight=isRight&&bool.Parse(sx.Attribute("isSelected").Value)RandomExam.CheckChoiceItemAnswerIsRight(int.Parse(
            }
        }

       /// <summary>
       /// 设置总分数
       /// </summary>
       /// <param name="xe"></param>
       /// <param name="singleRightNum"></param>
       /// <param name="mutilRightNum"></param>
       /// <param name="judgeNum"></param>
       public static void SetTotalMark(XElement xe, int singleRightNum, int mutilRightNum, int judgeNum,bool isTimeOut)
        {

            XElement singleChoicesElement = xe.Element("SingleChoices");

            XElement mutilChoicesElement = xe.Element("MutilChoices");

            XElement judgesElement = xe.Element("Judges");

            int sGetMark = GetAndSetMark(singleChoicesElement, singleRightNum,isTimeOut);

            int mGetMark = GetAndSetMark(mutilChoicesElement, mutilRightNum,isTimeOut);

            int jGetMark = GetAndSetMark(judgesElement, judgeNum,isTimeOut);

            int totalGetMark = sGetMark + mGetMark + jGetMark;

           if(isTimeOut==false) xe.SetAttributeValue("getMark", totalGetMark);

            ExamDbDataContext dc;
            RandomExamInfo r = RandomExam.GetRandomExamByID(int.Parse(xe.Attribute("randomExamID").Value),out dc);

            r.SingleGetMark = sGetMark;
            r.MutilGetMark = mGetMark;
            r.JudgeGetMark = jGetMark;
            r.TotalGetMark = totalGetMark;

            r.EndTime = DateTime.Now;

            r.HasFinished = true;
            r.InExaming = false;
           // r.TimeLeft = 0;
           dc.SubmitChanges();
        }

        /// <summary>
        /// 获取每一大题的的分数
        /// </summary>
        /// <param name="theElement"></param>
        /// <param name="rightNum"></param>
        /// <returns></returns>
        private static int GetAndSetMark(XElement theElement, int rightNum,bool isTimeOut)
        {
            int getMark = 0;
            if (theElement == null) return 0;
            if (isTimeOut == false)
            {
                int eachMark = int.Parse(theElement.Attribute("eachMark").Value);

                getMark = eachMark * rightNum;

                theElement.SetAttributeValue("getMark", getMark);
            }
            else getMark = int.Parse(theElement.Attribute("getMark").Value);


            return getMark;
        }

        /// <summary>
        /// 将id列表转换成字符串
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        private static string getIDs(IList<int> idList)
        {
            string ids = "";

            foreach (int s in idList)
            {
                ids += s + ",";
            }

            return ids;
        }


        private static XElement GetJudgesELement(ExamDbDataContext dc, BasicExamInfo exam, IList<int> judgesIDs)
        {
            XElement judgesElement = new XElement("Judges");

            judgesElement.SetAttributeValue("count", exam.JudgeNum);

            judgesElement.SetAttributeValue("eachMark", exam.JudgeMark);

            judgesElement.SetAttributeValue("getMark", 0);

            foreach (int judgeID in judgesIDs)
            {
                JudgeInfo j = dc.JudgeInfo.Single(p => p.JudgeID == judgeID);

                XElement judgeElement = new XElement("JudgeItem");

                judgeElement.SetAttributeValue("judgeID",j.JudgeID);

                judgeElement.SetAttributeValue("title", j.Title);

                judgeElement.SetAttributeValue("setTrue", "-1");

                judgesElement.Add(judgeElement);
            
            }

            return judgesElement;
        
        }

        /// <summary>
        /// 建立选择题结点，包括单选和多选
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="exam"></param>
        /// <param name="choiceIDs"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        private static XElement GetChoices(ExamDbDataContext dc, BasicExamInfo exam, IList<int> choiceIDs,string elementName)
        {
            if (choiceIDs == null) return null;
            XElement choicesElement = new XElement(elementName+"s");

            if (elementName == "SingleChoice")
            {
                choicesElement.SetAttributeValue("count", exam.SingChoiceNum);

                choicesElement.SetAttributeValue("eachMark", exam.SingChoiceMark);
            }
            else
            {
                choicesElement.SetAttributeValue("count", exam.MutilChoiceNum);

                choicesElement.SetAttributeValue("eachMark", exam.MutilChoiceMark);
            }

            choicesElement.SetAttributeValue("getMark", 0);

           
            foreach (int choiceID in choiceIDs)
            {
                //每一选择题都创建一个xml结点

                SelectChoiceInfo s = dc.SelectChoiceInfo.Single(p => p.SelectChoiceID == choiceID);

                XElement choice = GetChoice(dc, choiceID, s, elementName);

                choicesElement.Add(choice);
            }

            return choicesElement;
        }

        private static XElement GetChoice(ExamDbDataContext dc, int ChoiceID, SelectChoiceInfo s,string parentName)
        {
            XElement choice = new XElement(parentName);

            choice.SetAttributeValue("choiceID", s.SelectChoiceID);

            choice.SetAttributeValue("title", s.Title);

            var choices = dc.ChoiceItemInfo.Where(p => p.SelectChoiceID == ChoiceID).ToList<ChoiceItemInfo>();

            //如何打乱次序？
            IList<int> indexs = new List<int> { 0, 1, 2, 3 }; //假设都是只有四个选项。

            IList<int> items = Utility.GetRandomList(indexs, 4);//假设都是只有四个选项。

            IList<string> seqs = new List<string> { "A. ","B. ","C. ","D. " }; //假设都是只有四个选项。

            for(int i=0;i<4;i++)///如何加上A,B,C,D的序号
            {
                ChoiceItemInfo c = choices[items[i]];
                XElement choiceItem = new XElement("ChoiceItem");
                choiceItem.SetAttributeValue("choiceItemID",c.ChoiceItemID);
                choiceItem.SetAttributeValue("title",seqs[i]+c.Title);
                choiceItem.SetAttributeValue("isSelected", "false");
                choice.Add(choiceItem);
            }
            return choice;
        }


        private static IList<int> GetJudgesIDs(ExamDbDataContext dc, BasicExamInfo exam)
        {
            var allJudgeIDs = dc.JudgeInfo.Where(p=>exam.ChapterID == 0 ? p.CourseID == exam.CourseID : p.ChapterID == exam.ChapterID).Select(p => p.JudgeID).ToList();
            int totalCount = allJudgeIDs.Count();
            IList<int> judges = Utility.GetRandomList(allJudgeIDs, (int)exam.JudgeNum);
            return judges;
        }

        private static IList<int> GetMutilChoicesIDs(ExamDbDataContext dc, BasicExamInfo exam)
        {
            var allMutilChoiceIDs = dc.SelectChoiceInfo.Where(p => p.IsSingleSelect == false&&(exam.ChapterID == 0 ? p.CourseID == exam.CourseID : p.ChapterID == exam.ChapterID)).Select(p => p.SelectChoiceID).ToList();
            if (allMutilChoiceIDs == null || allMutilChoiceIDs.Count() == 0) return null;
            int totalCount = allMutilChoiceIDs.Count();
            IList<int> mutilChoices = Utility.GetRandomList(allMutilChoiceIDs, (int)exam.MutilChoiceNum);

            return mutilChoices;
        }

        private static IList<int> GetSingleChoicesIDs(ExamDbDataContext dc, BasicExamInfo exam)
        {
            var allsingleChoiceIDs = dc.SelectChoiceInfo.Where(p => p.IsSingleSelect == true&&(exam.ChapterID==0?p.CourseID==exam.CourseID:p.ChapterID==exam.ChapterID)).Select(p => p.SelectChoiceID).ToList();

            int totalCount = allsingleChoiceIDs.Count();

            IList<int> singleChoices = Utility.GetRandomList(allsingleChoiceIDs, (int)exam.SingChoiceNum);

            return singleChoices;
        }

        /// <summary>          
        /// 插入随机试卷信息
        /// </summary>
        /// <param name="s">随机试卷实体</param>
        public static int InsertRandomExam(RandomExamInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            dc.RandomExamInfo.InsertOnSubmit(s);

            dc.SubmitChanges();

            PurgeCacheItems("RandomExams_");
            // PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
            return s.RandomExamID;
        }

        /// <summary>
        /// 根据指定id返回对应随机试卷
        /// </summary>
        /// <param name="id">随机试卷id</param>
        /// <returns></returns>
        public static RandomExamInfo GetRandomExamByID(int id)
        {
            ExamDbDataContext d;
           return GetRandomExamByID(id, out d);
        }

        /// <summary>
        /// 根据学生id返回对应随机试卷,一个学生只能考一次试，不同科目呢？？？
        /// </summary>
        /// <param name="stuNum">随机试卷id</param>
        /// <returns></returns>
        public static RandomExamInfo GetRandomExamByStuNumAndBasicExameID(string stuNum,int examID)
        {
            ExamDbDataContext d = DataAccess.CreateDBContext();

        //一场考试只允许学生考一次。
            var Exams = from s in d.RandomExamInfo
                        where s.StuNum==stuNum&&s.BasicExamID==examID
                        select s;
            return ToSingle<RandomExamInfo>(Exams);
        }


        /// <summary>
        /// 根据指定id返回对应随机试卷
        /// </summary>
        /// <param name="id">随机试卷id</param>
        /// <returns></returns>
        public static RandomExamInfo GetRandomExamByID(int id, out ExamDbDataContext dc)
        {
            ExamDbDataContext d = DataAccess.CreateDBContext();

            dc = d;
            var Exams = from s in dc.RandomExamInfo
                        where s.RandomExamID == id
                        select s;
            return ToSingle<RandomExamInfo>(Exams);
        }

        /// <summary>
        /// 返回所有随机试卷
        /// </summary>
        /// <param name="id">随机试卷id</param>
        /// <returns></returns>
        public static IQueryable<RandomExamInfo> GetAllExams()
        {
            string key = "RandomExamsAll";
            if (MyCache[key] != null)
            {
                return (IQueryable<RandomExamInfo>)MyCache[key];
            }
            else
            {
                ExamDbDataContext dc = DataAccess.CreateDBContext();
                var Exams = from s in dc.RandomExamInfo
                            select s;
                MyCache[key] = Exams;
                return Exams; //ToList<RandomExamInfo>(Exams);
            }
        }

        /// <summary>
        /// 返回所有随机试卷
        /// </summary>
        /// <param name="id">随机试卷id</param>
        /// <returns></returns>
        public static IQueryable<RandomExamInfo> GetRandomExamsByBasicExamID(int basicExamID)
        {
                string key = "RandomExamsByBasicId_"+basicExamID.ToString();
                if (MyCache[key] != null)
                {
                    return (IQueryable<RandomExamInfo>)MyCache[key];
                }
                else
                {
                    ExamDbDataContext dc = DataAccess.CreateDBContext();
                    var Exams = from s in dc.RandomExamInfo
                                where s.BasicExamID == basicExamID
                                select s;
                    MyCache[key] = Exams;
                    return Exams;// ToList<RandomExamInfo>(Exams);
                }
        }

        /// <summary>
        /// 根据专业id返回随机试卷
        /// </summary>
        /// <param name="id">随机试卷id</param>
        /// <returns></returns>
        public static IQueryable<RandomExamInfo> GetRandomExamsByMajorID(int majorID)
        {
                string key = "RandomExamsByMajorId_"+majorID.ToString();
                if (MyCache[key] != null)
                {
                    return (IQueryable<RandomExamInfo>)MyCache[key];
                }
                else
                {
                    ExamDbDataContext dc = DataAccess.CreateDBContext();
                    var Exams = from s in dc.RandomExamInfo
                                from p in dc.ClassInfo
                                where p.ClassID == s.ClassID && p.MajorID == majorID
                                select s;
                    MyCache[key] = Exams;
                    return Exams;// ToList<RandomExamInfo>(Exams);
                }
        }


        public static bool SetRandomExamFinished(int rExamID)
        { ExamDbDataContext dc;
       RandomExamInfo r= GetRandomExamByID(rExamID, out dc);
       if (r.HasFinished == false || (bool)r.InExaming)
       {
           r.HasFinished = true;
           r.InExaming = false;
           dc.SubmitChanges();
       }
       

            return true;
        }

        /// <summary>
        /// 根据系别id返回随机试卷
        /// </summary>
        /// <param name="id">随机试卷id</param>
        /// <returns></returns>
        public static IQueryable<RandomExamInfo> GetRandomExamsByDeptID(int deptID)
        {
             string key = "RandomExamsByDeptID_"+deptID.ToString();
             if (MyCache[key] != null)
             {
                 return (IQueryable<RandomExamInfo>)MyCache[key];
             }
             else
             {
                 ExamDbDataContext dc = DataAccess.CreateDBContext();
                 var Exams = from s in dc.RandomExamInfo
                             from p in dc.ClassInfo
                             where p.ClassID == s.ClassID && p.DepartmentID == deptID
                             select s;
                 MyCache[key] = Exams;
                 return Exams;// ToList<RandomExamInfo>(Exams);
             }
        }

        ///// <summary>
        ///// 根据基本试卷id，返回统计结果
        ///// </summary>
        ///// <param name="id">随机试卷id</param>
        ///// <returns></returns>
        //public static IList<RandomExamInfo> GetResultsByBasicExamID(int basicExamID)
        //{
        //    IList<RandomExamInfo> randomList = GetRandomExamsByBasicExamID(basicExamID);

        //    var results = from s in randomList
        //                  group s by s.ClassID;
            
        //    var rr=from r in results        
        //           select new{

                          

        //}


        /// <summary>
        /// 删除随机试卷信息
        /// </summary>
        /// <param name="s">随机试卷实体</param>
        public static void DeleteExam(RandomExamInfo s, ExamDbDataContext dc)
        {
            AttachInfo<RandomExamInfo>(dc.RandomExamInfo, s);

            if (Utility.DeleteStuXMLFile(s.StuNum, s.BasicExamID))
            {
                dc.RandomExamInfo.DeleteOnSubmit(s);
                dc.SubmitChanges();
                PurgeCacheItems("RandomExams");
            }
            else return;
        }

        /// <summary>
        ///通过id删除随机试卷信息
        /// </summary>
        /// <param name="RandomExamID">随机试卷实体ID</param>
        public static void DeleteExam(int RandomExamID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            RandomExamInfo s = dc.RandomExamInfo.Single(p => p.RandomExamID == RandomExamID);
            DeleteExam(s, dc);
           
        }


        /// <summary>
        /// 更新随机试卷信息
        /// </summary>
        /// <param name="s">随机试卷实体</param>
        public static void UpdateExam(RandomExamInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<RandomExamInfo>(dc.RandomExamInfo, s);
            // PurgeCacheItems("AllExams_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }

        /// <summary>
        /// 更新随机试卷信息
        /// </summary>
        /// <param name="s">随机试卷实体</param>
        public static void UpdateExam(RandomExamInfo s, ExamDbDataContext dc)
        {
            AttachInfo<RandomExamInfo>(dc.RandomExamInfo, s);
            dc.SubmitChanges();

            // PurgeCacheItems("AllExams_" + s.DepartmentID.ToString());
        }

        /// <summary>
        /// 根据班级id返回缺考学生名单
        /// </summary>
        /// <param name="id">随机试卷id</param>
        /// <returns></returns>
        public static IQueryable<StudentInfo> GetNotExamStusByClassID(int classID)
        {
            string key = "NotExamStusByClassID_" + classID.ToString();
            if (MyCache[key] != null)
            {
                return (IQueryable<StudentInfo>)MyCache[key];
            }
            else
            {
                ExamDbDataContext dc = DataAccess.CreateDBContext();
                var stus = from s in dc.StudentInfo                           
                            where !(from p in dc.RandomExamInfo select p.StudentID).Contains(s.StudentID)&&(classID==0||s.ClassID==classID)
                            select s;
                MyCache[key] = stus;
                return stus;// ToList<RandomExamInfo>(Exams);
            }
        }

    }
}
