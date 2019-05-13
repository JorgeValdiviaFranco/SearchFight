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
        bool Disabled { get; }
        Task<long> Run(string query);
    }

    public class SearchRunner : ISearchEngine
    {
        public string Name { get; set; }
        public Dictionary<string, long> Results { get; set; }

        public bool Disabled
        {
            get { return false; }
        }

        public SearchRunner(string name, Dictionary<string, long> results)
        {
            Name = name;
            Results = results;
        }

        public Task<long> Run(string query)
        {
            return Task.FromResult(Results[query]);
        }
    }
}
