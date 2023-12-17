using Kyrsach.Core.map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kyrsach.Core.CollisionHandler
{
    using TileMap = List<List<int>>;
    public class CollisionHandler
    {
	    public static CollisionHandler GetInstance() { return s_Instance = (s_Instance != null ? s_Instance : new CollisionHandler()); }
        public bool MapCollision(Rectangle a, int number_layer)
        {
            m_CollisionLayer = (TileLayer)Core.GetInstance().GetMap().GetMapLayers()[number_layer];
            m_CollisionTileMap = m_CollisionLayer.GetTileMap();
            int left_tile = (int)(a.X / CurrentLevel.GetInstance().GetLevel().tilesize);
            int rigth_tile = (int)((a.X + a.Width) / CurrentLevel.GetInstance().GetLevel().tilesize);
            int top_tile = (int)(a.Y / CurrentLevel.GetInstance().GetLevel().tilesize);
            int bottom_tile = (int)((a.Y + a.Height) / CurrentLevel.GetInstance().GetLevel().tilesize);
            if (left_tile < 0) left_tile = 0;
            if (bottom_tile < 0) bottom_tile = 0;
            if (rigth_tile < 0) rigth_tile = 0;
            if (rigth_tile > CurrentLevel.GetInstance().GetLevel().colcount) rigth_tile = CurrentLevel.GetInstance().GetLevel().colcount;
            if (top_tile < 0) top_tile = 0;
            if (bottom_tile > CurrentLevel.GetInstance().GetLevel().rowcount) bottom_tile = CurrentLevel.GetInstance().GetLevel().rowcount;
	 
	 for (int i = left_tile; i <= rigth_tile; ++i)
	 {
		 for (int j = top_tile; j <= bottom_tile; ++j)
		 {
			 if (m_CollisionTileMap[j][i] > 0) {
				 return true;
			 }
		 }
	 }
	 return false;
        }
        public bool CheckCollision(Rectangle a, Rectangle b)
        {
            bool x_overlaps = (a.X < b.X + b.Width) && (a.X + a.Width > b.X);
            bool y_overlaps = (a.Y < b.Y + b.Height) && (a.Y + a.Height > b.Y);
            return (x_overlaps && y_overlaps);
        }


        private CollisionHandler() { }
        private static CollisionHandler s_Instance;
        private TileMap m_CollisionTileMap;
        private TileLayer m_CollisionLayer;
    }
}
