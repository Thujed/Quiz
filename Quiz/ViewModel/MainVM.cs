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
using Quiz.Supports.Worker;

namespace Quiz.ViewModel
{
    public class MainVM : BindableBase
    {
        private const int MIN_PLAYERS_COUNT = 8;
        private const double MAX_POINT_VALUE = 60;

        private Color[] _defaultPlayerColours = {
            Color.FromRgb(52, 219, 208),
            Color.FromRgb(233, 89, 52),
            Color.FromRgb(146, 35, 219)
        };

        public MainVM() {
            Players.CollectionChanged += (s, e) =>
                PlayersPresentationData.OnPlayersInfoUpdate(Players.OrderByDescending(p => p.PlayerName.Length).First().PlayerName);

            DBWorker dataBase = new DBWorker();

            Players.AddRange(dataBase.GetAllPlayers().
                             Select(p => new PlayerDataVM(p)).
                             ToList());
            int playersCount = Players.Count;
            for (int i = 0; i < MIN_PLAYERS_COUNT - playersCount; ++i) {
                Players.Add(new PlayerDataVM(new Player(
                    "Player " + (Players.Count + 1).ToString(),
                    0,
                    Players.Count >= _defaultPlayerColours.Length ? Color.FromRgb(125, 125, 125) : _defaultPlayerColours[Players.Count]
                )));
            }

            Players.LinkActionToPlayerDataList(Player_OnPlayerSelected);
            ChangePointsVM.OnPlayerPointsChanged += ChangePointsVM_OnPlayerPointsChanged;
        }

        public ObservableCollection<PlayerDataVM> Players { get; set; } = new ObservableCollection<PlayerDataVM>();
        public PlayerDataVM SelectedPlayer { get; set; }

        public MainBarVM BarVM { get; set; } = new MainBarVM();
        public ChangeGroupPointsVM ChangePointsVM { get; set; } = new ChangeGroupPointsVM();
        public GeneralPlayerDataVM PlayersPresentationData { get; set; } = new GeneralPlayerDataVM(MAX_POINT_VALUE);

        public MainViewInteractionsVM InteractionsVM { get; set; } = new MainViewInteractionsVM();

        private void ChangePointsVM_OnPlayerPointsChanged(double obj)
        {
            double newPoints = SelectedPlayer.PlayerPoints + obj;

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

            ChangePointsVM.ShowView();
            //PlayersPresentationData.PlayersVisibility = false;
        }
    }   
}
