using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Pokemon
{
    public class Line : Label
    { 
        public Line() 
        {
            this.Location = new Point(10,10);
            this.AutoSize = false;
            this.Height = 5;
            this.Width = 5;
            this.BackColor = Color.Red;
        }

        public Line(Point start, Point end)
        {   
            if (start.X < end.X)
            {
                this.Location = start;
                this.Height = 5;
                this.Width = end.X - start.X + 5;
            }else if (start.X > end.X)
            {
                this.Location = end;
                this.Height = 5;
                this.Width = start.X - end.X;
            }else if (start.Y < end.Y)
            {
                this.Location = start;
                this.Width = 5;
                this.Height = end.Y - start.Y + 5;
            }
            else if (start.Y > end.Y)
            {
                this.Location = end;
                this.Width = 5;
                this.Height = start.Y - end.Y;
            }
            else if (start == end)
            {
                this.Width = 0;
                this.Height = 0;
            }

            this.BackColor = Lib.ColorLine;
            
        }
    }
}
