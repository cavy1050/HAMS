using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    public class UserKind : SettingKind
    {
        public string PassWord { get; set; }

        public UserKind()
        {

        }

        public UserKind(string codeArg, string itemarg, string nameArg, string passWordArg, string contentArg, string descriptionArg,
                            string noteArg, int rankArgs, bool defaultFlag, bool enabledFlag) :
                                base(codeArg, itemarg, nameArg, contentArg, descriptionArg, noteArg, rankArgs, defaultFlag, enabledFlag)
        {
            PassWord = passWordArg;
        }
    }
}
