using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeRedis.API.Connect;

namespace AwesomeRedis.API
{
    public class GenericCommand : Command
    {
        public GenericCommand(CommandSender sender, ResponseGetter getter) : base(sender, getter)
        {
        }

        public IEnumerable<string> ExecuteCommand(string command)
        {
            var commandElements = command.Split(' ');

            var arguments = new List<string>();
            for (var i = 1; i < commandElements.Length; i++)
            {
                if (commandElements[i].StartsWith("\""))
                {
                    var quotedString = commandElements[i].TrimStart('"');

                    do
                    {
                        if (i+1 >= commandElements.Length) { throw new Exception("No matching quotes");}
                        quotedString += String.Format("{0}{1}", " ", commandElements[++i].TrimEnd('"'));
                    } while (!commandElements[i].EndsWith("\""));

                    arguments.Add(quotedString);
                }
                else
                {
                    arguments.Add(commandElements[i]);
                }
            }

            Sender.SendMessage(commandElements[0], arguments.ToArray());

            return Getter.ReadResponse();
        }
    }
}