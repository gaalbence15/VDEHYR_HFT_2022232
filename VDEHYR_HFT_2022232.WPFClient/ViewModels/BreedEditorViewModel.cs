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
    partial class BreedEditorViewModel : ObservableRecipient
    {
        private Breed selectedItem;
        public Breed SelectedItem
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

        private Breed inputItem;
        public Breed InputItem
        {
            get { return inputItem; }
            set
            {
                SetProperty(ref inputItem, value);
                if (value != null)
                {
                    InputId = value.Id;
                    InputName = value.Name;
                    InputOrigin = value.Origin;
                    InputLifeSpan = value.Lifespan;
                }
                else
                {
                    InputId = null;
                    InputName = null;
                    InputOrigin = null;
                    InputLifeSpan = null;
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

        private string inputOrigin;
        public string InputOrigin
        {
            get { return inputOrigin; }
            set => SetProperty(ref inputOrigin, value);
        }

        private int? inputLifeSpan;
        public int? InputLifeSpan
        {
            get { return inputLifeSpan; }
            set => SetProperty(ref inputLifeSpan, value);
        }

        public bool IsButtonExecutable()
        {
            return SelectedItem != null;
        }

        public RestCollection<Breed> Breeds { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public BreedEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Breeds = new RestCollection<Breed>("http://localhost:21058/", "Breed", "hub");
            }
        }

        [RelayCommand]
        public void Create()
        {
            if (InputId != null && InputName != null && InputName != "" && InputOrigin != null && InputOrigin != "" && InputLifeSpan != null)
            {
                Breeds.Add(new Breed((int)InputId, InputName, InputOrigin, (int)InputLifeSpan));
            }
            else { MessageBox.Show("Wrong Input!"); }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Update()
        {
            if (InputId != null && InputName != null && InputName != "" && InputOrigin != null && InputOrigin != "" && InputLifeSpan != null)
            {
                SelectedItem.Id = (int)InputId;
                SelectedItem.Name = InputName;
                SelectedItem.Origin = InputOrigin;
                SelectedItem.Lifespan = (int)InputLifeSpan;
                Breeds.Update(SelectedItem);
            }
            else { MessageBox.Show("Wrong Input!"); }
            SelectedItem = null;
        }

        [RelayCommand(CanExecute = nameof(IsButtonExecutable))]
        public void Delete()
        {
            Breeds.Delete(SelectedItem.Id);
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
