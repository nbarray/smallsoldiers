using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace smallsoldiers.entity
{
    enum act_mode { Move, Attack, Wait }

    class Soldier : Entity
    {
        private int dest_x, dest_y;
        private float speed;
        private act_mode mode;
        protected float pos_x, pos_y;
        private Flag fanion;

        public Soldier(string _asset, int _x, int _y, Flag _link)
            : base(_asset,
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   Color.White, 0.6f)
        {
            speed = 1.1f;
            pos_x = _x;
            dest_x = _x;
            rect.X = _x;
            pos_y = _y;
            dest_y = _y;
            rect.Y = _y;
            mode = act_mode.Move;
            fanion = _link;
            fanion.add_new_soldier(this);
        }

        public void move_to(int _dest_x, int _dest_y)
        {
            dest_x = _dest_x;
            dest_y = _dest_y;
            mode = act_mode.Move;
        }
        public void go_to_flag()
        {
            Random r = new Random();
            int s_x = ((r.Next(100) % 100) + (r.Next(100) % 100)) / 2 - 50;
            int s_y = ((r.Next(100) % 100) + (r.Next(100) % 100)) / 2 - 50;
            move_to(fanion.get_X()+s_x, fanion.get_Y()+s_y);
        }

        public void Update()
        {
            //move_to(Mouse.GetState().X, Mouse.GetState().Y);
            switch (mode)
            {
                case act_mode.Move:
                    double total_distance = Math.Sqrt((dest_x - pos_x) * (dest_x - pos_x)
                        + (dest_y - pos_y) * (dest_y - pos_y));
                    pos_x += (float)(((dest_x - pos_x) * speed) / total_distance);
                    pos_y += (float)(((dest_y - pos_y) * speed) / total_distance);
                    rect.X = (int)pos_x;
                    rect.Y = (int)pos_y;
                    if (Math.Abs(rect.X - dest_x) < 2 && Math.Abs(rect.Y - dest_y) < 2)
                        mode = act_mode.Wait;
                    break;
                case act_mode.Attack:
                    break;
                default:
                    break;
            }
            depth = 0.5f + ((float)(rect.Y + 32))/10000f;
        }
    }
}
