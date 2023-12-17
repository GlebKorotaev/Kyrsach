using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Kyrsach.Core
{
    public static class StartGame
    {
        public static bool Start(Image myImage)
        {
            if (Core.GetInstance().IsRunning())
            {

                Core.GetInstance().Events();
                Core.GetInstance().Update();
                myImage.Source = Core.GetInstance().Render();
                Timer.GetInstance().Tick();
                return true;
            }
            else
            {
               // Core.GetInstance().Clean();
                return false;
            }
        }
    }
}
