using Kyrsach.Core.map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;
using static Kyrsach.Core.map.TileLayer;

namespace Kyrsach.Core.map
{
    using TileMap = List<List<int>>;
    

    public class MapParser
    {

        public static MapParser GetInstance() { return s_Instance = (s_Instance != null) ? s_Instance : new MapParser(); }
        public bool Load(string name,string sourse) {
           
             Parse(name, "C:\\Users\\user\\source\\repos\\Kyrsach\\Kyrsach\\assets\\map\\"+sourse);
            return true;
        }

        public void Clean()
        {

        }
        public GameMap GetMap(string id) { return m_MapDict[id]; }

        public int rowcount, colcount, tilesize = 0;

        private MapParser() { }
        private static MapParser s_Instance;
        private Dictionary<string, GameMap> m_MapDict = new Dictionary<string, GameMap>();
        



        private bool Parse(string id, string source)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(source);
            XmlElement root = xml.DocumentElement;

            colcount = int.Parse(root.GetAttributeNode("width").Value);
            rowcount = int.Parse(root.GetAttributeNode("height").Value);
            tilesize = int.Parse(root.GetAttributeNode("tilewidth").Value);
            List<TileSet> tilesets = new List<TileSet>();
            for (XmlNode element = root.FirstChild; element != null; element = element.NextSibling)
            {
                if (element.Name == "tileset")
                {
                    tilesets.Add(ParseTileSet(element));
                }
            }
                GameMap gamemap = new GameMap();
            for (XmlNode element1 = root.FirstChild; element1 != null; element1 = element1.NextSibling)
            {
                if (element1.Name == "layer")
                {
                    TileLayer tilelayer = ParseTileLayer(element1, tilesets, tilesize, rowcount, colcount);
                    gamemap.GetMapLayers().Add(tilelayer);
                }
            }
            Level level = new Level();
            level.rowcount = rowcount;
            level.tilesize = tilesize;
            level.colcount = colcount;
            CurrentLevel.GetInstance().AddLevel(id, level);
                    m_MapDict[id] = gamemap;
                    return true;
                }

                private TileSet ParseTileSet(XmlNode xmlTileset)
                {
                    TileLayer.TileSet tileset;
                    tileset.Name = xmlTileset.Attributes["name"].Value;
                    tileset.FirstID = int.Parse(xmlTileset.Attributes["firstgid"].Value);
                    //xmlTileset->Attribute("firstgid", &tileset.FirstID);
                    tileset.TileCount = int.Parse(xmlTileset.Attributes["tilecount"].Value);
                    //xmlTileset->Attribute("tilecount", &tileset.TileCount);

                    tileset.LastID = tileset.FirstID + tileset.TileCount - 1;
                    tileset.ColCount = int.Parse(xmlTileset.Attributes["columns"].Value);
                    //xmlTileset->Attribute("columns", &tileset.ColCount);

                    tileset.RowCount = tileset.TileCount / tileset.ColCount;
                    tileset.TileSize = int.Parse(xmlTileset.Attributes["tilewidth"].Value);
                    //xmlTileset->Attribute("tilewidth", &tileset.TileSize);
                    XmlNode image = xmlTileset.FirstChild;
                    tileset.Source = image.Attributes["source"].Value;
                    return tileset;
                }
                private TileLayer ParseTileLayer(XmlNode xmllayer, List<TileSet> tilesets, int tilesize, int rowcount, int colcount)
                {
                    XmlNode data = null;
                    for (XmlNode element = xmllayer.FirstChild; element != null; element = element.NextSibling)
                    {
                     if (element.Name == "data")
		            {
                        data = element;
                        break;
                    }
                    }
        string matrix = new string(data.InnerText);
        string id;
            TileMap tilemap = new TileMap  ();
            for(int i=0;i<tilemap.Count;i++)
            {
                tilemap[i] = new List<int>(colcount);
            }
            matrix = matrix.Replace('\r', ' ');
            matrix = matrix.Replace('\n', ' ');
            string[] s = matrix.Split(new char[] { ',' });
            for (int row = 0; row < rowcount; row++)
            {
                List<int> m =new List<int>();
                for (int col = 0; col < colcount; col++)
                {
                    int a = row * colcount + col;
                    try
                    {
                        m.Add(int.Parse(s[row * colcount + col]));
                    }
                    catch
                    {
                        ;
                    }
                }
                tilemap.Add(m);
            }
            return (new TileLayer(tilesize, rowcount, colcount, tilemap, tilesets));
                    
                }
            }
        }

