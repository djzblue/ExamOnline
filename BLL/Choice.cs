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
   public class Choice:BLLBase
    {
        /// <summary>          
        /// 插入选择题信息
        /// </summary>
        /// <param name="s">选择题实体</param>
        public static int InsertSelectChoice(SelectChoiceInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            dc.SelectChoiceInfo.InsertOnSubmit(s);
            
            dc.SubmitChanges();
           // PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
            return s.SelectChoiceID;
        }

        /// <summary>
        /// 根据指定id返回对应选择题
        /// </summary>
        /// <param name="id">选择题id</param>
        /// <returns></returns>
        public static SelectChoiceInfo GetSelectChoiceByID(int id)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var SelectChoices = from s in dc.SelectChoiceInfo
                              where s.SelectChoiceID == id
                              select s;
            return ToSingle<SelectChoiceInfo>(SelectChoices);
        }

        /// <summary>
        /// 返回单项选择题
        /// </summary>
        /// <returns></returns>
        public static IList<SelectChoiceInfo> GetSingleSelectChoicesByExamID(int examID)
        {

            return null;
        }
        

        /// <summary>
        /// 返回所有选择题
        /// </summary>
        /// <returns></returns>
        public static IQueryable<SelectChoiceInfo> GetAllSelectChoices()
        {
           string key = "AllChoices";
           if (MyCache[key] != null)
           {
               return (IQueryable<SelectChoiceInfo>)MyCache[key];
           }
           else
           {
               ExamDbDataContext dc = DataAccess.CreateDBContext();
               var SelectChoices = from s in dc.SelectChoiceInfo
                                   select s;

               MyCache[key] = SelectChoices;
               return SelectChoices;// ToList<SelectChoiceInfo>(SelectChoices);
           }
        }

        /// <summary>
        /// 根据指定专业id返回对应选择题,
        /// </summary>
        /// <param name="id">选择题id</param>
        /// <returns></returns>
        public static IList<SelectChoiceInfo> GetSelectChoicesByID(int majorID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var SelectChoices = from s in dc.SelectChoiceInfo
                                where s.MajorID==majorID
                                select s;
            return ToList<SelectChoiceInfo>(SelectChoices);
        }

        /// <summary>
        /// 删除选择题信息
        /// </summary>
        /// <param name="s">选择题实体</param>
        public static void DeleteSelectChoice(SelectChoiceInfo s, ExamDbDataContext dc)
        {
            AttachInfo<SelectChoiceInfo>(dc.SelectChoiceInfo, s);
            dc.SelectChoiceInfo.DeleteOnSubmit(s);
            dc.SubmitChanges();
        }

        /// <summary>
        ///通过id删除选择题信息
        /// </summary>
        /// <param name="SelectChoiceID">选择题实体ID</param>
        public static void DeleteSelectChoice(int SelectChoiceID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            SelectChoiceInfo s = dc.SelectChoiceInfo.Single(p => p.SelectChoiceID == SelectChoiceID);
            DeleteSelectChoice(s, dc);
            PurgeCacheItems("AllChoices");
        }


        /// <summary>
        /// 更新选择题信息
        /// </summary>
        /// <param name="s">选择题实体</param>
        public static void UpdateSelectChoice(SelectChoiceInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<SelectChoiceInfo>(dc.SelectChoiceInfo, s);
           // PurgeCacheItems("AllSelectChoices_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }

        /// <summary>
        /// 更新选择题信息
        /// </summary>
        /// <param name="s">选择题实体</param>
        public static void UpdateSelectChoice(SelectChoiceInfo s, ExamDbDataContext dc)
        {
            AttachInfo<SelectChoiceInfo>(dc.SelectChoiceInfo, s);
            dc.SubmitChanges();

           // PurgeCacheItems("AllSelectChoices_" + s.DepartmentID.ToString());
        }

        /// <summary>
        /// 更新选择题标题
        /// </summary>
        /// <param name="title">选择题标题</param>
        public static void UpdateSelectChoice(int choiceID,string title,bool isSingle)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            SelectChoiceInfo c = dc.SelectChoiceInfo.Single(p => p.SelectChoiceID == choiceID);

            c.IsSingleSelect = isSingle;
            c.Title = title;

            // PurgeCacheItems("AllSelectChoices_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }
    }

public class ChoiceItem:BLLBase
    {
        /// <summary>          
        /// 插入选择项信息
        /// </summary>
        /// <param name="s">选择项实体</param>
        public static int InsertChoiceItem(ChoiceItemInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            dc.ChoiceItemInfo.InsertOnSubmit(s);
            
            dc.SubmitChanges();
           // PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
            return s.ChoiceItemID;
        }
        /// <summary>          
        /// 插入一组选择项信息
        /// </summary>
        /// <param name="s">选择项实体集合</param>
        public static bool InsertChoiceItems(IList<ChoiceItemInfo> items)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            dc.ChoiceItemInfo.InsertAllOnSubmit<ChoiceItemInfo>(items);

            dc.SubmitChanges();
            // PurgeCacheItems("AllSubjects_" + s.DepartmentID.ToString());
            return true;
        }
        /// <summary>
        /// 根据指定id返回对应选择项
        /// </summary>
        /// <param name="id">选择项id</param>
        /// <returns></returns>
        public static ChoiceItemInfo GetChoiceItemByID(int id)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var ChoiceItems = from s in dc.ChoiceItemInfo
                              where s.ChoiceItemID == id
                              select s;
            return ToSingle<ChoiceItemInfo>(ChoiceItems);
        }
      

        /// <summary>
        /// 返回所有选择项
        /// </summary>
        /// <returns></returns>
        public static IList<ChoiceItemInfo> GetAllChoiceItems()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var ChoiceItems = from s in dc.ChoiceItemInfo
                                select s;
            return ToList<ChoiceItemInfo>(ChoiceItems);
        }

        /// <summary>
        /// 返回所有选择项
        /// </summary>
        /// <returns></returns>
        public static IList<ChoiceItemInfo> GetChoiceItems(int choiceID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var ChoiceItems = from s in dc.ChoiceItemInfo
                              where s.SelectChoiceID==choiceID
                              select s;
            return ToList<ChoiceItemInfo>(ChoiceItems);
        }

        /// <summary>
        /// 根据指定专业id返回对应选择项,
        /// </summary>
        /// <param name="id">选择项id</param>
        /// <returns></returns>
        //public static IList<ChoiceItemInfo> GetChoiceItemsByID(int majorID)
        //{
        //    ExamDbDataContext dc = DataAccess.CreateDBContext();
        //    var ChoiceItems = from s in dc.ChoiceItemInfo
        //                        where s.MajorID==majorID
        //                        select s;
        //    return ToList<ChoiceItemInfo>(ChoiceItems);
        //}

        /// <summary>
        /// 删除选择项信息
        /// </summary>
        /// <param name="s">选择项实体</param>
        public static void DeleteChoiceItem(ChoiceItemInfo s, ExamDbDataContext dc)
        {
            AttachInfo<ChoiceItemInfo>(dc.ChoiceItemInfo, s);
            dc.ChoiceItemInfo.DeleteOnSubmit(s);
            dc.SubmitChanges();
        }

        /// <summary>
        ///通过id删除选择项信息
        /// </summary>
        /// <param name="ChoiceItemID">选择项实体ID</param>
        public static void DeleteChoiceItem(int ChoiceItemID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            ChoiceItemInfo s = dc.ChoiceItemInfo.Single(p => p.ChoiceItemID == ChoiceItemID);
            DeleteChoiceItem(s, dc);
            PurgeCacheItems("AllChoiceItems_");
        }


      /// <summary>
        /// 更新选择项信息
        /// </summary>
        /// <param name="s">选择项实体</param>
        public static void UpdateChoiceItem(ChoiceItemInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<ChoiceItemInfo>(dc.ChoiceItemInfo, s);
           // PurgeCacheItems("AllChoiceItems_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }
 


        /// <summary>
        /// 更新选择项信息
        /// </summary>
        /// <param name="s">选择项实体</param>
        public static void UpdateChoiceItemByCopy(ChoiceItemInfo s)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            ChoiceItemInfo c = dc.ChoiceItemInfo.Single(p => p.ChoiceItemID == s.ChoiceItemID);

            c.Title = s.Title;
            c.IsRight = s.IsRight;
            
            // PurgeCacheItems("AllChoiceItems_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }

        /// <summary>
        /// 更新选择项信息
        /// </summary>
        /// <param name="s">选择项实体</param>
        public static void UpdateChoiceItem(ChoiceItemInfo s, ExamDbDataContext dc)
        {
            AttachInfo<ChoiceItemInfo>(dc.ChoiceItemInfo, s);
            dc.SubmitChanges();

           // PurgeCacheItems("AllChoiceItems_" + s.DepartmentID.ToString());
        }


        /// <summary>
        /// 更新项标题
        /// </summary>
        /// <param name="title">选择项标题</param>
        public static void UpdateChoiceItemTitle(int choiceItemID, string title)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();

            ChoiceItemInfo c = dc.ChoiceItemInfo.Single(p => p.ChoiceItemID == choiceItemID);

            c.Title = title;

            // PurgeCacheItems("AllSelectChoices_" + s.DepartmentID.ToString());
            dc.SubmitChanges();
        }

    }

}
