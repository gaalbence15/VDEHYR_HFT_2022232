using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.WPFClient.ViewModels
{
    partial class NonCRUDViewModel : ObservableRecipient
    {
        public class ShowItem
        {
            public ShowItem(string display)
            {
                Item = display;
            }
            public string Item { get; set; }
        }
        private ObservableCollection<ShowItem> display;

        public ObservableCollection<ShowItem> Display
        {
            get { return display; }
            set => SetProperty(ref display, value);
        }

        private RestService restService;
        private static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public NonCRUDViewModel()
        {
            if (!IsInDesignMode)
            {
                restService = new("http://localhost:21058/");
            }
        }

        [RelayCommand]
        public void DogStats()
        {
            Display = new();
            List<DogStats> dogstats = restService.Get<DogStats>("ExtendMethodLogic/DogStats");

            foreach (var item in dogstats)
            {
                Display.Add(new ShowItem(item.ToString()));
            }
        }

        [RelayCommand]
        public void Return(Window thisWindow)
        {
            var window = new MainWindow();
            window.Show();
            thisWindow.Close();
        }

    }
}
