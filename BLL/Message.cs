using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPTExam.DAL.Model;
using SDPTExam.DAL.Linq;
using System.IO;

namespace SDPTExam.BLL
{
   public class Message:BLLBase
    {
        /// <summary>
        ///插入公告
        /// </summary>
        /// <param name="n">公告实体</param>
        public static void InsertNotice(NoticeInfo n)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            dc.NoticeInfo.InsertOnSubmit(n);
            dc.SubmitChanges();
        }
        /// <summary>
        /// 返回公告信息
        /// </summary>
        /// <param name="NoticeID">id号</param>

        /// <returns></returns>
        public static NoticeInfo GetNoticeByID(int noticeID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var messages = from m in dc.NoticeInfo
                           where m.NoticeID == noticeID
                           select m;
            return ToSingle<NoticeInfo>(messages);
        }

        /// <summary>
        /// 返回公告信息
        /// </summary>
        /// <param name="NoticeID">id号</param>

        /// <returns></returns>
        public static NoticeInfo GetNoticeByID(int noticeID, out ExamDbDataContext d)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            d = dc;
            var messages = from m in dc.NoticeInfo
                           where m.NoticeID == noticeID
                           select m;
            return ToSingle<NoticeInfo>(messages);
        }

        /// <summary>
        /// 获取本系的所有公告
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static IList<NoticeInfo> GetNotices(int classID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var a = from n in dc.NoticeInfo
                    where n.ClassID == classID || n.ClassID == null //.DepartmentID==0 ///或者是来自学院部门的公告。
                    orderby n.AddedTime descending
                    select n;
            return ToList<NoticeInfo>(a);
        }

        /// <summary>
        /// 获取本系的所有公告
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static IList<NoticeInfo> GetAllNotices()
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            var a = from n in dc.NoticeInfo
                    orderby n.AddedTime descending
                    select n;
            return ToList<NoticeInfo>(a);
        }

        /// <summary>
        ///更新通知
        /// </summary>
        /// <param name="n">通知实体</param>
        public static void UpdateNotice(NoticeInfo n)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            AttachInfo<NoticeInfo>(dc.NoticeInfo, n);
            dc.SubmitChanges();
        }

        /// <summary>
        ///更新通知
        /// </summary>
        /// <param name="n">通知实体</param>
        public static void UpdateNotice(NoticeInfo n, ExamDbDataContext dc)
        {
            AttachInfo<NoticeInfo>(dc.NoticeInfo, n);
            dc.SubmitChanges();
        }
        /// <summary>
        /// 根据id删除通知
        /// </summary>
        /// <param name="noticeID">通知id</param>
        public static void DeleteNotice(int noticeID)
        {
            ExamDbDataContext dc = DataAccess.CreateDBContext();
            NoticeInfo s = dc.NoticeInfo.Single(p => p.NoticeID == noticeID);
            if (s == null) return;
            if (s.HasAttachment == true && s.AttachmentPath != null)
            {
                if (File.Exists(s.AttachmentPath)) File.Delete(s.AttachmentPath);
            }
            dc.NoticeInfo.DeleteOnSubmit(s);
            dc.SubmitChanges();
        }
    }
}
