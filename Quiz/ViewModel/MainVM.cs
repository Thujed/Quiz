using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows;
using System.Windows.Input;

namespace Quiz.ViewModel
{
    public class MainVM : BindableBase
    {
        private const int MAX_POINT_VALUE = 60;

        public MainVM() {
            Players = new ObservableCollection<PlayerDataVM>() {
                new PlayerDataVM(new Player("ПКС-23", 25, Color.FromRgb(52, 219, 208)), this), 
                new PlayerDataVM(new Player("КС-23", 35, Color.FromRgb(233, 89, 52)), this) 
            };

            Players.ToList().ForEach(p => p.UpdateMaxPointValue(MAX_POINT_VALUE));

            //@TODO: Remove after debagging
            PlayersPresentationData.OnPlayersInfoUpdate(Players.OrderByDescending(p => p.PlayerName.Length).First().PlayerName);

            Players.CollectionChanged += (s, e) =>
                PlayersPresentationData.OnPlayersInfoUpdate(Players.OrderByDescending(p => p.PlayerName.Length).First().PlayerName);
        }

        public ObservableCollection<PlayerDataVM> Players { get; set; }

        public MainBarVM BarVM { get; set; } = new MainBarVM();
        public ChangeGroupPointsVM ChangePointsVM { get; set; } = new ChangeGroupPointsVM();
        public GeneralPlayerDataVM PlayersPresentationData { get; set; } = new GeneralPlayerDataVM(MAX_POINT_VALUE);

        public MainViewInteractionsVM InteractionsVM { get; set; } = new MainViewInteractionsVM();
    }   
}
