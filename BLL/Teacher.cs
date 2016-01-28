using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;
using System.Web;
using System.Configuration;
using System.ComponentModel;
namespace SDPTExam.BLL
{

    [DataObject(true)]
    /// <summary>
    /// 负责教师信息表，教师角色，专业负责人信息表，指导学生信息表，导师信息表的维护
    /// </summary>     
  public  class Teacher:BLLBase,IUser
    {

      
        public Teacher()
        {

        }

        #region 实现IUser接口
        /// <summary>
      /// 验证教师用户的合法性
      /// </summary>
      /// <param name="uname"></param>
      /// <param name="pwd"></param>
      /// <returns></returns>
        public bool ValidateUser(string uname, string pwd)
        {
            string pwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1");
           
             ExamDbDataContext dc = DataAccess.CreateDBContext(); 
            var teachers = from t in dc.TeacherInfo
                           where t.LoginName == uname && t.Password == pwdEncode
                           select t;
            if (teachers.Count<TeacherInfo>() > 0)
            {
                TeacherInfo t=teachers.FirstOrDefault<TeacherInfo>();
                HttpContext.Current.Session["DepartmentID"] = t.DepartmentID;
                HttpContext.Current.Session["UserRealName"] = t.TeacherName;
                HttpContext.Current.Session["UserID"] = t.TeacherID;

                if(t.MajorID==null)
                    HttpContext.Current.Session["MajorID"] = 0;
                else
                    HttpContext.Current.Session["MajorID"] = t.MajorID;
          
                HttpContext.Current.Session["IsStudent"] = false;
                if (t.IsMajorManager == true)
                {
                    HttpContext.Current.Session["Role"] = "majorManager";
                    HttpContext.Current.Session["IsMajorManager"] = true;
                }
                else HttpContext.Current.Session["Role"] = "teacher";

                string basePath = Utility.GetConfigValue(t.DepartmentID, "UploadedFilePath");             
               

                HttpContext.Current.Session["PersonalDirectory"] = basePath+ "/PersonalFiles/Teacher/"+t.TeacherName +"_"+ (t.TeacherNum==null?"N":t.TeacherNum);

                t.LoginCount = t.LoginCount + 1;

                LogInfo l = new LogInfo();
                l.LogTitle = "登录";
                l.LogContent = t.TeacherName + "老师登录成功！";
                l.LogType = "登录";
                l.AddedTime = DateTime.Now;
                dc.LogInfo.InsertOnSubmit(l);
                dc.SubmitChanges();
                return true;
            }
            else return false;
        }

        #endregion


     
        public static bool InsertTeacher(TeacherInfo teacher)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            if(dc.TeacherInfo.Any(p=>p.LoginName==teacher.LoginName))return false;
            dc.TeacherInfo.InsertOnSubmit(teacher);
            dc.SubmitChanges();
            return true;       
           
        }

        /// <summary>
        /// 将excel文件中的数据转换成教师实体集合
        /// </summary>
        /// <param name="filePath">excel文件的物理路径</param>
        /// <returns></returns>
        private static IList<TeacherInfo> ChangeExcelToTeachers(string filePath)
        {

            IList<TeacherInfo> teachers = new List<TeacherInfo>();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            if (filePath.EndsWith("xlsx")) strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";

            string deptName = "";
            OleDbConnection myConn = new OleDbConnection(strConn);
            string strCom = " SELECT * FROM [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(strCom, myConn);
            myConn.Open();
            OleDbDataReader dr = cmd.ExecuteReader();
            string defaultPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("123456", "SHA1");
            try
            {

                while (dr.Read())
                {
                    if (dr["姓名"] == null || dr["职称"] == null || dr["系别"] == null)
                        continue;
                    TeacherInfo t = new TeacherInfo();
                    t.TeacherName = dr["姓名"].ToString();

                    t.LoginName = dr["教工号"].ToString();

                    t.Password = defaultPwd;

                    t.ProfessionalTitle = dr["职称"].ToString();
                    //t.Position = dr["职务"].ToString();

                    deptName = dr["系别"].ToString();

                    t.DepartmentID = ReturnID(deptDic, deptName);

                    if (t.DepartmentID == 0)
                        throw new Exception("系别名称中有错，请按照提示填写！");

                    //else
                    //    t.DepartmentID = 10002;

                    if (dr["性别"].ToString().Equals("男"))
                        t.Sex = false;
                    else t.Sex = true;
                  
                    //t.MotherSchool = dr["毕业院校"].ToString();
                    //t.Major = dr["专业"].ToString();
                    t.ResearchField = dr["研究领域"].ToString();
      
                    t.TeacherNum = dr["教工号"].ToString();

                    teachers.Add(t);
                    //    HttpContext.Current.Response.Write("一条记录<br>");

                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("错误信息：" + e.Message);
                teachers = null;
            }
            finally
            {
                dr.Close();
                myConn.Close();

            }
            return teachers;
        }
        /// <summary>
        /// 通过excel文件批量倒入教师基本数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool InsertTeachersByExcleFile(string filePath)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            IList<TeacherInfo> teachers = ChangeExcelToTeachers(filePath);
            // dc.TeacherInfo.AttachAll<TeacherInfo>(teachers);
            if (teachers == null) return false;
            dc.TeacherInfo.InsertAllOnSubmit<TeacherInfo>(teachers);
            dc.SubmitChanges();
         
           return true;
        }

        /// <summary>
        /// 检查登录名是否已经存在
        /// </summary>
        /// <param name="loginname">登录名</param>
        /// <returns></returns>
        public static bool CheckLoginNameExisted(string loginname)
        {
             ExamDbDataContext dc = DataAccess.CreateDBContext(); 
            var teachers = from t in dc.TeacherInfo
                           where t.LoginName==loginname
                           select t;
            if (teachers.Count<TeacherInfo>() == 0)
            {

                return false;
            }




            else return true;
           
        }




      /// <summary>
      /// 根据ID值返回对应的教师实体
      /// </summary>
      /// <param name="teacherID">id值</param>
      /// <returns></returns>
        public static TeacherInfo GetTeacherByID(int teacherID)
        {
             ExamDbDataContext dc = DataAccess.CreateDBContext();
            var teachers = from t in dc.TeacherInfo
                    where t.TeacherID == teacherID
                    select t;
            if (teachers.Count<TeacherInfo>() == 0) return null;
            else return ToSingle<TeacherInfo>(teachers);
        }

        /// <summary>
        /// 根据ID值返回对应的教师实体
        /// </summary>
        /// <param name="teacherID">id值</param>
        /// <returns></returns>
        public static TeacherInfo GetTeacherByID(int teacherID, out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var teachers = from t in dc.TeacherInfo
                           where t.TeacherID == teacherID
                           select t;
            d = dc;
            if (teachers.Count<TeacherInfo>() == 0) return null;
            else return ToSingle<TeacherInfo>(teachers);
        }

      /// <summary>
      /// 获取所有教师列表
      /// </summary>
      /// <returns></returns>
        public static IList<TeacherInfo> GetAllTeachers()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            return dc.TeacherInfo.ToList<TeacherInfo>();
        }



        /// <summary>
        /// 获取所有教师列表
        /// </summary>
        /// <returns></returns>
        public static IList<TeacherInfo> GetAllTeachers(out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            d = dc;
            return dc.TeacherInfo.ToList<TeacherInfo>();
        }
      /// <summary>
      /// 根据系别ID返回教师集合
      /// </summary>
      /// <param name="depID">系别ID</param>
      /// <returns>该系的所有教师</returns>
        public static IList<TeacherInfo> GetAllTeachersByDepartmentID(int depID)
        {
             ExamDbDataContext dc = DataAccess.CreateDBContext();
            var teachers = from t in dc.TeacherInfo
                           where t.DepartmentID == depID
                           select t;
            return ToList<TeacherInfo>(teachers);
        }


        /// <summary>
        /// 根据专业ID返回教师集合
        /// </summary>
        /// <param name="depID">系别ID</param>
        /// <returns>该系的所有教师</returns>
        public static IList<TeacherInfo> GetAllTeachers(int depID,int mID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var teachers = from t in dc.TeacherInfo
                           where t.DepartmentID == depID&&( mID==0? true:t.MajorID==mID)
                           select t;
            return ToList<TeacherInfo>(teachers);
        }



        /// <summary>
        /// 更新教师实体信息
        /// </summary>
        /// <param name="s">需要更新的教师实体</param>
        public static void UpdateTeacher(TeacherInfo t)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<TeacherInfo>(dc.TeacherInfo, t);
            dc.SubmitChanges();            
        }

        /// <summary>
        /// 更新教师实体信息
        /// </summary>
        /// <param name="s">需要更新的教师实体</param>
        public static void UpdateTeacher(TeacherInfo t,  ExamDbDataContext dc)
        {
           AttachInfo<TeacherInfo>(dc.TeacherInfo, t);
            dc.SubmitChanges();
        }
      /// <summary>
      /// 删除教师信息
      /// </summary>
      /// <param name="teacherID"></param>
        public static void DeleteTeacher(int teacherID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var stus = from s in dc.StudentInfo
                       where s.TeacherID == teacherID || s.PractiseTeacherID == teacherID
                       select s;
            if(stus!=null)
            {
                foreach (StudentInfo s in stus)
                {
                    s.TeacherID = null;
                    s.PractiseTeacherID = null;
                    s.State = -1;
                }
            }
            TeacherInfo t = dc.TeacherInfo.Single(p => p.TeacherID == teacherID);
            //t.TeacherID = teacherID;
           // dc.TeacherInfo.Attach(t);
            dc.TeacherInfo.DeleteOnSubmit(t);
            dc.SubmitChanges();
        }
       






       
        /// <summary>
      /// 返回姓名集合
      /// </summary>
      /// <param name="ids">id集合</param>
      /// <returns></returns>
        public static IList<string> GetTeacherNamesByIds(IList<int> ids)
        {

            IList<string> mnames = new List<string>();
            foreach (int i in ids)
            {
                TeacherInfo t = GetTeacherByID(i);
                mnames.Add(t.TeacherName);
            }

            return mnames;
        }




      /// <summary>
      /// 插入指导信息实体


    }
}
