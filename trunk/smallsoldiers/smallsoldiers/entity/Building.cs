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

        public Building(string _asset)
            : base(_asset,
                   new Rectangle(0, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   new Rectangle(0, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   Color.White, 0.4f)
        {
            model = new Soldier("fighter_louis", 50, 75);
            model.move_to(Cons.WIDTH / 2, Cons.HEIGHT / 2);
        }

        public void Update()
        {
            model.Update();
        }
    }
}
