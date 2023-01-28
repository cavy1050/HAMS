using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    public enum EventServicePart
    {
        /// <summary>
        /// 程序变更服务
        /// </summary>
        ApplicationAlterationService,

        /// <summary>
        /// 程序验证服务
        /// </summary>
        ApplictionVerificationService,

        /// <summary>
        /// 用户验证服务
        /// </summary>
        AccountVerificationService,

        /// <summary>
        /// 用户授权服务
        /// </summary>
        AccountAuthenticationService,

        /// <summary>
        /// 菜单初始化服务
        /// </summary>
        MenuInitializationService,

        /// <summary>
        /// 菜单激活服务	
        /// </summary>
        MenuActivationService
    }
}
