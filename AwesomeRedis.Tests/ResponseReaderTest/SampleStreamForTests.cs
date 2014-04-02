using System.IO;

namespace AwesomeRedis.Tests.ResponseReaderTest
{
    public class SampleStreamForTests
    {
        private MemoryStream memStream;

        public SampleStreamForTests()
        {
            this.memStream = new MemoryStream();
        }

        public void WriteToStream(string toWrite)
        {
            var writer = new StreamWriter(memStream);

            writer.Write(toWrite);
            writer.Flush();

            memStream.Seek(0, SeekOrigin.Begin);
        }

        public StreamReader GetStreamReaderForTesting()
        {
            return new StreamReader(memStream);
        }
    }
}