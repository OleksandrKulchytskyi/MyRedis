using System.Linq;
using AwesomeRedis.API.Reader;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.ResponseReaderTest
{
    [TestFixture]
    public class TestIntegerResponse
    {
        [Test]
        public void Should_return_ienumerable_of_size_one()
        {
            var testReader = new IntegerResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("0\r\n");

            var result = testReader.ReadResponse(testStream.GetStreamReaderForTesting());

            result.Count().ShouldEqual(1);
        }

        [Test]
        public void Should_contain_string_100()
        {
            var testReader = new IntegerResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("100\r\n");

            var result = testReader.ReadResponse(testStream.GetStreamReaderForTesting());

            result.ShouldContain("100");
        }
    }
}