using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Prism.Ioc;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;
using Npoi.Mapper;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

namespace HQMS.Extension.Control.Main.Models
{
    public class DataExportingModel : BindableBase
    {
        //设置项
        //汇总文件导出路径,明细文件导出路径
        string masterExportFilePath, detailExportFilePath;

        string sqlSentence, sqlRetSentence;
        string beginDate, endDate;
        List<CategoryKind> yearHub;
        List<CategoryKind> monthHub;
        List<MasterKind> masterHub;
        List<DetailKind> detailHub;

        ISnackbarMessageQueue messageQueue;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IDataBaseController BAGLDBController;

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

        ObservableCollection<MasterKind> masters;
        public ObservableCollection<MasterKind> Masters
        {
            get => masters;
            set => SetProperty(ref masters, value);
        }

        ObservableCollection<DetailKind> details;
        public ObservableCollection<DetailKind> Details
        {
            get => details;
            set => SetProperty(ref details, value);
        }

        public DataExportingModel(IContainerProvider containerProviderArg)
        {
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            BAGLDBController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.BAGLDB);

            Masters = new ObservableCollection<MasterKind>();
            Details = new ObservableCollection<DetailKind>();
        }

        public void Loaded()
        {
            Years = new ObservableCollection<CategoryKind>();
            Months = new ObservableCollection<CategoryKind>();

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

        public void LoadMasterData()
        {
            CalcQueryDate();

            Masters.Clear();

            sqlSentence = "EXEC usp_hqms_gettjsj '" + beginDate + "','" + endDate + "',1";
            if (BAGLDBController.QueryWithMessage<MasterKind>(sqlSentence, out masterHub, out sqlRetSentence))
                Masters.AddRange(masterHub);
            else
                messageQueue.Enqueue(sqlRetSentence);
        }

        public void LoadDetailData()
        {
            Details.Clear();

            if (Masters.Where(x => x.IsSelected).Count() > 1)
                messageQueue.Enqueue("请单选汇总数据!");
            else
            {
                sqlSentence = "EXEC usp_hqms_gettjsj '" + beginDate + "','" + endDate + "',0,'" + Masters.FirstOrDefault(x => x.IsSelected).FCODE + "'";
                if (BAGLDBController.QueryWithMessage<DetailKind>(sqlSentence, out detailHub, out sqlRetSentence))
                    Details.AddRange(detailHub);
                else
                    messageQueue.Enqueue(sqlRetSentence);
            }
        }

        public void ExprotMasterData()
        {
            Mapper mapper = new Mapper();
            mapper.Save(masterExportFilePath, Masters, sheetIndex: 1, overwrite: true, xlsx: false);
        }

        public void ExprotDetailData()
        {
            Mapper mapper = new Mapper();
            mapper.Save(detailExportFilePath, Details, sheetIndex: 1, overwrite: true, xlsx: false);
        }
    }
}
