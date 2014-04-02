using System.Collections.Generic;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestMSet
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
        public void Should_return_status_ok()
        {
            var testKeys = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("key1", "value1") };
            _redisCommand.MSet(testKeys).ShouldEqual("OK");
        }

        [Test]
        public void Should_set_a_key()
        {
            var testKeys = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("key1", "value1") };
            _redisCommand.MSet(testKeys).ShouldEqual("OK");

            _redisCommand.Get("key1").ShouldEqual("value1");
        }

        [Test]
        public void Should_set_multiple_keys()
        {
            var testKeys = new List<KeyValuePair<string, string>>
                               {
                                   new KeyValuePair<string, string>("key1", "value1"),
                                   new KeyValuePair<string, string>("key2", "value2"),
                                   new KeyValuePair<string, string>("key3", "value3")
                               };
            _redisCommand.MSet(testKeys).ShouldEqual("OK");

            _redisCommand.Get("key1").ShouldEqual("value1");
            _redisCommand.Get("key2").ShouldEqual("value2");
            _redisCommand.Get("key3").ShouldEqual("value3");
        }
    }
}