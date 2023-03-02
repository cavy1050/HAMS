using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

namespace HQMS.Extension.Control.Main.Models
{
    public class DataMappingModel : BindableBase
    {
        string sqlSentence;
        List<CatalogKind> catalogHub;
        List<LocalKind> localHub;
        List<StandardKind> standardHub;
        List<MatchedKind> matchedHub;

        IEnvironmentMonitor environmentMonitor;
        IDataBaseController BAGLDBController;
        ISnackbarMessageQueue messageQueue;

        ObservableCollection<CatalogKind> catalogs;
        public ObservableCollection<CatalogKind> Catalogs
        {
            get => catalogs;
            set => SetProperty(ref catalogs, value);
        }

        CatalogKind currentCatalog;
        public CatalogKind CurrentCatalog
        {
            get => currentCatalog;
            set => SetProperty(ref currentCatalog, value);
        }

        ObservableCollection<LocalKind> locals;
        public ObservableCollection<LocalKind> Locals
        {
            get => locals;
            set => SetProperty(ref locals, value);
        }

        LocalKind currentLocal;
        public LocalKind CurrentLocal
        {
            get => currentLocal;
            set => SetProperty(ref currentLocal, value);
        }

        ObservableCollection<StandardKind> standards;
        public ObservableCollection<StandardKind> Standards
        {
            get => standards;
            set => SetProperty(ref standards, value);
        }

        StandardKind currentStandard;
        public StandardKind CurrentStandard
        {
            get => currentStandard;
            set => SetProperty(ref currentStandard, value);
        }

        ObservableCollection<MatchedKind> matcheds;
        public ObservableCollection<MatchedKind> Matcheds
        {
            get => matcheds;
            set => SetProperty(ref matcheds, value);
        }

        MatchedKind currentMatched;
        public MatchedKind CurrentMatched
        {
            get => currentMatched;
            set => SetProperty(ref currentMatched, value);
        }

        public DataMappingModel(IContainerProvider containerProviderArg)
        {
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            BAGLDBController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.BAGLDB);
        }

        public void Load()
        {
            Catalogs = new ObservableCollection<CatalogKind>();
            Locals = new ObservableCollection<LocalKind>();
            Standards = new ObservableCollection<StandardKind>();
            Matcheds = new ObservableCollection<MatchedKind>();

            LoadCatalogData();
        }

        private void LoadCatalogData()
        {
            sqlSentence = "SELECT CatalogCode,CatalogName FROM dbo.tf_hqms_getppsj('Catalog','')";
            if (!BAGLDBController.Query<CatalogKind>(sqlSentence, out catalogHub))
                messageQueue.Enqueue("加载字典类别数据失败!");
            else
                Catalogs.AddRange(catalogHub);
        }

        public void RefreshCatalogData()
        {
            Locals.Clear();
            Standards.Clear();
            Matcheds.Clear();

            sqlSentence = "SELECT CatalogCode,CatalogName,LocalCode,LocalName from dbo.tf_hqms_getppsj('Local','" + CurrentCatalog.CatalogCode + "')";

            if (!BAGLDBController.Query<LocalKind>(sqlSentence, out localHub))
                messageQueue.Enqueue("刷新本地字典数据失败!");
            else
                Locals.AddRange(localHub);

            sqlSentence = "SELECT CatalogCode,CatalogName,StandardCode,StandardName from dbo.tf_hqms_getppsj('Standard','" + CurrentCatalog.CatalogCode + "')";

            if (!BAGLDBController.Query<StandardKind>(sqlSentence, out standardHub))
                messageQueue.Enqueue("刷新标准字典数据失败!");
            else 
                Standards.AddRange(standardHub);

            sqlSentence = "SELECT CatalogCode,CatalogName,LocalCode,LocalName,StandardCode,StandardName from dbo.tf_hqms_getppsj('Matched','" + CurrentCatalog.CatalogCode + "')";

            if (!BAGLDBController.Query<MatchedKind>(sqlSentence, out matchedHub))
                messageQueue.Enqueue("刷新已匹配数据失败!");
            else 
                Matcheds.AddRange(matchedHub);
        }

        public void MatchData()
        {
            string retString;

            if (string.IsNullOrEmpty(CurrentLocal.LocalCode))
                messageQueue.Enqueue("请选择本地数据!");
            else if (string.IsNullOrEmpty(CurrentStandard.StandardCode))
                messageQueue.Enqueue("请选择标准数据!");
            else
            {
                sqlSentence = "exec usp_hqms_getppsj 'Match','" + CurrentCatalog.CatalogCode + "','" + CurrentCatalog.CatalogName + "','" +
                    CurrentLocal.LocalCode + "','" + CurrentLocal.LocalName + "','" + CurrentStandard.StandardCode + "','" + CurrentStandard.StandardName + "'";

                if (!BAGLDBController.ExecuteWithMessage(sqlSentence, out retString))
                    messageQueue.Enqueue(retString);
                else
                    messageQueue.Enqueue("匹配数据成功!");
            }

            RefreshCatalogData();
        }

        public void CancelMatchData()
        {
            string retString;

            if (string.IsNullOrEmpty(CurrentMatched.LocalCode))
                messageQueue.Enqueue("请选择已匹配数据!");
            else
            {
                sqlSentence = "exec usp_hqms_getppsj 'UnMatch','" + CurrentCatalog.CatalogCode + "','" + CurrentCatalog.CatalogCode + "','" +
                CurrentMatched.LocalCode + "','" + CurrentMatched.LocalName + "','" + CurrentMatched.StandardCode + "','" + CurrentMatched.StandardName + "'";

                if (!BAGLDBController.ExecuteWithMessage(sqlSentence, out retString))
                    messageQueue.Enqueue(retString);
                else
                    messageQueue.Enqueue("取消匹配数据成功!");
            }

            RefreshCatalogData();
        }
    }
}
