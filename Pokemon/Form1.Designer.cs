namespace Pokemon
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Hutao = new ImageList(components);
            imgIcon = new ImageList(components);
            SuspendLayout();
            // 
            // Hutao
            // 
            Hutao.ColorDepth = ColorDepth.Depth16Bit;
            Hutao.ImageStream = (ImageListStreamer)resources.GetObject("Hutao.ImageStream");
            Hutao.TransparentColor = Color.Transparent;
            Hutao.Images.SetKeyName(0, "hutao.jpg");
            Hutao.Images.SetKeyName(1, "hutao2.jpg");
            Hutao.Images.SetKeyName(2, "hutao3.jpg");
            // 
            // imgIcon
            // 
            imgIcon.ColorDepth = ColorDepth.Depth32Bit;
            imgIcon.ImageStream = (ImageListStreamer)resources.GetObject("imgIcon.ImageStream");
            imgIcon.TransparentColor = Color.Transparent;
            imgIcon.Images.SetKeyName(0, "1.png");
            imgIcon.Images.SetKeyName(1, "2.png");
            imgIcon.Images.SetKeyName(2, "3.png");
            imgIcon.Images.SetKeyName(3, "4.png");
            imgIcon.Images.SetKeyName(4, "5.png");
            imgIcon.Images.SetKeyName(5, "6.png");
            imgIcon.Images.SetKeyName(6, "7.png");
            imgIcon.Images.SetKeyName(7, "8.png");
            imgIcon.Images.SetKeyName(8, "9.png");
            imgIcon.Images.SetKeyName(9, "10.png");
            imgIcon.Images.SetKeyName(10, "11.png");
            imgIcon.Images.SetKeyName(11, "12.png");
            imgIcon.Images.SetKeyName(12, "13.png");
            imgIcon.Images.SetKeyName(13, "14.png");
            imgIcon.Images.SetKeyName(14, "15.png");
            imgIcon.Images.SetKeyName(15, "16.png");
            imgIcon.Images.SetKeyName(16, "17.png");
            imgIcon.Images.SetKeyName(17, "18.png");
            imgIcon.Images.SetKeyName(18, "19.png");
            imgIcon.Images.SetKeyName(19, "20.png");
            imgIcon.Images.SetKeyName(20, "21.png");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1382, 1055);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private ImageList imgIcon;
        //private ImageList Display;
        private ImageList Hutao;
    }
}