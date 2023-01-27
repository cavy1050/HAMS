using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public class SecurityFactory : IFactory<SecurityPart>
    {
        IContainerRegistry containerRegistry;
        IContainerProvider containerProvider;

        public ICertificateController CertificateController { get; set; }

        public SecurityFactory(IContainerRegistry containerRegistryArg,IContainerProvider containerProviderArg)
        {
            containerRegistry = containerRegistryArg;
            containerProvider = containerProviderArg;
        }

        public void Register(SecurityPart securityPartArg)
        {
            if (securityPartArg == SecurityPart.DataBase)
                throw new ArgumentException("数据库无需使用安全类工厂方法!", nameof(securityPartArg));
            else
                containerRegistry.Register<ICertificateController, CertificateController>();
        }

        public void Init(SecurityPart securityPartArg)
        {
            if (securityPartArg == SecurityPart.DataBase)
                throw new ArgumentException("数据库无需使用安全类工厂方法!",nameof(securityPartArg));
        }
    }
}