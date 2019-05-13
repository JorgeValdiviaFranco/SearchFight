using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchFight.SearchEngine;
using System.IO;
using SearchFight.Utils;
using System.Xml.Serialization;

namespace SearchFight

{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] args2 = { ".net", "java" };
                Search(args2);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("An unexpected exception has occurred: " + Environment.NewLine + ex.ToString());
            }
            Console.ReadLine();
        }

        private static void Search(string[] args)
        {
            try
            {
                if (args.Length == 0)
                    throw new Exception("Expected at least one argument.");

                if (args.Length == 1)
                {

                    Console.WriteLine("{0} winner: {1}", args[0], "Not Applicable");
                }
                else
                {

                    var engines = new ConfigurationFactory().CreateEngines();
                    List<SearchEngineResult> resultList = new List<SearchEngineResult>();

                    Parallel.ForEach(engines, engine =>
                    {
                        foreach (var query in args)
                        {
                            long result = engine.Run(query);
                            resultList.Add(new SearchEngineResult() { Count = result, Name = engine.Name, Query = query });
                        }
                    }
                    );
                    resultList.ForEach(
                        r => Console.WriteLine("Engine:{0} Language: {1}  Total:{2} ", r.Name.ToString(), r.Query, r.Count.ToString()));

                    Console.WriteLine();
                    var calculateResults = resultList.GroupBy(
                                                 r => r.Query,
                                                 r => r.Count,
                                                 (language, count) => new
                                                 {
                                                     Key = language,
                                                     Total = count.Sum()
                                                 });



                    calculateResults.ToList().Sort((x, y) => x.Total.CompareTo(y.Total));
                    calculateResults.ToList().ForEach(
                          r => Console.WriteLine("Language: {0} Total: {1} ", r.Key, r.Total.ToString()));

                    Console.WriteLine();
                    Console.WriteLine("Total winner: {0}", calculateResults.FirstOrDefault().Key);
                    Console.WriteLine();
                }
            }
            catch (ConfigurationException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
            catch (AggregateException ex)
            {


                Console.WriteLine();
                Console.WriteLine(string.Format("Runner '{0}' failed. {1}", ex, ex.Message));

            }
        }

    }
}

