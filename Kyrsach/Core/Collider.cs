using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Kyrsach.Core
{
    public class Collider
    {
        public Rectangle Get() { return m_Box; }
        public void SetBuffer(int x, int y, int w, int h) { m_Buffer = new Rectangle( x,y,w,h ); }
        public void Set(int x, int y, int w, int h)
        {
            m_Box = new Rectangle( x - m_Buffer.X,y - m_Buffer.Y,w - m_Buffer.Width,h - m_Buffer.Height );
        }
	    private Rectangle m_Box;
        private Rectangle m_Buffer;
    }
}
