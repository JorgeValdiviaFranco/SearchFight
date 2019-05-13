using System;
using System.Collections.Generic;
using System.IO;
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


    public class SearchEngineConfiguration
    {
        [XmlArrayItem("SearchRunner")]
        public List<SerializableSearchRunner> SearchRunners { get; set; }



    }

    public class ConfigurationFactory : ISearchEngineFactory
    {

       public  List<SerializableSearchRunner> CreateEngines()
        {


                try
                {
                    using (var stream = File.OpenRead("SearchEngineConfiguration.xml"))
                    {
                        try
                        {
                            var serializer = new XmlSerializer(typeof(SearchEngineConfiguration));
                            return ((SearchEngineConfiguration)serializer.Deserialize(stream)).SearchRunners;
                        }
                        catch (InvalidOperationException ex)
                        {
                            throw new ConfigurationException("The configuration file is invalid. " + ex.Message, ex);
                        }
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    throw new ConfigurationException("Unauthorized exception trying to access cofiguration file.", ex);
                }
                catch (FileNotFoundException ex)
                {
                    throw new ConfigurationException("Could not find configuration file.", ex);
                }
                catch (IOException ex)
                {
                    throw new ConfigurationException("An error occurred when reading configuration file.", ex);
                }
        }


    }


}
