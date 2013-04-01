using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using smallsoldiers.son;

namespace smallsoldiers.entity
{
    enum act_mode { Move, Attack, Wait }
    enum sold_type { Fighter, Ranger, Healer }

    class Soldier : Entity
    {
        private int dest_x, dest_y, range, accuracy;
        private float speed, life, maxlife, armor, damage, mana, maxmana;
        private act_mode mode;
        protected float pos_x, pos_y;
        private Flag fanion;
        private Random r;
        private Soldier target;
        private SpriteEffects se;
        private bool dead, blind;
        private sold_type type;
        private float elapsed_time;

        private Animation walk_anim, attack_anim, death_anim;

        public int get_Y()
        {
            return rect.Y;
        }
        public int get_X()
        {
            return rect.X;
        }
        public act_mode get_mode()
        {
            return mode;
        }
        public bool can_be_pushed()
        {
            return mode == act_mode.Wait ||
                (mode == act_mode.Attack && type == sold_type.Ranger);
        }
        public void push(float _x, float _y)
        {
            pos_x += _x;
            pos_y += _y;
            rect.X = (int)pos_x;
            rect.Y = (int)pos_y;
        }
        public bool isdead()
        {
            return dead;
        }
        public bool to_clear()
        {
            return dead && death_anim.ended();
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
        public void set_flag(Flag _new)
        {
            fanion = _new;
        }

        public string GetAsset() { return base.asset; }
        public sold_type GetSoldierType() { return type; }


        public Soldier(string _asset, sold_type _t, int _x, int _y, Flag _link, int _level)
            : base(_asset,
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   new Rectangle(0, Cons.MAN_SIZE * (_level - 1), Cons.MAN_SIZE, Cons.MAN_SIZE),
                   Color.White, 0.6f)
        {
            #region Carac
            type = _t;
            r = new Random();
            switch (type)
            {
                case sold_type.Ranger:
                    speed = 1.2f + 0.1f * _level;
                    armor = 0.3f * _level;
                    maxlife = 20 + 2 * _level;
                    range = 320;
                    damage = 6 + _level;
                    maxmana = 0;
                    break;
                case sold_type.Healer:
                    speed = 1.2f + 0.05f * _level;
                    armor = 0.2f * _level;
                    maxlife = 20 + 3 * _level;
                    range = 96;
                    damage = -2 - _level;
                    maxmana = 100;
                    break;
                default:
                    speed = 1.1f + 0.05f * _level;
                    armor = 0.5f * _level;
                    maxlife = 30 + 3 * _level;
                    range = 32;
                    damage = 4f + _level;
                    maxmana = 0;
                    break;
            }
            life = maxlife;
            mana = maxmana;
            accuracy = 9 + _level;
            #endregion
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
            blind = false;
            elapsed_time = 0f;

            walk_anim = new Animation(asset, new Rectangle(0, Cons.MAN_SIZE * (_level - 1),
                Cons.MAN_SIZE, Cons.MAN_SIZE), 6, 0, depth, false);
            death_anim = new Animation(asset, new Rectangle(0, Cons.MAN_SIZE * (_level - 1),
                Cons.MAN_SIZE, Cons.MAN_SIZE), 3, 13, depth, true);
            attack_anim = new Animation(asset, new Rectangle(0, Cons.MAN_SIZE * (_level - 1),
                Cons.MAN_SIZE, Cons.MAN_SIZE), 7, 6, depth,
                false, (type == sold_type.Ranger));
        }

        public void move_to(int _dest_x, int _dest_y)
        {
            dest_x = _dest_x;
            dest_y = _dest_y;
            mode = act_mode.Move;
        }
        public void go_to_flag(bool _blindness)
        {
            if (dist_from_a_point(fanion.get_X(), fanion.get_Y()) > 50)
            {
                int s_x = ((r.Next(1000) % 100) + (r.Next(1000) % 100)) / 2 - 50;
                int s_y = ((r.Next(1000) % 100) + (r.Next(1000) % 100)) / 2 - 50;
                blind = _blindness;
                move_to(fanion.get_X() + s_x, fanion.get_Y() + s_y);
            }
        }

        public void Update(GameTime _gameTime, Army _allies, Army _ennemies, Music _soundengine)
        {

            if (dead)
            {
                death_anim.Update(_gameTime);
            }
            else
            {
                int detect_ennemy = (type != sold_type.Ranger) ? (range * 3) / 2 : range;
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
                        if (blind)
                            detect_ennemy = 0;
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
                                        if (type == sold_type.Ranger)
                                        {
                                            int a = 40 - accuracy;
                                            Random r = new Random();
                                            _allies.Add_arrows(new Arrow("arrow_louis",
                                                rect.X + Cons.MAN_SIZE / 2, rect.Y - Cons.MAN_SIZE / 4,
                                                target.get_X() + r.Next(100) % a - a / 2,
                                                target.get_Y() + r.Next(100) % a - a / 2,
                                                damage));
                                            _soundengine.Play("fleche");
                                        }
                                        else if (type == sold_type.Healer)
                                        {
                                            if (mana > 25 && target.life < target.maxlife)
                                            {
                                                mana = mana > 0 ? mana - 25 : 0;
                                                target.do_damage(damage);
                                                _soundengine.Play("wololo");
                                            }
                                            else
                                            {
                                                goto case act_mode.Wait;
                                            }
                                        }
                                        else
                                        {
                                            target.do_damage(damage);
                                            _soundengine.Play("epee");
                                        }
                                }
                            }
                            else
                            {
                                if (type != sold_type.Ranger)
                                    move_to(target.get_X(), target.get_Y());
                                else
                                {
                                    target = _ennemies.get_target(rect.X, rect.Y, detect_ennemy, 64);
                                    //if (target == null)
                                    //    go_to_flag(false);
                                }
                            }
                        }
                        else
                        {
                            target = null;
                            go_to_flag(false);
                        }
                        #endregion
                        break;
                    case act_mode.Wait:
                        detect_ennemy = (type != sold_type.Ranger) ? range * 3 : range;
                        goto default;
                    default:
                        #region default
                        switch (type)
                        {
                            case sold_type.Fighter:
                                target = _ennemies.get_target(rect.X, rect.Y, detect_ennemy);
                                break;
                            case sold_type.Ranger:
                                target = _ennemies.get_target(rect.X, rect.Y, detect_ennemy, 64);
                                break;
                            case sold_type.Healer:
                                target = _allies.get_target_to_heal(rect.X, rect.Y, detect_ennemy);
                                elapsed_time += _gameTime.ElapsedGameTime.Milliseconds;
                                if (elapsed_time > 1000)
                                {
                                    elapsed_time -= 1000;
                                    mana = mana <= maxmana - 20 ? (mana + 20) : maxmana;
                                }
                                break;
                            default:
                                break;
                        }
                        if (target != null && target != this)
                            set_attack_on(target);
                        #endregion
                        break;
                }
            }
        }

        public override void Draw(bool _isOffset)
        {
            if (dead)
            {
                death_anim.Draw(rect, se);
            }
            else
            {
                //health bar
                Ressource.Draw("pixel", new Rectangle(rect.X, rect.Y - 10, 32, 4), Color.LightGray, Cons.DEPTH_HUD, _isOffset);
                Ressource.Draw("pixel", new Rectangle(rect.X, rect.Y - 10, (int)(life * 32 / maxlife), 4), Color.Green, Cons.DEPTH_HUD + 0.01f, _isOffset);
                //mana bar
                if (type == sold_type.Healer)
                {
                    Ressource.Draw("pixel", new Rectangle(rect.X, rect.Y - 6, 32, 4), Color.LightGray, Cons.DEPTH_HUD, _isOffset);
                    Ressource.Draw("pixel", new Rectangle(rect.X, rect.Y - 6, (int)(mana * 32 / maxmana), 4), Color.Blue, Cons.DEPTH_HUD + 0.01f, _isOffset);
                }

                switch (mode)
                {
                    case act_mode.Move:
                        walk_anim.Draw(rect, se);
                        break;
                    case act_mode.Attack:
                        attack_anim.Draw(rect, se);
                        break;
                    case act_mode.Wait:
                        base.Draw(se, _isOffset);
                        break;
                    default:
                        break;
                }
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
