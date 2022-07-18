using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnS.Model
{
    internal class Player : INotifyPropertyChanged
    {
        private string playerName { get; set; }

        private int playerMove { get; set; }

        public string PlayerName
        {
            get
            {
                return playerName;
            }
            private set
            {
                playerName = value;
                OnPropertyChanged("PlayerName");
            }
        }

        public int PlayerMove
        {
            get
            {
                return playerMove;
            }
            private set
            {
                playerMove = value;
                OnPropertyChanged("PlayerMove");
            }
        }

        public Player()
        {
            PlayerName = "Player";
            PlayerMove = 0;
        }

        public Player(string name)
        {
            PlayerName = name;
            PlayerMove = 0;
        }

        public void Move(int num)
        {
            PlayerMove += num;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
