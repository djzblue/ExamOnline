using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;
using System.Web;
using System.ComponentModel;
namespace SDPTExam.BLL
{

    [DataObject(true)]
    /// <summary>
    /// 负责学生信息表，学生状态表的维护
    /// </summary>
    public class Student : BLLBase, IUser
    {

        #region 实现IUser接口
        /// <summary>
        /// 验证学生用户的合法性
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ValidateUser(string uname, string pwd)
        {

            string pwdEncode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1");
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var students = from s in dc.StudentInfo
                           where s.LoginName == uname && s.Password == pwdEncode
                           select s;
            if (students.Count<StudentInfo>() > 0)
            {
                StudentInfo s = students.FirstOrDefault<StudentInfo>();
                if (s == null) return false;
                HttpContext.Current.Session["DepartmentID"] = s.DepartmentID;
                HttpContext.Current.Session["UserRealName"] = s.StuName;
                HttpContext.Current.Session["UserID"] = s.StudentID;
                HttpContext.Current.Session["MajorID"] = s.MajorID;
                HttpContext.Current.Session["FullProfile"] = s.IsFullProfile;

                HttpContext.Current.Session["StuNum"] = s.StuNum;
                HttpContext.Current.Session["ClassName"] = s.Class;

                HttpContext.Current.Session["IsStudent"] = true;
                HttpContext.Current.Session["Role"] = "student";
                //HttpContext.Current.Session["PersonalDirectory"] = "~/Files/PersonalFiles/Student/"+s.DepartmentID+"/" + s.StuName + s.StudentID;
                string basePath = Utility.GetConfigValue(s.DepartmentID, "UploadedFilePath");
                HttpContext.Current.Session["PersonalDirectory"] = basePath + "/PersonalFiles/Student/" + s.StuNum + "_" + s.StuName;

                s.LoginCount = s.LoginCount + 1;

                LogInfo l = new LogInfo();
                l.LogTitle = "登录";
                l.LogContent ="学号为"+s.StuNum+ s.StuName + "同学登录成功！";
                l.LogType = "登录";
                l.AddedTime = DateTime.Now;
                dc.LogInfo.InsertOnSubmit(l);
                dc.SubmitChanges();
                return true;
            }
            else return false;
        }

        #endregion



        #region 处理学生信息

        /// <summary>
        /// 插入一个学生实体
        /// </summary>
        /// <param name="Student">学生实体</param>
        /// <returns></returns>
        public static bool InsertStudent(StudentInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            if (dc.StudentInfo.Any(p => p.LoginName == s.LoginName)) return false;
            dc.StudentInfo.InsertOnSubmit(s);
            dc.SubmitChanges();
            PurgeCacheItems("AllStudents");

            return true;

        }


        public static bool UpdateStudentPhotoPaths(string photoPath, int deptID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var stus = from s in dc.StudentInfo
                       where (deptID == 0 ? true : s.DepartmentID == deptID)
                       select s;
            foreach (var ss in stus)
            {
                ss.ImagePath = photoPath + ss.StuNum + ".jpg";
            }

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
            var stus = from s in dc.StudentInfo
                       where s.LoginName == loginname
                       select s;
            if (stus.Count<StudentInfo>() == 0)
            {

                return false;
            }
            else return true;

        }

        /// <summary>
        /// 根据id返回对应的学生实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static StudentInfo GetStudentByID(int id)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var a = from s in dc.StudentInfo
                    where s.StudentID == id
                    select s;
            return ToSingle<StudentInfo>(a);

        }


        /// <summary>
        /// 根据id返回对应的学生实体,利用缓存数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static StudentInfo GetStudentByID(int id, int deptID, int majorID)
        {
            string key = "AllStudents_" + deptID.ToString() + "_" + majorID.ToString(); ;
            if (MyCache[key] != null)
            {
                var stus = (IQueryable<StudentInfo>)MyCache[key];
                
                if (stus!=null&&stus.Any(p => p.StudentID == id))
                    return stus.Single(p => p.StudentID == id);
                else return null;
            }
            else
            {
                return GetStudentByID(id);
            }

        }

        /// <summary>
        /// 根据id返回对应的学生实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static StudentInfo GetStudentByID(int id, out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            d = dc;
            var a = from s in dc.StudentInfo
                    where s.StudentID == id
                    select s;
            return ToSingle<StudentInfo>(a);

        }

        /// <summary>
        /// 更新学生实体信息
        /// </summary>
        /// <param name="s">需要更新的学生实体</param>
        public static void UpdateStudent(StudentInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            StudentInfo ss = new StudentInfo();
            ss.CellPhone = s.CellPhone;
            ss.Class = s.Class;
            ss.Email = s.Email;
            ss.DepartmentID = s.DepartmentID;
            ss.DepartmentInfo = s.DepartmentInfo;
            ss.Grade = s.Grade;
        
         //  ss.HasSelectedExam = s.HasSelectedExam;
            ss.HomeAddress = s.HomeAddress;
            ss.HomePhoneNum = s.HomePhoneNum;
            ss.ImagePath = s.ImagePath;
            ss.IsFullProfile = s.IsFullProfile;
            ss.LoginName = s.LoginName;
            ss.MajorID = s.MajorID;
            ss.Password = s.Password;
            ss.PersonalDesc = s.PersonalDesc;
            ss.QQNum = s.QQNum;
            ss.State = s.State;
            ss.StudentID = s.StudentID;
            ss.StuName = s.StuName;
            ss.StuNum = s.StuNum;
             ss.TeacherID = s.TeacherID;

            AttachInfo<StudentInfo>(dc.StudentInfo, ss);
            dc.SubmitChanges();

            PurgeCacheItems("AllStudents");
        }

        public static void UpdateStudent(StudentInfo s, ExamDbDataContext dc)
        {

            AttachInfo<StudentInfo>(dc.StudentInfo, s);
            dc.SubmitChanges();
            PurgeCacheItems("AllStudents");
            PurgeCacheItems("StudentsToSelect_" + s.DepartmentID);
        }
        /// <summary>
        /// 根据id删除学生
        /// </summary>
        /// <param name="studentID"></param>
        public static void DeleteStudent(int studentID)
        {
            ExamDbDataContext dc; // = DataAccess.CreateDBContext();
            StudentInfo s = GetStudentByID(studentID, out dc);

            dc.StudentInfo.DeleteOnSubmit(s);
            dc.SubmitChanges();
            PurgeCacheItems("AllStudents");
        }

        /// <summary>
        /// 删除某系的所有学生
        /// </summary>
        /// <param name="studentID"></param>
        public static void DeleteAllStudents(int deptID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var stus = dc.StudentInfo.Where(p => p.DepartmentID == deptID);
            dc.StudentInfo.DeleteAllOnSubmit(stus);
            dc.SubmitChanges();
            PurgeCacheItems("AllStudents_" + deptID.ToString());
        }


        /// <summary>
        /// 删除所有学生
        /// </summary>
        /// <param name="studentID"></param>
        public static void DeleteAllStudents()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var stus = dc.StudentInfo.ToList();
            dc.StudentInfo.DeleteAllOnSubmit(stus);
            dc.SubmitChanges();
             PurgeCacheItems("AllStudents");
        }

        /// <summary>
        /// 通过excel文件批量倒入学生基本数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool InsertStudentsByExcleFile(string filePath)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            IList<StudentInfo> Students = ChangeExcelToStudents(filePath);
            if (Students == null) return false;
            // dc.StudentInfo.AttachAll<StudentInfo>(Students);
            dc.StudentInfo.InsertAllOnSubmit<StudentInfo>(Students);
            dc.SubmitChanges();
            PurgeCacheItems("AllStudents");
            return true;
        }



        /// <summary>
        /// 将excel文件中的数据转换成学生实体集合
        /// </summary>
        /// <param name="filePath">excel文件的物理路径</param>
        /// <returns></returns>
        private static IList<StudentInfo> ChangeExcelToStudents(string filePath)
        {

            IList<StudentInfo> students = new List<StudentInfo>();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            if (filePath.EndsWith("xlsx")) strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";


            string deptName = "";
            string majorName = "";
            OleDbConnection myConn = new OleDbConnection(strConn);
            string strCom = " SELECT * FROM [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(strCom, myConn);
            string defaultPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("123456", "SHA1");
            OleDbDataReader dr = null;
            try
            {
                myConn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //if(dr["姓名"]==null||dr["学号"]==null||dr["班级"]==null||dr["专业"]==null|| dr["家庭地址"]==null)
                    //   continue;
                    StudentInfo s = new StudentInfo();
                    s.StuName = dr["姓名"].ToString();
                    s.LoginName = dr["学号"].ToString();
                    s.Password = defaultPwd;
                    s.StuNum = dr["学号"].ToString();
                    s.Class = dr["班级"].ToString();
                    deptName = dr["系别"].ToString();

                    s.DepartmentID = ReturnID(deptDic, deptName);

                    // else s.DepartmentID = deptDic["外语系"];

                    majorName = dr["专业"].ToString();

                    s.MajorID = ReturnID(majorDic, majorName);


                    if (s.MajorID == 0 || s.DepartmentID == 0)
                    {
                                                
                        throw new Exception("系别或专业名称中有错，请按照提示填写！ "+deptName+"  "+ majorName);
                    }

                    //else s.MajorID = 10001;
                    if (dr["性别"] != null && dr["性别"].ToString().Equals("男"))
                        s.Sex = false;
                    else s.Sex = true;
                    // if(dr["家庭地址"]!=null) s.HomeAddress = dr["家庭地址"].ToString();
                    if (dr["年级"] != null) s.Grade = dr["年级"].ToString();
                    // else s.Grade = "2010";

                    if (dr["学生类别"] != null)
                        s.StuType = dr["学生类别"].ToString();

                    //s.IdentityCardNo=decimal.Parse(dr["身份证号"].ToString());

                    s.MajorDirection = dr["专业方向"].ToString();

                    if (dr["民族"] != null)
                        s.Race = dr["民族"].ToString();

                    //  s.LengthOfSchool = (char)dr["学制"];

                    //  s.Birthday = DateTime.Parse(dr["出生日期"].ToString());
                    // if(dr["相片地址"]!=null)s.ImagePath="~/Files/Student/Photos/"+ dr["相片地址"].ToString();
                    s.ImagePath = "~/Files/PublicFiles/Student/Photos/" + deptName + "/" + s.StuNum + ".jpg";
                    s.IsFullProfile = false;
                    s.State = -1;
                    s.TeacherID = null;
                    students.Add(s);

                    //   HttpContext.Current.Response.Write("一条记录<br>");

                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("错误信息：" + e.Message);
                students = null;

            }
            finally
            {
                if (dr != null) dr.Close();
                myConn.Close();

            } return students;
        }
        /// <summary>
        /// 根据系别ID返回学生集合
        /// </summary>
        /// <param name="depID">系别ID</param>
        /// <returns>该系的所有学生</returns>
        /// 
        [Obsolete]
        public static IList<StudentInfo> GetStudentsByDepartmentID(int deptID)
        {
            string key = "AllStudents_" + deptID.ToString();
            if (MyCache[key] != null)
            {
                return (IList<StudentInfo>)MyCache[key];
            }
            else
            {
                ExamDbDataContext dc = DataAccess.CreateDBContext();
                var students = from s in dc.StudentInfo
                               where s.DepartmentID == deptID
                               select s;
                IList<StudentInfo> stus = ToList<StudentInfo>(students);
                if (stus != null) MyCache[key] = stus;
                return stus;
            }
        }

        /// <summary>
        /// 返回全校所有学生
        /// </summary>
        /// <returns></returns>
        public static IList<StudentInfo> GetAllStudents()
        {
            string key = "AllStudents";
            if (MyCache[key] != null)
            {
                return (IList<StudentInfo>)MyCache[key];
            }
            else
            {
                ExamDbDataContext dc = DataAccess.CreateDBContext();
                IList<StudentInfo> allresults = ToList<StudentInfo>(dc.StudentInfo);
                if(allresults!=null)MyCache[key] = allresults;
                return allresults;
            }
        }

        /// <summary>
        /// 根据一组学生id返回对应的学生姓名
        /// </summary>
        /// <param name="ids">一组学生id</param>
        /// <returns></returns>
        public static IList<string> GetStudentNamesByIds(IList<int> ids)
        {
            IList<string> snames = new List<string>();
            foreach (int i in ids)
            {
                StudentInfo s = GetStudentByID(i);
                snames.Add(s.StuName);
            }
            return snames;
        }

        public static IQueryable<StudentInfo> GetStudents(int deptID, int majorID, string classnum)
        {
            return GetStudents(deptID, majorID, classnum, null,true);
        }
        /// <summary>
        /// 根据专业ｉｄ和班机获取还未分配给导师的学生列表
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="majorID"></param>
        /// <param name="classnum"></param>
        /// <returns></returns>
        public static IQueryable<StudentInfo> GetStudents(int deptID, int majorID, string classnum, ExamDbDataContext d,bool canUserCache)
        {
            string key = "AllStudents_" + deptID.ToString() + "_" + majorID.ToString();
            if (MyCache[key] != null&&canUserCache==true)
            {
                return (IQueryable<StudentInfo>)MyCache[key];
            }
            else
            {
                ExamDbDataContext dc;
                if (d == null) dc = DataAccess.CreateDBContext();
                else dc = d;
                var stus = from s in dc.StudentInfo
                           where s.DepartmentID == deptID
                           select s;


                if (majorID != 0)
                    stus = stus.Where<StudentInfo>(p => p.MajorID == majorID);

                if (classnum != null)
                    stus = stus.Where<StudentInfo>(p => p.Class.IndexOf(classnum) != -1);

                if(stus!=null)MyCache[key] = stus;
                return stus;
            }
        }


      




        #endregion
    }


    /// <summary>
    /// 用于作为分配学生时的学生信息，加上了论文题目信息
    /// </summary>
    public class StudentToSelect
    {
        public int StudentID
        {
            get;
            set;
        }
        public string SName
        {
            get;
            set;
        }
        public string TheClass
        {
            get;
            set;

        }
        public string StuNum
        {
            get;
            set;
        }
        public string ExamTitle
        {
            get;
            set;
        }
        public int? TeacherID
        {
            get;
            set;
        }

        public bool FreeToUploadForExam
        {
            get;
            set;
        }

        public string SelectSubjectState
        {
            get;
            set;
        }
    }
}
