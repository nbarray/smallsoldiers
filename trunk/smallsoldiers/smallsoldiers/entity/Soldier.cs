using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    enum act_mode {Move, Attack }

    class Soldier : Entity
    {
        private int dest_x, dest_y;
        private float speed;
        private act_mode mode;
        private Building home;
        protected float pos_x, pos_y;

        public Soldier(string _asset, int _x, int _y, Building _home)
            : base(_asset, 
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE), 
                   Color.White)
        {
            speed = 2;
            dest_x = _x;
            dest_y = _y;
            mode = act_mode.Move;
            home = _home;
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
                    int total_distance = 0;
                    pos_x += 0;
                    pos_y += 0;
                    break;
                default:
                    break;
            }
        }
    }
}
