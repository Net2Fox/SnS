using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnS.Model
{
    internal class Game : INotifyPropertyChanged
    {
        private bool isGameStart { get; set; }
        private int playerIndex { get; set; }
        private ObservableCollection<Player> playersList { get; set; }
        private ObservableCollection<Cell> cellsList { get; set; }

        public bool IsGameStart
        {
            get
            {
                return isGameStart;
            }
            private set
            {
                isGameStart = value;
                OnPropertyChanged("IsGameStart");
            }
        }

        public int PlayerIndex
        {
            get
            {
                return playerIndex;
            }
            private set
            {
                if (IsGameStart == false && (value >= 0 && value < 4))
                {
                    playerIndex = value;
                    OnPropertyChanged("PlayerIndex");
                }
                else if(IsGameStart == true && (value >= 0 && value < PlayersList.Count))
                {
                    playerIndex = value;
                    OnPropertyChanged("PlayerIndex");
                }
                else
                {
                    playerIndex = 0;
                    OnPropertyChanged("PlayerIndex");
                }
            }
        }

        public ObservableCollection<Player> PlayersList
        {
            get
            {
                return playersList;
            }
            private set
            {
                playersList = value;
                OnPropertyChanged("PlayersList");
            }
        }

        public ObservableCollection<Cell> CellsList
        {
            get
            {
                return cellsList;
            }
            private set
            {
                cellsList = value;
                OnPropertyChanged("CellsList");
            }
        }

        public Game(ObservableCollection<Player> playersList, ObservableCollection<Cell> cellsList)
        {
            IsGameStart = false;
            PlayerIndex = 0;
            PlayersList = playersList;
            CellsList = cellsList;
        }

        public void StartGame()
        {
            IsGameStart = true;
        }

        public void PlayerMove(int num)
        {
            if(PlayersList[PlayerIndex].PlayerMove + num >= 100)
            {
                System.Windows.MessageBox.Show($"{PlayersList[PlayerIndex].PlayerName} дошёл до финиша!");
                IsGameStart = false;
                return;
            }
            CellsList[PlayersList[PlayerIndex].PlayerMove].Content = $"Cell {CellsList[PlayersList[PlayerIndex].PlayerMove].Number}";
            PlayersList[PlayerIndex].Move(num);
            if(CellsList[PlayersList[PlayerIndex].PlayerMove].Content == "Snake")
            {
                PlayersList[PlayerIndex].Move(-2);
                System.Windows.MessageBox.Show($"{PlayersList[PlayerIndex].PlayerName} попал на змею!\nЕго переместило на 2 шаги назад.");
            }
            else if(CellsList[PlayersList[PlayerIndex].PlayerMove].Content == "Stair")
            {
                PlayersList[PlayerIndex].Move(4);
                System.Windows.MessageBox.Show($"{PlayersList[PlayerIndex].PlayerName} попал на лестницу!\nЕго переместило на 4 шага вперёд.");
            }
            CellsList[PlayersList[PlayerIndex].PlayerMove].Content = PlayersList[PlayerIndex].PlayerName;
            PlayerIndex++;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
