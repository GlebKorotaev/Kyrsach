using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Core.map
{
    public struct Level
    {
        public int rowcount, colcount, tilesize;
    }
    public class CurrentLevel
    {
        public static CurrentLevel GetInstance() { return s_Instance = (s_Instance != null ? s_Instance : new CurrentLevel()); }
        private static CurrentLevel s_Instance;
        private CurrentLevel() { }
        private Dictionary<string, Level> m_Levels = new Dictionary<string, Level>();
        private int m_NumberLevel;
        public void SetFirstLevel()
        {
            m_NumberLevel = 1;
        }
        public bool NextLevel()
        {
            if (m_NumberLevel != 3)
            {    m_NumberLevel++;
                return true;
            } else
                return false;
        }
        public void AddLevel(string name, Level level)
        {
            m_Levels[name] = level;
        }
        public Level GetLevel()
        {
            return m_Levels["level"+m_NumberLevel];
        }
        public string GetCurrentLevel()
        {
            return "level" + m_NumberLevel;
        }
        public void SetNumberLevel(int level )
        {
            m_NumberLevel = level;
        }
        public int GetNumberLevel()
        {
            return m_NumberLevel;
        }
        

    }
}
