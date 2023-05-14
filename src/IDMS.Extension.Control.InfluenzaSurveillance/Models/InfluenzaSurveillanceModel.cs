using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Prism.Ioc;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;
using Npoi.Mapper;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

namespace IDMS.Extension.Control.InfluenzaSurveillance.Models
{
    public class InfluenzaSurveillanceModel : BindableBase
    {
        ISnackbarMessageQueue messageQueue;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController, mzcisdbBaseController, zycisdbBaseController;

        string sqlSentence, retMessage;
        List<SettingKind> visitCategoryHub;
        List<ResultKind> resultHub;

        DateTime beginDate = DateTime.Now;
        public DateTime BeginDate
        {
            get => beginDate;
            set => SetProperty(ref beginDate, value);
        }

        DateTime endDate = DateTime.Now;
        public DateTime EndDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }

        ObservableCollection<SettingKind> visitCategorys;
        public ObservableCollection<SettingKind> VisitCategorys
        {
            get => visitCategorys;
            set => SetProperty(ref visitCategorys, value);
        }

        string currentVisitCategoryItem;
        public string CurrentVisitCategoryItem
        {
            get => currentVisitCategoryItem;
            set => SetProperty(ref currentVisitCategoryItem, value);
        }

        ObservableCollection<ResultKind> results;
        public ObservableCollection<ResultKind> Results
        {
            get => results;
            set => SetProperty(ref results, value);
        }

        int currentPage;
        public int CurrentPage
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }

        int pageRecordCount = 15;
        public int PageRecordCount
        {
            get => pageRecordCount;
            set => pageRecordCount = value;
        }

        int totalRecordCount;
        public int TotalRecordCount
        {
            get => totalRecordCount;
            set => SetProperty(ref totalRecordCount, value);
        }

        public InfluenzaSurveillanceModel(IContainerProvider containerProviderArg)
        {
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();

            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            mzcisdbBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.MZCISDB);
            zycisdbBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.ZYCISDB);
        }

        public void Loaded()
        {
            VisitCategorys = new ObservableCollection<SettingKind>();
            Results = new ObservableCollection<ResultKind>();

            LoadCategoryData();
        }

        private void LoadCategoryData()
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_DictionarySetting WHERE CategoryCode = '01GZ5QN77XYF46FCP1H2KV5M0M'";
            if (!nativeBaseController.Query(sqlSentence, out visitCategoryHub))
                messageQueue.Enqueue("加载就诊类别数据失败!");
            else
                VisitCategorys.AddRange(visitCategoryHub);
        }

        public void QueryData()
        {
            sqlSentence = "EXEC USP_TYBB_LGBLJC @KSRQ='" + BeginDate.ToString("yyyyMMdd") + "',@JSRQ='" + EndDate.ToString("yyyyMMdd") + "'";

            if (CurrentVisitCategoryItem == "OutPatient")
            {
                if (!mzcisdbBaseController.Query<ResultKind>(sqlSentence, out resultHub,commandTimeoutArg:300))
                    messageQueue.Enqueue(retMessage);
            }
            else
            {
                if (!zycisdbBaseController.Query<ResultKind>(sqlSentence, out resultHub, commandTimeoutArg: 300))
                    messageQueue.Enqueue(retMessage);
            }

            CurrentPage = 1;

            TotalRecordCount = resultHub.Count();
            DisplayRecordPage();
        }

        private void DisplayRecordPage()
        {
            Results.Clear();

            int currentRecordCount = (CurrentPage - 1) * PageRecordCount;

            Results.AddRange(resultHub.ToList().GetRange(currentRecordCount, (TotalRecordCount - currentRecordCount) / PageRecordCount > 0 ? PageRecordCount : (TotalRecordCount - currentRecordCount) % PageRecordCount));
        }
        
        public void ExportData()
        {
            if (resultHub == null)
                messageQueue.Enqueue("请先查询数据再导出!");
            else
            {
                string exportFileCatalogue = environmentMonitor.PathSetting.GetContent(PathPart.ExportFileCatalogue);
                string exportFilePath = CurrentVisitCategoryItem == "OutPatient" ? exportFileCatalogue + @"流感监测数据(门诊).xls" : exportFileCatalogue + @"流感监测数据(住院).xls";

                Mapper mapper = new Mapper();
                mapper.Save(exportFilePath, resultHub, sheetIndex: 1, overwrite: true, xlsx: false);
                messageQueue.Enqueue("成功导出至<" + exportFilePath + ">!");
            }
        }

        public void NavigateFirstPage()
        {
            CurrentPage = 1;
            DisplayRecordPage();
        }

        public void NavigateBeforePage()
        {
            if (CurrentPage == 1)
                messageQueue.Enqueue("当前页已是第一页!");
            else
            {
                CurrentPage -= 1;
                DisplayRecordPage();
            }
        }

        public void NavigateNextPage()
        {
            if (CurrentPage == (int)Math.Ceiling((decimal)TotalRecordCount / (decimal)PageRecordCount))
                messageQueue.Enqueue("当前页已是最后页!");
            else
            {
                CurrentPage += 1;
                DisplayRecordPage();
            }
        }

        public void NavigateLastPage()
        {
            CurrentPage = (int)Math.Ceiling((decimal)TotalRecordCount / (decimal)PageRecordCount);
            DisplayRecordPage();
        }

        public void NavigateCurrentPage()
        {
            DisplayRecordPage();
        }
    }
}
