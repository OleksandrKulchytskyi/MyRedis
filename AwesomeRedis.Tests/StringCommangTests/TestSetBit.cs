using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestSetBit
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
        public void Should_set_bit_of_value_to_1()
        {
            _redisCommand.FlushDb();
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.SetBit("testkey", 0, 1);
            _redisCommand.GetBit("testkey", 0).ShouldEqual(1);
        }

        [Test]
        public void Should_set_bit_of_value_to_0()
        {
            _redisCommand.FlushDb();
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.SetBit("testkey", 1, 0);
            _redisCommand.GetBit("testkey", 1).ShouldEqual(0);
        }

        [Test]
        public void Should_return_original_value_of_bit()
        {
            _redisCommand.FlushDb();
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.SetBit("testkey", 1, 0).ShouldEqual(1);
            _redisCommand.SetBit("testkey", 0, 1).ShouldEqual(0);
        }

    }
}