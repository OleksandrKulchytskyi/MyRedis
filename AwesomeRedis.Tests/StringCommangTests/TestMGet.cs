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
    public class TestMGet
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
        public void Should_get_one_key_value()
        {
            _redisCommand.Set("testkey", "testvalue");

            _redisCommand.MGet(new[]{"testkey"}).ShouldEqual(new List<string>(new[] { "testvalue" }));
        }

        [Test]
        public void Should_get_multiple_key_values()
        {
            _redisCommand.Set("key1", "value1");
            _redisCommand.Set("key2", "value2");
            _redisCommand.Set("key3", "value3");

            var results = _redisCommand.MGet(new[]{"key1", "key2", "key3"}).ToArray();
            results.ShouldContain("value1");
            results.ShouldContain("value2");
            results.ShouldContain("value3");
        }

        [Test]
        public void Should_return_null_when_key_doesnt_exist()
        {
            _redisCommand.FlushDb();
            
            _redisCommand.MGet(new []{"testkey"}).ShouldEqual(new List<string>(new string[] { null }));
        }

        [Test]
        public void Should_return_null_in_array_where_key_doesnt_exist()
        {
            _redisCommand.FlushDb();

            _redisCommand.Set("key1", "value1");
            _redisCommand.Set("key3", "value3");

            var results = _redisCommand.MGet(new[] { "key1", "key2", "key3" }).ToArray();
            results[0].ShouldEqual("value1");
            results[1].ShouldBeNull();
            results[2].ShouldEqual("value3");
        }
    }
}