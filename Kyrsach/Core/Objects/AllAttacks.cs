using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Core.Objects
{
    public class AllAttacks
    {
        private static AllAttacks s_Instance;
        public static AllAttacks GetInstance()
        {
            return s_Instance = (s_Instance != null) ? s_Instance : new AllAttacks();
        }
        private AllAttacks() { }
        private List<GameObject> m_PlayerAttacks = new List<GameObject>();
        private List<GameObject> m_EnemysAttacks = new List<GameObject>();
        public List<GameObject> GetPlayers()
        {
            return m_PlayerAttacks;
        }
        public List<GameObject> GetEnemys()
        {
            return m_EnemysAttacks;
        }

        public void AddPlayerAttack(GameObject attack)
        {
            m_PlayerAttacks.Add(attack);
        }
        public void AddEnemyAttack(GameObject attack)
        {
            m_EnemysAttacks.Add(attack);
        }

        public void Update(float dt)
        {
            for(int i=0; i<m_PlayerAttacks.Count; i++)
            {
                if (!m_PlayerAttacks[i].Update(dt))
                {
                    m_PlayerAttacks.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < m_EnemysAttacks.Count; i++)
            {
                if (!m_EnemysAttacks[i].Update(dt))
                {
                    m_EnemysAttacks.RemoveAt(i);
                    i--;
                }
            }
        }
        public void Draw()
        {
            for (int i = 0; i < m_PlayerAttacks.Count; i++)
            {
                m_PlayerAttacks[i].Draw();
            }
            for (int i = 0; i < m_EnemysAttacks.Count; i++)
            {
                m_EnemysAttacks[i].Draw();
            }
        }
        public void Clean()
        {
            m_EnemysAttacks.Clear();
            m_PlayerAttacks.Clear();
        }



    }
}
