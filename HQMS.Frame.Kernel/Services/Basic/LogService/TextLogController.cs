using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Prism.Ioc;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    public class TextLogController : ILogController
    {
        IEnvironmentMonitor environmentMonitor;

        string textLogFilePath;
        TextFormatter textFormatter;
        FlatFileTraceListener flatFileTraceListener;
        List<TraceListener> traceListeners;
        LogEnabledFilter logEnabledFilter;
        List<ILogFilter> logFilters;
        LogSource generalLogSource, ErrorsLogSource;
        List<LogSource> logSources;
        LogWriter logWriter;

        public TextLogController(IContainerProvider containerProviderArgs)
        {
            environmentMonitor= containerProviderArgs.Resolve<IEnvironmentMonitor>();

            textLogFilePath = environmentMonitor.LogSetting["TextLog"].Content;

            textFormatter = new TextFormatter("{timestamp(local)}{tab}{message}");
            flatFileTraceListener = new FlatFileTraceListener(textLogFilePath, null, null, textFormatter);
            traceListeners = new List<TraceListener>();
            traceListeners.Add(flatFileTraceListener);

            logEnabledFilter = new LogEnabledFilter("enabledFilter", true);
            logFilters = new List<ILogFilter>();
            logFilters.Add(logEnabledFilter);

            generalLogSource = new LogSource("General", traceListeners, SourceLevels.All);
            ErrorsLogSource = new LogSource("Errors", traceListeners, SourceLevels.Error);
            logSources = new List<LogSource>();
            logSources.Add(generalLogSource);

            logWriter = new LogWriter(logFilters, logSources, ErrorsLogSource, "General");
        }

        public void WriteLog(string MessageArgs)
        {
            if (environmentMonitor.ValidationResults.IsValid)
            {
                logWriter.Write(MessageArgs);
            }
        }
    }
}
