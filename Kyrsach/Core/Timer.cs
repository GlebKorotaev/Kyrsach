using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Kyrsach.Core
{
    public class Timer
    {
        
	    public static Timer GetInstance() { return s_Instance = (s_Instance != null ? s_Instance : s_Instance = new Timer()); }
        public void Tick() {
            m_DeltaTime = (stopWatch.ElapsedMilliseconds - m_LastTime) * (60 / 1000.0f);
            if (m_DeltaTime > 1.5f)
                m_DeltaTime = 1.5f;
            m_LastTime = stopWatch.ElapsedMilliseconds;
        }
        public void Init()
        {
            stopWatch.Start();
        }
        public void Quit()
        {
            stopWatch.Stop();
        }
        public float GetDeltaTime() { return m_DeltaTime; }
        public long GetTime() { return stopWatch.ElapsedMilliseconds; }

	    private Timer() { }
        private static Timer? s_Instance =null;
        private float m_DeltaTime;
        private float m_LastTime=0;
        private Stopwatch stopWatch = new Stopwatch();
    }
}
