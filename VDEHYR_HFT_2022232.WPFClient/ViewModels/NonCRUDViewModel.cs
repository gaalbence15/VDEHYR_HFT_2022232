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
using System.Windows.Controls;
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

        private int? inputX;

        public int? InputX
        {
            get { return inputX; }
            set { inputX = value; }
        }

        private int? inputY;

        public int? InputY
        {
            get { return inputY; }
            set { inputY = value; }
        }

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

        private bool CheckInputX()
        {
            return InputX != null;
        }

        private bool CheckInputY()
        {
            return InputY != null;
        }

        [RelayCommand]
        public void DogsBornBeforeIsBreed()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Dog> Out = restService.Get<Dog>("ExtendMethodLogic/DogsBornBeforeIsBreed/" + InputX + "/" + InputY);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
            }
        }

        [RelayCommand]
        public void DogsBornAfterIsBreed()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Dog> Out = restService.Get<Dog>("ExtendMethodLogic/DogsBornAfterIsBreed/" + InputX + "/" + InputY);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
            }
        }

        [RelayCommand]
        public void BreedWithDogsMoreThan()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Breed> Out = restService.Get<Breed>("ExtendMethodLogic/BreedWithDogsMoreThan/" + InputX);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
            }
        }

        [RelayCommand]
        public void OwnerWithMoreDogsThan()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Owner> Out = restService.Get<Owner>("ExtendMethodLogic/OwnerWithMoreDogsThan/" + InputX);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
            }
        }

        [RelayCommand]
        public void OwnerWithMoreDogsThanAndOlderThan()
        {
            if (CheckInputX() && CheckInputY())
            {
                Display = new();
                List<Owner> Out = restService.Get<Owner>("ExtendMethodLogic/OwnerWithMoreDogsThanAndOlderThan/" + InputX + "/" + InputY);
                foreach (var item in Out)
                {
                    Display.Add(new ShowItem(item.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Wrong Input!");
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
