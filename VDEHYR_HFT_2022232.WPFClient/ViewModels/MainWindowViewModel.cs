using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VDEHYR_HFT_2022232.WPFClient.ViewModels
{
    partial class MainWindowViewModel
    {
        [RelayCommand]
        public void EditOwners(Window window)
        {
            var OwnerWindow = new OwnerEditor();
            OwnerWindow.Show();
            window.Close();
        }

        [RelayCommand]
        public void NonCRUD(Window window)
        {
            var Window = new NonCRUD();
            Window.Show();
            window.Close();
        }
        public MainWindowViewModel()
        {
            
        }
    }
}
