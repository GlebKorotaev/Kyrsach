using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Core
{
    public class CountAnimation
    {
        public CountAnimation() { }
        public int GetCurrentFrame()
        {
            return m_SpriteFrame;
        }

        public void Update()
        {
            
            if (m_TargetSpriteFrame != (int)(Timer.GetInstance().GetTime() / m_AnimSpeed % m_FrameCount))
            {
                m_TargetSpriteFrame=(int)(Timer.GetInstance().GetTime() / m_AnimSpeed % m_FrameCount);
                m_SpriteFrame++;
            }
            if (m_SpriteFrame == m_FrameCount)
            {
                m_SpriteFrame = 0;
                m_Repeat = false;
            }
        }

        public void Draw(int x, int y, int spriteWidth, int spriteHeigth, double compression, double angel = 0)
        {
            if (m_Repeat)
            {  
                TextureManager.GetInstance().DrawFrame(m_TextureID, x, y, spriteWidth, spriteHeigth, compression, m_SpriteRow, m_SpriteFrame, angel); 
            }
        }

        public void SetProps(string textureID, int spriteRow, int frameCount, int animSpeed)
        {
            m_TextureID = textureID;
            m_SpriteRow = spriteRow;
            m_FrameCount = frameCount;
            m_AnimSpeed = animSpeed;
            m_SpriteFrame = 0;
            m_Repeat = true;
            m_TargetSpriteFrame = (int)(Timer.GetInstance().GetTime() / m_AnimSpeed % m_FrameCount);
        }
        public bool Playing()
        {
            return m_Repeat;
        }

        int m_SpriteRow, m_SpriteFrame;
        int m_AnimSpeed=1, m_FrameCount=1;
        bool m_Repeat=false;
        int m_TargetSpriteFrame;
        string m_TextureID;

    }
}
