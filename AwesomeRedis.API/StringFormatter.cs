using System;
using System.Collections.Generic;
using AwesomeRedis.API.Connect;

namespace AwesomeRedis.API
{
    public class StringFormatter
    {
        public static string FormatStringToSend(List<string> commandList)
        {
            var message = String.Format("*{0}{1}", commandList.Count, CommandSender.Crlf);
            foreach (var s in commandList)
            {
                message += String.Format("${0}{1}", s.Length, CommandSender.Crlf);
                message += String.Format("{0}{1}", s, CommandSender.Crlf);
            }
            return message;
        }
    }
}