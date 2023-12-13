using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class ThanhCongCu : Panel
    {
        public ImageList ImageList { get; set; }
        public Panel imgHutao = new Panel();

        public List<NutCongCu> Buttons = new List<NutCongCu>();
        private static Image imgThanhCC = Image.FromFile(Application.StartupPath + "\\icon\\background.jpg");
        public ThanhCongCu(ImageList Hutao)
        {
            this.Dock = DockStyle.Left;
            //Panel
            this.AutoSize = false;
            
            this.ImageList = Hutao;
            this.imgHutao.BackgroundImage = ImageList.Images[0];
            this.imgHutao.Dock = DockStyle.Top;
            this.imgHutao.Size = new Size(250, 250);
            this.Size = new Size(250, 0);

            //Button
            
            this.Buttons.Add(new NutCongCu("NEW GAME") { Location = new Point(0, 300) });
            //this.Buttons.Add(new NutCongCu("REVERSE")  { Location = new Point(0, 393) });
            //this.Buttons.Add(new NutCongCu("SUGGEST") { Location = new Point(0,  393 + 93) });
            this.Buttons.Add(new NutCongCu("SOUND: ON") { Location = new Point(0, 393 + 93 + 93)}); ; ;
            
            foreach (NutCongCu button in this.Buttons)
            {
                this.Controls.Add(button);
            }
            this.Controls.Add(imgHutao);

            try
            {
                this.BackgroundImage = imgThanhCC;
            } catch { this.BackgroundImage = new Bitmap(1, 1); }
        }
    }
}
