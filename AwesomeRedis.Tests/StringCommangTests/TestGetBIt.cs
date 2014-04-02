using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestGetBit
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
        public void Should_get_bit_at_offset_0()
        {
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.GetBit("testkey", 0).ShouldEqual(0);
        }

        [Test]
        public void Should_get_bit_at_offset_1()
        {
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.GetBit("testkey", 1).ShouldEqual(1);
        }
    }
}