using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchFight.SearchEngine
{
    [XmlInclude(typeof(WebSearchEngine))]
    public abstract class SerializableSearchRunner : ISearchEngine
    {
        [XmlAttribute]
        public string Name { get; set; }

        public abstract Task<long> Run(string query);
    }
}
