using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Numerics;

namespace Kyrsach.Core
{
    
    public class Rigidbody
    {
    public Rigidbody()
        {
            m_Mass = 1.0f;
            m_Gravity = 0f;
        }
        //задаем массу m и гравитацию g
        public void SetMass(float mass) { m_Mass = mass; }
        public void SetGravity(float gravity) { m_Gravity = gravity; }
        //установка сил
        public void ApplyForce(Vector2 F) { m_Force = F; }
        public void ApplyForceX(float Fx) { m_Force.X = Fx; }
        public void ApplyForceY(float Fy) { m_Force.Y = Fy; }
        //снятие сил
        public void UnSetForse() { m_Force.X = 0; m_Force.Y = 0; }
        //установка трения
        public void ApplyFriction(Vector2 Fr) { m_Friction = Fr; }
        public void ApplyFrictionX(float Fx) { m_Friction.X = Fx; }
        public void ApplyFrictionY(float Fy) { m_Friction.Y = Fy; }
        public void UnSetFriction() { m_Friction.X = 0; m_Friction.Y = 0; }
        //обновление ускорения
        public void Update(float dt)
        {
            m_Accelaration.X = (m_Force.X + m_Friction.X) / m_Mass;
            m_Accelaration.Y = m_Force.Y / m_Mass + m_Gravity;
            m_Velocity = m_Accelaration * dt;
            m_Position = m_Velocity * dt;
        }
        //getters
        public float GetMass() { return m_Mass; }
        public Vector2 Position() { return m_Position; }
        public Vector2 Velocity() { return m_Velocity; }
        public Vector2 Accelaration() { return m_Accelaration; }



        private float m_Mass;
        private float m_Gravity;

        private Vector2 m_Force;
        private Vector2 m_Friction;
        
        private Vector2 m_Position;
        private Vector2 m_Velocity;
        private Vector2 m_Accelaration;
        public const int FORWARD = 1;
        public const int BACKWARD = -1;

        public const int UPWARD = -1;
        public const int DOWNWARD = 1;
    }
}
