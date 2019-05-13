using SearchFight.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace SearchFight.SearchEngine
{
    public class WebSearchEngine : SerializableSearchRunner
    {
        [XmlAttribute]
        public string Address { get; set; }

        [XmlAttribute]
        public string QueryName { get; set; }

        public StringDictionary Parameters { get; set; }
        public ResultFinder Finder { get; set; }
        public QueryFormatter QueryFormatter { get; set; }
        public TextClient Client { get; set; }

        public WebSearchEngine() { }

     
        public override long Run(string query)
        {
            if (Finder == null)
                throw new ConfigurationException("Finder cannot be null.");

            if (Address == null)
                throw new ConfigurationException("Address cannot be null.");

            if (string.IsNullOrWhiteSpace(QueryName))
                throw new ConfigurationException("QueryName cannot be empty.");

            var uriBuilder = BuildUri(query);
            var responseText = (Client ?? TextClient.Default).GetResponseText(uriBuilder.Uri);
            var resultText = Finder.Find(responseText);
            return Parse(resultText);
        }

        private long Parse(string text)
        {
            return long.Parse(text.Replace(",", "").Replace(".", ""));
        }

        private UriBuilder BuildUri(string query)
        {
            var parameters = HttpUtility.ParseQueryString(String.Empty);

           if (Parameters != null)
           {
                foreach (var param in Parameters)
                   parameters[param.Key] = param.Value;
            }

            parameters[QueryName] = query;   
            try
            {
                var uriBuilder = new UriBuilder(Address);
                uriBuilder.Query = parameters.ToString();
                return uriBuilder;
            }
            catch (UriFormatException ex)
            {
                throw new ConfigurationException("The given Address is not a valid URL.", ex);
            }
        }
    }
}
