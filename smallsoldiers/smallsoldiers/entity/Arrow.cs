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
            angle = 0;
        }

        public void Update(GameTime _gameTime, Army _a, bool write)
        {
            if (!sleep)
            {
                if (right)
                {
                    rect.X += speed;
                    z = (d / 3) * (1 - (1 - 2 * (rect.X - start_x) / d) * (1 - 2 * (rect.X - start_x) / d));

                    rect.Y = (int)(start_y - z + (rect.X - start_x) * (dest_y - start_y) / d);
                    angle = (float)Math.Asin(4 * (rect.X - start_x) / d - 1) / 2;
                    if ((rect.X - start_x) < d / 2)
                    {
                        angle = (float)(/*Math.PI / 2 - */Math.Asin((8f / 6f) * (rect.X - start_x) / d - 2f / 3f));
                    }
                    else
                    {
                        angle = (float)(- Math.Asin((8f / 6f) * (dest_x - rect.X) / d - 2f / 3f));
                    }
                }
                else
                {
                    rect.X -= speed;
                    z = (d / 3) * (1 - (1 - 2 * (start_x - rect.X) / d) * (1 - 2 * (start_x - rect.X) / d));

                    rect.Y = (int)(start_y - z + (start_x - rect.X) * (dest_y - start_y) / d);
                    if ((start_x - rect.X) < d / 2)
                    {
                        angle = (float)(/*Math.PI / 2 - */-Math.Asin((8f / 6f) * (start_x - rect.X) / d - 2f / 3f));
                    }
                    else
                    {
                        angle = (float)(Math.Asin((8f / 6f) * (rect.X - dest_x) / d - 2f / 3f));
                    }
                }


                if (Math.Abs(rect.X - dest_x) < 3 && Math.Abs(rect.Y - dest_y) < 3)
                {
                    sleep = true;
                    Soldier hit = _a.get_target(rect.X, rect.Y, Cons.MAN_SIZE / 4);
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
                if (d > 3000)
                {
                    dead = true;
                }
            }
        }

        public override void Draw()
        {
            if (right)
            {
                Ressource.Draw(asset, rect, source, Color.White, depth, SpriteEffects.None, angle);
                //Ressource.Draw(asset, rect, source, Color.White, depth, SpriteEffects.None);
            }
            else
                Ressource.Draw(asset, rect, source, Color.White, depth, SpriteEffects.FlipHorizontally, angle);
        }
    }
}
