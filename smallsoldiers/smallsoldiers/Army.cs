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
        private List<Arrow> arrows;

        public Army(bool _isplayer)
        {
            soldiers = new List<Soldier>();
            arrows = new List<Arrow>();
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
        public void Add_arrows(Arrow _a)
        {
            arrows.Add(_a);
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
            for (int i = soldiers.Count - 1; i > -1; i--)
            {
                if (soldiers[i].to_clear())
                    soldiers.RemoveAt(i);
                else
                    soldiers[i].Update(_gameTime, this, _ennemy, _soundengine);
            }
            for (int i = arrows.Count - 1; i >= 0; i--)
            {
                if (arrows[i].isdead())
                    arrows.RemoveAt(i);
                else
                    arrows[i].Update(_gameTime, _ennemy);
            }
        }

        public void make_pushes(ref List<Point> _push)
        {
            _push = new List<Point>();
            foreach (Soldier item in soldiers)
            {
                if (item.can_be_pushed())
                    _push.Add(new Point(item.get_X(), item.get_Y()));
            }
        }
        public void push(List<Point> _push)
        {
            foreach (Point p in _push)
            {
                foreach (Soldier item in soldiers)
                {
                    if (item.can_be_pushed())
                    {
                        int dx = 0;
                        int dy = 0;
                        if (item.get_X() - p.X != 0)
                        {
                            dx = 5 / (item.get_X() - p.X);
                        }
                        if (item.get_Y() - p.Y != 0)
                        {
                            dy = 5 / (item.get_Y() - p.Y);
                        }
                        item.push((float)dx / 10.0f, (float)dy / 10.0f);
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (Soldier item in soldiers)
            {
                item.Draw(true);
            }
            foreach (Arrow item in arrows)
            {
                item.Draw(true);
            }
        }

        public Soldier get_target(int _x, int _y, int _dist)
        {
            Soldier target = null;
            int min = _dist;
            foreach (Soldier item in soldiers)
            {
                if (item.dist_from_a_point(_x, _y) < min && item.dist_from_a_point(_x, _y) > 1)
                {
                    target = item;
                    min = (int)item.dist_from_a_point(_x, _y);
                }
            }
            return target;
        }
        public Soldier get_target(int _x, int _y, int _dist, int _mindist)
        {
            Soldier target = null;
            int min = _dist;
            foreach (Soldier item in soldiers)
            {
                if (item.dist_from_a_point(_x, _y) < min && item.dist_from_a_point(_x, _y) > _mindist)
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
