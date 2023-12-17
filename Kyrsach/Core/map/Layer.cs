using Kyrsach.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Core.map
{
    public interface Layer
    {
        void Render();
	    void Update();
    }
}
