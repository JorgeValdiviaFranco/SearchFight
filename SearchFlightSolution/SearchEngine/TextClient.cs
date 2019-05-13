using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SearchFight.Utils;
using System.ComponentModel;
using System.Diagnostics;

namespace SearchFight.SearchEngine
{
    [XmlInclude(typeof(HttpTextClient))]
    public abstract class TextClient
    {
        public static readonly TextClient Default = new HttpTextClient();

      
        public abstract string GetResponseText(Uri uri);

    }

    public class HttpTextClient : TextClient
    {
        private const long DefaultTimeout = 30000;

        [XmlAttribute]
        [DefaultValue(DefaultTimeout)]
        public long Timeout { get; set; }

       


        public  override string GetResponseText(Uri uri)
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip }))
            {
                if (Timeout > 0)
                    client.Timeout = TimeSpan.FromMilliseconds(Timeout);
                else
                    client.Timeout = TimeSpan.FromMilliseconds(DefaultTimeout);


                try
                {
                    return client.GetStringAsync(uri).Result;
                }
                catch (HttpRequestException ex)
                {
                    throw new WebRequestException(ex.Message, ex);
                }
                catch (TaskCanceledException ex)
                {
                    throw new WebRequestException("The request timed out.", ex);
                }
            }
        }
    }
}
