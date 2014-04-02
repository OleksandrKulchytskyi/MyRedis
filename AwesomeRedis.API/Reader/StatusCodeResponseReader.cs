using System.Collections.Generic;
using System.IO;
using AwesomeRedis.API.Connect;

namespace AwesomeRedis.API.Reader
{
    public class StatusCodeResponseReader : IResponseReader
    {
        const char ErrorStatusIdentifier = '-';
        const char OkStatusIdentifier = '+';

        public IEnumerable<string> ReadResponse(StreamReader stream)
        {
            var response = stream.ReadLine();
            return new[] {response.TrimStart(new[] { OkStatusIdentifier, ErrorStatusIdentifier })};
        }

        public bool ShouldRead(char identifier)
        {
            return (identifier == OkStatusIdentifier) || (identifier == ErrorStatusIdentifier);
        }
    }
}