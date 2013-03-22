using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.gui;
using smallsoldiers.land;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;

namespace smallsoldiers
{
    class Player
    {
        private string name;
        private Homeland home;
        private bool player;
        public Army army;
        private Flag default_flag;

        public bool IsPlayer() { return player; }

        public Player(string _name, bool _player)
        {
            name = _name;
            home = new Homeland(_player);
            player = _player;
            army = new Army();
            default_flag = new Flag("flag_louis");
        }

        public void Update(GameTime _gameTime, Hud _hud, int _mx, int _my, bool _mpressed, bool _rpressed)
        {
            army.Update(_gameTime);
            home.Update(_mx, _my, _mpressed, _rpressed, this);
        }

        public void Draw()
        {
            home.Draw();
            army.Draw();
        }
    }
}
