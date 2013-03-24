using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace smallsoldiers.entity
{
    enum act_mode { Move, Attack, Wait }
    enum sold_type { Fighter, Ranger, Healer }

    class Soldier : Entity
    {
        private int dest_x, dest_y, range;
        private float speed, life, maxlife, armor, damage;
        private act_mode mode;
        protected float pos_x, pos_y;
        private Flag fanion;
        private Random r;
        private Soldier target;
        private SpriteEffects se;
        private bool dead;
        private sold_type type;

        private Animation walk_anim, attack_anim;
        public int get_Y()
        {
            return rect.Y;
        }
        public int get_X()
        {
            return rect.X;
        }
        public bool isdead()
        {
            return dead;
        }
        public void do_damage(float _d)
        {
            if (_d < 0)
                life = Math.Min(maxlife, life - _d);
            else
                life = (_d > armor) ? life - _d + armor : life;
            dead = life <= 0;
        }
        public bool is_healable()
        {
            return life < maxlife;
        }

        public Soldier(string _asset, sold_type _t, int _x, int _y, Flag _link)
            : base(_asset,
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   Color.White, 0.6f)
        {
            type = _t;
            r = new Random();
            switch (type)
            {
                case sold_type.Ranger:
                    speed = 1.3f;
                    armor = 0;
                    maxlife = 20;
                    range = 320;
                    damage = 1;
                    break;
                case sold_type.Healer:
                    speed = 1.2f;
                    armor = 0;
                    maxlife = 20;
                    range = 96;
                    damage = -0.9f;
                    break;
                default:
                    speed = 1.1f;
                    armor = 0.5f;
                    maxlife = 30;
                    range = 32;
                    damage = 1.1f;
                    break;
            }
            life = maxlife;
            pos_x = _x;
            dest_x = _x;
            rect.X = _x;
            pos_y = _y;
            dest_y = _y;
            rect.Y = _y;
            mode = act_mode.Move;
            fanion = _link;
            fanion.add_new_soldier(this);
            target = null;
            se = SpriteEffects.None;
            dead = false;

            walk_anim = new Animation(asset, new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE), 6, 0, depth, false);
            attack_anim = new Animation(asset, new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE), 6, 7, depth, false);
        }

        public void move_to(int _dest_x, int _dest_y)
        {
            dest_x = _dest_x;
            dest_y = _dest_y;
            mode = act_mode.Move;
        }
        public void go_to_flag()
        {
            int s_x = ((r.Next(1000) % 100) + (r.Next(1000) % 100)) / 2 - 50;
            int s_y = ((r.Next(1000) % 100) + (r.Next(1000) % 100)) / 2 - 50;
            move_to(fanion.get_X() + s_x, fanion.get_Y() + s_y);
        }

        public void Update(GameTime _gameTime, Army _allies, Army _ennemies)
        {
            //move_to(Mouse.GetState().X, Mouse.GetState().Y);
            int detect_ennemy = (type != sold_type.Ranger) ? (range * 3) / 2 : range / 2;
            switch (mode)
            {
                case act_mode.Move:
                    #region move
                    walk_anim.Update(_gameTime);
                    double total_distance = Math.Sqrt((dest_x - pos_x) * (dest_x - pos_x)
                        + (dest_y - pos_y) * (dest_y - pos_y));
                    pos_x += (float)(((dest_x - pos_x) * speed) / total_distance);
                    pos_y += (float)(((dest_y - pos_y) * speed) / total_distance);
                    se = (rect.X < dest_x - 1) ? SpriteEffects.None : se;
                    se = (rect.X > dest_x + 1) ? SpriteEffects.FlipHorizontally : se;
                    rect.X = (int)pos_x;
                    rect.Y = (int)pos_y;
                    if (Math.Abs(rect.X - dest_x) < 2 && Math.Abs(rect.Y - dest_y) < 2)
                        mode = act_mode.Wait;
                    #endregion
                    goto default;
                case act_mode.Attack:
                    #region Attack
                    if (target != null && !target.isdead())
                    {
                        if (target.dist_from_a_point(rect.X, rect.Y) < range)
                        {
                            se = (rect.X < target.get_X()) ? SpriteEffects.None
                                : SpriteEffects.FlipHorizontally;
                            if (type == sold_type.Fighter && target.get_Y() < rect.Y)
                            {
                                rect.Y--;
                                pos_y--;
                            }
                            else
                            {
                                if (attack_anim.Update(_gameTime))
                                    target.do_damage(damage);
                            }
                        }
                        else
                        {
                            if (type != sold_type.Ranger)
                                move_to(target.get_X(), target.get_Y());
                        }
                    }
                    else
                    {
                        target = null;
                        go_to_flag();
                    }
                    #endregion
                    break;
                case act_mode.Wait:
                    detect_ennemy = (type != sold_type.Ranger) ? range*3 : range;
                    goto default;
                default:
                    if (type == sold_type.Healer)
                        target = _allies.get_target_to_heal(rect.X, rect.Y, detect_ennemy);
                    else
                        target = _ennemies.get_target(rect.X, rect.Y, detect_ennemy);
                    if (target != null && target != this)
                        set_attack_on(target);
                    break;
            }
            depth = 0.5f + ((float)(rect.Y + 32)) / 10000f;
        }

        public override void Draw()
        {
            Ressource.Draw("pixel", new Rectangle(rect.X, rect.Y - 8, 32, 4), Color.LightGray, Cons.DEPTH_HUD);
            Ressource.Draw("pixel", new Rectangle(rect.X, rect.Y - 8, (int)(life * 32 / maxlife), 4), Color.Green, Cons.DEPTH_HUD + 0.01f);

            switch (mode)
            {
                case act_mode.Move:
                    walk_anim.Draw(rect, se);
                    break;
                case act_mode.Attack:
                    attack_anim.Draw(rect, se);
                    break;
                case act_mode.Wait:
                    base.Draw(se);
                    break;
                default:
                    break;
            }
        }

        public double dist_from_a_point(int _x, int _y)
        {
            return Math.Sqrt((_x - rect.X) * (_x - rect.X)
                + (_y - rect.Y) * (_y - rect.Y));
        }

        public void set_attack_on(Soldier _s)
        {
            mode = act_mode.Attack;
            target = _s;
        }

    }
}
