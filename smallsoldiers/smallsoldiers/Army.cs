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
            if (soldiers.Count < 25)
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

        public void Update(GameTime _gameTime, Army _ennemy)
        {
            for (int i = soldiers.Count-1; i >-1; i--)
            {
                if (soldiers[i].isdead())
                    soldiers.RemoveAt(i);
                else
                    soldiers[i].Update(_gameTime, this, _ennemy);
            }
        }

        public void Draw()
        {
            foreach (Soldier item in soldiers)
            {
                item.Draw();
            }
        }

        public Soldier get_target(int _x, int _y, int _dist)
        {
            Soldier target = null;
            int min = _dist;
            foreach (Soldier item in soldiers)
            {
                if (item.dist_from_a_point(_x, _y) < min && item.dist_from_a_point(_x, _y)>1)
                {
                    target = item;
                    min = (int)item.dist_from_a_point(_x, _y);
                }
            }
            return target;
        }
    }
}
