using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchFight.SearchEngine
{

    interface ISearchEngineFactory

    {

        List<SerializableSearchRunner> CreateEngines();

    }


    public class ConfigurationFactory : ISearchEngineFactory
    {

       public List<SerializableSearchRunner> CreateEngines()
        {

            throw new Exception("");
        }


    }


    public class SearchEngineConfiguration
    {
        [XmlArrayItem("SearchRunner")]
        public List<SerializableSearchRunner> SearchRunners { get; set; }


         
    }
}
