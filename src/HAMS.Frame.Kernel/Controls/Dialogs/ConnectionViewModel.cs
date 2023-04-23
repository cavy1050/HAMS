using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace HAMS.Frame.Kernel.Controls
{
    public class ConnectionViewModel : BindableBase, IDialogAware
    {
        string title = "数据库连接字符串设置";
        public string Title { get => title; }

        public string DataBaseIdentifier { get; set; }
        public string ConnectionString { get; set; }

        public event Action<IDialogResult> RequestClose;

        ConnectionModel connectionModel;
        public ConnectionModel ConnectionModel
        {
            get => connectionModel;
            set => SetProperty(ref connectionModel, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand ConfirmCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public ConnectionViewModel(IContainerProvider containerProviderArg)
        {
            ConnectionModel = new ConnectionModel(containerProviderArg);

            LoadedCommand = new DelegateCommand(OnLoaded);
            ConfirmCommand = new DelegateCommand(OnConfirm);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogOpened(IDialogParameters dialogParametersArg)
        {
            DataBaseIdentifier = dialogParametersArg.GetValue<string>("DataBaseIdentifier");
            ConnectionString = dialogParametersArg.GetValue<string>("ConnectionString");
        }

        public void OnDialogClosed()
        {

        }

        private void OnLoaded()
        {
            ConnectionModel.Loaded(DataBaseIdentifier, ConnectionString);
        }

        private void OnCancel()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        private void OnConfirm()
        {
            ConnectionModel.Refresh();

            DialogParameters connectStringParameter = new DialogParameters();
            connectStringParameter.Add("DataBaseIdentifier", DataBaseIdentifier);
            connectStringParameter.Add("ConnectionString", ConnectionModel.SqlConnectionStringBuilder.ConnectionString);

            RequestClose.Invoke(new DialogResult(ButtonResult.OK, connectStringParameter));
        }
    }
}
