using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using smallsoldiers;
using Microsoft.Xna.Framework.Graphics;
using smallsoldiers.entity;

namespace smallsoldiers.land
{
    class Homeland
    {
        private Slot[] slots;
        private Town town;
        private Rectangle rect;
        private bool isPlayer;

        private Player owner;

        public Homeland(bool _isPlayer, Player _p)
        {
            isPlayer = _isPlayer;
            owner = _p;
            town = new Town("building_nicolas", _p);

            slots = new Slot[4];
            for (int i = 0; i < slots.Length; i++)
            {
                int padding = 32;
                int y = Cons.BUILDING_SIZE * i + i * padding / 2 + padding * 3;
                
                if (isPlayer)
                    slots[i] = new Slot(padding, y + (i > 1 ? 96 : 0));
                else
                    slots[i] = new Slot(Cons.WIDTH - Cons.BUILDING_SIZE - padding, y + (i > 1 ? 96 : 0));
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
            if (owner.IsPlayer() || Cons.mode == e_GameMode.multi)
            {
                town.Update(_gameTime, _mx, _my, _mpressed, _rpressed);
                for (int i = 0; i < slots.Length; i++)
                {
                    slots[i].Update(_gameTime, _mx, _my, _mpressed, _rpressed, owner.default_flag);
                }
            }
        }

        public void Update_IA(GameTime _gameTime, Player _p)
        {
            town.Update_IA(_gameTime);
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Update_IA(_gameTime, _p, _p.default_flag);
            }
        }

        public void Draw()
        {
            town.Draw();
            if (isPlayer)
                Ressource.Draw("homelands_nicolas", rect, Color.White, 0.1f);
            else
                Ressource.Draw("homelands_louis", rect, Color.White, 0.1f, SpriteEffects.FlipHorizontally);

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Draw();
            }
            town.Draw();
        }
    }
}
