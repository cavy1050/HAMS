using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace HAMS.Frame.Kernel.Core
{
    public class SeverityCollector : List<SeverityKind>, ILocator<SeverityLevelPart, ValidationResult>
    {
        public SeverityKind this[SeverityLevelPart severityIndex]
        {
            set
            {
                if (severityIndex != SeverityLevelPart.All)
                    base[FindIndex(x => x.Item == severityIndex.ToString())] = value;
                else
                    throw new ArgumentException("索引器只能单个存取!", nameof(severityIndex));
            }

            get
            {
                if (severityIndex != SeverityLevelPart.All)
                    return Find(x => x.Item == severityIndex.ToString());
                else
                    throw new ArgumentException("索引器只能单个存取!", nameof(severityIndex));
            }
        }

        /// <summary>
        /// 获取指定严重级别操作接口
        /// </summary>
        public ValidationResult GetContent(SeverityLevelPart severityIndex)
        {
            if (severityIndex != SeverityLevelPart.All)
                return Find(x => x.Item == severityIndex.ToString()).Results;
            else
                throw new ArgumentException("定位器只能单个存取!", nameof(severityIndex));
        }
    }
}
