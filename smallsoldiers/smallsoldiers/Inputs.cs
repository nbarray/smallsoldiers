using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace smallsoldiers
{
    class Inputs
    {
        private MouseState ms;
        private KeyboardState ks;

        private Dictionary<Keys, bool> keys;

        #region Getters

        public int GetX() { return ms.X; }
        public int GetY() { return ms.Y; }

        public bool GetMLpressed() { return ms.LeftButton == ButtonState.Pressed; }
        public bool GetMLreleased() { return ms.LeftButton == ButtonState.Released; }

        public bool GetMRpressed() { return ms.RightButton == ButtonState.Pressed; }
        public bool GetMRreleased() { return ms.RightButton == ButtonState.Released; }

        public bool GetIsPressed(Keys _k) { return ks.IsKeyDown(_k); }

        // i = { 1 si enfoncée, -1 sinon (Au moment du relachement de la touche, i = 0) }
        public void GetIsUPressed(Keys _k, ref int _i)
        {
            if (!keys[_k] && ks.IsKeyDown(_k))
            {
                keys[_k] = true;
                _i = 1; // Touche enfoncée
            }
            else if (keys[_k] && ks.IsKeyUp(_k))
            {
                keys[_k] = false;
                _i = 0; // Touche dé(en)foncée
            }

            _i = -1; // Touche toujours enfoncée
        }

        #endregion

        public Inputs()
        {
            ms = default(MouseState);
            ks = default(KeyboardState);
            keys = new Dictionary<Keys, bool>();
            LoadKeys();
        }

        private void LoadKeys() 
        {
            keys.Add(Keys.Z, false);
            keys.Add(Keys.Q, false);
            keys.Add(Keys.S, false);
            keys.Add(Keys.D, false);

            keys.Add(Keys.Space, false);
            keys.Add(Keys.Enter, false);
            keys.Add(Keys.Back, false);
            keys.Add(Keys.Escape, false);

            keys.Add(Keys.NumPad1, false);
            keys.Add(Keys.NumPad2, false);
            keys.Add(Keys.NumPad3, false);
            keys.Add(Keys.NumPad4, false);
        }

        public void Update(MouseState _ms, KeyboardState _ks)
        {
            ms = _ms;
            ks = _ks;
        }        
    }
}
