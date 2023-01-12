using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public class PathManager : IManager<PathPart>
    {
        /// <summary>
        /// 默认程序运行目录
        /// </summary>
        public string DefaultApplictionCatalogue { get; set; }

        /// <summary>
        /// 默认本地数据库文件路径
        /// </summary>
        public string DefaultNativeDataBaseFilePath { get; set; }

        IEnvironmentMonitor environmentMonitor;

        public PathManager(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        private void InnerLoad(PathPart pathPartArg)
        {

        }

        public void Load(PathPart pathPartArg)
        {
            
        }

        private void InnerSave(PathPart pathPartArg)
        {

        }

        public void Save(PathPart pathPartArg)
        {
            
        }
    }
}
