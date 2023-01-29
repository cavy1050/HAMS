using FluentValidation.Results;

namespace HAMS.Frame.Kernel.Core
{
    public class SeverityKind : SettingKind
    {
        public ValidationResult Results { get; set; }

        public SeverityKind() : base()
        {

        }

        public SeverityKind(string codeArg, string itemArg, string nameArg, string contentArg, string descriptionArg, string noteArg,
                                int rankArg, bool defaultFlag, bool enabledFlag, ValidationResult resultsArg) :
                                    base(codeArg, itemArg, nameArg, contentArg, descriptionArg, noteArg, rankArg, defaultFlag, enabledFlag)
        {
            Results = resultsArg;
        }
    }
}
