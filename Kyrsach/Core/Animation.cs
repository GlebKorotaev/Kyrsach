using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Core
{
    public class Animation
    {
        public Animation() { }

        public void Update()
        {
            m_SpriteFrame = (int)(Timer.GetInstance().GetTime() / m_AnimSpeed % m_FrameCount);
        }

        public void Draw(int x, int y, int spriteWidth, int spriteHeigth, double compression,double angel=0)
        {
            TextureManager.GetInstance().DrawFrame(m_TextureID, x, y, spriteWidth, spriteHeigth, compression, m_SpriteRow, m_SpriteFrame,angel);
        }

      public  void SetProps(string textureID, int spriteRow, int frameCount, int animSpeed)
        {
            m_TextureID = textureID;
            m_SpriteRow = spriteRow;
            m_FrameCount = frameCount;
            m_AnimSpeed = animSpeed;
        }


	int m_SpriteRow, m_SpriteFrame;
    int m_AnimSpeed, m_FrameCount;
    string m_TextureID;
        
    }
}
