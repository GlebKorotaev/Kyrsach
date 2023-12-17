using Kyrsach.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Kyrsach.Core.map
{
    using TileSetList = List<TileLayer.TileSet>;
    using TileMap = List<List<int>>;
    
    
    public class TileLayer:Layer
    {
       public struct TileSet
        {
            public int FirstID, LastID;
            public int RowCount, ColCount;
            public int TileCount, TileSize;
            public string Name, Source;
        };
        public TileLayer(int tilesize, int rowcount, int colcount, TileMap tilemap, TileSetList tilesets)
        {
            m_TileSets = tilesets;m_TileMap=tilemap;
            m_TileSize = tilesize; m_RowCount=rowcount; m_ColCount=colcount;
            for ( int i = 0; i < m_TileSets.Count; i++)
            {
                TextureManager.GetInstance().Load(m_TileSets[i].Name, "C:\\Users\\user\\source\\repos\\Kyrsach\\Kyrsach\\assets\\map\\" + m_TileSets[i].Source);
            }
        }
        public void Render() {
            for ( int i = 0; i < m_RowCount; i++)
            {


                for ( int j = 0; j < m_ColCount; j++)
                {
                    int tileID = m_TileMap[i][j];
                    if (tileID == 0)
                        continue;
                    int index = 0;
                    if (m_TileSets.Count > 1)
                    {
                        for ( int k = 1; k < m_TileSets.Count; k++)
                        {
                            if (tileID >= m_TileSets[k].FirstID && tileID <= m_TileSets[k].LastID)
                            {
                                tileID = tileID + m_TileSets[k].TileCount - m_TileSets[k].LastID;
                                index = k;
                                break;
                            }
                        }
                    }
                    TileSet ts = m_TileSets[index];
                    int tileRow = tileID / ts.ColCount;
                    int tileCol = tileID - tileRow * ts.ColCount - 1;
                    //если последняя
                    if (tileID % ts.ColCount == 0)
                    {
                        tileRow--;
                        tileCol = ts.ColCount - 1;
                    }
                    TextureManager.GetInstance().DrawTile(ts.Name, ts.TileSize, j * ts.TileSize, i * ts.TileSize, tileRow, tileCol);
                }
            }
        }
        public void Update() {
        
        }
        public TileMap GetTileMap() { return m_TileMap; }

	    private int m_TileSize;
        private int m_RowCount, m_ColCount;
        private TileMap m_TileMap;
        private TileSetList m_TileSets;

    }
}
