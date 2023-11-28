using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.WPFClient.ViewModels
{
    partial class DogEditorViewModel : ObservableRecipient
    {
        private Dog selectedItem;
        public Dog SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value);
                InputItem = value;
                DeleteCommand.NotifyCanExecuteChanged();
                UpdateCommand.NotifyCanExecuteChanged();
            }
        }

        private Dog inputItem;
        public Dog InputItem
        {
            get { return inputItem; }
            set
            {
                SetProperty(ref inputItem, value);
                if (value != null)
                {
                    InputId = value.Id;
                    InputName = value.Name;
                    InputBirthyear = value.BirthYear;
                    InputWeight = value.Weight;
                    //InputColor = value.Color;
                }
                else
                {
                    InputId = null;
                    InputName = null;
                    InputBirthyear = null;
                    InputWeight = null;
                    //InputColor = null;
                }
            }
        }

        private int? inputId;
        public int? InputId
        {
            get { return inputId; }
            set => SetProperty(ref inputId, value);
        }

        private string inputName;
        public string InputName
        {
            get { return inputName; }
            set => SetProperty(ref inputName, value);
        }

        private int? inputBirthyear;
        public int? InputBirthyear
        {
            get { return inputBirthyear; }
            set => SetProperty(ref inputBirthyear, value);
        }

        private int? inputWeight;
        public int? InputWeight
        {
            get { return inputWeight; }
            set => SetProperty(ref inputWeight, value);
        }

        ////private int? inputColor;
        ////public int? InputColor
        ////{
        ////    get { return InputColor; }
        ////    set => SetProperty(ref inputColor, value);
        ////}

        public bool IsButtonExecutable()
        {
            return SelectedItem != null;
        }

        public RestCollection<Dog> Dogs { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public DogEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Dogs = new RestCollection<Dog>("http://localhost:21058/", "Dog", "hub");
            }
        }

        [RelayCommand]
        public void Create()
        {
            if (InputId != null && InputName != null && InputName != "" && InputBirthyear != null && InputWeight != null/* && InputColor != null*/)
            {
                Dogs.Add(new Dog((int)InputId, InputName, (int)InputBirthyear, (int)InputWeight/*, (int)InputColor*/));
            }
            else { MessageBox.Show("Wrong Input!"); }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Update()
        {
            if (InputId != null && InputName != null && InputName != "" && InputBirthyear != null && InputWeight != null/* && InputColor != null*/)
            {
                SelectedItem.Id = (int)InputId;
                SelectedItem.Name = InputName;
                SelectedItem.BirthYear = (int)InputBirthyear;
                SelectedItem.Weight = (int)InputWeight;
                //SelectedItem.Color = (int)InputColor;
                Dogs.Update(SelectedItem);
            }
            else { MessageBox.Show("Wrong Input!"); }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Delete()
        {
            Dogs.Delete(SelectedItem.Id);
            SelectedItem = null;
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
