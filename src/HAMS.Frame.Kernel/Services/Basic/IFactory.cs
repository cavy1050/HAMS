using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Services
{
    public interface IFactory<TEnum> where TEnum : Enum
    {
        /// <summary>
        /// 注册IController接口
        /// </summary>
        void Register(TEnum tenum);

        /// <summary>
        /// 初始化非默认参数设置
        /// </summary>
        void Init(TEnum tenum);

        /*
        /// <summary>
        /// 初始化IController接口,对IEnvironmentMonitor加载数据
        /// </summary>
        void Load(SecurityController securityController);

        /// <summary>
        /// 对IEnvironmentMonitor持久化数据
        /// </summary>
        void Save(TEnum tenum);
        */
    }
}
