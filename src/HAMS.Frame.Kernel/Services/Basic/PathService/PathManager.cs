using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Extensions;

namespace HAMS.Frame.Kernel.Services
{
    public class PathManager : IManager<PathPart>
    {
        IContainerProvider containerProvider;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;

        string sqlSentence;
        List<SettingKind> costomPathSettingHub;

        /// <summary>
        /// 程序运行目录
        /// </summary>
        public string ApplictionCatalogue { get; set; }

        /// <summary>
        /// 本地数据库文件路径
        /// </summary>
        public string NativeDataBaseFilePath { get; set; }

        /// <summary>
        /// 日志文件目录
        /// </summary>
        public string LogFileCatalogue { get; set; }

        public PathManager(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public void DeInit(PathPart pathPartArg)
        {
            switch (pathPartArg)
            {
                case PathPart.ApplictionCatalogue:
                    ApplictionCatalogue = AppDomain.CurrentDomain.BaseDirectory;
                    break;

                case PathPart.NativeDataBaseFilePath:
                    NativeDataBaseFilePath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName.Replace("exe", "db");
                    break;

                case PathPart.LogFileCatalogue:
                    LogFileCatalogue = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
                    break;

                case PathPart.All:
                    DeInit(PathPart.ApplictionCatalogue);
                    DeInit(PathPart.NativeDataBaseFilePath);
                    DeInit(PathPart.LogFileCatalogue);
                    break;
            }
        }

        public void Init(PathPart pathPartArg)
        {
            if (pathPartArg == PathPart.ApplictionCatalogue || pathPartArg == PathPart.NativeDataBaseFilePath)
                throw new ArgumentException("自定义参数不能是<程序运行目录>或<本地数据库文件路径>!", nameof(pathPartArg));
            {
                nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
                sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_PathSetting WHERE EnabledFlag = True AND DefaultFlag = False";
                nativeBaseController.QueryNoLog<SettingKind>(sqlSentence, out costomPathSettingHub);

                switch (pathPartArg)
                {
                    //如果自定义日志目录不为空则覆盖默认值,否则保持默认值
                    case PathPart.LogFileCatalogue:
                        if (!string.IsNullOrEmpty(costomPathSettingHub.FirstOrDefault(x => x.Code == "01GPSK8EY3VD74Y0508D7KP2Z4").Content))
                            LogFileCatalogue = costomPathSettingHub.FirstOrDefault(x => x.Code == "01GPSK8EY3VD74Y0508D7KP2Z4").Content;
                        break;

                    case PathPart.All:
                        Init(PathPart.LogFileCatalogue);
                        break;
                }
            }
        }

        public void Load(PathPart pathPartArg)
        {
            switch (pathPartArg)
            {
                case PathPart.ApplictionCatalogue:
                    if (!environmentMonitor.PathSetting.Exists(x => x.Code == "01GPKA6WE841SE31MQWH3Y5WNF"))
                        environmentMonitor.PathSetting.Add(new SettingKind
                        {
                            Code = "01GPKA6WE841SE31MQWH3Y5WNF",
                            Item = PathPart.ApplictionCatalogue.ToString(),
                            Name = EnumExtension.GetDescription(PathPart.ApplictionCatalogue),
                            Content = ApplictionCatalogue,
                            Rank = Convert.ToInt32(PathPart.ApplictionCatalogue),
                            EnabledFlag = true
                        });
                    break;

                case PathPart.NativeDataBaseFilePath:
                    if (!environmentMonitor.PathSetting.Exists(x => x.Code == "01GPKA6WE85VSFC0S16CF7MCBJ"))
                        environmentMonitor.PathSetting.Add(new SettingKind
                        {
                            Code = "01GPKA6WE85VSFC0S16CF7MCBJ",
                            Item = PathPart.NativeDataBaseFilePath.ToString(),
                            Name = EnumExtension.GetDescription(PathPart.NativeDataBaseFilePath),
                            Content = NativeDataBaseFilePath,
                            Rank = Convert.ToInt32(PathPart.NativeDataBaseFilePath),
                            EnabledFlag = true
                        });
                    break;

                case PathPart.LogFileCatalogue:
                    if (!environmentMonitor.PathSetting.Exists(x => x.Code == "01GPSK8EY3VD74Y0508D7KP2Z4"))
                        environmentMonitor.PathSetting.Add(new SettingKind
                        {
                            Code = "01GPSK8EY3VD74Y0508D7KP2Z4",
                            Item = PathPart.LogFileCatalogue.ToString(),
                            Name = EnumExtension.GetDescription(PathPart.LogFileCatalogue),
                            Content = LogFileCatalogue,
                            Rank = Convert.ToInt32(PathPart.LogFileCatalogue),
                            EnabledFlag = true
                        });
                    else
                        environmentMonitor.PathSetting[PathPart.LogFileCatalogue].Content = LogFileCatalogue;
                    break;

                case PathPart.All:
                    Load(PathPart.ApplictionCatalogue);
                    Load(PathPart.NativeDataBaseFilePath);
                    Load(PathPart.LogFileCatalogue);
                    break;
            }
        }

        public void Save(PathPart pathPartArg)
        {
            switch (pathPartArg)
            {
                case PathPart.ApplictionCatalogue:
                    sqlSentence = "UPDATE System_PathSetting SET Content='" + ApplictionCatalogue + "' WHERE Code='01GPKA6WE841SE31MQWH3Y5WNF'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case PathPart.NativeDataBaseFilePath:
                    sqlSentence = "UPDATE System_PathSetting SET Content='" + NativeDataBaseFilePath + "' WHERE Code='01GPKA6WE85VSFC0S16CF7MCBJ'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case PathPart.LogFileCatalogue:
                    sqlSentence = "UPDATE System_PathSetting SET Content='" + LogFileCatalogue + "' WHERE Code='01GPSK8EY3VD74Y0508D7KP2Z4'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case PathPart.All:
                    Save(PathPart.ApplictionCatalogue);
                    Save(PathPart.NativeDataBaseFilePath);
                    Save(PathPart.LogFileCatalogue);
                    break;
            }
        }
    }
}
