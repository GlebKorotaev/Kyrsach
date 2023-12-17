using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace Kyrsach.Core.Objects
{
    [Serializable]
    public struct Properties
    {
        public Properties(string TextureID = "", string Name = "", float X = 0, float Y = 0, int Width = 0, int Heigth = 0, double Compression = 1, int type_of_weapons=1, int Health=100,int Mana=100, bool Target_Player=false, bool WasTarget_Player=false, System.Windows.Point point=new Point(),bool now =false,int range=300)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Heigth = Heigth;
            this.TextureID = TextureID;
            this.Compression = Compression;
            this.Name = Name;
            this.type_of_weapons = type_of_weapons;
            this.Health = Health;
            this.Mana = Mana;
            this.Target_Player = Target_Player;
            this.WasTarget_Player = WasTarget_Player;
            this.point = point;
            this.range = range;
            this.now = now;
        }
        [JsonInclude]
        public string TextureID;
        [JsonInclude]
        public int type_of_weapons;
        [JsonInclude]
        public string Name;
        [JsonInclude]
        public float X, Y;
        [JsonInclude]
        public int Width;
        [JsonInclude]
        public int Heigth;
        [JsonInclude]
        public double Compression;
        [JsonInclude]
        public int Health;
        [JsonInclude]
        public int Mana;
        [JsonInclude]
        public bool Target_Player;
        [JsonInclude]
        public bool WasTarget_Player;
        [JsonInclude]
        public System.Windows.Point point;
        [JsonInclude]
        public bool now;
        [JsonInclude]
        public int range;

    };
    public abstract class GameObject:IObject
    {
     public GameObject() { }
	 public GameObject(Properties props)
        {
            m_TextureID = props.TextureID;
            m_Width = props.Width;
            m_Heigth = props.Heigth;
            m_Compression = props.Compression;
            m_Transform = new Vector2(props.X, props.Y);
            float px = props.X + (float)(props.Width / 2 / m_Compression);
            float py = props.Y + (float)(props.Heigth / 2 / m_Compression);
            m_Origin = new Point(px, py);
            name = props.Name;
            health = props.Health;
            type_of_weapons = props.type_of_weapons;
            mana = props.Mana;
            m_IsDied = false;
            Target_Player = props.Target_Player;
            WasTarget_Player = props.Target_Player;
            now = props.now;
            range = props.range;
            point = props.point;

    }
        public Properties GetProperties()
        {
            return new Properties(m_TextureID, name,(float)m_Transform.X, (float)m_Transform.Y, m_Width, m_Heigth, m_Compression,type_of_weapons,health,(int)mana,Target_Player,WasTarget_Player,point,now,range);
        }
        public void SetProperties(Properties props)
        {
            m_TextureID = props.TextureID;
            m_Width = props.Width;
            m_Heigth = props.Heigth;
            m_Compression = props.Compression;
            m_Transform = new Vector2(props.X, props.Y);
            float px = props.X + (float)(props.Width / 2 / m_Compression);
            float py = props.Y + (float)(props.Heigth / 2 / m_Compression);
            m_Origin = new System.Windows.Point(px, py);
            name = props.Name;
            health = props.Health;
            mana = props.Mana;
            type_of_weapons = 1;
            m_IsDied = false;
            Target_Player = props.Target_Player;
            WasTarget_Player = props.WasTarget_Player;
            point = props.point;
            range = props.range;
            now = props.now;
    }
        public virtual void Clean()
        {

        }
        public virtual void Draw()
        {
            
        }

        public Vector2 GetTransform()
        {
            return m_Transform;
        }
        public virtual bool Update(float dt)
        {
            return true;
        }
        public Point GetOrigin()
        {
            return m_Origin;
        }
        protected int type_of_weapons;
        protected Vector2 m_Transform;
        protected int m_Width;
        protected int m_Heigth;
        protected double m_Compression;
        protected string m_TextureID;
        protected Point m_Origin;
        protected string name;
        protected int health;
        protected float mana;
        protected Collider m_Collider;
        protected bool m_IsDied;
        protected bool Target_Player;
        protected bool WasTarget_Player;
        protected System.Windows.Point point;
        protected bool now;
        protected int range;
        public int GetHealth()
        {
            return health;
        }
        public void SetHealth(int p)
        {
            health = p;
        }
        public void SetMana(int p)
        {
            mana = p;
        }
        public Collider GetCollider()
        {
            return m_Collider;
        }
    }
}
