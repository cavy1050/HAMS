using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace HAMS.Frame.Kernel.Core
{
    public class SeverityKind : BaseKind
    {
        public ValidationResult Results { get; set; }

        public SeverityKind() : base()
        {

        }

        public SeverityKind(string codeArg, string itemArg, string nameArg, string contentArg, string descriptionArg, string noteArg,
                                int rankArg, bool flagArg, ValidationResult resultsArg)
                    : base(codeArg, itemArg, nameArg, contentArg, descriptionArg, noteArg, rankArg, flagArg)
        {
            Results = resultsArg;
        }
    }
}
