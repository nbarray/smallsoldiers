using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace smallsoldiers.entity
{
    class Arrow : Entity
    {
        private float start_x, start_y, dest_x, dest_y, ya, yb, x, dx, dy;
        private float damage, angle, speed, maxhigh;
        private int North, East;
        private bool dead, sleep;
        private SpriteEffects se;
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
            dx = Math.Abs(dest_x - start_x);
            dy = Math.Abs(dest_y - start_y);
            x = 0; ya = 0; yb = 0;
            East = dest_x > start_x ? 1 : -1;
            North = dest_y > start_y ? 1 : -1;
            speed = 2 * (float)((dx + 1) / Math.Sqrt(dx * dx + dy * dy));
            maxhigh = dx / 3;
            dead = false;
            damage = _damage;
            angle = 0;
            //rect.Width = (int)(1 + (float)Cons.MAN_SIZE * (float)Math.Abs(start_y - dest_y) / d);
            se = SpriteEffects.None;
            //se = (East > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        public void Update(GameTime _gameTime, Army _a)
        {
            if (!sleep)
            {
                #region Movement
                x += speed;
                ya = x * (dy + 1) / (dx + 1);
                yb = ((dx + 1) * (dx + 1) - (2 * x - dx) * (2 * x - dx)) * maxhigh / ((dx + 1) * (dx + 1));
                rect.X = (int)(start_x + East * x);
                rect.Y = (int)(start_y + North * ya - yb);
                angle = North * (float)Math.Asin((dy + 1) / (dx + 1));
                angle -= (float)Math.Asin((4 * dx - 6 * x) * maxhigh / ((dx + 1) * (dx + 1)));
                if (East<0)
                {
                    angle *= -1;
                    angle += (float)Math.PI;
                }
                #endregion

                if (Math.Abs(rect.X - dest_x) < 4 && Math.Abs(rect.Y - dest_y) < 4)
                {
                    sleep = true;
                    Soldier hit = _a.get_target(rect.X, rect.Y, Cons.MAN_SIZE / 2);
                    if (hit != null)
                    {
                        hit.do_damage(damage);
                        dead = true;
                    }
                    dx = 0;
                    depth = 0.5f + ((float)(rect.Y + 32)) / 10000f;
                }
            }
            else
            {
                dx += _gameTime.ElapsedGameTime.Milliseconds;
                if (dx > 7000)
                {
                    dead = true;
                }
            }
        }

        public override void Draw(bool _isOffset)
        {
            base.Draw(se, (int)yb, angle, _isOffset);
        }
    }
}
