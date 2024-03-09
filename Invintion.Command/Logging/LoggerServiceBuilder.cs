using Serilog;
using Serilog.Debugging;
using ILogger = Serilog.ILogger;

namespace Invitation.Command.Logging
{
    public class LoggerServiceBuilder
    {
        public static ILogger Build()
        {
            var configuration = AppConfiguration.Build();

            var logger = new LoggerConfiguration().ReadFrom.Configuration(configuration);

            logger.WriteTo.Console();

            SelfLog.Enable(Console.Error);

            return logger.CreateLogger();
        }
    }
}
