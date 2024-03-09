namespace Invitation.Command.Logging
{
    public class AppConfiguration
    {
        public static IConfiguration Build()
            => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();
    }
}
