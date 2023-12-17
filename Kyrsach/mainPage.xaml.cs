using Kyrsach.Core;
using Kyrsach.Core.map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        
        public mainPage()
        {
            InitializeComponent();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            App.GetNavigationWindow().Navigate(App.GetPlayPage());
        }
        
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            App.GetNavigationWindow().Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            App.GetNavigationWindow().Navigate(App.GetRecordPage());
        }
    }
}
