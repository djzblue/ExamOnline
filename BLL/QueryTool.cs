using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPTExam.DAL.Model;
using SDPTExam.DAL.Linq;
namespace SDPTExam.BLL
{
    public class ExamResult
    {

        public double AvgMark
        {
            get;
            set;
        }



        public string Jigelv
        {
            get;
            set;
        }



        public string Youlianglv
        {
            get;
            set;
        }

        public int ClassID
        {
            get;
            set;
        }
        public int MajorID
        {
            get;
            set;
        }
        public int DeptID
        {
            get;
            set;
        }

        public int ShouldExamNum
        {
            get;
            set;
        }

        public int ActualExamNum
        {
            get;
            set;
        }


    }

   public class QueryTool:BLLBase
    {

        static string GetPencent(int a, int sum)
        {
            double result = 100 * (double)a / (double)sum;

            result = Math.Round(result, 2);

            return result.ToString() + "%";

        }
        public static IQueryable<ExamResult> GetAllQueryResults(int basicExamID, int classID)
        {
            string key = "QueryResults";
            if (MyCache[key] != null)
            {
                return (IQueryable<ExamResult>)MyCache[key];
            }
            else
            {

                IQueryable<RandomExamInfo> results = RandomExam.GetRandomExamsByBasicExamID(basicExamID);
                if (results == null || results.Count() == 0) return null;

                // 用于各个班级
                var rr = from s in results
                         group s by s.ClassID into g
                         select new ExamResult
                         {
                             ClassID = g.Key,
                             ActualExamNum = g.Count(),
                             ShouldExamNum = (int)(BaseData.GetClassByID(g.Key).StuCount),
                             AvgMark = g.Average(p => p.TotalGetMark),
                             Jigelv = GetPencent(g.Where(p => p.TotalGetMark >= 60).Count(), g.Count()),
                             Youlianglv = GetPencent(g.Where(p => p.TotalGetMark >= 80).Count(), g.Count()),
                             MajorID = BaseData.GetClassByID(g.Key).MajorID,
                             DeptID = (int)BaseData.GetClassByID(g.Key).DepartmentID
                         };

                //int i = rr.Count();
                //foreach (ExamResult r in rr)
                //{
                //    int a = r.ActualExamNum;
                //}
                MyCache[key] = rr;
                return rr;
            }

        }
    }
}
