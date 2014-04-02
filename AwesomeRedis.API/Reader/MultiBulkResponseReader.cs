using System;
using System.Collections.Generic;
using System.IO;
using AwesomeRedis.API.Connect;

namespace AwesomeRedis.API.Reader
{
    public class MultiBulkResponseReader : IResponseReader
    {
        public const char Identifier = '*';

        public IEnumerable<string> ReadResponse(StreamReader stream)
        {
            var results = new List<string>();

            var numberOfResults = Int32.Parse(stream.ReadLine());
            if (numberOfResults == -1)
            {
                return null;
            }
            for (var i = 0; i < numberOfResults; i++)
            {
                stream.Read();
                results.AddRange(new BulkResponseReader().ReadResponse(stream));
            }

            return results;
        }

        public bool ShouldRead(char identifier)
        {
            return identifier == Identifier;
        }
    }
}