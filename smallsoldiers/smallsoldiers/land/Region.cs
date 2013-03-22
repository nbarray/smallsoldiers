using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;

namespace smallsoldiers.land
{
    class Region : Entity
    {
        public Region(string _asset, int _x, int _y)
            : base(_asset, new Rectangle(_x, _y, Cons.REGION_SIZE, Cons.REGION_SIZE), Color.White)
        {

        }
    }
}
