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
using Quiz.Supports.Extensions;

namespace Quiz.ViewModel
{
    public class MainVM : BindableBase
    {
        private const int MAX_POINT_VALUE = 60;

        public MainVM() {
            Players = new ObservableCollection<PlayerDataVM>() {
                new PlayerDataVM(new Player("ПКС-23", 25, Color.FromRgb(52, 219, 208))), 
                new PlayerDataVM(new Player("КС-23", 35, Color.FromRgb(233, 89, 52))),
                new PlayerDataVM(new Player("ИКС-13", 17, Color.FromRgb(146, 35, 219))),
                new PlayerDataVM(new Player("Б-31", 9, Color.FromRgb(238, 238, 59))),
                new PlayerDataVM(new Player("ЭО-41", 54, Color.FromRgb(113, 219, 52))),
                new PlayerDataVM(new Player("ИБ-22", 23, Color.FromRgb(219, 63, 52))),
                new PlayerDataVM(new Player("ПКС-12", 38, Color.FromRgb(219, 52, 152))),
                new PlayerDataVM(new Player("КС-11", 22, Color.FromRgb(52, 219, 152))),
                new PlayerDataVM(new Player("ЭО-12", 29, Color.FromRgb(52, 162, 219))),
                new PlayerDataVM(new Player("ИКС-42", 42, Color.FromRgb(52, 63, 219)))
            };

            foreach (PlayerDataVM player in Players) {
                player.OnPlayerSelected += Player_OnPlayerSelected;
            }

            //@TODO: Remove after debagging
            PlayersPresentationData.OnPlayersInfoUpdate(Players.OrderByDescending(p => p.PlayerName.Length).First().PlayerName);

            Players.CollectionChanged += (s, e) =>
                PlayersPresentationData.OnPlayersInfoUpdate(Players.OrderByDescending(p => p.PlayerName.Length).First().PlayerName);

            ChangePointsVM.OnPlayerPointsChanged += ChangePointsVM_OnPlayerPointsChanged;
        }

        public ObservableCollection<PlayerDataVM> Players { get; set; }
        public PlayerDataVM SelectedPlayer { get; set; }

        public MainBarVM BarVM { get; set; } = new MainBarVM();
        public ChangeGroupPointsVM ChangePointsVM { get; set; } = new ChangeGroupPointsVM();
        public GeneralPlayerDataVM PlayersPresentationData { get; set; } = new GeneralPlayerDataVM(MAX_POINT_VALUE);

        public MainViewInteractionsVM InteractionsVM { get; set; } = new MainViewInteractionsVM();

        private void ChangePointsVM_OnPlayerPointsChanged(int obj)
        {
            int newPoints = SelectedPlayer.PlayerPoints + obj;

            if (newPoints > MAX_POINT_VALUE) { 
                SelectedPlayer.SetPlayerPoints(MAX_POINT_VALUE);
            } else if (newPoints < 0) {
                SelectedPlayer.SetPlayerPoints(0);
            } else {
                SelectedPlayer.SetPlayerPoints(newPoints);
            }
            
        }

        private void Player_OnPlayerSelected(PlayerDataVM obj)
        {
            SelectedPlayer = obj;
            RaisePropertyChanged("SelectedPlayer");
            PlayersPresentationData.FadeOutAnimationValue = OtherExtensions.ReverceAnimationTriggerValue(PlayersPresentationData.FadeOutAnimationValue);
        }

    }   
}
