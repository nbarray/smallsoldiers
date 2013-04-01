using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using smallsoldiers.gui;

namespace smallsoldiers.entity
{
    class Building : Entity
    {
        private Flag fanion;
        public bool display_flag, blind_flag, dead, working;
        private int delay, time_since_last, building_state, elapsed, life, life_max, level;
        private float xp;
        private Animation working_anim;
        private Soldier product;
       
        public bool is_dead()
        {
            return dead;
        }
        public void change_working()
        {
            working = !working;
        }

        public void SetPosition(Point _p) { rect.X = _p.X; rect.Y = _p.Y; }

        public Building(string _asset, string _soldierAsset, sold_type _soldierType, Point _p)
            : base(_asset,
                   new Rectangle(_p.X, _p.Y, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   new Rectangle(3 * Cons.BUILDING_SIZE, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   Color.White, 0.3f)
        {
            fanion = new Flag("flag_louis");
            display_flag = false;
            delay = 60; // 700
            time_since_last = 0;
            building_state = 0;
            elapsed = 0;
            blind_flag = false;
            life_max = 100;
            life = life_max;
            level = 1;
            xp = 0;
            working = true;

            product = new Soldier(_soldierAsset, _soldierType, rect.X + 32, rect.Y + 64, fanion, 1);

            working_anim = new Animation(_asset, new Rectangle(0, 0, 96, 96), 3, 0, depth, false);
            //model = new Soldier("fighter_louis", 50, 75, fanion);
            //model.move_to(Cons.WIDTH / 2, Cons.HEIGHT / 2);
        }

        public void Update(GameTime _gameTime, Army _a, Player _owner, Flag _default_flag)
        {
            if (building_state < 3)
            {
                #region Build
                elapsed += _gameTime.ElapsedGameTime.Milliseconds;
                if (elapsed > 1000)
                {
                    elapsed -= 1000;
                    building_state++;
                    source.X = Cons.BUILDING_SIZE * (3 + building_state);
                    if (building_state == 3)
                    {
                        source.X = 0;
                    }
                }
                #endregion
            }
            else
            {
                if (working)
                {
                    #region Production
                    time_since_last++;
                    if (time_since_last >= delay)
                    {
                        if (_owner.GetIncome() > 0)
                        {
                            if (_a.Add_soldier(new Soldier(product.GetAsset(),
                                product.GetSoldierType(), rect.X + 32, rect.Y + 64, fanion, level), blind_flag))
                            {
                                _owner.RemoveFromIncome(1);
                                time_since_last = 0;
                                xp += 160f / (float)level;
                                if (level < (int)xp / 160 + 1)
                                {
                                    //level = (int)xp / 160+1;
                                    //level_up();
                                }
                            }
                        }
                    }
                    if (time_since_last <= delay)
                    {
                        working_anim.Update(_gameTime);
                    }
                    #endregion
                }
                else
                {
                    time_since_last = 0;
                }
            }

            if (display_flag)
            {
                fanion.Update(_gameTime);
            }

            if (life < 0)
            {
                dead = true;
                fanion.pass_the_flag(_default_flag);
            }
            //model.Update();
        }

        public void Draw_flag()//or_not
        {
            if (display_flag)
                fanion.Draw(true);
        }
        public void set_new_flag_pos(int _x, int _y, bool _blindness)
        {
            blind_flag = _blindness;
            fanion.set_new_pos(_x, _y, _blindness);
        }
        private void level_up()
        {
            working_anim.level_up(Cons.BUILDING_SIZE);
        }

        public override void Draw(bool _isOffset)
        {
            if (building_state > 2 && time_since_last <= delay)
                working_anim.Draw(rect, color);
            else
                base.Draw(true);
        }

    }
}
