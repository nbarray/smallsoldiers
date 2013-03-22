using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;

namespace smallsoldiers
{
    class Army
    {
        public List<Soldier> soldiers;

        public Army()
        {
            soldiers = new List<Soldier>();
        }

        public bool Add_soldier(Soldier _s)
        {
            if (soldiers.Count < 10)
            {
                soldiers.Add(_s);
                soldiers[soldiers.Count - 1].go_to_flag();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update(GameTime _gameTime)
        {
            foreach (Soldier item in soldiers)
            {
                item.Update(_gameTime);
            }
        }

        public void Draw()
        {
            foreach (Soldier item in soldiers)
            {
                item.Draw();
            }
        }
    }
}
