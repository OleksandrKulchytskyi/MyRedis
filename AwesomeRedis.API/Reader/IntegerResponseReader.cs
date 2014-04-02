using System.Collections.Generic;
using System.IO;
using AwesomeRedis.API.Connect;

namespace AwesomeRedis.API.Reader
{
    public class IntegerResponseReader : IResponseReader
    {
        const char IntegerIdentifier = ':';

        public IEnumerable<string> ReadResponse(StreamReader stream)
        {
            var response = stream.ReadLine();
            return new[] {response.TrimStart(IntegerIdentifier)};
        }

        public bool ShouldRead(char identifier)
        {
            return identifier == IntegerIdentifier;
        }
    }
}