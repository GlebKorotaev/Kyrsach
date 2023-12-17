using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Core.Objects
{
    public class AllCharacter
    {
        private static AllCharacter s_Instance;
        public static AllCharacter GetInstance()
        {
            return s_Instance = (s_Instance != null) ? s_Instance : new AllCharacter();
        }
        private AllCharacter() { }
        private List<GameObject> m_Players = new List<GameObject>();
        private List<GameObject> m_Enemys = new List<GameObject>();

        public List<GameObject> GetPlayers()
        {
            return m_Players;
        }
        public List<GameObject> GetEnemys()
        {
            return m_Enemys;
        }

        public void AddPlayer(GameObject attack)
        {
            m_Players.Add(attack);
        }
        public void AddEnemy(GameObject attack)
        {
            m_Enemys.Add(attack);
        }
        public void Clean()
        {
            m_Enemys.Clear();
            m_Players.Clear();
        }
        public void Update(float dt)
        {
            for (int i = 0; i < m_Players.Count; i++)
            {
                if (!m_Players[i].Update(dt))
                {
                    m_Players.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < m_Enemys.Count; i++)
            {
                if (!m_Enemys[i].Update(dt))
                {
                    m_Enemys.RemoveAt(i);
                    i--;
                }
            }
        }
        public void Draw()
        {
            for (int i = 0; i < m_Players.Count; i++)
            {
                m_Players[i].Draw();
            }
            for (int i = 0; i < m_Enemys.Count; i++)
            {
                m_Enemys[i].Draw();
            }
        }



    }
}
