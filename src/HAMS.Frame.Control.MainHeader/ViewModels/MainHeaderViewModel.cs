using System;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using MaterialDesignExtensions.Controls;
using HAMS.Frame.Control.MainHeader.Models;

namespace HAMS.Frame.Control.MainHeader.ViewModels
{
    public class MainHeaderViewModel : BindableBase
    {
        Timer timer;
        object currentWindow;
        bool result;

        MainHeaderModel mainHeaderModel;
        public MainHeaderModel MainHeaderModel
        {
            get => mainHeaderModel;
            set => SetProperty(ref mainHeaderModel, value);
        }

        public DelegateCommand<object> MinimizeWindowCommand { get; private set; }
        public DelegateCommand<object> CloseWindowCommand { get; private set; }

        public MainHeaderViewModel(IContainerProvider containerProviderArgs)
        {
            MainHeaderModel = new MainHeaderModel(containerProviderArgs);

            MinimizeWindowCommand = new DelegateCommand<object>(OnMinimizeWindow);
            CloseWindowCommand = new DelegateCommand<object>(OnCloseWindow);

            timer = new Timer(1000);
            currentWindow = new object();
        }

        private void OnMinimizeWindow(object obj)
        {
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = false;
            timer.Start();

            currentWindow = obj;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Window window = currentWindow as Window;
            window.Dispatcher.Invoke(new Action<object>(ExecMinimize), currentWindow);
            timer.Stop();
        }

        private void ExecMinimize(object obj)
        {
            SystemCommands.MinimizeWindow(obj as Window);
        }

        private async void OnCloseWindow(object obj)
        {
            await ShowCloseDialogAsync(obj, false);
        }

        private async Task ShowCloseDialogAsync(object obj, bool stackedButtons)
        {
            ConfirmationDialogArguments dialogArgs = new ConfirmationDialogArguments
            {
                Title = "关闭程序",
                Message = "确定关闭程序吗?",
                OkButtonLabel = "确定",
                CancelButtonLabel = "取消",
                StackedButtons = stackedButtons
            };

            result = await ConfirmationDialog.ShowDialogAsync("MainDialog", dialogArgs);
            if (result == true)
                Application.Current.Shutdown();
        }
    }
}
