using System;
using System.Linq;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;
using Should;

namespace AwesomeRedis.Tests.GenericCommandTest
{
    [TestFixture]
    public class TestGenericCommand
    {
        private RedisInstance _redis;
        private GenericCommand _redisCommand;

        [TestFixtureSetUp]
        public void InitTests()
        {
            _redis = TestObjectGetter.GetDefaultRedisInstance();
            _redis.Start();
            _redisCommand = TestObjectGetter.GetLocalhostGenericCommandSender();
        }

        [TestFixtureTearDown]
        public void DisposeTests()
        {
            _redisCommand.ExecuteCommand("flushdb");
            _redis.Stop();
        }

        [Test]
        public void Should_set_quoted_string_value()
        {
            const string command = "SET key \"This is a valid value\"";

            _redisCommand.ExecuteCommand(command).ShouldContain("OK");
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Should_return_error_on_unmatched_quote()
        {
            const string command = "SET key \"This is a value";

            _redisCommand.ExecuteCommand(command).ToList();
        }

        [Test]
        public void Should_sent_command_with_no_params()
        {
            _redisCommand.ExecuteCommand("flushdb").ShouldContain("OK");
        }
    }
}