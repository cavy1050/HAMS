using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Prism.Ioc;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;
using CsvHelper;
using HQMS.Extension.Kernel.Core;

namespace HQMS.Extension.Control.Main.Models
{
    public class DataQueryingModel : BindableBase
    {
        //获取设置项 医院代码,导出CSV文件路径,上传文件路径,文件名称
        string exportFilePath, upLoadFilePath,fileName;

        string sqlSentence;
        List<CategoryKind> yearHub;
        List<CategoryKind> monthHub;
        List<ResultKind> resultHub;

        string beginDate, endDate;

        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IDataBaseController BAGLDBController;
        ISnackbarMessageQueue messageQueue;
        ISetting setting;

        ObservableCollection<CategoryKind> years;
        public ObservableCollection<CategoryKind> Years
        {
            get => years;
            set => SetProperty(ref years, value);
        }

        string currentYear;
        public string CurrentYear
        {
            get => currentYear;
            set => SetProperty(ref currentYear, value);
        }

        ObservableCollection<CategoryKind> months;
        public ObservableCollection<CategoryKind> Months
        {
            get => months;
            set => SetProperty(ref months, value);
        }

        string currentMonth;
        public string CurrentMonth
        {
            get => currentMonth;
            set => SetProperty(ref currentMonth, value);
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

        int pageRecordCount = 20;
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

        DisplayModePart currentDisplayMode;
        public DisplayModePart CurrentDisplayMode
        {
            get => currentDisplayMode;
            set => SetProperty(ref currentDisplayMode, value);
        }

        public DataQueryingModel(IContainerProvider containerProviderArg)
        {
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            BAGLDBController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.BAGLDB);

            setting = containerProviderArg.Resolve<ISetting>();
        }

        public void Loaded()
        {
            Years = new ObservableCollection<CategoryKind>();
            Months = new ObservableCollection<CategoryKind>();
            Results = new ObservableCollection<ResultKind>();
            CurrentDisplayMode = setting.DisplayMode;

            LoadCategoryData();
        }

        private void LoadCategoryData()
        {
            sqlSentence = "SELECT Code,Item,Name,Content,CategoryCode,CategoryItem,Description,Note,Rank,DefaultFlag,EnabledFlag FROM HQMS_DictionarySetting WHERE CategoryCode='01GTN95ZFWMSK11T1YWYMEGS9Z' AND EnabledFlag = True ORDER BY Rank";

            if (!nativeBaseController.Query<CategoryKind>(sqlSentence, out yearHub))
                messageQueue.Enqueue("加载年份数据失败!");
            else
                Years.AddRange(yearHub);

            sqlSentence = "SELECT Code,Item,Name,Content,CategoryCode,CategoryItem,Description,Note,Rank,DefaultFlag,EnabledFlag FROM HQMS_DictionarySetting WHERE CategoryCode='01GTNFQDADBJCMK9Q8V1ZK8R0M' AND EnabledFlag = True ORDER BY Rank";

            if (!nativeBaseController.Query<CategoryKind>(sqlSentence, out monthHub))
                messageQueue.Enqueue("加载月份数据失败!");
            else
                Months.AddRange(monthHub);
        }

        public void QueryData()
        {
            string retString;
            CalcQueryDate();

            sqlSentence = "EXEC usp_hqms_getbasj '" + beginDate + "','" + endDate + "','" + setting.HospitalCode + "'";
            BAGLDBController.QueryWithMessage<ResultKind>(sqlSentence, out resultHub,out retString);

            CurrentPage = 1;

            TotalRecordCount = resultHub.Count();
            DisplayRecordPage();
        }

        private void CalcQueryDate()
        {
            switch (CurrentMonth)
            {
                case "January": beginDate = CurrentYear + "0101"; endDate = CurrentYear + "0201"; break;
                case "February": beginDate = CurrentYear + "0201"; endDate = CurrentYear + "0301"; break;
                case "March": beginDate = CurrentYear + "0301"; endDate = CurrentYear + "0401"; break;
                case "April": beginDate = CurrentYear + "0401"; endDate = CurrentYear + "0501"; break;
                case "May": beginDate = CurrentYear + "0501"; endDate = CurrentYear + "0601"; break;
                case "June": beginDate = CurrentYear + "0601"; endDate = CurrentYear + "0701"; break;
                case "July": beginDate = CurrentYear + "0701"; endDate = CurrentYear + "0801"; break;
                case "August": beginDate = CurrentYear + "0801"; endDate = CurrentYear + "0901"; break;
                case "September": beginDate = CurrentYear + "0901"; endDate = CurrentYear + "1001"; break;
                case "October": beginDate = CurrentYear + "1001"; endDate = CurrentYear + "1101"; break;
                case "November": beginDate = CurrentYear + "1101"; endDate = CurrentYear + "1201"; break;
                case "December": beginDate = CurrentYear + "1201"; endDate = (Convert.ToInt32(CurrentYear) + 1).ToString() + "0101"; break;
            }
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
                fileName = "hqmsts_" + beginDate.Substring(0, 4) + "M" + beginDate.Substring(4, 2) + ".csv";
                exportFilePath = environmentMonitor.PathSetting.GetContent(PathPart.ExportFileCatalogue) + fileName;

                using (StreamWriter writer = new StreamWriter(exportFilePath))
                {
                    using (CsvWriter csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                    {
                        csv.WriteRecords(resultHub);
                        messageQueue.Enqueue("文件成功导出至:<" + exportFilePath + ">");
                    }
                }
            }
        }

        public void UpLoadData()
        {
            if (!File.Exists(exportFilePath))
                messageQueue.Enqueue("请先导出数据再上传!");
            else
            {
                upLoadFilePath = setting.UpLoadFileCatalogue + fileName;

                try
                {
                    File.Copy(exportFilePath, upLoadFilePath, true);
                }
                catch (Exception ex)
                {
                    messageQueue.Enqueue(ex.Message);
                }
                finally
                {
                    messageQueue.Enqueue("文件成功上传至:<" + upLoadFilePath + ">");
                }
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