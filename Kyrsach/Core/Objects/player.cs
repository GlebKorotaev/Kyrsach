using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kyrsach.Core.Objects
{
    public class Player : GameObject
    {
        public Player(Properties props) : base(props)
        {
            m_Rigidbody = new Rigidbody();
            m_Animation = new Animation();
            m_Collider = new Collider();
            m_CountAnimation = new CountAnimation();
            m_Collider.SetBuffer(0, 0, 0, 0);
        }
        
        private Animation m_Animation;
        private Rigidbody m_Rigidbody;
        private CountAnimation m_CountAnimation;
        //private int type_of_weapons=1;//1 - кулаки 2 - камень 3 - файер бол
        private Vector2 m_LastSafePosition;
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
            DrawWeapons();
            DrawIkon();

           // DrawCollision();

            
        }
        public void DrawIkon()
        {
            TextureManager.GetInstance().Draw("ikon", 0, 0, 66, 40,(float)2.2);
            TextureManager.GetInstance().Draw("health", (int)(42*2.2), (int)((4+23)*2.2), 3, -23*health/100, (float)2.2);
            TextureManager.GetInstance().Draw("mana", (int)(56*2.2), (int)((4+23)*2.2), 3, (int)(-23*mana/100), (float)2.2);

        }
        public void DrawCollision()
        {
            Rectangle box = m_Collider.Get();
            Vector2 cam = Camera.Camera.GetInstance().GetPosition();
            box.X -= (int)cam.X;
            box.Y -= (int)cam.Y;
            Core.GetInstance().GetGraphics().DrawRectangle(new Pen(Color.Red, 3),box);
        }
        public void DrawWeapons()
        {
            TextureManager.GetInstance().Draw("weapon" + type_of_weapons, 1300, 10, 22, 22,(float)2.2);
        }
        Vector2 view = new Vector2();
        int direction;





        public override bool Update(float dt)
        {
         
            if(mana<100) mana+=(float)0.2;
            Attack(direction);
            int temp_direction = SetAnimation();
            if (!m_CountAnimation.Playing())
                direction = temp_direction;
            Move(dt, direction);
            ChangeAtackType();
            if (health <= 0)
                Core.GetInstance().LoseGame();
            return true;

            
        }





        private void Attack(int direction)
        {
            if(Events.GetInstance().GetLeftMouseKlick()&&!m_CountAnimation.Playing())
            {

                switch (type_of_weapons)
                {
                    case 1: {
                            AllAttacks.GetInstance().AddPlayerAttack(new MeleeAttack(GetProperties(),direction, AllCharacter.GetInstance().GetEnemys()));
                        }
                        break;
                    case 2: {
                            AllAttacks.GetInstance().AddPlayerAttack(new RengaAttack(new Properties("rock2", "RengaAttack",(float)m_Origin.X,(float)m_Origin.Y,16,16,1,1,34,1,true,true,Events.GetInstance().GetMousePosition(),false,500), AllCharacter.GetInstance().GetEnemys()));
                        }
                        break;
                    case 3: {
                            if (mana >= 10)
                            {
                                mana -= 10;
                                AllAttacks.GetInstance().AddPlayerAttack(new ExplosiveAttack(new Properties("rock3", "ExplosiveAttack", (float)m_Origin.X, (float)m_Origin.Y, 16, 16, 1, 1, 100,20,false,false, Events.GetInstance().GetMousePosition(),false,300), AllCharacter.GetInstance().GetEnemys()));
                            }
                        }
                        break;
                }
            }
        }
        private void ChangeAtackType()
        {
            if (!m_CountAnimation.Playing())
            {
                if (Events.GetInstance().GetStateKey(Key.D1))
                    type_of_weapons = 1;
                if (Events.GetInstance().GetStateKey(Key.D2))
                    type_of_weapons = 2;
                if (Events.GetInstance().GetStateKey(Key.D3))
                    type_of_weapons = 3;
            }
        }
        private int SetAnimation()
        {
            int direction = 0;// 0 - право  1 - лево 2 - вверх  3 - вниз 
            view.X = Camera.Camera.GetInstance().GetPosition().X;
            view.Y = Camera.Camera.GetInstance().GetPosition().Y;
            m_Animation.SetProps(m_TextureID, 0, 4, 100);
            System.Windows.Point kursor = Events.GetInstance().GetMousePosition();//точка курсора
            view.X += (float)(kursor.X - m_Origin.X); view.Y += (float)(kursor.Y - m_Origin.Y);//вектор взгляда
                if ((view.Y + view.X) < 0 && view.Y < view.X)
                {
                    if (Events.GetInstance().GetLeftMouseKlick()&&!m_CountAnimation.Playing())
                        m_CountAnimation.SetProps(m_TextureID, 2, 4, 150);
                    else
                        m_Animation.SetProps(m_TextureID, 2, 4, 100);
                    direction = 2;
                }
                if ((view.Y + view.X) > 0 && (view.X < view.Y))
                {
                    if (Events.GetInstance().GetLeftMouseKlick() && !m_CountAnimation.Playing())
                        m_CountAnimation.SetProps(m_TextureID, 6, 4, 150);
                    else
                        m_Animation.SetProps(m_TextureID, 3, 4, 100);
                    direction = 3;
                }
                if ((view.Y + view.X) > 0 && view.Y < view.X)
                {
                    if (Events.GetInstance().GetLeftMouseKlick() && !m_CountAnimation.Playing())
                        m_CountAnimation.SetProps(m_TextureID, 4, 6, 150);
                    else
                        m_Animation.SetProps(m_TextureID, 0, 6, 100);
                    direction = 0;
                }
                if ((view.Y > view.X) && (view.X + view.Y) < 0)
                {
                    if (Events.GetInstance().GetLeftMouseKlick() && !m_CountAnimation.Playing())
                        m_CountAnimation.SetProps(m_TextureID, 5, 6, 150);
                    else
                        m_Animation.SetProps(m_TextureID, 1, 6, 100);
                    direction = 1;
               }
            
            m_Animation.Update();
            m_CountAnimation.Update();
            return direction;
        }
        private void Move(float dt, int direction)// 0 - право  1 - лево 2 - вверх  3 - вниз 
        {
            m_Rigidbody.UnSetForse();
            if (Events.GetInstance().GetStateKey(Key.S))
            {
                m_Rigidbody.ApplyForceY((direction == 3 ? 4 : 2) * Rigidbody.DOWNWARD);

            }
            if (Events.GetInstance().GetStateKey(Key.W))
            {
                m_Rigidbody.ApplyForceY((direction == 2 ? 4 : 2) * Rigidbody.UPWARD);

            }
            if (Events.GetInstance().GetStateKey(Key.A))
            {
                m_Rigidbody.ApplyForceX((direction == 1 ? 4 : 2) * Rigidbody.BACKWARD);

            }
            if (Events.GetInstance().GetStateKey(Key.D))
            {
                m_Rigidbody.ApplyForceX((direction == 0 ? 4 : 2) * Rigidbody.FORWARD);

            }
            m_Rigidbody.Update(dt);
            m_LastSafePosition.X = m_Transform.X;
            m_Transform.X += m_Rigidbody.Position().X;
            m_Collider.Set((int)m_Transform.X, (int)m_Transform.Y, (int)(m_Width / m_Compression), (int)(m_Heigth / m_Compression));
            if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 0))
            {
                m_Transform.X = m_LastSafePosition.X;
            }
            for (int i = 0; i < AllCharacter.GetInstance().GetEnemys().Count; i++)
            {
                if (CollisionHandler.CollisionHandler.GetInstance().CheckCollision(m_Collider.Get(), AllCharacter.GetInstance().GetEnemys()[i].GetCollider().Get()))
                {
                    m_Transform.X = m_LastSafePosition.X;
                }
            }
            if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 1))
            {
                Core.GetInstance().NextLevel();
            }
            //на ось y
            m_LastSafePosition.Y = m_Transform.Y;
            m_Transform.Y += m_Rigidbody.Position().Y;
            m_Collider.Set((int)m_Transform.X, (int)m_Transform.Y, (int)(m_Width / m_Compression), (int)(m_Heigth / m_Compression));
            if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 0))
            {
                m_Transform.Y = m_LastSafePosition.Y;
            }
            for (int i = 0; i < AllCharacter.GetInstance().GetEnemys().Count; i++)
            {
                if (CollisionHandler.CollisionHandler.GetInstance().CheckCollision(m_Collider.Get(), AllCharacter.GetInstance().GetEnemys()[i].GetCollider().Get()))
                {
                    m_Transform.Y = m_LastSafePosition.Y;
                }
            }
            if (CollisionHandler.CollisionHandler.GetInstance().MapCollision(m_Collider.Get(), 1))
            {
                Core.GetInstance().NextLevel();
            }

            m_Origin.X = m_Transform.X + (m_Width / 2) / m_Compression;
            m_Origin.Y = m_Transform.Y + (m_Heigth / 2) / m_Compression;
            Camera.Camera.GetInstance().SetTarget(m_Origin);
        }
    }

}
