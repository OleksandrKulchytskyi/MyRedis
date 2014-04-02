using System.Collections.Generic;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestSet
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
        }

        [TestFixtureTearDown]
        public void DisposeTests()
        {
            _redisCommand.FlushDb();
            _redis.Stop();
        }

        [Test]
        public void Should_Set_Testkey_To_Testvalue()
        {
            _redisCommand.Set(_keyValuePair.Key, _keyValuePair.Value).ShouldEqual("OK");
        }
    }
}