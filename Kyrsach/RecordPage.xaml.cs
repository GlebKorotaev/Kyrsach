using Kyrsach.Core;
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
    /// Логика взаимодействия для RecordPage.xaml
    /// </summary>
    public partial class RecordPage : Page
    {
        
        public RecordPage()
        {
            InitializeComponent();
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            App.GetNavigationWindow().Navigate(App.GetMainPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<TextBlock> CompanyRecords = new List<TextBlock>
            {
                TextBlock1,
                TextBlock2,
                TextBlock3,
                TextBlock4
            };
            for (int i =0; i < Records.GetInstance().GetCompanyRecords().Count;i++ )
            {
                CompanyRecords[i].Text = Records.GetInstance().GetCompanyRecords()[i].name + ":\n" + Records.GetInstance().GetCompanyRecords()[i].score;
            }
            List<TextBlock> NotEndRecords = new List<TextBlock>
            {
                TextBlock5,
                TextBlock6,
                TextBlock7,
                TextBlock8
            };
            for (int i = 0; i < Records.GetInstance().GetNotEndRecords().Count; i++)
            {
                NotEndRecords[i].Text = Records.GetInstance().GetNotEndRecords()[i].name + ":\n" + Records.GetInstance().GetNotEndRecords()[i].score;
            }
        }
    }
}
