using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Lib
    {
        public static int PikachuHeight = 70;
        public static int PikachuWidth = 70;
        public static int numPikachuRow = 18;
        public static int numPikachuCol = 12;

        public static Color BackColorPikachu = Color.Transparent;
        public static Color ChoosePikachu = Color.FromArgb(57, 249, 0);

        public static Color ColorLine = Color.Red;

        public static string BackgroundSoundPath = Application.StartupPath + "\\sound\\nhacnen.wav";
        public static string ClickSoundPath = Application.StartupPath + "\\sound\\click.wav";
        public static string WinSoundPath = Application.StartupPath + "\\sound\\win.wav";
        public static string ClickButtonSoundPath = Application.StartupPath + "\\sound\\clickbutton.wav";
        public static string NoEatSoundPath = Application.StartupPath + "\\sound\\noeat.wav";

        public static bool IsOpenSound = true;

        public static Pokemon temp = null!;
        public static bool IsGetEvent = false;
    }
}
