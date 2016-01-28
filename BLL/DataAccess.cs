using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SDPTExam.DAL.Linq;
namespace SDPTExam.BLL
{

    /// <summary>
    /// 数据工厂类
    /// </summary>
 public static class DataAccess
    {
       public static ExamDbDataContext CreateDBContext()
       {
        return new ExamDbDataContext(ConfigurationManager.ConnectionStrings["SQLConnStr"].ConnectionString); 
       }
    }
}
