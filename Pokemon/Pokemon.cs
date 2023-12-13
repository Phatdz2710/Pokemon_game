using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    
    public class ImgIndex
    {
        public Image img = new Bitmap(1,1);
        public int Index = 0;
    }

    public class Pokemon : Label
    {
        public ImgIndex imgIndex = new ImgIndex();
        public int X; 
        public int Y;
        public Pokemon() 
        {
            this.Height = Lib.PikachuHeight;
            this.Width = Lib.PikachuWidth;
            this.BackColor = Lib.BackColorPikachu;
            this.AutoSize = false;
            this.imgIndex.Index = 0;
            this.imgIndex.img = new Bitmap(1, 1);
            //this.Opacity = 1;
        }

        

        //Lay toa do trong tam cua Label
        public Point GetPointCenter()
        {
            Point point = new Point();
            point.X = this.Location.X + this.Width / 2;
            point.Y = this.Location.Y + this.Height / 2;

            return point; 
        }

        public void ReloadLabel()
        {
            this.Image = imgIndex.img;
            //this.Text  = imgIndex.Index.ToString(); 
        }

        public void ReloadLabel(int dX, int dY)
        {
            this.Image = imgIndex.img;
            this.X = dX; this.Y = dY;
        }
    }
}
