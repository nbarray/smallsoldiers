using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using smallsoldiers;
using Microsoft.Xna.Framework.Graphics;

namespace smallsoldiers.land
{
    // Classe décrivant les zones de construction
    class Homeland
    {
        private Slot[] slots;
        private Rectangle rect;
        private bool player;

        public Homeland(bool _player)
        {
            player = _player;
            slots = new Slot[6];
            for (int i = 0; i < slots.Length; i++)
            {
                int padding = 32;
                int y = Cons.BUILDING_SIZE * i + i * padding / 2 + padding * 3;
                if (player)
                    slots[i] = new Slot(padding, y);
                else
                    slots[i] = new Slot(Cons.WIDTH - Cons.BUILDING_SIZE - padding, y);
            }

            if (player)
                rect = new Rectangle(0, 0, Cons.HOMELAND_SIZE, Cons.HEIGHT);
            else
                rect = new Rectangle(Cons.WIDTH - Cons.HOMELAND_SIZE, 0, Cons.HOMELAND_SIZE, Cons.HEIGHT);
        }

        public void Update(int _mx, int _my, bool _mpressed)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Update(_mx, _my, _mpressed);   
            }
        }

        public void Draw()
        {
            if (player)
                Ressource.Draw("homelands_nicolas", rect, Color.White, 0.1f);
            else
                Ressource.Draw("homelands_louis", rect, Color.White, 0.1f, SpriteEffects.FlipHorizontally);

                for (int i = 0; i < slots.Length; i++)
                {
                    slots[i].Draw();
                }
        }
    }
}
