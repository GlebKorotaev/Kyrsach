using Kyrsach.Core.map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kyrsach.Core.Objects
{
    public class Enemy3:GameObject
    {
        public Enemy3(Properties props) : base(props)
        {

            m_Animation = new Animation();
            m_Collider = new Collider();
            m_Collider.SetBuffer(0, 0, 0, 0);
            m_Count = 0;
            m_CountAnimation = new CountAnimation();
            m_Collider.Set((int)m_Transform.X,(int) m_Transform.Y, (int)(m_Width / m_Compression), (int)(m_Heigth / m_Compression));
        }

        private int m_Count;
        private float m_Couldown = 70;
        private Animation m_Animation;
        private CountAnimation m_CountAnimation;

        public override void Draw()
        {

            if (m_CountAnimation.Playing())
            {
                m_CountAnimation.Draw((int)m_Transform.X, (int)m_Transform.Y, m_Width, m_Heigth, m_Compression);
            }
            else
            {
                m_Animation.Draw((int)m_Transform.X, (int)m_Transform.Y, m_Width, m_Heigth, m_Compression);
            }

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
        Rect rect = new Rect(1, 1, 5, 5);
        Vector2 player = new Vector2();
        Rectangle p = new Rectangle(1, 1, 1, 1);
        public override bool Update(float dt)
        {

            player.X = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X - m_Transform.X);
            player.Y = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y - m_Transform.Y);
            if (player.Length() <= 500 && CurrentLevel.GetInstance().GetNumberLevel() != 2)
            {
                player = Vector2.Normalize(player);
                rect.X = (m_Transform.X);
                rect.Y = m_Transform.Y;
                while (Math.Abs((int)rect.X - (int)AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X) > 2 || Math.Abs((int)rect.Y - (int)AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y) > 2)
                {
                    rect.X += player.X;
                    rect.Y += player.Y;
                    p.X = (int)rect.X;
                    p.Y = (int)rect.Y;
                    if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(p, 0))
                    {
                        break;
                    }
                }
                if (Math.Abs(rect.X - AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X) < 15 && Math.Abs(rect.Y - AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y) < 15)
                {
                    Target_Player = true;
                    WasTarget_Player = true;
                }
                else Target_Player = false;
                player.X = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X - m_Transform.X);
                player.Y = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y - m_Transform.Y);
            }
            else if (CurrentLevel.GetInstance().GetNumberLevel() == 2)
            {
                Target_Player = true;
                WasTarget_Player = true;
                if (player.Length() > 500)
                    Target_Player = false;
            }
            else if (player.Length() > 500)
                Target_Player = false;
            if (Target_Player == false)
            {
                m_Animation.SetProps(m_TextureID, 0, 1, 100);
                m_Couldown -= Timer.GetInstance().GetDeltaTime();
            }
            else
            {
                m_Animation.SetProps(m_TextureID, 0, 1, 100);
                if (m_Couldown <= 0)
                {
                    m_CountAnimation.SetProps(m_TextureID, 0, 2, 13);
                    Vector2 cam = Camera.Camera.GetInstance().GetPosition();
                    AllAttacks.GetInstance().AddEnemyAttack(new RengaAttack(new Properties("rock2", "RengaAttack", (float)m_Origin.X, (float)m_Origin.Y, 16, 16, 1, 1, 20, 20, false, false, new System.Windows.Point(AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X + 25 - cam.X, AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y - cam.Y + 15), false, 750), AllCharacter.GetInstance().GetPlayers()));
                    AllAttacks.GetInstance().AddEnemyAttack(new RengaAttack(new Properties("rock2", "RengaAttack", (float)m_Origin.X, (float)m_Origin.Y, 16, 16, 1, 1, 20, 20, false, false, new System.Windows.Point(AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X - 25 - cam.X, AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y - cam.Y - 15), false, 750), AllCharacter.GetInstance().GetPlayers()));
                    AllAttacks.GetInstance().AddEnemyAttack(new RengaAttack(new Properties("rock2", "RengaAttack", (float)m_Origin.X, (float)m_Origin.Y, 16, 16, 1, 1, 20, 20, false, false, new System.Windows.Point(AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X - cam.X, AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y - cam.Y), false, 750), AllCharacter.GetInstance().GetPlayers()));

                    m_Couldown = 80;
                }
                else m_Couldown -= Timer.GetInstance().GetDeltaTime();
            }
            m_CountAnimation.Update();
            m_Animation.Update();
            if (health < 0)
            {
                Core.GetInstance().AddScore(250);
                AllAttacks.GetInstance().AddEnemyAttack(new ExplosiveAttack(new Properties("rock3", "ExplosiveAttack", m_Transform.X, m_Transform.Y, 16, 16, 1, 1, 40, 20, false, false, new System.Windows.Point(m_Transform.X, m_Transform.Y), true, 300), AllCharacter.GetInstance().GetPlayers()));
                return false;
            }
            else
                return true;

        }
        
    }

}
