using System.Collections.Generic;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestAppend
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
        public
        void TestSendAppendToInstance()
        {
            const string toAppend = "+append";
            _redisCommand.Append(_keyValuePair.Key, toAppend).ShouldEqual(_keyValuePair.Value.Length + toAppend.Length);
        }

        [Test]
        public void Should_create_new_key_when_key_didnt_exist()
        {
            const string toAppend = "appendthis";
            _redisCommand.Append("thiskeydontexist", toAppend).ShouldEqual(toAppend.Length);
        }
    }
}