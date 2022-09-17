using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace HQMS.Frame.Kernel.Events
{
    /// <summary>
    /// 应答服务事件，按照json字符串传递数据。
    /// </summary>
    /// 
    /// <remarks>
    /// json字符串格式定义
    /// 数据元标识         数据元名称         类型           长度
    /// code               应答服务编号       字符型         2
    /// name               应答服务名称       字符型         50
    /// content            应答输出           字符型         8000
    /// </remarks>
    public class ResponseServiceEvent : PubSubEvent<string>
    {

    }
}
