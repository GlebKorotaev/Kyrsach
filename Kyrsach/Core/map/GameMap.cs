using Kyrsach.Core.map;
using Kyrsach.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Kyrsach.Core.map
{
    public class GameMap:Layer
    {
       public void Render() 
	{

	for (int i = 0; i < m_MapLayers.Count; i++)//с трех так как первые три слойла для коллизий и для пуль вообще только 2 фон см. Update
	m_MapLayers[i].Render();
    }
    public void Update() 
	{
		
	}
	public List<Layer> GetMapLayers() { return m_MapLayers; }
	

	private List<Layer> m_MapLayers=new List<Layer>();

    }
}
