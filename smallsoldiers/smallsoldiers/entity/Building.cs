using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    class Building : Entity
    {
        private Flag fanion;
        public bool display_flag;
        private int delay, time_since_last, building_state, elapsed;
        private Animation working_anim;

        private Soldier product;

        public void SetPosition(Point _p) { rect.X = _p.X; rect.Y = _p.Y; }

        public Building(string _asset, string _soldierAsset, sold_type _soldierType, Point _p)
            : base(_asset,
                   new Rectangle(_p.X, _p.Y, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   new Rectangle(3 * Cons.BUILDING_SIZE, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   Color.White, 0.3f)
        {
            fanion = new Flag("flag_louis");
            display_flag = false;
            delay = 60;
            time_since_last = 0;
            building_state = 0;
            elapsed = 0;

            product = new Soldier(_soldierAsset, _soldierType, rect.X + 32, rect.Y + 64, fanion);

            working_anim = new Animation(_asset, new Rectangle(0, 0, 96, 96), 3, 0, depth, false);
            //model = new Soldier("fighter_louis", 50, 75, fanion);
            //model.move_to(Cons.WIDTH / 2, Cons.HEIGHT / 2);
        }

        public void Update(GameTime _gameTime, Army _a, Player _owner, bool _producing)
        {
            if (building_state < 3)
            {
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
            }
            else
            {
                if (_producing)
                {
                    time_since_last++;
                    if (time_since_last >= delay)
                    {
                        if (_owner.GetIncome() > 0)
                        {
                            if (_a.Add_soldier(new Soldier(product.GetAsset(), product.GetSoldierType(), rect.X + 32, rect.Y + 64, fanion)))
                            {
                                _owner.RemoveFromIncome(1);
                                time_since_last = 0;
                            }
                        }
                    }
                    if (time_since_last <= delay)
                    {
                        working_anim.Update(_gameTime);
                    }
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
            //model.Update();
        }

        public void Draw_flag()//or_not
        {
            if (display_flag)
                fanion.Draw();
        }
        public void set_new_flag_pos(int _x, int _y)
        {
            fanion.set_new_pos(_x, _y);
        }

        public override void Draw()
        {
            if (building_state > 2 && time_since_last <= delay)
                working_anim.Draw(rect);
            else
                base.Draw();
        }

    }
}
