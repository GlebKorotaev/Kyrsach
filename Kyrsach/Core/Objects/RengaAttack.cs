using Kyrsach.Core.map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Kyrsach.Core.Objects
{
    public class RengaAttack : GameObject
    {
        public RengaAttack(Properties props, List<GameObject> enemys) : base(props)
        {
            Vector2 cam = Camera.Camera.GetInstance().GetPosition();
            m_Rigidbody = new Rigidbody();
            m_Collider = new Collider();
            m_CountAnimation = new CountAnimation();
            m_Collider.SetBuffer(0, 0, 0, 0);
            m_StartPosition = new System.Windows.Point(m_Transform.X, m_Transform.Y);
            direction = new Vector2((float)(point.X - m_Origin.X+cam.X), (float)(point.Y - m_Origin.Y+cam.Y));
            direction = Vector2.Normalize(direction);
            m_Rigidbody.ApplyForceX(direction.X);
            m_Rigidbody.ApplyForceY(direction.Y);
            m_IsDied = false;
            m_Range = range;
            this.enemys = enemys;
        }
        private System.Windows.Point m_StartPosition;
        private Vector2 direction;
        private bool m_IsDied;
        private int m_Range;
        private Rigidbody m_Rigidbody;
        private CountAnimation m_CountAnimation;
        private List<GameObject> enemys;
        public override bool Update(float dt)
        {
            if (m_IsDied)
            {
                m_CountAnimation.Update();
                if (m_CountAnimation.Playing())
                    return true;
                else
                    return false;
            }
                else
            {
                for (int i = 0; i < 10&& !m_IsDied; i++)
                {
                    m_Rigidbody.Update(dt);

                    m_Transform.X += m_Rigidbody.Position().X;
                    m_Collider.Set((int)m_Transform.X, (int)m_Transform.Y, (int)(m_Width / m_Compression), (int)(m_Heigth / m_Compression));
                    if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 0) && CurrentLevel.GetInstance().GetNumberLevel() != 2)
                    {
                        m_CountAnimation.SetProps("rock2ToWall", 0, 5, 75);
                        m_IsDied = true;
                        break;
                    }

                    m_Transform.Y += m_Rigidbody.Position().Y;
                    m_Collider.Set((int)m_Transform.X, (int)m_Transform.Y, (int)(m_Width / m_Compression), (int)(m_Heigth / m_Compression));
                    if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 0)&&CurrentLevel.GetInstance().GetNumberLevel()!=2)
                    {
                        m_CountAnimation.SetProps("rock2ToWall", 0, 5, 75);
                        m_IsDied = true;
                        break;
                    }


                    //дальность полета ограниченная растоянием
                    direction.X = (float)(m_Transform.X - m_StartPosition.X);
                    direction.Y = (float)(m_Transform.Y - m_StartPosition.Y);
                    if(direction.Length()> m_Range)
                    {
                        m_CountAnimation.SetProps("rock2ToWall", 0, 5, 75);
                        m_IsDied = true;
                        break;
                    }
                    //чтоб колизии не бухтели и за карту не вылетало
                    if (m_Transform.Y > CurrentLevel.GetInstance().GetLevel().tilesize*(CurrentLevel.GetInstance().GetLevel().rowcount-1))
                    {
                        //m_CountAnimation.SetProps("rock2ToWall", 0, 5, 75);
                        m_IsDied = true;
                        break;
                    }
                    if (m_Transform.X > CurrentLevel.GetInstance().GetLevel().tilesize * (CurrentLevel.GetInstance().GetLevel().colcount-1))
                    {
                        //m_CountAnimation.SetProps("rock2ToWall", 0, 5, 75);
                        m_IsDied = true;
                        break;
                    }
                    /*
                    //дальность полета ограниченная курсором в момент выстрела
                    if ( (m_Transform.X-target.X) > -15 && (m_Transform.X - target.X) < 15 && (m_Transform.Y-target.Y) < 15 && (m_Transform.Y - target.Y) > -15 )
                    {
                        m_CountAnimation.SetProps("rock2ToWall", 0, 5, 75);
                        m_IsDied = true;
                        break;
                    }
                    */



                    //нанесение урона
                    int prop;
                    for (int j = 0; j < enemys.Count; j++)
                    {
                        if (CollisionHandler.CollisionHandler.GetInstance().CheckCollision(m_Collider.Get(), enemys[j].GetCollider().Get()))
                        {
                            prop = enemys[j].GetHealth();
                            prop -= health;
                            enemys[j].SetHealth(prop);
                            m_CountAnimation.SetProps("rock2ToWall", 0, 5, 75);
                            m_IsDied = true;
                            break;
                        }
                    }


                }
                m_Origin.X = m_Transform.X + (m_Width / 2) / m_Compression;
                m_Origin.Y = m_Transform.Y + (m_Heigth / 2) / m_Compression;
                return true;
            }

        }
        public override void Draw()
        {
           
            if (m_CountAnimation.Playing())
                m_CountAnimation.Draw((int)m_Transform.X-8, (int)m_Transform.Y-8, 32, 32, m_Compression);
            else
                //TextureManager.GetInstance().Draw(m_TextureID, (int)m_Transform.X, (int)m_Transform.Y, m_Width, m_Heigth,(float)m_Compression);
                TextureManager.GetInstance().DrawFrame(m_TextureID,(int) m_Transform.X, (int)m_Transform.Y, m_Width, m_Heigth,m_Compression,0,0);
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
