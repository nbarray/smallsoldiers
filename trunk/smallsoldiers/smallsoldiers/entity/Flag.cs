using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    class Flag : Entity
    {
        private List<Soldier> soldiers_linked;

        public Flag(string _asset)
            : base(_asset,
                   new Rectangle(Cons.WIDTH/2, Cons.HEIGHT/2, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   new Rectangle(0, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   Color.White, 0.5f)
        {
            soldiers_linked = new List<Soldier>();
        }

        public void add_new_soldier(Soldier _s)
        {
            soldiers_linked.Add(_s);
        }

        public void set_new_pos(int _x, int _y)
        {
            rect.X = _x;
            rect.Y = _y;
            foreach (Soldier item in soldiers_linked)
            {
                item.move_to(_x, _y);
            }
        }
    }
}
