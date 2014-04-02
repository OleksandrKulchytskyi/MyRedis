using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestGetSet
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
        public void Should_return_old_value_when_setting_new_value()
        {
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.GetSet("testkey", "newvalue").ShouldEqual("testvalue");
        }

        [Test]
        public void Should_set_value_to_new_value()
        {
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.GetSet("testkey", "newvalue");
      
            _redisCommand.Get("testkey").ShouldEqual("newvalue");
        }

        [Test]
        public void Should_return_null_when_key_didnt_exist()
        {
            _redisCommand.FlushDb();

            _redisCommand.GetSet("thiskeyshouldntexist", "newvalue").ShouldBeNull();
        }

        [Test]
        [Ignore]
        public void Should_return_error_when_value_is_not_string_value()
        {
            
        }
    }
}