using System.Collections.Generic;

namespace AwesomeRedis.API.Connect
{
    public class CommandSender
    {
        public const string Crlf = "\r\n";

        private readonly RedisConnectionHandler _connection;

        public CommandSender(RedisConnectionHandler connection)
        {
            _connection = connection;
        }

        public void SendMessage(string command, params string[] args)
        {
            var commandList = new List<string>(args);
            commandList.Insert(0, command);

            var message = StringFormatter.FormatStringToSend(commandList);

            _connection.StreamWrite.Write(message);
        }
    }
}