using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    public class EventServiceController : IEventServiceController
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeDataBaseController;
        ILogController servicEventLogController;

        string sqlSentence;
        List<BaseKind> serviceEventSettingHub;

        public EventServiceController(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();

            servicEventLogController = environmentMonitor.LogSetting.GetContent(LogPart.ServicEvent.ToString());
            nativeDataBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native.ToString());
        }

        public string Request(EventServicePart eventServiceArgs, FrameModulePart sourceModuleArgs, FrameModulePart targetModuleArgs, IServiceContent serviceContentArgs)
        {
            string retJsonText = string.Empty;
            string serviceEventCodeText = Convert.ToInt32(eventServiceArgs).ToString().PadLeft(2, '0');

            sqlSentence = "SELECT Code,Name,Content,Description,Rank,Flag FROM System_ServiceEventSetting";
            if (nativeDataBaseController.Query<BaseKind>(sqlSentence, out serviceEventSettingHub))
            {
                var serviceEventSettingInfo = from serviceEventSettingInfoHub in serviceEventSettingHub
                                              where serviceEventSettingInfoHub.Flag && serviceEventSettingInfoHub.Code == serviceEventCodeText
                                              select serviceEventSettingInfoHub;

                RequestServiceKind requestService = new RequestServiceKind
                {
                    Code = serviceEventSettingInfo.FirstOrDefault().Code,
                    Name = serviceEventSettingInfo.FirstOrDefault().Name,
                    Description = serviceEventSettingInfo.FirstOrDefault().Description,
                    SourceModuleName = sourceModuleArgs,
                    TargetModuleName = targetModuleArgs,
                    ServiceContent = serviceContentArgs
                };

                retJsonText = JsonConvert.SerializeObject(requestService);
                servicEventLogController.WriteLog(retJsonText);
            }

            return retJsonText;
        }

        public string Response(EventServicePart eventServiceArgs, FrameModulePart sourceModuleArgs, FrameModulePart targetModuleArgs,
                                bool returnCodeArgs, string returnMessageArgs,
                                 IServiceContent serviceContentArgs)
        {
            string retJsonText = string.Empty;
            string serviceEventCodeText = Convert.ToInt32(eventServiceArgs).ToString().PadLeft(2, '0');

            sqlSentence = "SELECT Code,Name,Content,Description,Rank,Flag FROM System_ServiceEventSetting";
            if (nativeDataBaseController.Query<BaseKind>(sqlSentence, out serviceEventSettingHub))
            {
                var serviceEventSettingInfo = from serviceEventSettingInfoHub in serviceEventSettingHub
                                              where serviceEventSettingInfoHub.Flag && serviceEventSettingInfoHub.Code == serviceEventCodeText
                                              select serviceEventSettingInfoHub;

                ResponseServiceKind responseService = new ResponseServiceKind
                {
                    Code = serviceEventSettingInfo.FirstOrDefault().Code,
                    Name = serviceEventSettingInfo.FirstOrDefault().Name,
                    Description = serviceEventSettingInfo.FirstOrDefault().Description,
                    SourceModuleName = sourceModuleArgs,
                    TargetModuleName = targetModuleArgs,
                    ServiceContent = serviceContentArgs,
                    ReturnCode = returnCodeArgs == true ? "1": "0",
                    ReturnMessage = returnMessageArgs
                };

                retJsonText = JsonConvert.SerializeObject(responseService);
                servicEventLogController.WriteLog(retJsonText);
            }

            return retJsonText;
        }
    }
}
