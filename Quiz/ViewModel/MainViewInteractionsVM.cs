using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

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
            OpenColorPickerAnimationTrigger = false;
        }

        private bool _openSettingsAnimationTrigger;
        public bool OpenSettingsAnimationTrigger
        {
            get => _openSettingsAnimationTrigger;
            set {
                if (value && _openQuestionsAnimationTrigger) {
                    OpenQuestionsAnimationTrigger = false;
                }

                if (!value) {
                    OpenColorPickerAnimationTrigger = false;
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
                    OpenSettingsAnimationTrigger = false;
                }

                SetProperty(ref _openQuestionsAnimationTrigger, value);
            }
        }

        private bool _openColorPickerAnimationTrigger;
        public bool OpenColorPickerAnimationTrigger
        {
            get => _openColorPickerAnimationTrigger;
            set {
                SetProperty(ref _openColorPickerAnimationTrigger, value);
            }
        }


        public DelegateCommand SettingsIconClickCommand { get; }
        public DelegateCommand CloseClick { get; }
    }
}
