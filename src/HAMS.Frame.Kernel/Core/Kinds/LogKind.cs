using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Core
{
    public class LogKind : BaseKind
    {
        public ILogController LogController { get; set; }

        public LogKind() : base()
        {

        }

        public LogKind(string codeArg, string itemArg, string nameArg, string contentArg, string descriptionArg, string noteArg,
                                int rankArg, bool flagArg, ILogController logControllerArg)
                    : base(codeArg, itemArg, nameArg, contentArg, descriptionArg, noteArg, rankArg, flagArg)
        {
            LogController = logControllerArg;
        }
    }
}
