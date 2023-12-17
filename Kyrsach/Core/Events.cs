using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kyrsach.Core
{
    public class Events
    {
        private static Events? s_Instance;
        
        public static Events GetInstance()
        {
            return s_Instance = (s_Instance != null) ? s_Instance : new Events();
        }
        private Events(){
           
        }
       
        
        private Dictionary<Key,bool> s_Events= new Dictionary<Key,bool>();
        private Point m_MousPosition = new Point();
        private bool LeftMouse_klick;
        public void SetLeftMouseUp(object sender, MouseButtonEventArgs e)
        {
            LeftMouse_klick = false;
        }
        public void SetLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            LeftMouse_klick = true;
        }

        public void setKeyUp(object sender,KeyEventArgs up)
        {
           s_Events[up.Key] = false;
       }
       public void setKeyDown(object sender,KeyEventArgs down)
       {
           s_Events[down.Key] = true;
        }
        public void setMousePosition(Point p)
        {
            m_MousPosition = p;
        }
        public Point GetMousePosition()
        {
            return m_MousPosition;
        }
        public bool GetStateKey(Key key)
        {
            if (s_Events.ContainsKey(key))
                return s_Events[key];
            else return false;
        }
        public bool GetLeftMouseKlick()
        {
            return LeftMouse_klick;
        }

    }
}
