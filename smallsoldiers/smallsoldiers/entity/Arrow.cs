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
        private float start_x, start_y, dest_x, dest_y, z, d, pos_x;
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
            speed = 4 * ((float)d / (float)Math.Sqrt(d * d + (start_y - dest_y) * (start_y - dest_y)));
            if (speed == 0)
                speed = (dest_x - start_x) / d * 0.1f;
            pos_x = _x;
            dead = false;
            damage = _damage;
            angle = 0;
            //rect.Width = (int)(1 + (float)Cons.MAN_SIZE * (float)Math.Abs(start_y - dest_y) / d);
            se = (right) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        public void Update(GameTime _gameTime, Army _a)
        {
            if (!sleep)
            {
                #region Movement
                if (right)
                {
                    pos_x += speed;
                    rect.X = (int)pos_x;
                    z = (d / 3) * (1 - (1 - 2 * (rect.X - start_x) / d) * (1 - 2 * (rect.X - start_x) / d));

                    rect.Y = (int)(start_y - z + (rect.X - start_x) * (dest_y - start_y) / d);
                    //angle = (float)Math.Asin(4 * (rect.X - start_x) / d - 1) / 2;
                    if ((rect.X - start_x) < d / 2)
                    {
                        //angle = (float)(Math.Asin((4f / 3f) * (rect.X - start_x) / d - 2f / 3f /*+ (dest_y - start_y) / d*/));
                    }
                    else
                    {
                        //angle = (float)(-Math.Asin((4f / 3f) * (dest_x - rect.X) / d - 2f / 3f /*- (dest_y - start_y) / d*/));
                    }
                    angle = (float)(Math.Asin((dest_y - start_y) / d));
                    //angle = (float)Math.Asin(1);
                }
                else
                {
                    pos_x -= speed;
                    rect.X = (int)pos_x;
                    z = (d / 3) * (1 - (1 - 2 * (start_x - rect.X) / d) * (1 - 2 * (start_x - rect.X) / d));

                    rect.Y = (int)(start_y - z + (start_x - rect.X) * (dest_y - start_y) / d);
                    if ((start_x - rect.X) < d / 2)
                    {
                        angle = (float)(-Math.Asin((4f / 3f) * (start_x - rect.X) / d - 2f / 3f /*- (dest_y - start_y) / d*/));
                    }
                    else
                    {
                        angle = (float)(Math.Asin((4f / 3f) * (rect.X - dest_x) / d - 2f / 3f /*+ (dest_y - start_y) / d*/));
                    }
                    angle -= (float)(Math.Asin((dest_y - start_y) / d));
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

        public override void Draw(bool _isOffset)
        {
            base.Draw(se, (int)z, angle, _isOffset);
        }
    }
}
