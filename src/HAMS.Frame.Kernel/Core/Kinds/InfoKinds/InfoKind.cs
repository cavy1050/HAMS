using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 操作信息类型 记录程序框架操作信息
    /// </summary>
    public class InfoKind : BaseKind
    {
        public string RecordTime { get; set; }

        public InfoKind()
        {

        }

        public InfoKind(string codeArg, string contentArg, string noteArg, string recordTimeArg, bool enabledFlagArg) :
                            base(codeArg, contentArg, noteArg, enabledFlagArg)
        {
            RecordTime = recordTimeArg;
        }
    }
}
