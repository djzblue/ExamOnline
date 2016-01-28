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
   public class BasicExam : BLLBase
    {
        /// <summary>          
        /// 插入基本试卷信息
        /// </summary>
        /// <param name="s">基本试卷实体</param>
        public static int InsertBasicExam(BasicExamInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            dc.BasicExamInfo.InsertOnSubmit(s);

            dc.SubmitChanges();
            // PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
            return s.BasicExamID;
        }

        /// <summary>
        /// 根据指定id返回对应基本试卷
        /// </summary>
        /// <param name="id">基本试卷id</param>
        /// <returns></returns>
        public static BasicExamInfo GetBasicExamByID(int id)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var BasicExams = from s in dc.BasicExamInfo
                         where s.BasicExamID == id
                         select s;
            return ToSingle<BasicExamInfo>(BasicExams);
        }

        /// <summary>
        /// 根据指定id返回对应基本试卷
        /// </summary>
        /// <param name="id">基本试卷id</param>
        /// <returns></returns>
        public static BasicExamInfo GetBasicExamByID(int id, out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            d = dc;
            var BasicExams = from s in dc.BasicExamInfo
                             where s.BasicExamID == id
                             select s;
            return ToSingle<BasicExamInfo>(BasicExams);
        }

        /// <summary>
        /// 返回所有基本试卷
        /// </summary>
        /// <param name="id">基本试卷id</param>
        /// <returns></returns>
        public static IList<BasicExamInfo> GetAllBasicExams()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var BasicExams = from s in dc.BasicExamInfo                      
                        select s;
            return ToList<BasicExamInfo>(BasicExams);
        }


        /// <summary>
        /// 根据指定专业id返回对应基本试卷,
        /// </summary>
        /// <param name="id">基本试卷id</param>
        /// <returns></returns>
        public static IList<BasicExamInfo> GetBasicExamsByMajorID(int majorID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var BasicExams = from s in dc.BasicExamInfo
                         where s.MajorID == majorID
                         select s;
            return ToList<BasicExamInfo>(BasicExams);
        }

        /// <summary>
        /// 删除基本试卷信息
        /// </summary>
        /// <param name="s">基本试卷实体</param>
        public static void DeleteBasicExam(BasicExamInfo s, ExamDbDataContext dc)
        {
            AttachInfo<BasicExamInfo>(dc.BasicExamInfo, s);
            dc.BasicExamInfo.DeleteOnSubmit(s);
            dc.SubmitChanges();
        }

        /// <summary>
        ///通过id删除基本试卷信息
        /// </summary>
        /// <param name="BasicExamID">基本试卷实体ID</param>
        public static void DeleteBasicExam(int BasicExamID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            BasicExamInfo s = dc.BasicExamInfo.Single(p => p.BasicExamID == BasicExamID);
            DeleteBasicExam(s, dc);
            PurgeCacheItems("AllBasicExams_");
        }


        /// <summary>
        /// 更新基本试卷信息
        /// </summary>
        /// <param name="s">基本试卷实体</param>
        public static void UpdateBasicExam(BasicExamInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<BasicExamInfo>(dc.BasicExamInfo, s);
            // PurgeCacheItems("AllBasicExams_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }

        /// <summary>
        /// 更新基本试卷信息
        /// </summary>
        /// <param name="s">基本试卷实体</param>
        public static void UpdateBasicExam(BasicExamInfo s, ExamDbDataContext dc)
        {
            AttachInfo<BasicExamInfo>(dc.BasicExamInfo, s);
            dc.SubmitChanges();

            // PurgeCacheItems("AllBasicExams_" + s.DepartmentID.ToString());
        }

        public static void SetActiveExam(int examID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var BasicExams = from s in dc.BasicExamInfo
                             select s;
            foreach (BasicExamInfo exam in BasicExams)
            {
                if (exam.BasicExamID == examID) exam.IsActive = true;
                else exam.IsActive = false;
            }

            dc.SubmitChanges();
        }

    }
}
