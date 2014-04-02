using System.Linq;
using AwesomeRedis.API.Reader;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.ResponseReaderTest
{
    [TestFixture]
    public class TestBulkResponse
    {
        [Test]
        public void Should_have_only_one_payload_in_result()
        {
            var testResponseReader = new BulkResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("6\r\nfoobar\r\n");

            var result = testResponseReader.ReadResponse(testStream.GetStreamReaderForTesting());
            result.Count().ShouldEqual(1);
        }

        [Test]
        public void Should_have_correct_payload_in_result()
        {
            var testResponseReader = new BulkResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("6\r\nfoobar\r\n");

            var result = testResponseReader.ReadResponse(testStream.GetStreamReaderForTesting());

            result.ShouldContain("foobar");            
        }

        [Test]
        public void Should_return_null_when_theres_nothing_in_bulk()
        {
            var testResponseReader = new BulkResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("-1\r\n");

            var result = testResponseReader.ReadResponse(testStream.GetStreamReaderForTesting());

            result.ShouldContain(null);            
        }
    }
}