using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace smallsoldiers.son
{
    class Music
    {
        private SoundEffectInstance soundengine;

        public Music()
        {
            soundengine = null;
            Ressource.PlayTheme("theme01");
        }

        public void Play(string _asset)
        {
            soundengine = Ressource.GetFX(_asset).CreateInstance();
            soundengine.IsLooped = false;
            soundengine.Volume = 0.4f;
            if (soundengine.State == SoundState.Stopped) soundengine.Play();
            else soundengine.Resume();
        }
    }
}
