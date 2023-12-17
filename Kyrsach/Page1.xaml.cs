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
using System.Windows.Threading;

namespace Kyrsach
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private DispatcherTimer timer = new DispatcherTimer();
        public Page1()
        {
          
            InitializeComponent();
            
            timer.Tick += new EventHandler(Tick);
            timer.Interval = new TimeSpan();
            timer.Stop();
            
        }

        private void Tick(object sender, EventArgs e)
        {
            if (!Core.StartGame.Start(myImage))
            {
                timer.Stop();
            }
        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            Core.Events.GetInstance().setMousePosition(position);
        }

       

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}
