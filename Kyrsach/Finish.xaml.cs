using Kyrsach.Core;
using Kyrsach.Core.map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Логика взаимодействия для Finish.xaml
    /// </summary>
    public partial class Finish : Page
    {
        public string Text { get; set; } = "Поздравляю уровень пройден!!!\nСчет: 10000";
        public int score;
        public Finish()
        {
            InitializeComponent();
            myText.Text = Text;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (myInput.Text.Length > 18 && myInput.Text[18] != ' ')
            {
                if(Core.map.CurrentLevel.GetInstance().GetNumberLevel()!=4)
                Records.GetInstance().AddCompanyRecords(score, myInput.Text.Substring(18));
                else
                    Records.GetInstance().AddNotEndRecords(score, myInput.Text.Substring(18));
                CurrentLevel.GetInstance().SetFirstLevel();
                App.GetNavigationWindow().Navigate(App.GetMainPage());
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            score = Core.Core.GetInstance().GetScore();
            myText.Text = "Поздравляю игра пройдена!!!\nОбщий счет: " + Core.Core.GetInstance().GetScore();
            myInput.Text = "Введите ваш ник: \n";
            Core.Core.GetInstance().AddScore(-Core.Core.GetInstance().GetScore());
            myInput.Foreground = (Brush)(new BrushConverter().ConvertFrom("#d0d0d0"));
        }

        private void myInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (!tb.Text.StartsWith("Введите ваш ник: \n"))
            {
                Dispatcher.BeginInvoke(new Action(() => tb.Undo()));
            }
        }

        private void myInput_TextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = (TextBox)sender;
            if (!tb.Text.StartsWith("Введите ваш ник: \n"))
            {
                Dispatcher.BeginInvoke(new Action(() => tb.Undo()));
            }
        }

        private void myInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb.CaretIndex < 18) e.Handled = true;
            if (!tb.Text.StartsWith("Введите ваш ник: \n"))
            {
                e.Handled = true;
            }
        }
    }
}
