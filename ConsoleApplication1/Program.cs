using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.OleDb;
using SDPTThesis.DAL.Linq;
using SDPTThesis.DAL.Model;
using System.Web;
using System.ComponentModel;
using SDPTThesis.BLL;
using SDPTThesis.Web.UI;
using System.Reflection;
using System.Data.Linq.Mapping;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            CheckConflictReason();
            Console.Read();
        }

      static  void CheckConflictReason()
        {

            ThesisDbDataContext db = new ThesisDbDataContext(ConfigurationManager.ConnectionStrings["SQLConnStr"].ConnectionString);
            StudentInfo s =  db.StudentInfo.Where(p => p.StudentID == 11125).Single();
            s.HomePhoneNum = "asdfasdf";
            s.StuName = "hahah";
            //db.StudentInfo.Attach(s,);
            try
            {
          //      db.StudentInfo.InsertOnSubmit(s);
              db.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch (ChangeConflictException e)
            {
                Console.WriteLine("Optimistic concurrency error.");
                Console.WriteLine(e.Message);
             //   Console.ReadLine();
                foreach (ObjectChangeConflict occ in db.ChangeConflicts)
                {
                    MetaTable metatable = db.Mapping.GetTable(occ.Object.GetType());
                    StudentInfo  entityInConflict = (StudentInfo)occ.Object;
                    Console.WriteLine("Table name: {0}", metatable.TableName);
                    Console.Write("Student ID: ");
                    Console.WriteLine(entityInConflict.StudentID);
                    Console.Write("Student Name: ");
                    Console.WriteLine(entityInConflict.StuName);
                    Console.WriteLine(occ.Object.ToString());
                  //  occ.Resolve(RefreshMode.KeepChanges);
                    Console.WriteLine("isresolve:" + occ.IsResolved);
                    Console.WriteLine(occ.MemberConflicts.Count);
                    foreach (MemberChangeConflict mcc in occ.MemberConflicts)
                    {
                        Console.WriteLine("还是有成员冲突阿");
                        object currVal = mcc.CurrentValue;
                        object origVal = mcc.OriginalValue;
                        object databaseVal = mcc.DatabaseValue;
                        MemberInfo mi = mcc.Member;
                        Console.WriteLine("Member: {0}", mi.Name);
                        Console.WriteLine("current value: {0}", currVal);
                        Console.WriteLine("original value: {0}", origVal);
                        Console.WriteLine("database value: {0}", databaseVal);
                    }
                }
            }
            catch (Exception ee)
            {
                // Catch other exceptions.
                Console.WriteLine(ee.Message);
            }
            finally
            {
                Console.WriteLine("TryCatch block has finished.");
            }

          //  db.SubmitChanges(ConflictMode.FailOnFirstConflict);

        }
    }
}
