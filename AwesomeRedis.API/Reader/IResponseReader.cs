using System.Collections.Generic;
using System.IO;

namespace AwesomeRedis.API.Reader
{
    public interface IResponseReader
    {
        IEnumerable<string> ReadResponse(StreamReader stream);
        bool ShouldRead(char identifier);
    }
}