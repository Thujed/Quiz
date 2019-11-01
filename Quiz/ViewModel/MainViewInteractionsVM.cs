using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Quiz.Supports.Extensions;
using Quiz.Supports.PropertyAnimationHandlers;

namespace Quiz.ViewModel
{
    public class MainViewInteractionsVM : BindableBase
    {

        //public event Action

        public MainViewInteractionsVM()
        {
            Application.Current.MainWindow.KeyDown += (s, e) => {
                switch (e.Key)
                {
                    case Key.Q:
                        OpenQuestionsAnimationTrigger = !OpenQuestionsAnimationTrigger;
                        break;
                }
            };

            SettingsIconClickCommand = new DelegateCommand(() =>
                OpenSettingsAnimationTrigger = !OpenSettingsAnimationTrigger
            );

            OpenSettingsAnimationTrigger = false;
            OpenQuestionsAnimationTrigger = false;
        }

        private bool _openSettingsAnimationTrigger;
        public bool OpenSettingsAnimationTrigger
        {
            get => _openSettingsAnimationTrigger;
            set {
                if (value && _openQuestionsAnimationTrigger) {
                    _openQuestionsAnimationTrigger = false;
                    RaisePropertyChanged("OpenQuestionsAnimationTrigger");
                }

                SetProperty(ref _openSettingsAnimationTrigger, value);
            }
        }

        private bool _openQuestionsAnimationTrigger;
        public bool OpenQuestionsAnimationTrigger
        {
            get => _openQuestionsAnimationTrigger;
            set {
                if (value && _openSettingsAnimationTrigger) {
                    _openSettingsAnimationTrigger = false;
                    RaisePropertyChanged("OpenSettingsAnimationTrigger");
                }

                SetProperty(ref _openQuestionsAnimationTrigger, value);
            }
        }

        public DelegateCommand SettingsIconClickCommand { get; }
        public DelegateCommand CloseClick { get; }
    }
}
