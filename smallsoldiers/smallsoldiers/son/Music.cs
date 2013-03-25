﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace smallsoldiers.son
{
    class Music
    {
        private SoundEffectInstance soundengine;

        public Music()
        {
            soundengine = null;
            Ressource.PlayTheme("theme01");
            MediaPlayer.IsRepeating = true;
        }

        public void Play(string _asset)
        {
            soundengine = Ressource.GetFX(_asset).CreateInstance();
            soundengine.IsLooped = false;
            soundengine.Volume = 0.1f;
            if (soundengine.State == SoundState.Stopped) soundengine.Play();
            else soundengine.Resume();
        }

        public void UnloadInstance()
        {
            if(soundengine != null)
            soundengine.Dispose();
        }
    }
}
