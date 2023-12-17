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
    public class Enemy1 : GameObject
    {
        public Enemy1(Properties props) : base(props)
        {
            m_Rigidbody = new Rigidbody();
            m_Animation = new Animation();
            m_Collider = new Collider();
            m_Collider.SetBuffer(0, 0, 0, 0);
            m_Count = 0;
            m_StartPosition = new System.Windows.Point(m_Transform.X,m_Transform.Y);
        }
        private System.Windows.Point m_StartPosition;
        private int m_Count;
        private Animation m_Animation;
        private Rigidbody m_Rigidbody;
        //private int type_of_weapons=1;//1 - кулаки 2 - камень 3 - файер бол
        private Vector2 m_LastSafePosition;
        public override void Draw()
        {
            
            m_Animation.Draw((int)m_Transform.X, (int)m_Transform.Y, m_Width, m_Heigth, m_Compression);

            //DrawCollision();
        }
        public void DrawCollision()
        {
            Rectangle box = m_Collider.Get();
            Vector2 cam = Camera.Camera.GetInstance().GetPosition();
            box.X -= (int)cam.X;
            box.Y -= (int)cam.Y;
            Core.GetInstance().GetGraphics().DrawRectangle(new Pen(Color.Orange, 3), box);
        }
        Rect rect = new Rect(1,1,5,5);
        Vector2 player = new Vector2();
        Rectangle p = new Rectangle(1,1,1,1);
        public override bool Update(float dt)
        {
            
                player.X = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X-m_Transform.X);
                player.Y = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y- m_Transform.Y);
            if (player.Length() <= 600 && CurrentLevel.GetInstance().GetNumberLevel() != 2)
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
                else {
                    Target_Player = false;
                }
                player.X = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X - m_Transform.X);
                player.Y = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y - m_Transform.Y);
            }
            else if (CurrentLevel.GetInstance().GetNumberLevel() == 2)
            {
                Target_Player = true;
                WasTarget_Player = true;
                if (player.Length() > 600)
                    Target_Player = false;
            }
                if (Target_Player == false)
                { 
                Vector2 m_LengthToStartPosition = new Vector2((float)(-m_Transform.X + m_StartPosition.X), (float)(-m_Transform.Y + m_StartPosition.Y));
                if (m_LengthToStartPosition.Length() > 200&&!WasTarget_Player)
                {
                    m_LengthToStartPosition = Vector2.Normalize(m_LengthToStartPosition);
                    m_LengthToStartPosition *= 2;
                    m_Rigidbody.ApplyForceX(m_LengthToStartPosition.X);
                    m_Rigidbody.ApplyForceY(m_LengthToStartPosition.Y);
                }
                else
                if (m_Count <= 0)
                {
                    m_Rigidbody.UnSetForse();
                    var rand = new Random((int)Timer.GetInstance().GetTime());
                    m_Count = (1 + rand.Next(3)) * 10;
                    m_Animation.SetProps(m_TextureID, 0, 4, 100);
                    for (int i = 0; i < 2; i++)
                    {
                        int direct = rand.Next(5);
                        switch (direct)//1 лево 2 право 3 вверх 4 вниз
                        {
                            case 1:
                                m_Rigidbody.ApplyForceX(2 * Rigidbody.BACKWARD);
                                m_Animation.SetProps(m_TextureID, 2, 4, 100);
                                break;
                            case 2:
                                m_Rigidbody.ApplyForceX(2 * Rigidbody.FORWARD);
                                m_Animation.SetProps(m_TextureID, 3, 4, 100);
                                break;
                            case 3:
                                m_Rigidbody.ApplyForceY(2 * Rigidbody.UPWARD);
                                m_Animation.SetProps(m_TextureID, 1, 4, 100);
                                break;
                            case 4:
                                m_Rigidbody.ApplyForceY(2 * Rigidbody.DOWNWARD);
                                m_Animation.SetProps(m_TextureID, 0, 4, 100);
                                break;
                        }
                    }
                }
                    m_Rigidbody.Update(dt);
                    m_LastSafePosition.X = m_Transform.X;
                    m_Transform.X += m_Rigidbody.Position().X;
                    m_Collider.Set((int)m_Transform.X, (int)m_Transform.Y, (int)(m_Width / m_Compression), (int)(m_Heigth / m_Compression));
                    if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 0))
                    {
                        m_Transform.X = m_LastSafePosition.X;
                        m_Count = 0;
                    }
                for (int i = 0; i < AllCharacter.GetInstance().GetEnemys().Count; i++)
                {
                    if (CollisionHandler.CollisionHandler.GetInstance().CheckCollision(m_Collider.Get(), AllCharacter.GetInstance().GetEnemys()[i].GetCollider().Get()) && m_Transform.X != AllCharacter.GetInstance().GetEnemys()[i].GetTransform().X)
                    {
                        m_Transform.X = m_LastSafePosition.X;
                        m_Count = 0;
                    }
                }
                m_LastSafePosition.Y = m_Transform.Y;
                    m_Transform.Y += m_Rigidbody.Position().Y;
                    m_Collider.Set((int)m_Transform.X, (int)m_Transform.Y, (int)(m_Width / m_Compression), (int)(m_Heigth / m_Compression));
                    if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 0))
                    {
                        m_Transform.Y = m_LastSafePosition.Y;
                        m_Count = 0;
                    }
                for (int i = 0; i < AllCharacter.GetInstance().GetEnemys().Count; i++)
                {
                    if (CollisionHandler.CollisionHandler.GetInstance().CheckCollision(m_Collider.Get(), AllCharacter.GetInstance().GetEnemys()[i].GetCollider().Get()) && m_Transform.X != AllCharacter.GetInstance().GetEnemys()[i].GetTransform().X)
                    {
                        m_Transform.Y = m_LastSafePosition.Y;
                        m_Count = 0;
                    }
                }
                m_Animation.Update();
                    m_Count--;
                } else
            if(Target_Player==true)
            {
                player.X = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().X - m_Transform.X);
                player.Y = (AllCharacter.GetInstance().GetPlayers()[0].GetTransform().Y - m_Transform.Y);
                player = Vector2.Normalize(player);
                player *= (float)3.5;
                m_Rigidbody.UnSetForse();
                m_Rigidbody.ApplyForceX(player.X);
                m_Rigidbody.ApplyForceY(player.Y);
                m_Rigidbody.Update(dt);
                m_LastSafePosition.X = m_Transform.X;
                m_Transform.X += m_Rigidbody.Position().X;
                m_Collider.Set((int)m_Transform.X, (int)m_Transform.Y, (int)(m_Width / m_Compression), (int)(m_Heigth / m_Compression));
                if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 0))
                {
                    m_Transform.X = m_LastSafePosition.X;
                }
                m_LastSafePosition.Y = m_Transform.Y;
                m_Transform.Y += m_Rigidbody.Position().Y;
                m_Collider.Set((int)m_Transform.X, (int)m_Transform.Y, (int)(m_Width / m_Compression), (int)(m_Heigth / m_Compression));
                if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 0))
                {
                    m_Transform.Y = m_LastSafePosition.Y;
                }
                if(player.X>=0&& player.X >= player.Y)
                {
                    m_Animation.SetProps(m_TextureID, 3, 4, 100);
                } else
                if(player.X < 0&& Math.Abs(player.X)>=Math.Abs(player.Y))
                {
                    m_Animation.SetProps(m_TextureID, 2, 4, 100);
                }
                else if (player.Y >= 0&&player.Y>=player.X)
                {
                    m_Animation.SetProps(m_TextureID, 0, 4, 100);
                }
                else if (player.Y < 0 && Math.Abs(player.Y) >= Math.Abs(player.X))
                {
                    m_Animation.SetProps(m_TextureID, 1, 4, 100);
                }
                m_Animation.Update();
            }
            if (CollisionHandler.CollisionHandler.GetInstance().CheckCollision(m_Collider.Get(), AllCharacter.GetInstance().GetPlayers()[0].GetCollider().Get()))
            {
                AllAttacks.GetInstance().AddEnemyAttack(new ExplosiveAttack(new Properties("rock3", "ExplosiveAttack", m_Transform.X+15*player.X, m_Transform.Y+15*player.Y, 16, 16, 1, 1, 80,20,false,false, new System.Windows.Point(m_Transform.X,m_Transform.Y),true,300), AllCharacter.GetInstance().GetPlayers()));
                return false;
            }
            if (health < 0)
            {
                Core.GetInstance().AddScore(100);
                AllAttacks.GetInstance().AddEnemyAttack(new ExplosiveAttack(new Properties("rock3", "ExplosiveAttack", m_Transform.X, m_Transform.Y, 16, 16, 1, 1, 40,20,false,false, new System.Windows.Point(m_Transform.X, m_Transform.Y),true,300), AllCharacter.GetInstance().GetPlayers()));
                return false;
            }
            else
                return true;
        }
    }
}
