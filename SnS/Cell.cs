using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SnS
{
    internal class Cell : INotifyPropertyChanged
    {
        private int number { get; set; }
        private string content { get; set; }

        public int Number
        {
            get
            {
                return number;
            }
            private set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        public Cell(int num, string text)
        {
            Number = num;
            Content = text;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
