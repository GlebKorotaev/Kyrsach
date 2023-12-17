using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Security.Cryptography;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Windows.Controls;
using System.Numerics;
using static Kyrsach.Core.map.TileLayer;
using System.Windows.Media.Media3D;

namespace Kyrsach.Core
{
    public class TextureManager:IDisposable
    {
	public static TextureManager GetInstance() { return s_Instance = (s_Instance != null ? s_Instance : new TextureManager()); }

        public bool Load(string id, string filename)
        {
            try
            {
                m_TextureMap.Add(id, new Bitmap(filename));
            }
            catch
            {
                Console.Write("ошибка открытия файла");
            }
            return true;
        }

        public void Drop(string name)
        {

        }

        public void Clean()
        {
            Dispose();
        }
        private PointF[] corners =
             {
         new PointF(0, 0),
         new PointF(0, 0),
         new PointF(0, 0)
     };
        public void Draw(string id, int x, int y, int width, int heigth, float compresion = 1)
        {
            corners[0].X = x;
            corners[0].Y= y;
            corners[1].X = x + width * compresion;
            corners[1].Y = y;
            corners[2].X = x;
            corners[2].Y = y + heigth * compresion;
           /* PointF[] corners =
             {
         new PointF(x, y),
         new PointF(x+width*compresion, y),
         new PointF(x, y+heigth*compresion)
     };*/
             Core.GetInstance().GetGraphics().DrawImage(m_TextureMap[id], corners);
        }
        private Rectangle srcRect = new Rectangle(0, 0, 0, 0);
        private Rectangle dstRect = new Rectangle(0, 0, 0, 0);
        public void DrawMap(string id, int x, int y, int width, int heigth, float compresion = 1)
        {
            Vector2 cam = Camera.Camera.GetInstance().GetPosition();
            dstRect.X = 0; dstRect.Y = 0; dstRect.Width = width; dstRect.Height = heigth;
            srcRect.X = x + (int)cam.X; srcRect.Y = y + (int)cam.Y; srcRect.Width = width; srcRect.Height = heigth;
            GraphicsUnit units = GraphicsUnit.Pixel;
            try
            {
                Core.GetInstance().GetGraphics().DrawImage(m_TextureMap[id], dstRect, srcRect, units);
            }
            catch {; }
            
        }
        
        public void DrawTile(string tilesetID, int tilesize, int x, int y, int row, int frame)
         {//1366 768
            //Vector cam = Camera::GetInstance()->GetPosition();
            Vector2 cam = Camera.Camera.GetInstance().GetPosition();
            //if (1366>(x - (int)cam.X )&& 768>(y - (int)cam.Y)) {
                dstRect.X = x; dstRect.Y = y; dstRect.Width = tilesize; dstRect.Height = tilesize;
                srcRect.X = tilesize * frame; srcRect.Y = tilesize * row; srcRect.Width = tilesize; srcRect.Height = tilesize;
                //dstRect.X = x-(int)cam.X; dstRect.Y = y-(int)cam.Y; dstRect.Width = tilesize; dstRect.Height = tilesize;
                //srcRect.X = tilesize * frame; srcRect.Y = tilesize * row; srcRect.Width = tilesize; srcRect.Height = tilesize;
                GraphicsUnit units = GraphicsUnit.Pixel;
                try
                {
                    Core.GetInstance().GetGraphics().DrawImage(m_TextureMap[tilesetID], dstRect, srcRect, units);
                }
                catch {; }
            //}
            //SDL_Rect dstRect = { x - cam.X, y - cam.Y, tilesize, tilesize };
            //SDL_Rect srcRect = { tilesize * frame, tilesize * row, tilesize, tilesize };
            //SDL_RenderCopyEx(Engine::GetInstance()->GetRenderer(), m_TextureMap[tilesetID], &srcRect, &dstRect, 0, 0, flip);
        }
        
        public  void DrawFrame(string id, int x, int y, int width, int heigth, double compression, int row, int frame, double angel = 0)
         {
            Vector2 cam = Camera.Camera.GetInstance().GetPosition();
            //Rectangle srcRect = new Rectangle( width * frame, heigth * row, width, heigth );
            //Rectangle dstRect = new Rectangle(x, y , (int)(width / compression), (int)(heigth / compression) );
            srcRect.X = width * frame;
            srcRect.Y = heigth * row;
            srcRect.Width = width;
            srcRect.Height = heigth;
            dstRect.X = x-(int)cam.X;
            dstRect.Y = y-(int)cam.Y;
            dstRect.Width = (int)(width / compression);
            dstRect.Height = (int)(heigth / compression);
            GraphicsUnit units = GraphicsUnit.Pixel;
            Core.GetInstance().GetGraphics().DrawImage(m_TextureMap[id], dstRect,srcRect,units);
        }
        public void Dispose()
        {
            m_TextureMap.Clear();
        }
        private TextureManager() { }
        private static TextureManager? s_Instance; 
        private Dictionary<string, Bitmap> m_TextureMap = new Dictionary<string, Bitmap>();
    }
}
