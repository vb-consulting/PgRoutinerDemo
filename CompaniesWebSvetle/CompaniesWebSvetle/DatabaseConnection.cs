using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace CompaniesWebSvetle
{
    public static class DatabaseConnection
    {
        private static readonly IEnumerable<string> InfoLevels = new[] { "INFO", "NOTICE", "LOG" };
        private static readonly IEnumerable<string> ErrorLevels = new[] { "ERROR", "PANIC" };
        private static ILogger<NpgsqlConnection> logger;
        private static string connection;

        public static void Configure(IServiceProvider provider, string connectionName)
        {
            logger = provider.GetRequiredService<ILogger<NpgsqlConnection>>();
            var configuration = provider.GetRequiredService<IConfiguration>();
            connection = configuration.GetConnectionString(connectionName) ?? configuration.GetValue<string>($"POSTGRESQLCONNSTR_{connectionName}");
        }

        public static NpgsqlConnection Create()
        {
            var connection = new NpgsqlConnection(DatabaseConnection.connection);
            connection.Notice += (sender, args) =>
            {
                var severity = args.Notice.Severity;
                var msg = $"{args.Notice.Where}:{Environment.NewLine}{args.Notice.MessageText}{Environment.NewLine}";
                if (InfoLevels.Contains(severity))
                {
                    logger.LogInformation(msg);
                }
                else if (severity == "WARNING")
                {
                    logger.LogWarning(msg);
                }
                else if (severity.StartsWith("DEBUG"))
                {
                    logger.LogDebug(msg);
                }
                else if (ErrorLevels.Contains(severity))
                {
                    logger.LogError(msg);
                }
                else
                {
                    logger.LogTrace(msg);
                }
            };
            return connection;
        }
    }
}
