using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Core.Objects
{
    public class MortarAtack:GameObject
    {
        public MortarAtack(Properties props, List<GameObject> enemys) : base(props)
        {
            Vector2 cam = Camera.Camera.GetInstance().GetPosition();
            m_Rigidbody = new Rigidbody();
            m_Collider = new Collider();
            m_CountAnimation = new CountAnimation();
            m_Collider.SetBuffer(0, 0, 0, 0);
            m_StartPosition = new System.Windows.Point(m_Transform.X, m_Transform.Y);
            direction = new Vector2((float)(point.X - m_Origin.X/2), (float)(point.Y - m_Origin.Y));
            direction = Vector2.Normalize(direction);
            direction *= 10;
            m_Rigidbody.ApplyForceX(direction.X);
            m_Rigidbody.ApplyForceY(direction.Y);
            m_IsDied = false;
            m_Range = range;
            this.enemys = enemys;
        }
        private System.Windows.Point m_StartPosition;
        private Vector2 direction;
        private int m_Range;
        private float m_Count=80;
        private Rigidbody m_Rigidbody;
        private CountAnimation m_CountAnimation;
        private List<GameObject> enemys;

        public override bool Update(float dt)
        {
            if (m_IsDied)
            {
                m_CountAnimation.Update();
                if (m_CountAnimation.Playing())
                {
                    if (m_CountAnimation.GetCurrentFrame() == 4)
                    {
                        int prop;
                        for (int i = 0; i < enemys.Count; i++)
                        {
                            if (CollisionHandler.CollisionHandler.GetInstance().CheckCollision(m_Collider.Get(), enemys[i].GetCollider().Get()))
                            {
                                prop = enemys[i].GetHealth();
                                prop -= health / 2;
                                enemys[i].SetHealth(prop);
                            }
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
                if (m_Count > 0)
                {
                    m_TextureID = "mortarGoal";
                    m_Transform.X = (float)point.X-20;
                    m_Transform.Y = (float)point.Y-20;
                    m_Count -= Timer.GetInstance().GetDeltaTime();
                    
                    
                    
                }
            else
            {
                m_Transform.X = (float)point.X - 20;
                m_Transform.Y = (float)point.Y - 20;
                m_CountAnimation.SetProps("rock3bach", 0, 7, 50);
                m_IsDied = true;
                m_Collider.Set((int)m_Transform.X, (int)m_Transform.Y, m_Width, m_Heigth);
            }

            return true;
        }




        public override void Draw()
        {

            if (m_CountAnimation.Playing())
                m_CountAnimation.Draw((int)m_Transform.X, (int)m_Transform.Y, 80, 80, m_Compression);
            else
                //TextureManager.GetInstance().Draw(m_TextureID, (int)m_Transform.X, (int)m_Transform.Y, m_Width, m_Heigth,(float)m_Compression);
                TextureManager.GetInstance().DrawFrame(m_TextureID, (int)m_Transform.X, (int)m_Transform.Y, m_Width, m_Heigth, m_Compression, 0, 0);
           // DrawCollision();
        }
        public void DrawCollision()
        {
            Rectangle box = m_Collider.Get();
            Vector2 cam = Camera.Camera.GetInstance().GetPosition();
            box.X -= (int)cam.X;
            box.Y -= (int)cam.Y;
            Core.GetInstance().GetGraphics().DrawRectangle(new Pen(Color.Orange, 3), box);
        }
    }



}
