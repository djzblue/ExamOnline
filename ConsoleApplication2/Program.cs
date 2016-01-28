using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<int> allsingleChoiceIDs = new List<int> { 234, 456, 67, 23, 56, 2, 67, 34, 55 };

            int totalCount = allsingleChoiceIDs.Count();

            IList<int> results = new List<int>();
            Random r = new Random();
            while(true)
            {              
                int j = r.Next(totalCount);
                if(results.Contains(allsingleChoiceIDs[j])==false)
                results.Add(allsingleChoiceIDs[j]);
                if (results.Count >= 8) break;

            }

            foreach (int j in results)
                Console.WriteLine(j);

            Console.Read();
            
        }

        public IList<int> GetRandomList(IList<int> source,  int count)
        {
            
            int totalCount = source.Count;

            if (totalCount < count) return null;

            IList<int> results = new List<int>();
            Random r = new Random();

            while (true)
            {
                int j = r.Next(totalCount);
                if (results.Contains(source[j]) == false)
                    results.Add(source[j]);
                if (results.Count == count) break;//已经获取到指定数目

            }

            return results;
        
        }


    }
}
