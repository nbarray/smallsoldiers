using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using smallsoldiers;

namespace smallsoldiers.land
{
    // Classe décrivant les zones de construction
    class Homeland
    {
        private Slot[] slots;
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
        }

        public void Update()
        {
            MouseState mstate = Mouse.GetState();
            int mx = mstate.X;
            int my = mstate.Y;
            bool mpressed = mstate.LeftButton == ButtonState.Pressed;
            bool mreleased = mstate.LeftButton == ButtonState.Released;

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Update(mx, my, mpressed);   
            }
        }

        public void Draw()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Draw();
            }
        }
    }
}
