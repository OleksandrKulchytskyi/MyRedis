using System.Threading;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestSetEx
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
        public void Should_return_status_code_OK()
        {
            _redisCommand.FlushDb();
            _redisCommand.SetEx("testkey", 12, "testvalue").ShouldEqual("OK");
        }

        [Test]
        public void Should_set_value_for_given_key()
        {
            _redisCommand.FlushDb();
            _redisCommand.SetEx("testkey", 60, "testvalue");

            _redisCommand.Get("testkey").ShouldEqual("testvalue");
        }

        [Test]
        public void Should_return_error_for_invalid_seconds()
        {
            _redisCommand.FlushDb();
            _redisCommand.SetEx("testkey", 0, "testvalue").Substring(0, 3).ShouldEqual("ERR");
        }

        [Test]
        public void Should_expire_value_after_timeout()
        {
            _redisCommand.FlushDb();
            _redisCommand.SetEx("testkey", 6, "testvalue");

            _redisCommand.Get("testkey").ShouldEqual("testvalue");

            Thread.Sleep(10000);

            _redisCommand.Get("testkey").ShouldBeNull();
        }
    }
}