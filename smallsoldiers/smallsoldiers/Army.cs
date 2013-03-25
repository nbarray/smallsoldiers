using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;
using smallsoldiers.son;
using smallsoldiers.land;

namespace smallsoldiers
{
    class Army
    {
        public List<Soldier> soldiers;
        private bool isPlayer;

        public Army(bool _isplayer)
        {
            soldiers = new List<Soldier>();
            isPlayer = _isplayer;
        }

        public bool Add_soldier(Soldier _s, bool _blindness)
        {
            if (soldiers.Count < Cons.test_max_pop)
            {
                soldiers.Add(_s);
                soldiers[soldiers.Count - 1].go_to_flag(_blindness);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int HowMany(Rectangle _r)
        {
            int temp = 0;
            for (int n = 0; n < soldiers.Count; n++)
            {
                temp += (_r.Contains(soldiers[n].get_X(), soldiers[n].get_Y()) ? 1 : 0);
            }

            return temp;
        }

        public void Update(GameTime _gameTime, Army _ennemy, Music _soundengine)
        {
            for (int i = soldiers.Count-1; i > -1; i--)
            {
                if (soldiers[i].isdead())
                    soldiers.RemoveAt(i);
                else
                    soldiers[i].Update(_gameTime, this, _ennemy, _soundengine);
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
        public Soldier get_target_to_heal(int _x, int _y, int _dist)
        {
            Soldier target = null;
            int min = _dist;
            foreach (Soldier item in soldiers)
            {
                if (item.is_healable() && item.dist_from_a_point(_x, _y) < min && item.dist_from_a_point(_x, _y) > 1)
                {
                    target = item;
                    min = (int)item.dist_from_a_point(_x, _y);
                }
            }
            return target;
        }
    }
}
