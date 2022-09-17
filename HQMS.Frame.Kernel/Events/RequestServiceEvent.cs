using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace HQMS.Frame.Kernel.Events
{
    /// <summary>
    /// 请求服务事件，与HQMS.Frame.ServiceLauncher进行数据通信，格式采用json字符串
    /// </summary>
    /// 
    /// <remarks>
    /// json字符串格式定义
    /// 数据元标识         数据元名称         类型           长度
    /// code               请求服务编号       字符型         2
    /// name               请求服务名称       字符型         50
    /// content            请求输入           字符型         8000
    /// </remarks>
    public class RequestServiceEvent : PubSubEvent<string>
    {
        
    }
}
