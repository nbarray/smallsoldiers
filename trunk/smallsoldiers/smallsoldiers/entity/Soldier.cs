using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    class Soldier : Entity
    {
        public Soldier(string _asset)
            : base(_asset, 
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE), 
                   Color.White)
        {

        }
    }
}
