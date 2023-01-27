using System;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using MaterialDesignExtensions.Controls;

namespace HAMS.Frame.Control.Login.ViewModels
{
    public class LoginHeaderViewModel : BindableBase
    {
        IRegionManager regionManager;
        Timer timer;
        object currentWindow;

        bool isConfigViewLoaded;
        public bool IsConfigViewLoaded
        {
            get => isConfigViewLoaded;
            set => SetProperty(ref isConfigViewLoaded, value);
        }

        public DelegateCommand<string> NavigateToContentViewCommand { get; private set; }
        public DelegateCommand<string> NavigateToConfigViewCommand { get; private set; }
        public DelegateCommand<object> MinimizeWindowCommand { get; private set; }
        public DelegateCommand<object> CloseWindowCommand { get; private set; }

        public LoginHeaderViewModel(IContainerProvider containerProviderArgs)
        {
            regionManager = containerProviderArgs.Resolve<IRegionManager>();

            IsConfigViewLoaded = false;

            NavigateToContentViewCommand = new DelegateCommand<string>(OnNavigateToContentView);
            NavigateToConfigViewCommand = new DelegateCommand<string>(OnNavigateToConfigView);
            MinimizeWindowCommand = new DelegateCommand<object>(OnMinimizeWindow);
            CloseWindowCommand = new DelegateCommand<object>(OnCloseWindow);

            timer = new Timer(1000);
            currentWindow = new object();
        }

        private void OnNavigateToConfigView(string navigateViewPath)
        {
            if (navigateViewPath != null)
                regionManager.RequestNavigate("LoginContentRegion", navigateViewPath, OnConfigViewLoaded);
        }

        private void OnConfigViewLoaded(NavigationResult result)
        {
            if (result.Result == true)
                IsConfigViewLoaded = true;
        }

        private void OnNavigateToContentView(string navigateViewPath)
        {
            if (navigateViewPath != null)
                regionManager.RequestNavigate("LoginContentRegion", navigateViewPath, OnContentViewLoaded);
        }

        private void OnContentViewLoaded(NavigationResult result)
        {
            if (result.Result == true)
                IsConfigViewLoaded = false;
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
            await ShowConfirmationDialogAsync(obj, false);
        }

        private async Task ShowConfirmationDialogAsync(object obj, bool stackedButtons)
        {
            ConfirmationDialogArguments dialogArgs = new ConfirmationDialogArguments
            {
                Title = "关闭程序",
                Message = "确定关闭程序吗?",
                OkButtonLabel = "确定",
                CancelButtonLabel = "取消",
                StackedButtons = stackedButtons
            };

            bool result = await ConfirmationDialog.ShowDialogAsync("LoginDialog", dialogArgs);
            if (result == true)
            {
                SystemCommands.CloseWindow(obj as Window);
                Application.Current.Shutdown();
            }
        }
    }
}
