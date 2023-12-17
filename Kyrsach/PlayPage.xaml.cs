using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kyrsach
{
    /// <summary>
    /// Логика взаимодействия для PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Page
    {
        public PlayPage()
        {
            InitializeComponent();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            Core.Core.GetInstance().SetNewGame();
            App.GetNavigationWindow().Navigate(App.GetPage1());
        }

        private void ContinueGame(object sender, RoutedEventArgs e)
        {
            Core.Core.GetInstance().Running();
            App.GetNavigationWindow().Navigate(App.GetPage1());
        }

        private void EndlessGame(object sender, RoutedEventArgs e)
        {
            Core.Core.GetInstance().SetNoEndGame();
            App.GetNavigationWindow().Navigate(App.GetPage1());
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            App.GetNavigationWindow().Navigate(App.GetMainPage());
        }
    }

}
