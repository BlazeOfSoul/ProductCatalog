using Serilog;
using Serilog.Events;

namespace ProductCatalog.API.Configuration;

public static class LoggerCreation
{
    public static void ConfigureLogger()
    {
        var logFolderPath = Path.Combine(AppContext.BaseDirectory, "logs");

        if (!Directory.Exists(logFolderPath)) Directory.CreateDirectory(logFolderPath);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(
                Path.Combine(logFolderPath, "ProductCatalog-.log"),
                shared: true,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 10485760,
                retainedFileCountLimit: null,
                restrictedToMinimumLevel: LogEventLevel.Information)
            .WriteTo.File(
                Path.Combine(logFolderPath, "ProductCatalog.Errors-.log"),
                shared: true,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 10485760,
                retainedFileCountLimit: null,
                restrictedToMinimumLevel: LogEventLevel.Error)
            .WriteTo.Console(
                outputTemplate:
                "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}")
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .CreateLogger();
    }
}