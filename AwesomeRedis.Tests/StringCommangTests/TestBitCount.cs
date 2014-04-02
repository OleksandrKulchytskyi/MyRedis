using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.Tests.Util;
using NUnit.Framework;

namespace AwesomeRedis.Tests.StringTest
{
    //These tests are ignored until the command is implemented in the Windows port of Redis

    [TestFixture]
    [Ignore]
    public class TestBitCount
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

//        [Test]
//        public void Should_return_number_of_set_bits_in_key_with_no_args()
//        {
//            _redisCommand.FlushDb();
//            _redisCommand.Set("samplekey", "samplevalue");
//
//            _redisCommand.BitCount("samplekey").ShouldEqual(45);
//        }
//
//        [Test]
//        public void Should_return_0_bits_when_key_not_set()
//        {
//            _redisCommand.FlushDb();
//
//            _redisCommand.BitCount("samplekey").ShouldEqual(0);
//        }
         
    }
}