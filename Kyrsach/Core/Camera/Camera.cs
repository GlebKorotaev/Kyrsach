using System;
using System.Collections.Generic;

using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Kyrsach.Core.Camera
{
    public class Camera
    {

        public void Update(float dt, int w, int h)
        {
            m_ViewBox.Width = w;
            m_ViewBox.Height = h;
            
            if (m_Target != null)
            {
                m_ViewBox.X = m_Target.X - w / 2;
                m_ViewBox.Y = m_Target.Y - h / 2;//наш таргет point camera перемещается согласно всех условий
                
                if (m_ViewBox.X < 0)
                {
                    m_ViewBox.X = 0;
                }
                if (m_ViewBox.Y < 0)
                {
                    m_ViewBox.Y = 0;
                }

                if (m_ViewBox.X > (map.CurrentLevel.GetInstance().GetLevel().colcount * map.CurrentLevel.GetInstance().GetLevel().tilesize - m_ViewBox.Width))
                {
                    m_ViewBox.X = map.CurrentLevel.GetInstance().GetLevel().colcount * map.CurrentLevel.GetInstance().GetLevel().tilesize - m_ViewBox.Width;
                }
                if (m_ViewBox.Y > (map.CurrentLevel.GetInstance().GetLevel().rowcount * map.CurrentLevel.GetInstance().GetLevel().tilesize - m_ViewBox.Height))
                {
                    m_ViewBox.Y = map.CurrentLevel.GetInstance().GetLevel().rowcount * map.CurrentLevel.GetInstance().GetLevel().tilesize - m_ViewBox.Height;

                }
                    m_Position.X = (float)m_ViewBox.X;
                    m_Position.Y = (float)m_ViewBox.Y;
               
            }
        }
        public  static Camera GetInstance() { return s_Instance = (s_Instance != null ? s_Instance : new Camera()); }
        public Rect GetViewBox() { return m_ViewBox; }
        public Vector2 GetPosition() { return m_Position; }
        public void SetTarget(Point target) { m_Target = target; }
	    private static Camera s_Instance;
        private Camera() { m_ViewBox =new Rect( 0,0,Core.GetInstance().m_WindowWidth,Core.GetInstance().m_WindowHeight); }
        private Point m_Target;
        private Vector2 m_Position = new Vector2();
        private Rect m_ViewBox;
    }
}
