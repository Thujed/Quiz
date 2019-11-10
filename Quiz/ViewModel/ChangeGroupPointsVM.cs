using Prism.Mvvm;
using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Quiz.ViewModel
{
    public class ChangeGroupPointsVM : BindableBase
    {
        public event Action<double> OnPlayerPointsChanged;

        public ChangeGroupPointsVM() {
            Application.Current.MainWindow.KeyDown += MainWindow_KeyDown;

            HideView();
            IsPositiveSign = true;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (!ShowingAnimationTrigger)
                return;

            switch (e.Key) {
                case Key.Enter:
                    double increasingValue = Convert.ToDouble(ScoreChangeValue.Replace('.', ','));
                    OnPlayerPointsChanged.Invoke(_isPositiveSign ? increasingValue : -increasingValue);

                    HideView();
                    break;
                case Key.OemTilde:
                    IsPositiveSign = !IsPositiveSign;
                    break;
            }
        }

        private bool _isPositiveSign;
        public bool IsPositiveSign {
            get => _isPositiveSign;
            set {
                SetProperty(ref _isPositiveSign, value);
            }
        }

        private bool _showingAnimationTrigger;
        public bool ShowingAnimationTrigger
        {
            get => _showingAnimationTrigger;
            set {
                SetProperty(ref _showingAnimationTrigger, value);
            }
        }
        
        public string ScoreChangeValue { get; set; } = "1";

        public void ShowView() =>
            ShowingAnimationTrigger = true;

        public void HideView() =>
            ShowingAnimationTrigger = false;
    }
}
