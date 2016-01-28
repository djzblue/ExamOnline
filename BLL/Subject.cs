using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;
using System.ComponentModel;
using System.Web;
namespace SDPTExam.BLL
{

    [DataObject(true)]
    /// <summary>
    /// 负责课题信息，临时课题信息，临时课题申报信息，课题申报等
    /// </summary>
    public class Subject:BLLBase
    {
     
        #region 处理课题信息的方法
               /// <summary>
        /// 根据部门ID返回所有的临时课题列表
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public static IList<SubjectInfo> GetTempSubjectsByDeptID(int deptID)
        {
             ExamDbDataContext dc = DataAccess.CreateDBContext();
            var subjects = from s in dc.SubjectInfo
                           where s.DepartmentID == deptID && s.IsFormal == false
                           select s;
            return ToList<SubjectInfo>(subjects);
        }


        /// <summary>
        /// 根据部门ID和作者id返回所有的临时课题列表
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public static IList<SubjectInfo> GetTempSubjectsByAuthorID(int authorID,bool isStudent, int deptID)
        {
           return GetSubjectsByAuthorID(authorID, isStudent, deptID, false);
        }

        /// <summary>
        /// 根据部门ID和作者id返回所有的课题列表
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public static IList<SubjectInfo> GetSubjectsByAuthorID(int authorID, bool isStudent, int deptID,bool isFormal)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var subjects = from s in dc.SubjectInfo
                           where s.DepartmentID == deptID && s.IsFormal == isFormal && s.AuthorID == authorID && s.AuthorIsStudent == isStudent
                           select s;
            return ToList<SubjectInfo>(subjects);
        }

        /// <summary>
        /// 根据部门ID和作者id返回所有的课题列表
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public static IList<SubjectInfo> GetSubjectsByAuthorID(int authorID, bool isStudent, int deptID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var subjects = from s in dc.SubjectInfo
                           where s.DepartmentID == deptID &&  s.AuthorID == authorID && s.AuthorIsStudent == isStudent
                           select s;
            return ToList<SubjectInfo>(subjects);
        }
        /// <summary>
        /// 根据指定id返回对应临时课题
        /// </summary>
        /// <param name="id">课题id</param>
        /// <returns></returns>
        public static SubjectInfo GetTempSubjectByID(int id,out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext(); 
            var subjects = from s in dc.SubjectInfo
                           where s.SubjectID == id && s.IsFormal == false
                           select s;
            d=dc;
            return ToSingle<SubjectInfo>(subjects);
            //SubjectInfo s = dc.SubjectInfo.Single(p => p.SubjectID == id);
            //return s;
        }

             /// <summary>
        /// 根据指定id返回对应临时课题
        /// </summary>
        /// <param name="id">课题id</param>
        /// <returns></returns>
        public static SubjectInfo GetTempSubjectByID(int id)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext(); 
            var subjects = from s in dc.SubjectInfo
                           where s.SubjectID == id && s.IsFormal == false
                           select s;
            return ToSingle<SubjectInfo>(subjects);
            //SubjectInfo s = dc.SubjectInfo.Single(p => p.SubjectID == id);
            //return s;
        }  
        
        /// <summary>
        /// 根据指定id返回对应课题,不管是正式的还是临时的
        /// </summary>
        /// <param name="id">课题id</param>
        /// <returns></returns>
        public static SubjectInfo GetSubjectByID(int id)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var subjects = from s in dc.SubjectInfo
                           where s.SubjectID == id 
                           select s;
            return ToSingle<SubjectInfo>(subjects);
           
        }  

        /// <summary>          
        /// 插入课题信息
        /// </summary>
        /// <param name="s">课题实体</param>
        public static int InsertSubject(SubjectInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            if(dc.SubjectInfo!=null&&dc.SubjectInfo.Any(p => p.Title == s.Title)) return 0;
            dc.SubjectInfo.InsertOnSubmit(s);
            dc.SubmitChanges();
            PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
            return s.SubjectID;
        }
        /// <summary>
        /// 删除课题信息
        /// </summary>
        /// <param name="s">课题实体</param>
        public static void DeleteSubject(SubjectInfo s, ExamDbDataContext dc)
        { 
            AttachInfo<SubjectInfo>(dc.SubjectInfo,s);
              
                
            dc.SubjectInfo.DeleteOnSubmit(s);
            dc.SubmitChanges();
        }


        /// <summary>
        ///通过id删除课题信息
        /// </summary>
        /// <param name="subjectID">课题实体ID</param>
        public static void DeleteSubject(int subjectID)
        { 
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            SubjectInfo s = dc.SubjectInfo.Single(p => p.SubjectID == subjectID);
            DeleteSubject(s, dc);
            PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
        }
        
        /// <summary>
        /// 删除某系的所有课题
        /// </summary>
        /// <param name="studentID"></param>
        public static void DeleteAllSubjects(int deptID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var stus = dc.SubjectInfo.Where(p => p.DepartmentID == deptID);
            dc.SubjectInfo.DeleteAllOnSubmit(stus);
            dc.SubmitChanges();
            PurgeCacheItems("AllSubjects_"+deptID.ToString());
        }
        /// <summary>
        /// 更新正式课题信息
        /// </summary>
        /// <param name="s">课题实体</param>
        public static void UpdateSubject(SubjectInfo s)
        { 
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<SubjectInfo>(dc.SubjectInfo, s);
            PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }

        /// <summary>
        /// 更新正式课题信息
        /// </summary>
        /// <param name="s">课题实体</param>
        public static void UpdateSubject(SubjectInfo s, ExamDbDataContext dc)
        {
            AttachInfo<SubjectInfo>(dc.SubjectInfo, s);
            dc.SubmitChanges();

            PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
        }

        /// <summary>
        /// 根据部门ID返回相应的正式课题列表
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public static IList<SubjectInfo> GetFormalSubjectsByDepartmentID(int deptID)
        {

             string key = "AllSubjects_" + deptID.ToString();
             if (MyCache[key] != null)
             {
                 return (IList<SubjectInfo>)MyCache[key];
             }
             else
             {
                 ExamDbDataContext dc = DataAccess.CreateDBContext();
                 var subjects = from s in dc.SubjectInfo
                                where s.DepartmentID == deptID && s.IsFormal == true
                                select s;
                 IList<SubjectInfo> subs=ToList<SubjectInfo>(subjects);
                  if(subs!=null) MyCache[key] =subs;
                  return subs;
             }
        }


        /// <summary>
        /// 根据专业ID返回相应的正式课题列表
        /// </summary>
        /// <param name="majorID">专业ID</param>
        /// <returns></returns>
        public static IList<SubjectInfo> GetFormalSubjectsByMajorID(int majorID)
        {

            string key = "MajorSubjects_" + majorID.ToString();
            if (MyCache[key] != null)
            {
                return (IList<SubjectInfo>)MyCache[key];
            }
            else
            {
                ExamDbDataContext dc = DataAccess.CreateDBContext();
                var subjects = from s in dc.SubjectInfo
                               where s.MajorID==majorID&& s.IsFormal == true
                               select s;
                IList<SubjectInfo> subs = ToList<SubjectInfo>(subjects);
                if (subs != null) MyCache[key] = subs;
                return subs;
            }
        }

           
        /// <summary>
        /// 根据标题名称返回对应的正式课题
        /// </summary>
        /// <param name="n">标题名称</param>
        /// <returns></returns>
        public static IList<SubjectInfo> GetFormalSubjectsByTitle(string n,int dept,int majorID)
        {
             ExamDbDataContext dc = DataAccess.CreateDBContext();
            var subjects = from s in dc.SubjectInfo
                           where s.Title.Contains(n)&&s.IsFormal==true&&s.DepartmentID==dept&&(majorID==0?true:s.MajorID==majorID)
                           select s;
            return ToList<SubjectInfo>(subjects);
        }

        /// <summary>
        /// 根据课题作者返回对应的正式课题
        /// </summary>
        /// <param name="n">标题名称</param>
        /// <returns></returns>
        public static IList<SubjectInfo> GetFormalSubjectsByAuthorName(string n, int dept,int majorID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var subjects = from s in dc.SubjectInfo
                           where s.AuthorName.IndexOf(n) != -1 && s.IsFormal == true && s.DepartmentID == dept && (majorID == 0 ? true : s.MajorID == majorID)
                           select s;
            return subjects.ToList<SubjectInfo>();
        }

       /// <summary>
        /// 根据指定id返回对应课题
        /// </summary>
        /// <param name="id">课题id</param>
        /// <returns></returns>
        public static SubjectInfo GetFormalSubjectByID(int id)
        {
             ExamDbDataContext dc = DataAccess.CreateDBContext(); var subjects = from s in dc.SubjectInfo
                           where s.SubjectID == id&&s.IsChecked==true
                           select s;
            return ToSingle<SubjectInfo>(subjects);
            //SubjectInfo s = dc.SubjectInfo.Single(p => p.SubjectID == id);
            //return s;
        }
        /// <summary>
        /// 根据指定id返回对应课题
        /// </summary>
        /// <param name="id">课题id</param>
        /// <returns></returns>
        public static SubjectInfo GetFormalSubjectByID(int id,out ExamDbDataContext d)
        {
             ExamDbDataContext dc = DataAccess.CreateDBContext(); var subjects = from s in dc.SubjectInfo
                           where s.SubjectID == id&&s.IsChecked==true
                           select s;
             d = dc;
            return ToSingle<SubjectInfo>(subjects);
            //SubjectInfo s = dc.SubjectInfo.Single(p => p.SubjectID == id);
            //return s;
        }
        /// <summary>
        /// 通过excel文件批量导入课题数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool InsertSubjectsByExcleFile(string filePath)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            IList<SubjectInfo> subjects = ChangeExcelToSubjects(filePath);
            // dc.SubjectInfo.AttachAll<SubjectInfo>(teachers);
            if (subjects == null) return false;
            dc.SubjectInfo.InsertAllOnSubmit<SubjectInfo>(subjects);
            dc.SubmitChanges();
            
            return true;
        }

        /// <summary>
        /// 将excel文件中的数据转换成课题实体集合
        /// </summary>
        /// <param name="filePath">excel文件的物理路径</param>
        /// <returns></returns>
        private static IList<SubjectInfo> ChangeExcelToSubjects(string filePath)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            IList<SubjectInfo> subjects = new List<SubjectInfo>();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            if (filePath.EndsWith("xlsx")) strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";

            string deptName = "";
            string majorName = "";
            OleDbConnection myConn = new OleDbConnection(strConn);
            string strCom = " SELECT * FROM [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(strCom, myConn);
                myConn.Open();
                OleDbDataReader dr = cmd.ExecuteReader();
            try
            {
                
                while (dr.Read())
                {
                    SubjectInfo s = new SubjectInfo();
                    s.Title = dr["课题名称"].ToString();
                    
                    
                    s.Description = dr["课题简介"].ToString();
                    s.Reference = dr["参考文献"].ToString();

                    deptName = dr["系别"].ToString();

                    s.DepartmentID = ReturnID(deptDic, deptName);

                    majorName = dr["所属专业"].ToString();

                    s.MajorID = ReturnID(majorDic, majorName);

                    s.AuthorName = "管理员";
                    s.IsChecked = true;
                    s.IsFormal = true;
                    s.AuthorID = 0;

                    if (s.MajorID == 0 || s.DepartmentID == 0)
                        throw new Exception("系别或专业名称中有错，请按照提示填写！");

                    //if (GetFormalSubjectsByTitle(s.Title, s.DepartmentID) != null)
                    //{
                    //    throw new Exception("与课题库中存在重复的课题名称，请修改再次导入！");                       
                    //}
                    subjects.Add(s);

                    //    HttpContext.Current.Response.Write("一条记录<br>");

                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("错误信息：" + e.Message);
                subjects = null;
            }
            finally
            {
              if(dr!=null)dr.Close();
                myConn.Close();
               
            } return subjects;
        }
        #endregion


    }

}
