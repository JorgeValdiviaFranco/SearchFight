using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight
{
    public interface ISearchEngine
    {
        string Name { get; }
         long Run(string query);

    }

    public class SearchEngineRunner : ISearchEngine
    {
        public string Name { get; set; }
        public Dictionary<string, long> Results { get; set; }


        public SearchEngineRunner(string name, Dictionary<string, long> results)
        {
            Name = name;
            Results = results;
        }

        public long Run(string query)
        {
            return Results[query];

        }
    }
}
