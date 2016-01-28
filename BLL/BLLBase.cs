using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Configuration;
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;
using System.Collections;
using System.Data.OleDb;
namespace SDPTExam.BLL
{
  public  class BLLBase
    {


      public delegate void SetObjectValue<T>(T t, OleDbDataReader dr) where T : new();
      /// <summary>
      /// 定义公用的DataContext对象
      /// </summary>
        protected static IDictionary<string, int> deptDic = GetDeptDic();
        protected static IDictionary<string, int> majorDic = GetMajorDic();
      /// <summary>
      /// 返回对象列表，加上空列表的判断
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vars"></param>
      /// <returns></returns>
        protected static IList<T> ToList<T>(IQueryable<T> vars)
        {
            if (vars == null) return null;
            if (vars.Count<T>() == 0) return null;
            else return vars.ToList<T>();
        }

        public static Cache MyCache
        {
            get { return HttpContext.Current.Cache; }
        }

      /// <summary>
      /// 根据名称返回对应的ID
      /// </summary>
      /// <param name="dic"></param>
      /// <param name="theName"></param>
      /// <returns></returns>
        public static int ReturnID(IDictionary<string, int> dic, string theName)
        {
            string dname = theName.Trim();
            if (dic.Keys.Contains(dname))
            {
                return dic[dname];
            }
            else return 0;
        }

        /// <summary>
        /// Remove from the ASP.NET cache all items whose key starts with the input prefix
        /// </summary>
        public static void PurgeCacheItems(string prefix)
        {
            prefix = prefix.ToLower();
            List<string> itemsToRemove = new List<string>();

            IDictionaryEnumerator enumerator = MyCache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                    itemsToRemove.Add(enumerator.Key.ToString());
            }

            foreach (string itemToRemove in itemsToRemove)
                MyCache.Remove(itemToRemove);
        }

        public static void RemoveCacheItem(string key)
        {
            MyCache.Remove(key);
        }

      /// <summary>
      /// 如果集合中数目为零或者大于1，都将返回类型T的默认值
      /// </summary>
      /// <typeparam name="T">类型</typeparam>
      /// <param name="vars">记录集合</param>
      /// <returns>返回单个记录</returns>
        protected static T ToSingle<T>(IQueryable<T> vars)
        {
            if (vars == null) return default(T);
            if (vars.Count<T>() == 0 || vars.Count<T>()>1) return default(T);
            else return vars.Single<T>();
        
        }


        protected static bool AttachInfo<T>(Table<T> tInfo,T t) where T:class
        {
            if (tInfo.Contains<T>(t) == false)
            {
                try
                {
                    tInfo.Attach(t);
                    return true;
                }
                catch (DuplicateKeyException de)
                { string s = de.Message;
                return false;
                }
                
            }
            else  return true;
        }

        protected static void AttachAllInfos<T>(Table<T> tInfo, IList<T> list) where T : class
        {
            foreach (T t in list)
            {
                if (tInfo.Contains(t) == false)
                {
                    try
                    {
                        tInfo.Attach(t);
                    }
                    catch (DuplicateKeyException de)
                    {string s= de.Message; }
                }
            }

        }

        /// <summary>
        /// 返回一个字典集合，键为字符串，值为ID
        /// </summary>
        /// <returns>返回字典集合</returns>
        private static IDictionary<string, int> GetDeptDic()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            IDictionary<string, int> deptDic = new Dictionary<string, int>();
            List<DepartmentInfo> departments = dc.DepartmentInfo.ToList<DepartmentInfo>();
      
            foreach (DepartmentInfo d in departments)
            {
                if (deptDic.Keys.Contains(d.DepartmentName)) continue;
                deptDic.Add(d.DepartmentName, d.DepartmentID);
            }
            return deptDic;
        }

        /// <summary>
        /// 返回一个字典集合，键为字符串，值为ID
        /// </summary>
        /// <returns>返回字典集合</returns>
       private  static IDictionary<string, int> GetMajorDic()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            IDictionary<string, int> majorDic = new Dictionary<string, int>();
            List<MajorInfo> majors = dc.MajorInfo.ToList<MajorInfo>();
            foreach (MajorInfo d in majors)
            {
                if (majorDic.Keys.Contains(d.MajorName)) continue;
                else  majorDic.Add(d.MajorName, d.MajorID);
            }
            return majorDic;
        }

       /// <summary>
       /// 返回一个字典集合，键为字符串，值为ID
       /// </summary>
       /// <returns>返回字典集合</returns>
       public static IDictionary<string, int> GetCourseDic()
       {
           ExamDbDataContext dc = DataAccess.CreateDBContext();

           IDictionary<string, int> courseDic = new Dictionary<string, int>();
           List<CourseInfo> Courses = dc.CourseInfo.ToList<CourseInfo>();

           foreach (CourseInfo d in Courses)
           {
               if (courseDic.Keys.Contains(d.Title)) continue;
               courseDic.Add(d.Title, d.CourseID);
           }
           return courseDic;
       }

       /// <summary>
       /// 返回一个字典集合，键为字符串，值为ID
       /// </summary>
       /// <returns>返回字典集合</returns>
       public static IDictionary<string, int> GetChapterDic()
       {
           ExamDbDataContext dc = DataAccess.CreateDBContext();

           IDictionary<string, int> chapterDic = new Dictionary<string, int>();
           List<ChapterInfo> Chapters = dc.ChapterInfo.ToList<ChapterInfo>();

           foreach (ChapterInfo d in Chapters)
           {
               if (chapterDic.Keys.Contains(d.Title)) continue;
               chapterDic.Add(d.Title, d.ChapterID);
           }
           return chapterDic;
       }






       /// <summary>
       /// 将excel文件中的数据转换成对象实体集合
       /// </summary>
       /// <param name="filePath">excel文件的物理路径</param>
       /// <returns></returns>
       protected IList<T> ChangeExcelToThings<T>(string filePath,SetObjectValue<T> setValue) where T:new()
       {

           IList<T> Things = new List<T>();
           string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
           if (filePath.EndsWith("xlsx")) strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
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
       
                   T s = new T();


                   setValue(s,dr);

                   Things.Add(s);

                   //   HttpContext.Current.Response.Write("一条记录<br>");

               }
           }
           catch (Exception e)
           {
               HttpContext.Current.Response.Write("错误信息：" + e.Message);
               Things = null;

           }
           finally
           {
               if (dr != null) dr.Close();
               myConn.Close();

           } return Things;
       }



        /// <summary>
        /// 根据系别ID返回学生集合
        /// </summary>
        /// <param name="depID">系别ID</param>
        /// <returns>该系的所有学生</returns>
        /// 

      

    }
}
