using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Kyrsach.Core;
using Kyrsach.Core.map;

namespace Kyrsach
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        private static NavigationWindow navigationWindow =  new NavigationWindow();
        private static Page page = new mainPage();
        private static Page page6 = new RecordPage();
        private static Page page1 = new Page1();
        private static Page page2 = new PageWinLevel();
        private static Page page3 = new Finish();
        private static Page page4 = new PlayPage();
        private static Page page5 = new LosePage();
        public static Page GetRecordPage()
        {
            return page6;
        }
        public static Page GetLosePage()
        {
            return page5;
        }
        public static NavigationWindow GetNavigationWindow()
        {
            return navigationWindow;
        }
        public static Page GetFinishPage()
        {
            return page3;
        }
        public static Page GetPlayPage()
        {
            return page4;
        }
        public static Page GetMainPage()
        {
            return page;
        }
        public static Page GetPage1()
        {
            return page1;
        }
        public static Page GetPageWinLevel()
        {
            return page2;
        }
        public new void Run()
        {
            // Do your stuff here
            // Call the base method
            
                base.Run();
          
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
            
            navigationWindow.WindowState = WindowState.Maximized;
            navigationWindow.WindowStyle = WindowStyle.None;
            navigationWindow.ShowsNavigationUI = false;
            navigationWindow.KeyUp += new KeyEventHandler(Core.Events.GetInstance().setKeyUp);
            navigationWindow.KeyDown += new KeyEventHandler(Core.Events.GetInstance().setKeyDown);
            navigationWindow.MouseLeftButtonUp += new MouseButtonEventHandler(Core.Events.GetInstance().SetLeftMouseUp);
            navigationWindow.MouseLeftButtonDown += new MouseButtonEventHandler(Core.Events.GetInstance().SetLeftMouseDown);
            navigationWindow.Closed += new EventHandler(Window_Closing);
            navigationWindow.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
            Core.Core.GetInstance().m_WindowWidth=(int)System.Windows.SystemParameters.PrimaryScreenWidth;
            Core.Core.GetInstance().m_WindowHeight = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
            Kyrsach.Core.Core.GetInstance().Init();
            navigationWindow.Navigate(page);
            navigationWindow.Show();
            
        }


        async private void Window_Closing(object sender, EventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream("SaveGame.json", FileMode.Create))
                {
                    List<Core.Objects.Properties> enemys = new List<Core.Objects.Properties>();
                    for (int i = 0; i < Core.Objects.AllCharacter.GetInstance().GetEnemys().Count; i++)
                    {
                        enemys.Add(Core.Objects.AllCharacter.GetInstance().GetEnemys()[i].GetProperties());
                    }
                    List<Core.Objects.Properties> enemysAttacks = new List<Core.Objects.Properties>();
                    for (int i = 0; i < Core.Objects.AllAttacks.GetInstance().GetEnemys().Count; i++)
                    {
                        enemysAttacks.Add(Core.Objects.AllAttacks.GetInstance().GetEnemys()[i].GetProperties());
                    }
                    List<Core.Objects.Properties> playerAttacks = new List<Core.Objects.Properties>();
                    for (int i = 0; i < Core.Objects.AllAttacks.GetInstance().GetPlayers().Count; i++)
                    {
                        playerAttacks.Add(Core.Objects.AllAttacks.GetInstance().GetPlayers()[i].GetProperties());
                    }
                    SaveGame.SaveGame savegame = new SaveGame.SaveGame(CurrentLevel.GetInstance().GetNumberLevel(), Core.Objects.AllCharacter.GetInstance().GetPlayers()[0].GetProperties(), enemys, enemysAttacks, playerAttacks,Core.Core.GetInstance().GetScore(),Core.Records.GetInstance().GetCompanyRecords(),Core.Records.GetInstance().GetNotEndRecords(),Core.Core.GetInstance().GetWave());
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    await JsonSerializer.SerializeAsync<SaveGame.SaveGame>(fs, savegame);
                }
            }
            catch
            {
                ;
            }
        }

    }
}
