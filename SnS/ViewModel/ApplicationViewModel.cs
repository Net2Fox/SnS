using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using SnS.Model;

namespace SnS.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        // Class Player
        private Player selectedPlayer;
        
        public ObservableCollection<Player> Players { get; set; }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Player player = new Player($"Player {Players.Count + 1}");
                      Players.Add(player);
                      SelectedPlayer = player;
                  },
                 (obj) => Players.Count < 4 && nowGame.IsGameStart == false));
            }
        }

        // команда удаления
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Player player = obj as Player;
                      if (player != null)
                      {
                          Players.Remove(player);
                      }
                  },
                 (obj) => Players.Count > 0 && nowGame.IsGameStart == false));
            }
        }

        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                selectedPlayer = value;
                OnPropertyChanged("SelectedPlayer");
            }
        }

        // Class Game
        private Game nowGame;

        // команда начала игры
        private RelayCommand startGameCommand;
        public RelayCommand StartGameCommand
        {
            get
            {
                return startGameCommand ??
                  (startGameCommand = new RelayCommand(obj =>
                  {
                      nowGame.StartGame();
                  },
                 (obj) => Players.Count != 0 && nowGame.IsGameStart == false));
            }
        }
        Random rand = new Random(DateTime.Now.Second);
        // команда хода
        private RelayCommand playerMoveCommand;
        public RelayCommand PlayerMoveCommand
        {
            get
            {
                return playerMoveCommand ??
                  (playerMoveCommand = new RelayCommand(obj =>
                  {
                      nowGame.PlayerMove(rand.Next(1, 7));
                  },
                 (obj) => nowGame.IsGameStart == true));
            }
        }

        public Game NowGame
        {
            get { return nowGame; }
            set
            {
                nowGame = value;
                OnPropertyChanged("NowGame");
            }
        }

        // Class Cell
        private Cell cell;

        public ObservableCollection<Cell> Cells { get; set; }

        public Cell Cell
        {
            get { return cell; }
            set
            {
                cell = value;
                OnPropertyChanged("Cell");
            }
        }

        public ApplicationViewModel()
        {
            Players = new ObservableCollection<Player>
            {
                new Player("Player 1")
            };
            Cells = new ObservableCollection<Cell>
            {
                new Cell(0, "Start")
            };
            for (int i = 0; i < 100; i++)
            {
                Cells.Add(new Cell(i, $"Cell {i}"));
            }
            for(int i = 0; i < 10; i++)
            {
                int s = rand.Next(0, 2);
                if (s == 0)
                {
                    int n = rand.Next(1, 100);
                    if(Cells[n].Content == "Snake" || Cells[n].Content == "Stair")
                    {
                        i--;
                    }
                    else
                        Cells[rand.Next(1, 100)].Content = "Snake";
                }
                else if (s == 1)
                {
                    int n = rand.Next(1, 100);
                    if (Cells[n].Content == "Snake" || Cells[n].Content == "Stair")
                    {
                        i--;
                    }
                    else
                        Cells[rand.Next(1, 100)].Content = "Stair";
                }
            }
            Cells.Add(new Cell(100, "Finish"));
            nowGame = new Game(Players, Cells);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
