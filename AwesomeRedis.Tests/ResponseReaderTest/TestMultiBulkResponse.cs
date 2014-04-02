using System.Linq;
using AwesomeRedis.API.Reader;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.ResponseReaderTest
{
    [TestFixture]
    public class TestMultiBulkResponse
    {
        [Test]
        public void Should_return_correct_number_of_items()
        {
            var testResponseReader = new MultiBulkResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("3\r\n$3\r\nfoo\r\n$3\r\nbar\r\n$5\r\nHello\r\n");

            var result = testResponseReader.ReadResponse(testStream.GetStreamReaderForTesting());

            result.Count().ShouldEqual(3);
        }

        [Test]
        public void Should_contain_correct_payload_contents()
        {
            var testResponseReader = new MultiBulkResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("3\r\n$3\r\nfoo\r\n$3\r\nbar\r\n$5\r\nHello\r\n");

            var result = testResponseReader.ReadResponse(testStream.GetStreamReaderForTesting());
            
            result.ShouldContain("foo");
            result.ShouldContain("bar");
            result.ShouldContain("Hello");
        }

        [Test]
        public void Should_return_empty_list_when_recieving_empty_payload()
        {
            var testResponseReader = new MultiBulkResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("0\r\n");

            var result = testResponseReader.ReadResponse(testStream.GetStreamReaderForTesting());

            result.ShouldBeEmpty();
        }

        [Test]
        public void Should_return_null_when_getting_nil_response()
        {
            var testResponseReader = new MultiBulkResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("-1\r\n");

            var result = testResponseReader.ReadResponse(testStream.GetStreamReaderForTesting());

            result.ShouldBeNull();
        }
    }
}