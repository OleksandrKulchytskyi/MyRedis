using System.Collections.Generic;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestStrLen
    {
        private RedisInstance _redis;
        private StringCommand _redisCommand;
        private KeyValuePair<string, string> _keyValuePair;

        [TestFixtureSetUp]
        public void InitTests()
        {
            _redis = TestObjectGetter.GetDefaultRedisInstance();
            _redis.Start();
            _redisCommand = TestObjectGetter.GetLocalhostStringCommandSender();

            _keyValuePair = new KeyValuePair<string, string>("testkey", "testvalue");
            _redisCommand.Set(_keyValuePair.Key, _keyValuePair.Value);
        }

        [TestFixtureTearDown]
        public void DisposeTests()
        {
            _redisCommand.FlushDb();
            _redis.Stop();
        }

        [Test]
        public void TestSendStrlenToInstance()
        {
            _redisCommand.Strlen(_keyValuePair.Key).ShouldEqual(_keyValuePair.Value.Length);
        }

        [Test]
        public void Should_return_0_when_key_dont_exist()
        {
            _redisCommand.Strlen("keythatdontexist").ShouldEqual(0);
        }

        [Test]
        [Ignore]
        public void Should_return_error_when_value_isnt_a_string()
        {
            //TODO write this test once set for int's has been implemented
        }
    }
}