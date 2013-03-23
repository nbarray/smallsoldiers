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
            home = new Homeland(_player, this);
            home.InitializeSlots();
            player = _player;
            army = new Army();
            default_flag = new Flag("flag_louis");
        }

        public void Update(GameTime _gameTime, Hud _hud, Army _ennemy, int _mx, int _my, bool _mpressed, bool _rpressed)
        {
            if (!player)
                Update_IA(_gameTime, this);
            else
                home.Update(_gameTime, _mx, _my, _mpressed, _rpressed);
            army.Update(_gameTime, _ennemy);
        }
        private void Update_IA(GameTime _gameTime, Player _p)
        {
            home.Update_IA(_gameTime, _p);
        }

        public void Draw()
        {
            home.Draw();
            army.Draw();
        }
    }
}
