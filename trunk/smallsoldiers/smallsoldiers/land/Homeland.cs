﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using smallsoldiers;
using Microsoft.Xna.Framework.Graphics;

namespace smallsoldiers.land
{
    class Homeland
    {
        private Slot[] slots;
        private Rectangle rect;
        private bool isPlayer;

        private Player owner;

        public Homeland(bool _isPlayer, Player _p)
        {
            isPlayer = _isPlayer;
            owner = _p;

            slots = new Slot[6];
            for (int i = 0; i < slots.Length; i++)
            {
                int padding = 32;
                int y = Cons.BUILDING_SIZE * i + i * padding / 2 + padding * 3;
                if (isPlayer)
                    slots[i] = new Slot(padding, y);
                else
                    slots[i] = new Slot(Cons.WIDTH - Cons.BUILDING_SIZE - padding, y);
            }

            if (isPlayer)
                rect = new Rectangle(0, 0, Cons.HOMELAND_SIZE, Cons.HEIGHT);
            else
                rect = new Rectangle(Cons.WIDTH - Cons.HOMELAND_SIZE, 0, Cons.HOMELAND_SIZE, Cons.HEIGHT);
        }

        public void InitializeSlots()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetOwner(owner);
            }
        }

        public void Update(GameTime _gameTime, int _mx, int _my, bool _mpressed, bool _rpressed)
        {
            if(owner.IsPlayer())
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Update(_gameTime, _mx, _my, _mpressed, _rpressed, owner);
            }
        }

        public void Draw()
        {
            if (isPlayer)
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
