using System;
using System.IO;
using System.Net.Sockets;

namespace AwesomeRedis.API.Connect {

    public class RedisConnectionHandler {

         public TcpClient Client;
         public StreamWriter StreamWrite;
         public StreamReader StreamRead;

        public RedisConnectionHandler(string host, int port) {
            Client = new TcpClient(host, port);
            try{
                var stream = Client.GetStream();
                StreamWrite = new StreamWriter(stream) {AutoFlush = true};
                StreamRead = new StreamReader(stream);
            }
            catch (Exception e){
                throw new Exception(String.Format("Connection failed! - {0}", e));
            }
        }

        public void Close() {
            Client.Close();
        }
    }
}