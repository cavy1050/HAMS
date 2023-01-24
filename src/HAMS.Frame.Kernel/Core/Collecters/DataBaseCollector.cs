using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Core
{
    public class DataBaseCollector : List<DataBaseKind>, ILocator<DataBasePart, IDataBaseController>
    {
        public DataBaseKind this[DataBasePart dataBaseIndex]
        {
            set
            {
                if (dataBaseIndex != DataBasePart.All)
                    base[FindIndex(x => x.Item == dataBaseIndex.ToString())] = value;
                else
                    throw new ArgumentException("索引器只能单个存取!", nameof(dataBaseIndex));
            }

            get
            {
                if (dataBaseIndex != DataBasePart.All)
                    return Find(x => x.Item == dataBaseIndex.ToString());
                else
                    throw new ArgumentException("索引器只能单个存取!", nameof(dataBaseIndex));
            }
        }

        /// <summary>
        /// 获取指定数据库操作接口
        /// </summary>
        public IDataBaseController GetContent(DataBasePart dataBaseIndex)
        {
            if (dataBaseIndex != DataBasePart.All)
                return Find(x => x.Item == dataBaseIndex.ToString()).DataBaseController;
            else
                throw new ArgumentException("定位器只能单个存取!", nameof(dataBaseIndex));
        }
    }
}
