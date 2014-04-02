using System.Collections.Generic;
using System.IO;

namespace AwesomeRedis.API.Reader
{
    public class BulkResponseReader : IResponseReader
    {
        private const char BulkIdentifier = '$';
        private const string NoResponseIdentifier = "-1";

        public IEnumerable<string> ReadResponse(StreamReader stream)
        {
            var results = new List<string>();
            var numberOfCharacters = stream.ReadLine();
            if (numberOfCharacters == NoResponseIdentifier) { results.Add(null); }
            else
            {
                results.Add(stream.ReadLine());
            }

            return results;
        }

        public bool ShouldRead(char identifier)
        {
            return identifier == BulkIdentifier;
        }
    }
}