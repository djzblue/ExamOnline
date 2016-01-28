using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;
using System.ComponentModel;
namespace SDPTExam.BLL
{
    [DataObject(true)]
   public class Judge : BLLBase
    {
        /// <summary>          
        /// 插入判断题信息
        /// </summary>
        /// <param name="s">判断题实体</param>
        public static int InsertJudge(JudgeInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            dc.JudgeInfo.InsertOnSubmit(s);

            dc.SubmitChanges();
            // PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
            return s.JudgeID;
        }

        /// <summary>
        /// 根据指定id返回对应判断题
        /// </summary>
        /// <param name="id">判断题id</param>
        /// <returns></returns>
        public static JudgeInfo GetJudgeByID(int id)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var Judges = from s in dc.JudgeInfo
                                where s.JudgeID == id
                                select s;
            return ToSingle<JudgeInfo>(Judges);
        }

        /// <summary>
        /// 返回所有判断题
        /// </summary>
        /// <param name="id">判断题id</param>
        /// <returns></returns>
        public static IList<JudgeInfo> GetAllJudges()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var Judges = from s in dc.JudgeInfo                        
                         select s;
            return ToList<JudgeInfo>(Judges);
        }

        /// <summary>
        /// 根据指定专业id返回对应判断题,
        /// </summary>
        /// <param name="id">判断题id</param>
        /// <returns></returns>
        public static IList<JudgeInfo> GetJudgesByMajorID(int majorID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var Judges = from s in dc.JudgeInfo
                                where s.MajorID == majorID
                                select s;
            return  ToList<JudgeInfo>(Judges);
        }

        /// <summary>
        /// 删除判断题信息
        /// </summary>
        /// <param name="s">判断题实体</param>
        public static void DeleteJudge(JudgeInfo s, ExamDbDataContext dc)
        {
            AttachInfo<JudgeInfo>(dc.JudgeInfo, s);
            dc.JudgeInfo.DeleteOnSubmit(s);
            dc.SubmitChanges();
        }

        /// <summary>
        ///通过id删除判断题信息
        /// </summary>
        /// <param name="JudgeID">判断题实体ID</param>
        public static void DeleteJudge(int JudgeID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            JudgeInfo s = dc.JudgeInfo.Single(p => p.JudgeID == JudgeID);
            DeleteJudge(s, dc);
            PurgeCacheItems("AllJudges_");
        }


        /// <summary>
        /// 更新判断题信息
        /// </summary>
        /// <param name="s">判断题实体</param>
        public static void UpdateJudge(JudgeInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<JudgeInfo>(dc.JudgeInfo, s);
            // PurgeCacheItems("AllJudges_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }

        /// <summary>
        /// 更新判断题信息
        /// </summary>
        /// <param name="s">判断题实体</param>
        public static void UpdateJudge(JudgeInfo s, ExamDbDataContext dc)
        {
            AttachInfo<JudgeInfo>(dc.JudgeInfo, s);
            dc.SubmitChanges();

            // PurgeCacheItems("AllJudges_" + s.DepartmentID.ToString());
        }

    }
}
