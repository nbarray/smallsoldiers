using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    class Building : Entity
    {
        public Soldier model;
        private Flag fanion;

        public Building(string _asset)
            : base(_asset,
                   new Rectangle(0, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   new Rectangle(0, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   Color.White, 0.4f)
        {
            fanion = new Flag("flag_louis");
            //model = new Soldier("fighter_louis", 50, 75, fanion);
            //model.move_to(Cons.WIDTH / 2, Cons.HEIGHT / 2);
        }

        public void Update()
        {
            //model.Update();
        }

        public void set_new_flag_pos(int _x, int _y)
        {
            fanion.set_new_pos(_x, _y);
        }
    }
}
