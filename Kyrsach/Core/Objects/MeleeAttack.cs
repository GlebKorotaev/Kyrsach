using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Core.Objects
{//Толька у игрока!!!
    public class MeleeAttack: GameObject// 0 - право  1 - лево 2 - вверх  3 - вниз 
    {
        public MeleeAttack(Properties props,int direction, List<GameObject> enemys) : base(props)
        {
            m_Collider = new Collider();
            SetCollider(direction);
            m_Collider.SetBuffer(0, 0, 0, 0);
            this.enemys = enemys;
            health = 51;
            count = 7;
            this.direction = direction;
            name = "MeleeAttack";
        }
        private int direction;
        private void SetCollider(int direction)
        {
            double x=0, y=0;
            switch (direction)
            {
                case 0:
                    x = m_Transform.X + m_Width/m_Compression;
                    y = m_Transform.Y +m_Heigth/m_Compression / 4;
                    break;
                    case 1:
                    y = m_Transform.Y+ m_Heigth/m_Compression / 4;
                    x=m_Transform.X - m_Width/m_Compression/2;
                    break;
                    case 2:
                    x = m_Transform.X + m_Width / 4 / m_Compression;
                    y = m_Transform.Y - m_Heigth / m_Compression / 2;
                    break;
                    case 3:
                    x = m_Transform.X + m_Width / 4 / m_Compression;
                    y = m_Transform.Y + m_Heigth / m_Compression;
                    break;
            }
            m_Collider.Set((int)x, (int)y, (int)(m_Width / m_Compression/2), (int)(m_Heigth / m_Compression/2));
        }
        private List<GameObject> enemys;
        private int count;
        private int m_TargetSpriteFrame;
        
        public override bool Update(float dt)
        {
            m_Transform = AllCharacter.GetInstance().GetPlayers()[0].GetTransform();
            SetCollider(direction);
            count--;
            int prop;
            for (int i=0; i<enemys.Count;i++)
            {
                if (CollisionHandler.CollisionHandler.GetInstance().CheckCollision(m_Collider.Get(), enemys[i].GetCollider().Get() ))
                {
                    prop = enemys[i].GetHealth();
                    prop -= health;
                    enemys[i].SetHealth(prop);
                    return false;
                }
            }
            if(count == 0) { return false; }
            return true;
        }
        public override void Draw()
        {
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
