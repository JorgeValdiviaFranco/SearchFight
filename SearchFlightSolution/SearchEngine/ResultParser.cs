using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchFight.SearchRunners
{
    [XmlInclude(typeof(DefaultResultParser2))]
    public abstract class ResultParser2
    {
        public static readonly ResultParser2 Default = new DefaultResultParser2();

        public abstract long Parse(string result);
    }

    public class DefaultResultParser2 : ResultParser2
    {
        public override long Parse(string result)
        {
            if(result == null)
                throw new ArgumentNullException("result");

            try
            {
                return long.Parse(result.Replace(",", "").Replace(".", ""));
            }
            catch (FormatException ex)
            {
                throw new ParsingException(ex.Message, ex);
            }
            catch (OverflowException ex)
            {
                throw new ParsingException(ex.Message, ex);
            }
        }
    }
}
