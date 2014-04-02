using System.Collections.Generic;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.StringTest
{
    [TestFixture]
    public class TestSetNx
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
        public void Should_set_if_key_doesnt_exist()
        {
            _redisCommand.FlushDb();

            var testKeyValue = new KeyValuePair<string, string>("samplekey", "samplevalue");

            //Return of int "1" implies successful key set according to redis spec
            _redisCommand.SetNx(testKeyValue.Key, testKeyValue.Value).ShouldEqual(1);
        }

        [Test]
        public void Should_not_set_if_key_does_exist()
        {
            _redisCommand.FlushDb();

            var testKeyValue = new KeyValuePair<string, string>("samplekey1", "samplevalue1");
            _redisCommand.SetNx(testKeyValue.Key, testKeyValue.Value).ShouldEqual(1);

            _redisCommand.SetNx(testKeyValue.Key, "valueshouldntbeset").ShouldEqual(0);
        }
    }
}