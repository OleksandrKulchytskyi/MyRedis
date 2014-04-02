using AwesomeRedis.API.Connect;

namespace AwesomeRedis.API
{
    public abstract class Command
    {
        protected readonly CommandSender Sender;
        protected readonly ResponseGetter Getter;

        protected Command(CommandSender sender, ResponseGetter getter)
        {
            Sender = sender;
            Getter = getter;
        }
    }
}