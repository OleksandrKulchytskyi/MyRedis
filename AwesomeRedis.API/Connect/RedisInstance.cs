using System.Diagnostics;

namespace AwesomeRedis.API.Connect
{
    public class RedisInstance
    {
        public string RedisLocation { get; set; }
        private int _id;

        public RedisInstance(string serverPath)
        {
            RedisLocation = serverPath;
        }

        public void Start()
        {
            var process = new Process {StartInfo = {FileName = RedisLocation, CreateNoWindow = true, UseShellExecute = false}};

            if (process.Start()) _id = process.Id;
        }

        public void Stop()
        {
            Process.GetProcessById(_id).Kill();
        }
    }
}