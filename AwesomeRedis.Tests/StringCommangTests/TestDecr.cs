using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestDecr
    {
        private RedisInstance _redis;
        private StringCommand _redisCommand;

        [TestFixtureSetUp]
        public void InitTests()
        {
            _redis = TestObjectGetter.GetDefaultRedisInstance();
            _redis.Start();
            _redisCommand = TestObjectGetter.GetLocalhostStringCommandSender();
        }

        [TestFixtureTearDown]
        public void DisposeTests()
        {
            _redisCommand.FlushDb();
            _redis.Stop();
        }

        [Test]
        public void Should_return_string()
        {
            _redisCommand.Set("samplekey", "samplevalue");

            _redisCommand.Decr("samplekey").ShouldBeType<string>();
        }

        [Test]
        public void Return_string_should_be_an_error_string()
        {
            _redisCommand.Set("samplekey", "samplevalue");

            _redisCommand.Decr("samplekey").Substring(0, 3).ShouldEqual("ERR");
        }
    }
}