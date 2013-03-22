using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.gui;
using smallsoldiers.land;

namespace smallsoldiers
{
    class Player
    {
        private string name;
        private Homeland home;
        private bool player;

        public bool IsPlayer() { return player; }

        public Player(string _name, bool _player)
        {
            name = _name;
            home = new Homeland(_player);
            player = _player;
        }

        public void Update(Hud _hud/*, Army _army*/, int _mx, int _my, bool _mpressed)
        {
            home.Update(_mx, _my, _mpressed, this);
        }

        public void Draw()
        {
            home.Draw();
        }
    }
}
