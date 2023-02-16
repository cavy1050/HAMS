using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using HAMS.Frame.Control.MainLeftDrawer.Models;

namespace HAMS.Frame.Control.MainLeftDrawer.ViewModels
{
    public class MainLeftDrawerViewModel : BindableBase
    {
        MainLeftDrawerModel mainLeftDrawerModel;
        public MainLeftDrawerModel MainLeftDrawerModel
        {
            get => mainLeftDrawerModel;
            set => SetProperty(ref mainLeftDrawerModel, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }

        public MainLeftDrawerViewModel(IContainerProvider containerProviderArgs)
        {
            MainLeftDrawerModel = new MainLeftDrawerModel(containerProviderArgs);
            LoadedCommand = new DelegateCommand(OnLoaded);
        }

        private void OnLoaded()
        {
            MainLeftDrawerModel.Loaded();
        }
    }
}
