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
        private float start_x, start_y, dest_x, dest_y, z, d;
        private float damage, angle;
        private bool dead, right, sleep;
        private float speed;
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
            d = Math.Abs(dest_x - start_x);
            right = dest_x > start_x;
            z = (d / 3) * (1 - (1 - 2 * (rect.X - start_x) / d) * (1 - 2 * (rect.X - start_x) / d)); ;
            speed = 4 * (float)(d / Math.Sqrt(d * d + (start_y - dest_y) * (start_y - dest_y)));
            dead = false;
            damage = _damage;
            angle = 0;
            se = (right) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        public void Update(GameTime _gameTime, Army _a)
        {
            if (!sleep)
            {
                #region Movement
                if (right)
                {
                    rect.X += (int)speed;
                    z = (d / 3) * (1 - (1 - 2 * (rect.X - start_x) / d) * (1 - 2 * (rect.X - start_x) / d));

                    rect.Y = (int)(start_y - z + (rect.X - start_x) * (dest_y - start_y) / d);
                    angle = (float)Math.Asin(4 * (rect.X - start_x) / d - 1) / 2;
                    if ((rect.X - start_x) < d / 2)
                    {
                        angle = (float)(Math.Asin((8f / 6f) * (rect.X - start_x) / d - 2f / 3f /*+ (dest_y - start_y) / d*/));
                    }
                    else
                    {
                        angle = (float)(-Math.Asin((8f / 6f) * (dest_x - rect.X) / d - 2f / 3f /*+ (dest_y - start_y) / d*/));
                    }
                }
                else
                {
                    rect.X -= (int)speed;
                    z = (d / 3) * (1 - (1 - 2 * (start_x - rect.X) / d) * (1 - 2 * (start_x - rect.X) / d));

                    rect.Y = (int)(start_y - z + (start_x - rect.X) * (dest_y - start_y) / d);
                    if ((start_x - rect.X) < d / 2)
                    {
                        angle = (float)(-Math.Asin((8f / 6f) * (start_x - rect.X) / d - 2f / 3f/*+(dest_y - start_y) / d*/));
                    }
                    else
                    {
                        angle = (float)(Math.Asin((8f / 6f) * (rect.X - dest_x) / d - 2f / 3f/*+(dest_y - start_y) / d*/));
                    }
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
                    d = 0;
                    depth = 0.5f + ((float)(rect.Y + 32)) / 10000f;
                }
            }
            else
            {
                d += _gameTime.ElapsedGameTime.Milliseconds;
                if (d > 7000)
                {
                    dead = true;
                }
            }
        }

        public override void Draw()
        {
            base.Draw(se, (int)z, angle);
        }
    }
}
