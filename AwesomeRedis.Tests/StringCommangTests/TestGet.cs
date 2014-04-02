using System;
using System.Collections.Generic;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestGet
    {
        private RedisInstance _redis;
        private StringCommand _redisCommand;
        private KeyValuePair<string, string> _testKeyValuePair;

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
        public void Should_Get_Testvalue_From_Testkey()
        {
            _testKeyValuePair = new KeyValuePair<string, string>("testkey", "testvalue");
            _redisCommand.Set(_testKeyValuePair.Key, _testKeyValuePair.Value);
            _redisCommand.Get(_testKeyValuePair.Key).ShouldEqual(_testKeyValuePair.Value);
        }

        [Test]
        public void Should_return_nil_when_getting_a_key_that_dont_exist()
        {
            const string testkey = "Ishouldntexist";
            _redisCommand.Get(testkey).ShouldBeNull();
        }

        [Test]
        public void Should_get_empty_value_from_key()
        {
            _redisCommand.Set("nothing", String.Empty);

            _redisCommand.Get("nothing").ShouldEqual(String.Empty);
        }

        [Test]
        public void Should_get_value_with_whitespace()
        {
            var val = new KeyValuePair<string, string>("testkey", "this key has many words");
            var response = _redisCommand.Set(val.Key, val.Value);

            _redisCommand.Get(val.Key).ShouldEqual(val.Value);
        }
    }
}