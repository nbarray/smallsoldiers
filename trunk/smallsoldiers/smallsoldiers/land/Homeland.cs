using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using smallsoldiers;
using Microsoft.Xna.Framework.Graphics;
using smallsoldiers.entity;
using smallsoldiers.gui;

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
            town = new Town("town_louis", _p, _isPlayer);

            slots = new Slot[4];
            for (int i = 0; i < slots.Length; i++)
            {
                int padding = 32;
                int y = Cons.BUILDING_SIZE * i + i * padding / 2 + padding * 3;

                slots[i] = new Slot(Cons.WIDTH - Cons.BUILDING_SIZE - padding, y + (i > 1 ? 96 : 0));
            }

            if (isPlayer)
            {
                rect = new Rectangle(0, 0, Cons.HOMELAND_SIZE, Cons.HEIGHT);
                slots[0] = new Slot(100, 40);
                slots[1] = new Slot(300, 40);
                slots[2] = new Slot(100, Cons.HEIGHT - 150);
                slots[3] = new Slot(300, Cons.HEIGHT - 150);
            }
            else
            {
                rect = new Rectangle(Cons.BATTLEFIELD_SIZE + Cons.HOMELAND_SIZE, 0, Cons.HOMELAND_SIZE, Cons.HEIGHT);
                slots[0] = new Slot(Cons.MAP_WIDTH - Cons.BUILDING_SIZE - 100, 40);
                slots[1] = new Slot(Cons.MAP_WIDTH - Cons.BUILDING_SIZE - 300, 40);
                slots[2] = new Slot(Cons.MAP_WIDTH - Cons.BUILDING_SIZE - 100, Cons.HEIGHT - 150);
                slots[3] = new Slot(Cons.MAP_WIDTH - Cons.BUILDING_SIZE - 300, Cons.HEIGHT - 150);
            }
        }

        public void InitializeSlots()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetOwner(owner);
            }
        }

        public void Update(GameTime _gameTime, Inputs _inputs, Hud _hud)
        {
            if (owner.IsPlayer() || Cons.mode == e_GameMode.multi)
            {
                town.Update(_gameTime, _inputs);
                for (int i = 0; i < slots.Length; i++)
                {
                    slots[i].Update(_gameTime, _inputs, owner.default_flag, _hud);
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
            town.Draw(true);
            if (isPlayer)
                Ressource.Draw("new_homeland", rect, Color.White, 0.1f, true);
            else
                Ressource.Draw("new_homeland", rect, Color.White, 0.1f, SpriteEffects.FlipHorizontally, true);

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Draw();
            }
        }
    }
}
