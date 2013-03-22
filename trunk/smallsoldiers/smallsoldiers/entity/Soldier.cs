using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    enum act_mode { Move, Attack }

    class Soldier : Entity
    {
        private int dest_x, dest_y;
        private float speed;
        private act_mode mode;
        protected float pos_x, pos_y;

        public Soldier(string _asset, int _x, int _y)
            : base(_asset,
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   Color.White)
        {
            speed = 2;
            dest_x = _x;
            dest_y = _y;
            mode = act_mode.Move;
        }

        public void move_to(int _dest_x, int _dest_y)
        {
            dest_x = _dest_x;
            dest_y = _dest_y;
        }

        public void Update()
        {
            switch (mode)
            {
                case act_mode.Move:
                    double total_distance = Math.Sqrt((dest_x - pos_x) * (dest_x - pos_x)
                        + (dest_y - pos_y) * (dest_y - pos_y));
                    pos_x += (float)(((dest_x - pos_x) * speed) / total_distance);
                    pos_y += (float)(((dest_y - pos_y) * speed) / total_distance);
                    break;
                default:
                    break;
            }
        }
    }
}
