using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.API.Reader;

namespace AwesomeRedis.Tests.Util
{
    internal static class TestObjectGetter
    {
        const string Filename = @"TestServer\redis-server.exe";

        public static RedisInstance GetDefaultRedisInstance()
        {
            return new RedisInstance(Filename);
        }

        public static StringCommand GetLocalhostStringCommandSender()
        {
            var redisConnectionHandler = GetLocalhostConnection();

            var responseStrategies = new IResponseReader[]
                                         {
                                             new BulkResponseReader(), new IntegerResponseReader(),
                                             new MultiBulkResponseReader(), new StatusCodeResponseReader(),
                                         };

            return new StringCommand(new CommandSender(redisConnectionHandler), new ResponseGetter(redisConnectionHandler, responseStrategies) );
        }

        public static GenericCommand GetLocalhostGenericCommandSender()
        {
            var redisConnectionHandler = GetLocalhostConnection();

            var responseStrategies = new IResponseReader[]
                                         {
                                             new BulkResponseReader(), new IntegerResponseReader(),
                                             new MultiBulkResponseReader(), new StatusCodeResponseReader(),
                                         };

            return new GenericCommand(new CommandSender(redisConnectionHandler), new ResponseGetter(redisConnectionHandler, responseStrategies));
        }

        private static RedisConnectionHandler GetLocalhostConnection()
        {
            return new RedisConnectionHandler("127.0.0.1", 6379);
        }
    }
}