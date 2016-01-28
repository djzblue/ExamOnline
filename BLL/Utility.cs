using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using System.Web;
using SDPTExam.DAL.Model;
using System.Data.OleDb;
using SDPTExam.DAL.Linq;
using System.IO;
namespace SDPTExam.BLL
{
  
    public static class Utility
    {


        #region 批量处理试题信息


        /// <summary>
        /// 将excel文件中的数据转换成选择题实体集合
        /// </summary>
        /// <param name="filePath">excel文件的物理路径</param>
        /// <returns></returns>
       private static IList<ChoiceItemInfo> ChangeExcelToChoices(string filePath)
        {

            IList<ChoiceItemInfo> choiceitems = new List<ChoiceItemInfo>();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            if (filePath.EndsWith("xlsx")) strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";

            OleDbConnection myConn = new OleDbConnection(strConn);
            string strCom = " SELECT * FROM [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(strCom, myConn);
            OleDbDataReader dr = null;
            try
            {
                myConn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //if(dr["姓名"]==null||dr["学号"]==null||dr["班级"]==null||dr["专业"]==null|| dr["家庭地址"]==null)
                    //   continue;
                    string sAnswers = "";                    
                    SelectChoiceInfo s = new SelectChoiceInfo();
                    s.Title=dr["题目"].ToString();
                    sAnswers = dr["答案"].ToString();
                    if (sAnswers.Length == 1) s.IsSingleSelect = true;
                    else s.IsSingleSelect = false;
                    s.ChoiceCount = 4;


                    s.CourseID = BLLBase.ReturnID(BLLBase.GetCourseDic(), dr["课程"].ToString());
                    s.ChapterID = BLLBase.ReturnID(BLLBase.GetChapterDic(), dr["章节"].ToString());

                   int choiceID= Choice.InsertSelectChoice(s);

                   string[] choiceNames = { "A", "B", "C", "D" };
                   for (int i = 0; i < 4; i++)
                   {
                      string m= choiceNames[i];
                       ChoiceItemInfo c = new ChoiceItemInfo();
                      c.Title= dr[m].ToString();
                      if (sAnswers.Contains(m))
                          c.IsRight = true;
                      else c.IsRight = false;
                      c.SelectChoiceID = choiceID;

                      choiceitems.Add(c);

                   }

  //   HttpContext.Current.Response.Write("一条记录<br>");

                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("错误信息：" + e.Message);
                choiceitems = null;

            }
            finally
            {
                if (dr != null) dr.Close();
                myConn.Close();

            } return choiceitems;
        }


        /// <summary>
        /// 通过excel文件批量倒入判断题基本数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool InsertJudgesByExcleFile(string filePath)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            IList<JudgeInfo> judges = ChangeExcelToJudges(filePath);
            if (judges == null) return false;
            dc.JudgeInfo.InsertAllOnSubmit<JudgeInfo>(judges);
            dc.SubmitChanges();
       
            return true;
        }


        /// <summary>
        /// 通过excel文件批量倒入判断题基本数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool InsertChoiceItemsByExcleFile(string filePath)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            IList<ChoiceItemInfo> choiceitems = ChangeExcelToChoices(filePath);
            if (choiceitems == null) return false;
            dc.ChoiceItemInfo.InsertAllOnSubmit<ChoiceItemInfo>(choiceitems);
            dc.SubmitChanges();
            return true;
        }


        private static IList<JudgeInfo> ChangeExcelToJudges(string filePath)
        {

            IList<JudgeInfo> judges = new List<JudgeInfo>();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            if (filePath.EndsWith("xlsx")) strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";

            OleDbConnection myConn = new OleDbConnection(strConn);
            string strCom = " SELECT * FROM [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(strCom, myConn);
            OleDbDataReader dr = null;
            try
            {
                myConn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //if(dr["姓名"]==null||dr["学号"]==null||dr["班级"]==null||dr["专业"]==null|| dr["家庭地址"]==null)
                    //   continue;
                    string sAnswers = "";
                    JudgeInfo s = new JudgeInfo();
                    s.Title = dr["题目"].ToString();
                    sAnswers = dr["答案"].ToString();

                    s.CourseID = BLLBase.ReturnID(BLLBase.GetCourseDic(), dr["课程"].ToString());
                    s.ChapterID = BLLBase.ReturnID(BLLBase.GetChapterDic(), dr["章节"].ToString());

                    if (sAnswers == "对") s.RightAnswer = true;
                    else s.RightAnswer = false;
                    judges.Add(s);
                    //   HttpContext.Current.Response.Write("一条记录<br>");

                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("错误信息：" + e.Message);
                judges = null;

            }
            finally
            {
                if (dr != null) dr.Close();
                myConn.Close();

            }
            
            return judges;
        }

        #endregion

        public static bool ModifyConfig(XmlDocument xmldoc, string appName, string appValue)
        {

            if (xmldoc == null) return false;

            if (xmldoc.DocumentElement.Name.Equals("appsettings", StringComparison.OrdinalIgnoreCase))
            {
                foreach (XmlElement xmlElement in xmldoc.DocumentElement.ChildNodes)
                {
                    if (appName.Equals(xmlElement.Attributes["key"].InnerXml)) //键值
                    {
                        xmlElement.Attributes["value"].Value = appValue;

                        break;
                    }


                }
                return true;

            }
            XmlNodeList DocdNodeNameArr = xmldoc.DocumentElement.ChildNodes;//文档节点名称数组
            foreach (XmlNode DocXmlElement in DocdNodeNameArr)
            {
                if (DocXmlElement is XmlComment) continue;//如果是注释节点，跳过
                if (DocXmlElement.Name.ToLower() == "appsettings")//找到名称为 appsettings 的节点
                {
                    XmlNodeList KeyNameArr = DocXmlElement.ChildNodes;//子节点名称数组
                    if (KeyNameArr.Count > 0)
                    {
                        foreach (XmlElement xmlElement in KeyNameArr)
                        {
                            if (appName.Equals(xmlElement.Attributes["key"].InnerXml)) //键值
                            {
                                xmlElement.Attributes["value"].Value = appValue;

                                break;
                            }


                        }
                    }

                }
            }

            return true;

        }


        /// <summary>
        /// 修改某个部门的某个配置信息
        /// </summary>
        /// <param name="departmentID">部门id</param>
        /// <param name="elementName">配置元素名</param>
        /// <returns></returns>
        public static string GetConfigValue(int departmentID, string elementName)
        {
            XElement deptElement = GetDeptmentElement(departmentID);

            if (deptElement == null) return null;
            IEnumerable<XElement> element = deptElement.Elements(elementName);

            if (element == null || element.Any() == false) return null;

            XElement modifyElement = element.First();

            return modifyElement.Value;


        }

        /// <summary>
        /// 获取Department节点
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        private static XElement GetDeptmentElement(int departmentID)
        {
            string xmlFilePath = GetConfigPath();

            XElement xe = XElement.Load(xmlFilePath);

            IEnumerable<XElement> element = from e in xe.Elements("Department")
                                            where e.Attribute("ID").Value == departmentID.ToString()
                                            select e;


            if (element == null||element.Any()==false) return null;

            XElement deptElement = element.First();
            return deptElement;
        }

        private static string GetConfigPath()
        {
            string vpath = ConfigurationManager.AppSettings["ConfigFilePath"];
            string xmlFilePath = HttpContext.Current.Server.MapPath(vpath);
            return xmlFilePath;
        }

        /// <summary>
        /// 修改某个部门的某个配置信息
        /// </summary>
        /// <param name="departmentID">部门id</param>
        /// <param name="elementName">配置元素名</param>
        /// <param name="elementValue">新值</param>
        /// <returns></returns>
        public static bool ModifyConfig(int departmentID, string elementName, string elementValue)
        {
            string xmlFilePath = GetConfigPath();

            XElement xe = XElement.Load(xmlFilePath);

            IEnumerable<XElement> element = from e in xe.Elements("Department")
                                            where e.Attribute("ID").Value == departmentID.ToString()
                                            select e;

            if (element == null || element.Any() == false) return false;

            XElement deptElement = element.First();

            XElement modifyElement = deptElement.Elements(elementName).First();

            modifyElement.Value = elementValue;

            xe.Save(xmlFilePath);

            return true;
        }

        /// <summary>
        /// 增加一个部门配置信息节点
        /// </summary>
        /// <param name="departmentID">部门id</param>
        /// <returns></returns>
        public static bool AddDepartmentConfig(int departmentID,string deptName)
        {
            string xmlFilePath= GetConfigPath();;
            XElement xe= XElement.Load(xmlFilePath);;
            AddDeptNode(departmentID, deptName, xe);
            xe.Save(xmlFilePath);
            return true;

        }

        private static void AddDeptNode(int departmentID, string deptName,XElement xe)
        {
         
            TimeLimitInfo t = new TimeLimitInfo();

            t.startTime = DateTime.Now;
            t.endTime = DateTime.Now.AddDays(2);

            int tid = BaseData.InsertTimeLimit(t);

            XElement deptElement = new XElement("Department", new XElement("SubjectSelectTimitLimitID", tid.ToString()), new XElement("UploadedFilePath", "~/Files/" + deptName),
                 new XElement("HesitationDays", "2"), new XElement("MessageDays", "2"), new XElement("SubjectManagedByTeacher", "true"),
                 new XElement("HasToSelectedSubjectBefore", "false"));
            deptElement.SetAttributeValue("ID", departmentID);
            deptElement.SetAttributeValue("name", deptName);

            xe.Add(deptElement);

            
        }


        /// <summary>
        /// 重置配置文件
        /// </summary>
        /// <returns></returns>
        public static bool ReBuildConfigFile()
        {
            string xmlFilePath=GetConfigPath();
            XElement xe = XElement.Load(xmlFilePath);

            foreach (XElement x in xe.Descendants("Department"))
            {
               string tid= x.Elements("SubjectSelectTimitLimitID").First().Value;
               if (tid != null)
               {
                   int timeLimitID = int.Parse(tid);
                   BaseData.DeleteTimeLimit(timeLimitID);
               }
            }
            xe.RemoveAll();
            xe.Name = "config";

          SDPTExam.DAL.Linq.ExamDbDataContext dc=  DataAccess.CreateDBContext();
          

          foreach (DepartmentInfo d in dc.DepartmentInfo)
          {
              
              AddDeptNode(d.DepartmentID, d.DepartmentName, xe);


          }
            xe.Save(xmlFilePath);
          return true;
        
        }

        /// <summary>
        /// 删除指定部门的配置信息
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        private static bool DeleteDeptmentElement(int departmentID)
        {
            string xmlFilePath = GetConfigPath();

            try
            {
                XElement xe = XElement.Load(xmlFilePath);

                XElement deptElement = GetDeptmentElement(departmentID);

                deptElement.Remove();

                xe.Save(xmlFilePath);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 从某一集合中随机获取指定数目的元素
        /// </summary>
        /// <param name="source">指定集合</param>
        /// <param name="count">元素数目</param>
        /// <returns></returns>
        public static IList<int> GetRandomList(IList<int> source, int count)
        {

            int totalCount = source.Count;

            if (totalCount < count) return null;

            IList<int> results = new List<int>();
            Random r = new Random();

            while (true)
            {
                int j = r.Next(totalCount);
                if (results.Contains(source[j]) == false)
                    results.Add(source[j]);
                if (results.Count == count) break;//已经获取到指定数目

            }

            return results;

        }

        /// <summary>
        /// 根据选择id获取选择题结点
        /// </summary>
        /// <param name="rootElement"></param>
        /// <param name="choiceID"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static XElement GetChoiceElementByChoiceID(XElement rootElement,string choiceID, string elementName)
        {
            var ds = from s in rootElement.Element(elementName + "s").Elements(elementName)
                     where s.Attribute("choiceID").Value == choiceID
                     select s;
            if (ds == null||ds.Count()==0) return null;
           else return ds.Single<XElement>();        
        
        }

        public static XElement GetChoiceItemElementByID(XElement parentElement,string choiceitemID)
        {
            var ds = from s in parentElement.Elements("ChoiceItem")
                     where s.Attribute("choiceItemID").Value == choiceitemID
                     select s;
            if (ds == null) return null;
            else return ds.Single<XElement>();

        }

        /// <summary>
        /// 根据id获取判断题结点
        /// </summary>
        /// <param name="rootElement"></param>
        /// <param name="choiceID"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static XElement GetJudgeElementByID(XElement parentElement, string judgeID)
        {
            var ds = from s in parentElement.Elements("JudgeItem")
                     where s.Attribute("judgeID").Value == judgeID
                     select s;
            if (ds == null) return null;
            else return ds.Single<XElement>();

        }


        public static bool CheckIfFinished(string stuNum,int examID)
        {
            ExamDbDataContext dc=new ExamDbDataContext();
            if (dc.RandomExamInfo.Any(p => p.StuNum==stuNum&&p.BasicExamID==examID) == false) return false;//还没有相关记录
            RandomExamInfo r = dc.RandomExamInfo.Where(p => p.StuNum == stuNum && p.BasicExamID == examID).First();
            return (bool)r.HasFinished;

        }


        public static bool DeleteStuXMLFile(string stuNum, int basicExamID)
        {

            try
            {
                string filePath =GetStuExamFilePath(stuNum,basicExamID) ;

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch
            { return false; }

            return true;

                    
        }

        public static string GetStuExamFilePath(string stuNum,int basicExamID)
        {
            string relativePath = "~/Students/StuExamFiles/" + basicExamID+"/";

            string physicalPath = HttpContext.Current.Server.MapPath(relativePath);
            if (Directory.Exists(physicalPath) == false)
                Directory.CreateDirectory(physicalPath);


            return  physicalPath+ stuNum + ".xml";
        }

        public static string GetRelativeExamFilePath(string stuNum, int basicExamID)
        {

            string relativePath = "~/Students/StuExamFiles/" + basicExamID + "/"+stuNum + ".xml";            
                

            return relativePath;
        }



        public static bool InsertQuestionsByExcleFile(string filePath)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            if (filePath.EndsWith("xlsx")) strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";

            OleDbConnection myConn = new OleDbConnection(strConn);
            string strCom = " SELECT * FROM [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(strCom, myConn);
            OleDbDataReader dr = null;
            IList<QuestionInfo> questions = new List<QuestionInfo>();
            IList<ChoiceItemInfo> choiceitems = new List<ChoiceItemInfo>();
            try
            {
                myConn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())//在此处逐条输入试题信息
                {
                    string questionTypeName = dr["题型"].ToString();

                    if (string.IsNullOrEmpty(questionTypeName)) continue;

                    QuestionInfo q = new QuestionInfo();
                    q.Title=dr["题目内容"].ToString();
                    q.RefAnswer=dr["参考答案"].ToString();
                    string sAnswers = ""; 

                    switch (questionTypeName)
                    {
                        case "选择":
                    
                    SelectChoiceInfo s = new SelectChoiceInfo();
                    s.Title = dr["题目内容"].ToString();
                    sAnswers = dr["参考答案"].ToString();
                    if (sAnswers.Length == 1) s.IsSingleSelect = true;
                    else s.IsSingleSelect = false;
                    s.ChoiceCount = 4;


                    s.CourseID = BLLBase.ReturnID(BLLBase.GetCourseDic(), dr["课程"].ToString());
                    s.ChapterID = BLLBase.ReturnID(BLLBase.GetChapterDic(), dr["章节"].ToString());

                   int choiceID= Choice.InsertSelectChoice(s);

                   string[] choiceNames = { "A", "B", "C", "D" };
                   for (int i = 0; i < 4; i++)
                   {
                      string m= choiceNames[i];
                       ChoiceItemInfo c = new ChoiceItemInfo();
                      c.Title= dr[m].ToString();
                      if (sAnswers.Contains(m))
                          c.IsRight = true;
                      else c.IsRight = false;
                      c.SelectChoiceID = choiceID;

                      choiceitems.Add(c);

                   }
                            break;

                        case "填空":

                            break;
                        case "判断":

                            break;
                        case "简答":

                            break;
                        case "程序填空":

                            break;
                    }


                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("错误信息：" + e.Message);
                return false;

            }
            finally
            {
                if (dr != null) dr.Close();
                myConn.Close();
                

            }
            
            if (questions == null) return false;
            dc.ChoiceItemInfo.InsertAllOnSubmit<ChoiceItemInfo>(choiceitems);
            dc.SubmitChanges();
            return true;
        }
    }
}
