using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SnS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        Random random = new Random(DateTime.Now.Second);
        List<Button> Buttons = new List<Button> { };
        ObservableCollection<Player> Players;
        int PlyaerIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
            //Players = (DataContext as ApplicationViewModel).Players;
            //foreach(Button button in Pole.Children)
            //{
                //Buttons.Add(button);
            //}
        }
    }
}
