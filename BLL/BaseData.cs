using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
namespace SDPTExam.BLL
{
    /// <summary>
    /// 处理基础数据，如部门信息，专业信息等等
    /// </summary>
    public class BaseData : BLLBase
    {
        static ExamDbDataContext dc = DataAccess.CreateDBContext();

        #region 处理部门信息
        /// <summary>
        /// 获取所有部门的集合
        /// </summary>
        /// <returns>返回部门集合</returns>
        public static IList<DepartmentInfo> GetDepartments()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext(); var depts = from d in dc.DepartmentInfo
                                                                             select d;

            return ToList<DepartmentInfo>(depts);

        }

        /// <summary>
        /// 返回单个部门信息
        /// </summary>
        /// <param name="dID"></param>
        /// <returns></returns>
        public static DepartmentInfo GetDepartmentByID(int dID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext(); var depts = from d in dc.DepartmentInfo
                                                                             where d.DepartmentID == dID
                                                                             select d;
            return ToSingle<DepartmentInfo>(depts);

        }

        /// <summary>
        /// 返回单个部门信息,同时返回数据上下文
        /// </summary>
        /// <param name="dID"></param>
        /// <returns></returns>
        public static DepartmentInfo GetDepartmentByID(int dID, out ExamDbDataContext dc)
        {
            ExamDbDataContext dd = DataAccess.CreateDBContext(); var depts = from d in dd.DepartmentInfo
                                                                             where d.DepartmentID == dID
                                                                             select d;
            dc = dd;
            return ToSingle<DepartmentInfo>(depts);

        }

        /// <summary>
        /// 更新部门实体信息
        /// </summary>
        /// <param name="s">需要更新的部门实体</param>
        public static bool UpdateDepartment(DepartmentInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            bool rvalue = AttachInfo<DepartmentInfo>(dc.DepartmentInfo, s);
            dc.SubmitChanges();
            return rvalue;
        }

        /// <summary>
        /// 插入部门实体信息
        /// </summary>
        /// <param name="d">部门实体信息</param>
        public static int InsertDepartment(DepartmentInfo d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            dc.DepartmentInfo.InsertOnSubmit(d);
            dc.SubmitChanges();
            return d.DepartmentID;
        }

        public static void DeleteDepartment(int departmentID)
        {
            ExamDbDataContext dc;
            DepartmentInfo d = GetDepartmentByID(departmentID, out dc);
            DeleteDepartment(d, dc);
        }

        public static void DeleteDepartment(DepartmentInfo d, ExamDbDataContext dc)
        {
            dc.DepartmentInfo.DeleteOnSubmit(d);
            dc.SubmitChanges();
        }
        #endregion


        #region 处理专业信息
        /// <summary>
        /// 更新专业实体信息
        /// </summary>
        /// <param name="s">需要更新的专业实体</param>
        public static void UpdateMajor(MajorInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<MajorInfo>(dc.MajorInfo, s);
            dc.SubmitChanges();
        }
        /// <summary>
        /// 返回指定部门的所有专业
        /// </summary>
        /// <param name="deptID">部门id</param>
        /// <returns>该部门的专业集合</returns>
        public static IList<MajorInfo> GetMajorsByDepartmentID(int deptID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var majors = from m in dc.MajorInfo
                         where m.DepartmentID == deptID
                         select m;
            return ToList<MajorInfo>(majors);

        }

        /// <summary>
        /// 根据指定的专业id返回专业实体信息
        /// </summary>
        /// <param name="mID">专业id</param>
        /// <returns>对应的专业实体</returns>
        public static MajorInfo GetMajorByID(int mID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var majors = from m in dc.MajorInfo
                         where m.MajorID == mID
                         select m;

            return ToSingle<MajorInfo>(majors);

        }


        /// <summary>
        /// 根据指定的专业id返回专业实体信息
        /// </summary>
        /// <param name="mID">专业id</param>
        /// <returns>对应的专业实体</returns>
        public static MajorInfo GetMajorByID(int mID, out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var majors = from m in dc.MajorInfo
                         where m.MajorID == mID
                         select m;
            d = dc;

            return ToSingle<MajorInfo>(majors);

        }

        /// <summary>
        /// 插入一个专业
        /// </summary>
        /// <param name="m"></param>
        public static void InsertMajor(MajorInfo m)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            dc.MajorInfo.InsertOnSubmit(m);
            dc.SubmitChanges();

        }


        public static void DeleteMajor(MajorInfo m, ExamDbDataContext dc)
        {
            AttachInfo<MajorInfo>(dc.MajorInfo, m);
            dc.MajorInfo.DeleteOnSubmit(m);
            dc.SubmitChanges();
        }

        public static void DeleteMajor(int majorID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            MajorInfo m = dc.MajorInfo.SingleOrDefault(p => p.MajorID == majorID);
            if (m == null) return;
            dc.MajorInfo.DeleteOnSubmit(m);
            dc.SubmitChanges();
        }

        /// <summary>
        /// 通过excel文件批量倒入学生基本数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool InsertMajorsByExcleFile(string filePath)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            IList<MajorInfo> majors = ChangeExcelToMajors(filePath);
            if (majors == null) return false;
            // dc.MajorInfo.AttachAll<MajorInfo>(Majors);
            dc.MajorInfo.InsertAllOnSubmit<MajorInfo>(majors);
            dc.SubmitChanges();
            PurgeCacheItems("AllMajors");
            return true;
        }



        /// <summary>
        /// 将excel文件中的数据转换成学生实体集合
        /// </summary>
        /// <param name="filePath">excel文件的物理路径</param>
        /// <returns></returns>
        private static IList<MajorInfo> ChangeExcelToMajors(string filePath)
        {

            IList<MajorInfo> majors = new List<MajorInfo>();
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
                    MajorInfo s = new MajorInfo();


                    deptName = dr["系别"].ToString();

                    s.DepartmentID = ReturnID(deptDic, deptName);

                    // else s.DepartmentID = deptDic["外语系"];

                    majorName = dr["专业"].ToString();

                    s.Description = majorName;

                    s.MajorName = majorName;

                    if (s.DepartmentID == 0)
                    {
                        throw new Exception("系别或专业名称中有错，请按照提示填写！" + deptName);
                    }

                    majors.Add(s);

                    //   HttpContext.Current.Response.Write("一条记录<br>");

                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("错误信息：" + e.Message);
                majors = null;

            }
            finally
            {
                if (dr != null) dr.Close();
                myConn.Close();

            } return majors;
        }
        /// <summary>
        /// 根据系别ID返回学生集合
        /// </summary>
        /// <param name="depID">系别ID</param>
        /// <returns>该系的所有学生</returns>
        /// 



        #endregion


        #region 处理班级信息

        /// <summary>
        /// 设置班级人数和学生对应的班级id
        /// </summary>
        public static void CheckClassID()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            IList<ClassInfo> clist = dc.ClassInfo.ToList();
            foreach (ClassInfo c in clist)
            {
                var stus = from s in dc.StudentInfo
                           where s.Class == c.ClassName
                           select s;
                foreach (StudentInfo s in stus)
                {
                    s.ClassID = c.ClassID;

                }
                c.StuCount = stus.Count();
                c.DepartmentID = stus.First().DepartmentID;
                

            }
            dc.SubmitChanges();

        }

        public void SetClassValue(ClassInfo c, OleDbDataReader dr)
        {
            //string deptName = "";
            string majorName = "";
            //deptName = dr["系别"].ToString();
            majorName = dr["专业"].ToString();

            //c.DepartmentID = ReturnID(deptDic, deptName);
            c.MajorID = ReturnID(majorDic, majorName);
            c.ClassName = dr["班级"].ToString();
            c.StuCount = 50;

        }

        public void SetClassValue1<T>(T c, OleDbDataReader dr)
        {
            SetClassValue(c as ClassInfo, dr);
        }

        /// <summary>
        /// 通过excel文件批量倒入班级基本数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool InsertClasssByExcleFile(string filePath)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            SetObjectValue<ClassInfo> s = SetClassValue1;
            IList<ClassInfo> cls = ChangeExcelToThings<ClassInfo>(filePath, s);
            if (cls == null) return false;
            // dc.MajorInfo.AttachAll<MajorInfo>(Majors);
            dc.ClassInfo.InsertAllOnSubmit<ClassInfo>(cls);
            dc.SubmitChanges();
            PurgeCacheItems("AllClasss");
            return true;
        }


        /// <summary>
        /// 返回指定部门的所有专业
        /// </summary>
        /// <param name="deptID">部门id</param>
        /// <returns>该部门的专业集合</returns>
        public static IList<ClassInfo> GetClasssByMajorID(int majorID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var Classs = from m in dc.ClassInfo
                         where m.MajorID == majorID
                         select m;
            return ToList<ClassInfo>(Classs);

        }

        /// <summary>
        /// 根据指定的专业id返回专业实体信息
        /// </summary>
        /// <param name="mID">专业id</param>
        /// <returns>对应的专业实体</returns>
        public static ClassInfo GetClassByID(int mID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var Classs = from m in dc.ClassInfo
                         where m.ClassID == mID
                         select m;

            return ToSingle<ClassInfo>(Classs);

        }


        /// <summary>
        /// 根据指定的专业id返回专业实体信息
        /// </summary>
        /// <param name="mID">专业id</param>
        /// <returns>对应的专业实体</returns>
        public static ClassInfo GetClassByID(int mID, out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var Classs = from m in dc.ClassInfo
                         where m.ClassID == mID
                         select m;
            d = dc;

            return ToSingle<ClassInfo>(Classs);

        }
        #endregion



        #region 处理时间限制信息
        /// <summary>
        /// 插入一个时间限制记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int InsertTimeLimit(TimeLimitInfo t)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            dc.TimeLimitInfo.InsertOnSubmit(t);
            dc.SubmitChanges();
            return t.TimeLimitID;
        }


        public static TimeLimitInfo GetTimeLimitByID(int tid)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            TimeLimitInfo t = dc.TimeLimitInfo.Single(p => p.TimeLimitID == tid);
            return t;
        }


        public static TimeLimitInfo GetTimeLimitByID(int tid, out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            TimeLimitInfo t = dc.TimeLimitInfo.SingleOrDefault(p => p.TimeLimitID == tid);
            d = dc;
            return t;
        }
        /// <summary>
        /// 更新时间限制实体信息
        /// </summary>
        /// <param name="s">需要更新的时间限制实体</param>
        public static void UpdateTimeLimit(TimeLimitInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            AttachInfo<TimeLimitInfo>(dc.TimeLimitInfo, s);
            dc.SubmitChanges();
        }

        /// <summary>
        /// 更新时间限制实体信息
        /// </summary>
        /// <param name="s">需要更新的时间限制实体</param>
        public static void UpdateTimeLimit(TimeLimitInfo s, ExamDbDataContext dc)
        {

            AttachInfo<TimeLimitInfo>(dc.TimeLimitInfo, s);
            dc.SubmitChanges();
        }


        public static void DeleteTimeLimit(int tid)
        {
            ExamDbDataContext dc;

            TimeLimitInfo t = GetTimeLimitByID(tid, out dc);
            dc.TimeLimitInfo.DeleteOnSubmit(t);
            dc.SubmitChanges();
        }

        #endregion



        #region 处理日志信息

        public static IList<LogInfo> GetAllLogs(int deptID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var logs = from l in dc.LogInfo

                       select l;
            return ToList<LogInfo>(logs);
        }

        #endregion


    }
}
