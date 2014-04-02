using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestSetRange
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
        public void Should_set_value_from_offset_to_end_of_new_value()
        {
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.SetRange("testkey", 4, "setrange");

            _redisCommand.Get("testkey").ShouldEqual("testsetrange");
        }

        [Test]
        public void Should_set_value_of_nonexisting_key_with_padded_0s()
        {
            _redisCommand.FlushDb();
            _redisCommand.SetRange("testkey", 4, "testvalue");

            _redisCommand.Get("testkey").ShouldEqual("\u0000\u0000\u0000\u0000testvalue");
        }

        [Test]
        public void Should_return_new_length_of_string()
        {
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.SetRange("testkey", 4, "setrange").ShouldEqual(12);
        }
    }
}