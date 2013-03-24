using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    class Arrow : Entity
    {
        private float start_x, start_y, dest_x, dest_y, z, d;
        private float damage;
        private bool dead, right;
        private int speed;
        public bool isdead()
        {
            return dead;
        }

        public Arrow(string _asset, int _x, int _y, int _x2, int _y2, float _damage)
            : base(_asset,
                   new Rectangle(_x, _y, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   Color.White, 0.7f)
        {
            start_x = _x; start_y = _y;
            dest_x = _x2; dest_y = _y2;
            d = Math.Abs(dest_x - start_x);
            right = dest_x > start_x;
            z = 0;
            speed = 2;
            dead = false;
            damage = _damage;
        }

        public void Update(GameTime _gameTime, Army _a)
        {
            if (right)
            {
                rect.X += speed;
                z = (d / 3) * (1 - (1 - 2 * (rect.X - start_x) / d) * (1 - 2 * (rect.X - start_x) / d));

                rect.Y = (int)(start_y - z + (rect.X - start_x) * (dest_y - start_y) / d);
            }
            else
            {
                rect.X -= speed;
                z = (d / 3) * (1 - (1 - 2 * (start_x - rect.X) / d) * (1 - 2 * (start_x - rect.X) / d));

                rect.Y = (int)(start_y - z + (start_x - rect.X) * (dest_y - start_y) / d);
            }

            if (Math.Abs(rect.X - dest_x) < 3 && Math.Abs(rect.X - dest_x) < 3)
            {
                dead = true;
                Soldier hit = _a.get_target(rect.X, rect.Y, 4);
                if (hit != null)
                    hit.do_damage(damage);
            }
        }
    }
}
