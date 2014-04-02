using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestGetRange
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
        public void Should_get_first_letter_of_key()
        {
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.GetRange("testkey", 0, 0).ShouldEqual("t");
        }

        [Test]
        public void Should_get_first_word_of_value()
        {
            _redisCommand.Set("testkey", "test me");

            _redisCommand.GetRange("testkey", 0, 3).ShouldEqual("test");
            
        }

        [Test]
        public void Should_get_second_word_of_value()
        {
            _redisCommand.Set("testkey", "test me");

            _redisCommand.GetRange("testkey", 5, 6).ShouldEqual("me");

        }

    }
}