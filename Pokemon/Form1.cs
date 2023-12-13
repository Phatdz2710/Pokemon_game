using System.Media;

namespace Pokemon
{
    public partial class Form1 : Form
    {
        public GameManager gameManager;
        List<ImgIndex> imgIndex = new List<ImgIndex>();
        GameBoard pnlGameBoard = new GameBoard();
        //Sound BackgroundSound = new Sound(Lib.BackgroundSoundPath);
        Sound ClickButton = new Sound(Lib.ClickButtonSoundPath);
        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Hutao.Images[2];
            this.BackgroundImageLayout = ImageLayout.Stretch;
            SetFormSize(1920, 1000);
            SetFormLocation(new Point(0, 0));
            AddImgToList();
            List<ImgIndex> bansao_imgIndex = imgIndex.ToList();

            gameManager = new GameManager(pnlGameBoard);
            this.gameManager.imgIndex = bansao_imgIndex;
            this.gameManager.LoadMyLabel();
            this.gameManager.DrawBoard();
            //this.BackgroundSound.PlaySound();
        }

        private void SetFormSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        private void SetFormLocation(Point points)
        {
            this.StartPosition = FormStartPosition.Manual;

            this.Location = points;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            ThanhCongCu thanhCongCu = new ThanhCongCu(Hutao);
            ThanhChucNang thanhChucNang = new ThanhChucNang(Hutao);
            this.Controls.Add(thanhChucNang);
            this.Controls.Add(thanhCongCu);
            this.Controls.Add(pnlGameBoard);
            this.pnlGameBoard.Location = new Point(250, 0);

            thanhCongCu.Buttons[0].Click += button1_Click_1;
            thanhCongCu.Buttons[1].Click += Sound_Click;
            //thanhCongCu.Buttons[1].Click += Sound_Click;

            thanhChucNang.Buttons[0].Click += button3_Click;
            thanhChucNang.Buttons[1].Click += button2_Click;
            thanhChucNang.Buttons[2].Click += button1_Click;
        }

        private void AddImgToList()
        {
            int count = 1;
            for (int i = 0; i < 21; i++)
            {
                if (i < 9)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        ImgIndex imgTemp = new ImgIndex()
                        {
                            img = imgIcon.Images[i],
                            Index = count,
                        };
                        imgIndex.Add(imgTemp);
                    }
                    count++;
                    continue;
                }
                for (int j = 0; j < 12; j++)
                {
                    ImgIndex imgTemp = new ImgIndex()
                    {
                        img = imgIcon.Images[i],
                        Index = count,
                    };
                    imgIndex.Add(imgTemp);
                }
                count++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ClickButton.PlaySound();
            this.gameManager.XaoTron();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //ClickButton.PlaySound();
            this.gameManager.ResetGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ClickButton.PlaySound();

            this.gameManager.GoiY();
        }

        
        private void Sound_Click(object sender, EventArgs e)
        {
            NutCongCu ntnCongCu = sender as NutCongCu;
            if (!Lib.IsOpenSound)
            {
                ntnCongCu.Text = "SOUND: ON";
                //BackgroundSound.PlaySound();
                Lib.IsOpenSound = true;
            } else
            {
                ntnCongCu.Text = "SOUND: OFF";
                //BackgroundSound.StopSound();
                Lib.IsOpenSound = false ;
            }

        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            ClickButton.PlaySound();

            //DialogResult result = MessageBox.Show("Bạn có muốn Auto Play (Hạn chế sử dụng) ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
                this.gameManager.AutoPlay();
           // }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}