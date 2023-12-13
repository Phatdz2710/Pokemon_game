using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Sound
    {

        public SoundPlayer SoundGame { get; set; }

        public Sound(string soundPath)
        {
            SoundGame = new SoundPlayer(soundPath);

        }

        public void PlaySound()
        {
            if (Lib.IsOpenSound)
            {
                SoundGame.Play();
            }
        }

        public void StopSound()
        {
            SoundGame.Stop();

        }



    }
}
