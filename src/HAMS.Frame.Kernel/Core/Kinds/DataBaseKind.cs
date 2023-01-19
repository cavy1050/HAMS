using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Core
{
    public class DataBaseKind : BaseKind
    {
        public IDataBaseController DataBaseController { get; set; }

        public DataBaseKind() : base()
        {

        }

        public DataBaseKind(string codeArg, string itemArg, string nameArg, string contentArg, string descriptionArg, string noteArg,
                                int rankArg, bool flagArg, IDataBaseController dataBaseControllerArg)
                    : base(codeArg, itemArg, nameArg, contentArg, descriptionArg, noteArg, rankArg, flagArg)
        {
            DataBaseController = dataBaseControllerArg;
        }
    }
}
