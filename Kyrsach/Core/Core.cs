using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Kyrsach.Core.map;
using System.Windows.Input;
using System.Text.Json;
using Kyrsach.Core.Objects;
using System.Windows.Media.Media3D;
using System.ComponentModel;

namespace Kyrsach.Core
{
   
     
    public class Core
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);
        private static Core s_Instance;
        private bool m_IsRunning=false;
        private Graphics Graphics;
        private Bitmap myBitmap;
        private GameMap m_levelMap;
        private int Score=0;
        private int wave = 0;
        public int m_WindowWidth { get; set; }
        public int m_WindowHeight { get; set; }
        public void AddScore(int add)
        {
            Score += add;
        }
        public GameMap GetMap()
        {
            return m_levelMap;
        }
        public Bitmap GetBitmap()
        {
            return myBitmap;
        }
        public Graphics GetGraphics()
        {
            if(myBitmap==null)
                myBitmap = new Bitmap(3200, 768);
            return Graphics=Graphics!=null?Graphics: Graphics = Graphics.FromImage(myBitmap);

        }
        public void Running()
        {
            m_IsRunning = true;
        }
        private void AddEnemyFromNotEndGame()
        {
            if (AllCharacter.GetInstance().GetEnemys().Count == 0)
            {
                AllCharacter.GetInstance().GetPlayers()[0].SetHealth(100);
                AllCharacter.GetInstance().GetPlayers()[0].SetMana(100);
                var rand = new Random((int)Timer.GetInstance().GetTime());
                wave++;
                for (int i = 0; i < wave && i < 6; i++)
                {
                    int m = rand.Next(4);
                    switch (m)
                    {
                        case 0:
                            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 60 + rand.Next(378 - 60), 60 + rand.Next(640 - 60), 20, 16, 0.5, 1, 60)));
                            break;
                        case 1:
                            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1486 + rand.Next(1780 - 1486), 60 + rand.Next(640 - 60), 20, 16, 0.5, 1, 60)));
                            break;
                        case 2:
                            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 415 + rand.Next(1505 - 415), 60 + rand.Next(165 - 60), 20, 16, 0.5, 1, 60)));
                            break;
                        case 3:
                            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 415 + rand.Next(1505 - 415), 500 + rand.Next(640 - 500), 20, 16, 0.5, 1, 60)));
                            break;
                    }
                    if (i % 2 == 0)
                    {
                        m = rand.Next(4);
                        switch (m)
                        {
                            case 0:
                                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 60 + rand.Next(378 - 60), 60 + rand.Next(640 - 60), 48, 48, 1, 1, 100 + wave)));
                                break;
                            case 1:
                                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 1486 + rand.Next(1780 - 1486), 60 + rand.Next(640 - 60), 48, 48, 1, 1, 100 + wave)));
                                break;
                            case 2:
                                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 415 + rand.Next(1505 - 415), 60 + rand.Next(165 - 60), 48, 48, 1, 1, 100 + wave)));
                                break;
                            case 3:
                                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 415 + rand.Next(1505 - 415), 500 + rand.Next(640 - 500), 48, 48, 1, 1, 100 + wave)));
                                break;
                        }
                    }
                }

            }


                Objects.AllCharacter.GetInstance().Update((float)0.1);
            
        }
        public void SetNoEndGame()
        {
            Score = 0;
            wave = 0;
            Objects.AllCharacter.GetInstance().Clean();
            Objects.AllAttacks.GetInstance().Clean();
            Objects.AllCharacter.GetInstance().AddPlayer(new Objects.Player(new Objects.Properties("player", "Player", 830, 300, 32, 32, 1)));
            Camera.Camera.GetInstance().SetTarget(Objects.AllCharacter.GetInstance().GetPlayers()[0].GetOrigin());
            CurrentLevel.GetInstance().SetNumberLevel(4);
            m_levelMap = MapParser.GetInstance().GetMap(CurrentLevel.GetInstance().GetCurrentLevel());
            m_IsRunning = true;


        }
        public void SetNewGame()
        {
            Score = 0;
            wave = 0;
            Objects.AllCharacter.GetInstance().Clean();
            Objects.AllAttacks.GetInstance().Clean();
            m_IsRunning = true;
            //CurrentLevel.GetInstance().SetNumberLevel(3);
            CurrentLevel.GetInstance().SetFirstLevel();
            m_levelMap = MapParser.GetInstance().GetMap(CurrentLevel.GetInstance().GetCurrentLevel());
            Objects.AllCharacter.GetInstance().AddPlayer(new Objects.Player(new Objects.Properties("player", "Player", 270, 280, 32, 32, 1)));
            Camera.Camera.GetInstance().SetTarget(Objects.AllCharacter.GetInstance().GetPlayers()[0].GetOrigin());
            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 570, 280, 20, 16, 0.5,1,60)));
            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 570, 320, 20, 16, 0.5,1,60)));
             Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 570, 600, 48, 48, 1)));
              Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1370, 170, 20, 16, 0.5,1,60)));
              Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 1170, 300, 48, 48, 1)));
          //    Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy4(new Objects.Properties("mortar", "Enemy4", 1000, 300, 48, 48, 0.5)));

        }
        public int GetWave()
        {
            return wave;
        }
        public void SetWave(int wave)
        {
            this.wave = wave;
        }
        public bool IsRunning()
        {
            return m_IsRunning;
        }
        public static Core GetInstance()
        {
            return s_Instance = (s_Instance != null) ? s_Instance : new Core();
        }
        public int GetScore()
        {
            return Score;
        }
        public void LoseGame()
        {
            Objects.AllCharacter.GetInstance().Clean();
            Objects.AllAttacks.GetInstance().Clean();
            m_IsRunning = false;
            CurrentLevel.GetInstance().SetFirstLevel();
            Objects.AllCharacter.GetInstance().AddPlayer(new Objects.Player(new Objects.Properties("player", "Player", 270, 280, 32, 32, 1)));
            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 570, 280, 20, 16, 0.5)));
            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 570, 320, 20, 16, 0.5)));
            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 570, 600, 48, 48, 1)));
            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1370, 170, 20, 16, 0.5)));
            Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 1170, 300, 48, 48, 1)));
            m_levelMap = MapParser.GetInstance().GetMap(CurrentLevel.GetInstance().GetCurrentLevel());
            Camera.Camera.GetInstance().SetTarget(Objects.AllCharacter.GetInstance().GetPlayers()[0].GetOrigin());
            App.GetNavigationWindow().Navigate(App.GetLosePage());
            Objects.AllCharacter.GetInstance().Update((float)0.1);
        }
        public void NextLevel()
        {
            if (CurrentLevel.GetInstance().NextLevel())
            {
                Objects.AllAttacks.GetInstance().Clean();
                Objects.AllCharacter.GetInstance().Clean();
                m_IsRunning = false;
                m_levelMap = MapParser.GetInstance().GetMap(CurrentLevel.GetInstance().GetCurrentLevel());
                Objects.AllCharacter.GetInstance().AddPlayer(new Objects.Player(new Objects.Properties("player", "Player", 220, 90, 32, 32, 1)));
                Camera.Camera.GetInstance().SetTarget(Objects.AllCharacter.GetInstance().GetPlayers()[0].GetOrigin());
                App.GetNavigationWindow().Navigate(App.GetPageWinLevel());
                Objects.AllCharacter.GetInstance().Update((float)0.1);
                switch(CurrentLevel.GetInstance().GetNumberLevel())
                {
                    case 2:
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel", "Enemy3", 180, 525, 96, 80, 1.5,1,120)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel1", "Enemy3", 705, 100, 96, 80, 1.5,1,120)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel", "Enemy3", 1072, 573, 96, 80, 1.5, 1, 150)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel", "Enemy3", 1430, 325, 96, 80, 1.5, 1, 120)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel1", "Enemy3", 1750, 100, 96, 80, 1.5, 1, 120)));
                        AddScore(1000);

                        break;
                    case 3:
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel", "Enemy3", 835, 625, 96, 80, 1.5, 1, 150)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel1", "Enemy3", 675, 280, 96, 80, 1.5, 1, 150)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel1", "Enemy3", 1335, 80, 96, 80, 1.5, 1, 150)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel1", "Enemy3", 1635, 490, 96, 80, 1.5, 1, 150)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel1", "Enemy3", 1830, 345, 96, 80, 1.5, 1, 150)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel1", "Enemy3", 2120, 80, 96, 80, 1.5, 1, 150)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(new Objects.Properties("turel1", "Enemy3", 2900, 370, 96, 80, 1.5, 1, 150)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy4(new Objects.Properties("mortar", "Enemy4", 3055, 70, 48, 48, 0.7,1,200)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy4(new Objects.Properties("mortar", "Enemy4", 3055, 640, 48, 48, 0.7,1,200)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy4(new Objects.Properties("mortar", "Enemy4", 1550, 80, 48, 48, 0.7,1,200)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy4(new Objects.Properties("mortar", "Enemy4", 1010, 640, 48, 48, 0.7,1,200)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 80, 450, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 250, 540, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 480, 655, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 415, 290, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 800, 290, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 880, 290, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1230, 100, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1008, 220, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1008, 320, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1105, 495, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1300, 495, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1455, 610, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1810, 640, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1840, 465, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1550, 430, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1550, 210, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1710, 80 , 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2030, 290, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2030, 340, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2030, 390, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2080, 320, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2080, 365, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2460, 240, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2520, 240, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2600, 240, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2700, 240, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 3010, 368, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2910, 440, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 2335, 590, 20, 16, 0.5, 1, 60)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 80, 480, 48, 48, 1,1,130)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 65, 670, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 570, 600, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 450, 500, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 880, 500, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 690, 145, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 1170, 305, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 1700, 655, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 1210, 592, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 2080, 240, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 2240, 400, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 2510, 530, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 2800, 530, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 2460, 660, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 2735, 655, 48, 48, 1)));
                        Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 3090, 500, 48, 48, 1)));
                        AddScore(2000);


                        break;
                }
            }
            else
            {
                AddScore(3000);
                Objects.AllCharacter.GetInstance().Clean();
                Objects.AllAttacks.GetInstance().Clean();
                m_IsRunning = false;
                CurrentLevel.GetInstance().SetFirstLevel();
                Objects.AllCharacter.GetInstance().AddPlayer(new Objects.Player(new Objects.Properties("player", "Player", 270, 280, 32, 32, 1)));
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 570, 280, 20, 16, 0.5)));
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 570, 320, 20, 16, 0.5)));
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 570, 600, 48, 48, 1)));
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1370, 170, 20, 16, 0.5)));
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 1170, 300, 48, 48, 1)));
                m_levelMap = MapParser.GetInstance().GetMap(CurrentLevel.GetInstance().GetCurrentLevel());
                Camera.Camera.GetInstance().SetTarget(Objects.AllCharacter.GetInstance().GetPlayers()[0].GetOrigin());
                App.GetNavigationWindow().Navigate(App.GetFinishPage());
                Objects.AllCharacter.GetInstance().Update((float)0.1);
            }
        }
        public bool Init() {
            try
            {
                using (FileStream fs = new FileStream("SaveGame.json", FileMode.Open))
                {
                    Objects.AllCharacter.GetInstance().Clean();
                    SaveGame.SaveGame? savegame = JsonSerializer.Deserialize<SaveGame.SaveGame>(fs);
                    CurrentLevel.GetInstance().SetNumberLevel(savegame.m_NumberLevel);
                    Score = savegame.Score;
                    wave = savegame.Wave;
                    if(savegame.NotEndRecords!=null)
                    Records.GetInstance().SetNotEndRecords(savegame.NotEndRecords);
                    if(savegame.CompanyRecords!=null)
                    Records.GetInstance().SetCompanyRecords(savegame.CompanyRecords);
                    Objects.AllCharacter.GetInstance().AddPlayer(new Objects.Player(savegame.player));
                    MapParser.GetInstance().Load("level3", "level3.tmx");
                    MapParser.GetInstance().Load("level2", "level2.tmx");
                    MapParser.GetInstance().Load("level1", "level1.tmx");
                    MapParser.GetInstance().Load("level4", "level4.tmx");
                    Camera.Camera.GetInstance().SetTarget(Objects.AllCharacter.GetInstance().GetPlayers()[0].GetOrigin());
                    Camera.Camera.GetInstance().Update((float)0.1, m_WindowWidth, m_WindowHeight);
                    for (int i = 0; i<savegame.enemys.Count;i++)
                    {
                        switch (savegame.enemys[i].Name)
                        {
                            case "Enemy1":
                                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(savegame.enemys[i]));
                                break;
                            case "Enemy2":
                                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(savegame.enemys[i]));
                                break;
                            case "Enemy3":
                                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy3(savegame.enemys[i]));
                                break;
                            case "Enemy4":
                                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy4(savegame.enemys[i]));
                                break;
                        }
                    }
                    for(int i =0; i<savegame.enemysAttacks.Count;i++)
                    {
                        switch (savegame.enemysAttacks[i].Name)
                        {
                            case "RengaAttack":
                                Objects.AllAttacks.GetInstance().AddEnemyAttack(new Objects.RengaAttack(savegame.enemysAttacks[i], AllCharacter.GetInstance().GetPlayers()));
                                break;
                            case "ExplosiveAttack":
                                Objects.AllAttacks.GetInstance().AddEnemyAttack(new Objects.ExplosiveAttack(savegame.enemysAttacks[i], AllCharacter.GetInstance().GetPlayers()));
                                break;
                            case "MortarAtack":
                                Objects.AllAttacks.GetInstance().AddEnemyAttack(new Objects.MortarAtack(savegame.enemysAttacks[i], AllCharacter.GetInstance().GetPlayers()));
                                break;
                        }
                    }
                    for (int i = 0; i < savegame.playerAttacks.Count; i++)
                    {
                        switch (savegame.playerAttacks[i].Name)
                        {
                            case "RengaAttack":
                                Objects.AllAttacks.GetInstance().AddPlayerAttack(new Objects.RengaAttack(savegame.playerAttacks[i], AllCharacter.GetInstance().GetEnemys()));
                                break;
                            case "ExplosiveAttack":
                                Objects.AllAttacks.GetInstance().AddPlayerAttack(new Objects.ExplosiveAttack(savegame.playerAttacks[i], AllCharacter.GetInstance().GetEnemys()));
                                break;
                        }
                    }
                }
            }
            catch
            {
                Objects.AllCharacter.GetInstance().Clean();
                Objects.AllAttacks.GetInstance().Clean();
                CurrentLevel.GetInstance().SetFirstLevel();
                Objects.AllCharacter.GetInstance().AddPlayer(new Objects.Player(new Objects.Properties("player", "Player", 270, 280, 32, 32, 1)));
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 570, 280, 20, 16, 0.5)));
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 570, 320, 20, 16, 0.5)));
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 570, 600, 48, 48, 1)));
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy1(new Objects.Properties("enemy1", "Enemy1", 1370, 170, 20, 16, 0.5)));
                MapParser.GetInstance().Load("level3", "level3.tmx");
                MapParser.GetInstance().Load("level2", "level2.tmx");
                MapParser.GetInstance().Load("level1", "level1.tmx");
                MapParser.GetInstance().Load("level4", "level4.tmx");
                Objects.AllCharacter.GetInstance().AddEnemy(new Objects.Enemy2(new Objects.Properties("enemy2", "Enemy2", 1170, 300, 48, 48, 1)));
            }
            Camera.Camera.GetInstance().SetTarget(Objects.AllCharacter.GetInstance().GetPlayers()[0].GetOrigin());
            m_levelMap = MapParser.GetInstance().GetMap(CurrentLevel.GetInstance().GetCurrentLevel());
            myBitmap = new Bitmap(3200, 768);
            Graphics = Graphics.FromImage(myBitmap);
            Timer.GetInstance().Init();//для первого уровня стартовые 270 280, второй 100 100, третий 100 100
            TextureManager.GetInstance().Load("player", "../../..\\assets\\enemy1.png");
            TextureManager.GetInstance().Load("enemy1", "../../..\\assets\\enemy2.png");
            TextureManager.GetInstance().Load("enemy2", "../../..\\assets\\enemy3.png");
            TextureManager.GetInstance().Load("level4", "../../..\\assets\\map\\level4.png");
            TextureManager.GetInstance().Load("level3", "../../..\\assets\\map\\level3.png");
            TextureManager.GetInstance().Load("level1", "../../..\\assets\\map\\level1.png");
            TextureManager.GetInstance().Load("weapon1", "../../..\\assets\\weapon1.png");
            TextureManager.GetInstance().Load("weapon2", "../../..\\assets\\weapon2.png");
            TextureManager.GetInstance().Load("weapon3", "../../..\\assets\\weapon3.png");
            TextureManager.GetInstance().Load("rock1", "../../..\\assets\\Rock1.png");
            TextureManager.GetInstance().Load("rock2", "../../..\\assets\\Rock2.png");
            TextureManager.GetInstance().Load("rock2ToWall", "../../..\\assets\\Rock2ToWall.png");
            TextureManager.GetInstance().Load("rock3", "../../..\\assets\\Rock3.png");
            TextureManager.GetInstance().Load("turel", "../../..\\assets\\turel.png");
            TextureManager.GetInstance().Load("turel1", "../../..\\assets\\turel1.png");
            TextureManager.GetInstance().Load("mortar", "../../..\\assets\\mortar.png");
            TextureManager.GetInstance().Load("mortarAtack", "../../..\\assets\\mortarAtack.png");
            TextureManager.GetInstance().Load("mortarGoal", "../../..\\assets\\mortarGoal.png");
            TextureManager.GetInstance().Load("rock3bach", "../../..\\assets\\Rock3Bach.png");
            TextureManager.GetInstance().Load("ikon", "../../..\\assets\\ikon.png");
            TextureManager.GetInstance().Load("health", "../../..\\assets\\health.png");
            TextureManager.GetInstance().Load("mana", "../../..\\assets\\mana.png");
            TextureManager.GetInstance().Load("level2", "../../..\\assets\\map\\level2.png");

            return m_IsRunning=true; 
        }
        public bool Clean()
        {
            TextureManager.GetInstance().Clean();
            Timer.GetInstance().Quit();
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            return true;
        }

        public void Quit()
        {
            m_IsRunning = false;
            App.GetNavigationWindow().Navigate(App.GetMainPage());
        }

        public void Update()
        {
            m_levelMap.Update();
            float dt = Timer.GetInstance().GetDeltaTime();
            Objects.AllCharacter.GetInstance().Update(dt);
            Objects.AllAttacks.GetInstance().Update(dt);
            Camera.Camera.GetInstance().Update(dt, m_WindowWidth, m_WindowHeight);
            if (CurrentLevel.GetInstance().GetNumberLevel() == 4)
                AddEnemyFromNotEndGame();
        }

        public ImageSource Render()
        {
            TextureManager.GetInstance().DrawMap(CurrentLevel.GetInstance().GetCurrentLevel(), 0, 0, 1366, 768);
            //m_levelMap.Render();
            //Сохранение карты!!! убери коментарий поставь точку останова измени текстур менеджер и дроу map
            //Проверь слои отображения
            //myBitmap.Save("C:\\Users\\user\\source\\repos\\Kyrsach\\Kyrsach\\assets\\map\\level4.png");
            Objects.AllAttacks.GetInstance().Draw();
            Objects.AllCharacter.GetInstance().Draw();
            GetGraphics().DrawString(Score.ToString(), new Font("Tahoma", 25), System.Drawing.Brushes.Red, m_WindowWidth/2-30, 10);
            var handle = myBitmap.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally {
                DeleteObject(handle);
            }

        }
        
        public void Events()
        {
            if(Kyrsach.Core.Events.GetInstance().GetStateKey(Key.Escape))
            {
                Quit();
            }
        }
    }
}
