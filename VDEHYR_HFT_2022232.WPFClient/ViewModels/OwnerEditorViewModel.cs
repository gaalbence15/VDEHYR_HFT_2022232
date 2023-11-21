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
    partial class OwnerEditorViewModel : ObservableRecipient
    {
        private Owner selectedItem;
        public Owner SelectedItem
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

        private Owner inputItem;
        public Owner InputItem
        {
            get { return inputItem; }
            set
            {
                SetProperty(ref inputItem, value);
                if (value != null)
                {
                    InputId = value.Id;
                    InputName = value.Name;
                    InputAge = value.Age;
                }
                else
                {
                    InputId = null;
                    InputName = null;
                    InputAge = null;
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

        private int? inputAge;
        public int? InputAge
        {
            get { return inputAge; }
            set => SetProperty(ref inputAge, value);
        }

        public bool IsButtonExecutable()
        {
            return SelectedItem != null;
        }

        public RestCollection<Owner> Owners { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public OwnerEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Owners = new RestCollection<Owner>("http://localhost:21058/", "Owner", "hub");
            }
        }

        [RelayCommand]
        public void Create()
        {
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Update()
        {
            MessageBox.Show(InputId + InputName + InputAge);
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Delete()
        {
            Owners.Delete(SelectedItem.Id);
            SelectedItem = null;
        }

    }
}
