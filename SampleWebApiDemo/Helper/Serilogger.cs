using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.RollingFile;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApiDemo.Helper {
    public class Serilogger {
        //Reference: https://andrewlock.net/writing-logs-to-elasticsearch-with-fluentd-using-serilog-in-asp-net-core/
        public static void GetLoggerConfiguration(string esConnectionString) {
              Log.Logger  = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: SystemConsoleTheme.Literate)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(esConnectionString)) // for the docker-compose implementation
                {
                    AutoRegisterTemplate = true,
                    RegisterTemplateFailure = RegisterTemplateRecovery.IndexAnyway,
                    FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback,
                    FailureSink = new RollingFileSink("./fail-{Date}.txt", new Serilog.Formatting.Json.JsonFormatter(), null, null)
                })
                .CreateLogger();

            Log.Information("Hello, from Serilog!");
        }

        public static void Information(string message) {
            Log.Information(message);
        }
    }
}
