using System.Linq;
using AwesomeRedis.API.Reader;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.ResponseReaderTest
{
    [TestFixture]
    public class TestStatusResponse
    {
        [Test]
        public void Should_return_ienum_with_one_response_value()
        {
            var testReader = new StatusCodeResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("OK");

            var results = testReader.ReadResponse(testStream.GetStreamReaderForTesting());

            results.Count().ShouldEqual(1);
        }

        [Test]
        public void Should_contain_response_in_first_element()
        {
                        var testReader = new StatusCodeResponseReader();
            var testStream = new SampleStreamForTests();

            testStream.WriteToStream("OK");

            var results = testReader.ReadResponse(testStream.GetStreamReaderForTesting());

            results.ShouldContain("OK");
        }
    }
}