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
    /// Логика взаимодействия для PageWinLevel.xaml
    /// </summary>
    public partial class PageWinLevel : Page
    {
        public string Text { get; set; } = "Поздравляю уровень пройден!!!\nСчет: 10000";
        public PageWinLevel()
        {
            InitializeComponent();
            myText.Text = Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Core.Core.GetInstance().Running();
            App.GetNavigationWindow().Navigate(App.GetPage1());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            myText.Text= "Поздравляю уровень пройден!!!\nОбщий счет: "+ Core.Core.GetInstance().GetScore();
        }
    }
}
