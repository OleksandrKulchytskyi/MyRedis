using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeRedis.API.Connect;

namespace AwesomeRedis.API
{
    public class StringCommand : Command
    {
        public StringCommand(CommandSender sender, ResponseGetter getter) : base(sender, getter)
        {
        }

        public string Set(string key, string value)
        {
            Sender.SendMessage(CommandStringConstants.SET, key, value);
            
            return Getter.ReadResponse().ElementAt(0);
        }

        public int SetNx(string key, string value)
        {
            Sender.SendMessage(CommandStringConstants.SETNX, key, value);

            return Convert.ToInt32(Getter.ReadResponse().ElementAt(0));
        }

        public string Get(string key)
        {
            Sender.SendMessage(CommandStringConstants.GET, key);

            var response = Getter.ReadResponse();
            if (response == null)
            {
                return null;
            }
            return response.ElementAt(0);
        }

        public int Strlen(string key)
        {
            Sender.SendMessage(CommandStringConstants.STRLEN, key);

            return Convert.ToInt32(Getter.ReadResponse().ElementAt(0));
        }

        public int Append(string key, string valueToAppend)
        {
            Sender.SendMessage(CommandStringConstants.APPEND, key, valueToAppend);

            return Convert.ToInt32(Getter.ReadResponse().ElementAt(0));
        }

        public string Decr(string key)
        {
            Sender.SendMessage(CommandStringConstants.DECR, key);

            return Getter.ReadResponse().ElementAt(0);
        }

        public string DecrBy(string key, int value)
        {
            Sender.SendMessage(CommandStringConstants.DECRBY, key, value.ToString());

            return Getter.ReadResponse().ElementAt(0);
        }

        public int GetBit(string key, int offset)
        {
            Sender.SendMessage(CommandStringConstants.GETBIT, key, offset.ToString());

            return Convert.ToInt32(Getter.ReadResponse().ElementAt(0));
        }

        public string GetRange(string key, int startIndex, int endIndex)
        {
            Sender.SendMessage(CommandStringConstants.GETRANGE, key, startIndex.ToString(), endIndex.ToString());

            return Getter.ReadResponse().ElementAt(0);
        }

        public string GetSet(string key, string value)
        {
            Sender.SendMessage(CommandStringConstants.GETSET, key, value);


            var response = Getter.ReadResponse();
            if (response == null)
            {
                return null;
            }
            return response.ElementAt(0);
        }

        public string Incr(string key)
        {
            Sender.SendMessage(CommandStringConstants.INCR, key);

            return Getter.ReadResponse().ElementAt(0);
        }

        public string IncrBy(string key, int value)
        {
            Sender.SendMessage(CommandStringConstants.INCR, key, value.ToString());

            return Getter.ReadResponse().ElementAt(0);
        }

        public string IncrByFloat(string key, float value)
        {
            Sender.SendMessage(CommandStringConstants.INCRBYFLOAT, key, value.ToString());

            return Getter.ReadResponse().ElementAt(0);
        }

        public IEnumerable<string> MGet(IEnumerable<string> keys)
        {
            Sender.SendMessage(CommandStringConstants.MGET, keys.ToArray());

            return Getter.ReadResponse();
        }

        public string MSet(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var argumentList = new List<string>();
            foreach (var keyValuePair in keyValuePairs)
            {
                argumentList.Add(keyValuePair.Key);
                argumentList.Add(keyValuePair.Value);
            }
            Sender.SendMessage(CommandStringConstants.MSET, argumentList.ToArray());

            return Getter.ReadResponse().ElementAt(0);
        }

        public int MSetNx(List<KeyValuePair<string, string>> keyValuePairs)
        {
            var argumentList = new List<string>();
            foreach (var keyValuePair in keyValuePairs)
            {
                argumentList.Add(keyValuePair.Key);
                argumentList.Add(keyValuePair.Value);
            }
            Sender.SendMessage(CommandStringConstants.MSETNX, argumentList.ToArray());

            return Convert.ToInt32(Getter.ReadResponse().ElementAt(0));
        }

        public int SetBit(string key, int offset, int value)
        {
            Sender.SendMessage(CommandStringConstants.SETBIT, key, offset.ToString(), value.ToString());

            return Convert.ToInt32(Getter.ReadResponse().ElementAt(0));
        }

        public string SetEx(string key, int seconds, string value)
        {
            Sender.SendMessage(CommandStringConstants.SETEX, key, seconds.ToString(), value);

            return Getter.ReadResponse().ElementAt(0);
        }

        public int SetRange(string key, int offset, string value)
        {
            Sender.SendMessage(CommandStringConstants.SETRANGE, key, offset.ToString(), value);

            return Convert.ToInt32(Getter.ReadResponse().ElementAt(0));
        }

        public void FlushDb()
        {
            Sender.SendMessage(CommandStringConstants.FLUSHDB);
            Getter.ReadResponse();
        }
    }
}