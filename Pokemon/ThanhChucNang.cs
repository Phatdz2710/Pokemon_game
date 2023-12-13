using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class ThanhChucNang : Panel
    {
        public ImageList ImageList { get; set; }
        public Panel imgHutao = new Panel();

        public List<NutCongCu> Buttons = new List<NutCongCu>();
        private static Image imgThanhCC = Image.FromFile(Application.StartupPath + "\\icon\\bg2.jpg");
        public ThanhChucNang(ImageList Hutao)
        {
            this.Dock = DockStyle.Right;
            //Panel
            this.AutoSize = false;

            this.ImageList = Hutao;
            this.imgHutao.BackgroundImage = ImageList.Images[1];
            this.imgHutao.Dock = DockStyle.Top;
            this.imgHutao.Size = new Size(250, 250);
            this.Size = new Size(200, 0);

            //Button
            Font font = new Font("Arial", 10, FontStyle.Bold);
            this.Buttons.Add(new NutCongCu("AUTO PLAY") { Dock = DockStyle.None, Location = new Point(50, 300), Font = font }) ;
            //this.Buttons.Add(new NutCongCu("REVERSE")  { Location = new Point(0, 393) });
            this.Buttons.Add(new NutCongCu("SUGGEST") { Dock = DockStyle.None, Location = new Point(50,  393 + 20 ), Font = font });
            this.Buttons.Add(new NutCongCu("RESERVE") { Dock = DockStyle.None, Location = new Point(50, 393 + 93 + 40), Font = font }); 

            foreach (NutCongCu button in this.Buttons)
            {
                this.Controls.Add(button);
            }
            this.Controls.Add(imgHutao);

            try
            {
                this.BackgroundImage = imgThanhCC;
            }
            catch { this.BackgroundImage = new Bitmap(1, 1); }
        }
    }
}
