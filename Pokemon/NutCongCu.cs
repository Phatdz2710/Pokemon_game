using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class NutCongCu : Button
    {

        private Image imgButton = Image.FromFile(Application.StartupPath + "\\icon\\buttonimg.jpg");


        public NutCongCu(string typeName) 
        {
            try
            {
                this.Image = imgButton;
            } catch { this.Image = new Bitmap(1, 1);  }

            this.Width = 100;
            this.Height = 100;
            this.Dock = DockStyle.Top;
            this.Location = new Point(0, 0);
            this.Text = typeName;
            this.Font = new Font("Arial", 16, FontStyle.Bold);
            this.ForeColor = Color.Maroon;
            this.BackColor = Color.Transparent;
            this.ImageAlign = ContentAlignment.MiddleCenter;
            
        }


    }
}
