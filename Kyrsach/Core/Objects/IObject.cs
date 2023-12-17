using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Core.Objects
{
    public interface IObject
    {
        void Draw();
        bool Update(float dt);
	    void Clean();
    }
}
