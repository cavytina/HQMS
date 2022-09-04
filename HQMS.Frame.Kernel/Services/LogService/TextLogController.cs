using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Prism.Ioc;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    [HasSelfValidation]
    public class TextLogController : ILogController
    {
        IEnvironmentMonitor environmentMonitor;

        string defaultTextLogFilePath;
        LogWriter logWriter;

        public TextLogController(IContainerProvider containerProviderArgs)
        {
            environmentMonitor= containerProviderArgs.Resolve<IEnvironmentMonitor>();

            defaultTextLogFilePath = environmentMonitor.AppPathSetting["TextLogFilePath"];

            TextFormatter textFormatter = new TextFormatter("{timestamp(local)}{tab}{message}");
            FlatFileTraceListener flatFileTraceListener = new FlatFileTraceListener(defaultTextLogFilePath, null, null, textFormatter);
            List<TraceListener> traceListeners = new List<TraceListener>();
            traceListeners.Add(flatFileTraceListener);

            LogEnabledFilter logEnabledFilter = new LogEnabledFilter("enabledFilter", true);
            List<ILogFilter> logFilters = new List<ILogFilter>();
            logFilters.Add(logEnabledFilter);

            LogSource generalLogSource = new LogSource("General", traceListeners, SourceLevels.All);
            LogSource ErrorsLogSource = new LogSource("Errors", traceListeners, SourceLevels.Error);
            List<LogSource> logSources = new List<LogSource>();
            logSources.Add(generalLogSource);

            logWriter = new LogWriter(logFilters, logSources, ErrorsLogSource, "General");
        }

        [SelfValidation]
        public void Validate(ValidationResults results)
        {
            defaultTextLogFilePath = environmentMonitor.AppPathSetting["TextLogFilePath"];

            if (string.IsNullOrEmpty(defaultTextLogFilePath))
                results.AddResult(new ValidationResult("默认日志文件不能为空!", this, "defaultTextLogFilePath", "", null));
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
