using System;
using System.Collections.Generic;
using AwesomeRedis.API.Reader;

namespace AwesomeRedis.API.Connect
{
    public class ResponseGetter
    {
        private readonly RedisConnectionHandler _connection;
        private readonly IEnumerable<IResponseReader> _readers;

        public ResponseGetter(RedisConnectionHandler connection, IEnumerable<IResponseReader> readers)
        {
            _connection = connection;
            this._readers = readers;
        }

        public IEnumerable<string> ReadResponse()
        {
            var identifier = _connection.StreamRead.Read();
            foreach (var responseReader in _readers)
            {
                if (responseReader.ShouldRead((char) identifier))
                {
                    return responseReader.ReadResponse(_connection.StreamRead);
                }
            }

            #region OLDCODE
//            switch (response[0])
//            {
//                case '+':
//                    return GetStatusCodeReply(response);
//                case '-':
//                    return GetStatusCodeReply(response);
//                case ':':
//                    return GetIntegerResponse(response);
//                case '$':
//                    return GetBulkResponse(response);
//                case '*':
//                    return GetMultiBulkResponse(response);
//            }
            #endregion

            throw new Exception("Unrecognize message type. Holy Shit.");
        }

        #region OLDCODE
//        private string GetIntegerResponse(string response)
//        {
//            return response.TrimStart(':');
//        }

//        private string GetStatusCodeReply(string response)
//        {
//            return response.TrimStart(new[]{'+', '-'});
//        }

//        private string GetBulkResponse(string numberOfCharacters)
//        {
//            if (numberOfCharacters == "$-1") { return null; }
//            return GetNextLine();
//        }

//        private string GetMultiBulkResponse(string numberOfBulks)
//        {
//            var results = String.Empty;

//            var numberOfResults = Int32.Parse(numberOfBulks.Substring(1));
//            for (var i = 0; i < numberOfResults; i++)
//            {
//                var numberOfCharacters = _connection.StreamRead.ReadLine();
//                results += GetBulkResponse(numberOfCharacters);
//            }

//            return results;
//        }
        #endregion
    }
}