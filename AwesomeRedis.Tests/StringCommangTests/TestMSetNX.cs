using System.Collections.Generic;
using System.Linq;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestMSetNX
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
        public void Should_set_a_key()
        {
            _redisCommand.FlushDb();

            var testKeys = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("key1", "value1") };
            _redisCommand.MSetNx(testKeys);

            _redisCommand.Get(testKeys[0].Key).ShouldEqual(testKeys[0].Value);
        }

        [Test]
        public void Should_set_multiple_keys()
        {
            _redisCommand.FlushDb();

            var testKeys = new List<KeyValuePair<string, string>>
                               {
                                   new KeyValuePair<string, string>("key1", "value1"),
                                   new KeyValuePair<string, string>("key2", "value2"),
                                   new KeyValuePair<string, string>("key3", "value3")
                               };

            _redisCommand.MSetNx(testKeys);
            var results = _redisCommand.MGet(testKeys.Select(k => k.Key)).ToArray();

            foreach (var testKey in testKeys)
            {
                results.ShouldContain(testKey.Value);
            }
        }

        [Test]
        public void Should_return_1_when_all_keys_set()
        {
            _redisCommand.FlushDb();
            var testKeys = new List<KeyValuePair<string, string>>
                               {
                                   new KeyValuePair<string, string>("key1", "value1"),
                                   new KeyValuePair<string, string>("key2", "value2"),
                                   new KeyValuePair<string, string>("key3", "value3")
                               };

            _redisCommand.MSetNx(testKeys).ShouldEqual(1);
        }

        [Test]
        public void Should_return_0_when_not_all_keys_are_set()
        {
            _redisCommand.FlushDb();

            _redisCommand.Set("key1", "value1");

            var testKeys = new List<KeyValuePair<string, string>>
                               {
                                   new KeyValuePair<string, string>("key1", "value1"),
                                   new KeyValuePair<string, string>("key2", "value2"),
                                   new KeyValuePair<string, string>("key3", "value3")
                               };

            _redisCommand.MSetNx(testKeys).ShouldEqual(0);
        }
    }
}