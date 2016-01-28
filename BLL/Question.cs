using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPTExam.DAL.Linq;
using SDPTExam.DAL.Model;

namespace SDPTExam.BLL
{
    class Question:BLLBase
    {
        /// <summary>          
        /// 插入试题信息
        /// </summary>
        /// <param name="s">试题实体</param>
        public static int InsertQuestion(QuestionInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            dc.QuestionInfo.InsertOnSubmit(s);

            dc.SubmitChanges();
            // PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
            return s.QuestionID;
        }

        /// <summary>
        /// 根据指定id返回对应试题
        /// </summary>
        /// <param name="id">试题id</param>
        /// <returns></returns>
        public static QuestionInfo GetQuestionByID(int id)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var Questions = from s in dc.QuestionInfo
                                where s.QuestionID == id
                                select s;
            return ToSingle<QuestionInfo>(Questions);
        }



        /// <summary>
        /// 返回所有试题
        /// </summary>
        /// <returns></returns>
        public static IQueryable<QuestionInfo> GetAllQuestions()
        {
            string key = "AllQuestions";
            if (MyCache[key] != null)
            {
                return (IQueryable<QuestionInfo>)MyCache[key];
            }
            else
            {
                ExamDbDataContext dc = DataAccess.CreateDBContext();
                var Questions = from s in dc.QuestionInfo
                                    select s;

                MyCache[key] = Questions;
                return Questions;// ToList<QuestionInfo>(Questions);
            }
        }

        /// <summary>
        /// 根据指定课程id返回对应试题,
        /// </summary>
        /// <param name="id">试题id</param>
        /// <returns></returns>
        public static IList<QuestionInfo> GetQuestionsByCourseID(int courseID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var Questions = from s in dc.QuestionInfo
                            where s.CourseID == courseID
                                select s;
            return ToList<QuestionInfo>(Questions);
        }


        /// <summary>
        /// 根据指定试题类型id返回对应试题,
        /// </summary>
        /// <param name="id">试题类型id</param>
        /// <returns></returns>
        public static IList<QuestionInfo> GetQuestionsByTypeID(int typeID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var Questions = from s in dc.QuestionInfo
                            where s.QuestionTypeID == typeID
                            select s;
            return ToList<QuestionInfo>(Questions);
        }

        /// <summary>
        /// 删除试题信息
        /// </summary>
        /// <param name="s">试题实体</param>
        public static void DeleteQuestion(QuestionInfo s, ExamDbDataContext dc)
        {
            AttachInfo<QuestionInfo>(dc.QuestionInfo, s);
            dc.QuestionInfo.DeleteOnSubmit(s);
            dc.SubmitChanges();
        }

        /// <summary>
        ///通过id删除试题信息
        /// </summary>
        /// <param name="QuestionID">试题实体ID</param>
        public static void DeleteQuestion(int QuestionID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            QuestionInfo s = dc.QuestionInfo.Single(p => p.QuestionID == QuestionID);
            DeleteQuestion(s, dc);
            PurgeCacheItems("AllQuestions");
        }


        /// <summary>
        /// 更新试题信息
        /// </summary>
        /// <param name="s">试题实体</param>
        public static void UpdateQuestion(QuestionInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<QuestionInfo>(dc.QuestionInfo, s);
            // PurgeCacheItems("AllQuestions_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }

        /// <summary>
        /// 更新试题信息
        /// </summary>
        /// <param name="s">试题实体</param>
        public static void UpdateQuestion(QuestionInfo s, ExamDbDataContext dc)
        {
            AttachInfo<QuestionInfo>(dc.QuestionInfo, s);
            dc.SubmitChanges();

            // PurgeCacheItems("AllQuestions_" + s.DepartmentID.ToString());
        }

        /// <summary>
        /// 更新试题标题
        /// </summary>
        /// <param name="title">试题标题</param>
        public static void UpdateQuestion(int QuestionID, string title, bool isSingle)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            QuestionInfo c = dc.QuestionInfo.Single(p => p.QuestionID == QuestionID);

            c.IsSingleOrRightForJudge = isSingle;
            c.Title = title;

            // PurgeCacheItems("AllQuestions_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }
    }
}
