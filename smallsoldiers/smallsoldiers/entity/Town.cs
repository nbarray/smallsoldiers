using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using smallsoldiers.gui;
using Microsoft.Xna.Framework.Input;

namespace smallsoldiers.entity
{
    class Town : Entity
    {
        #region Var & get-set
        private Flag fanion;
        public bool display_flag, blind_flag, dead;
        private int delay, time_since_last, building_state, elapsed, life, life_max, level;
        //private float xp;
        //private Animation working_anim;private Rectangle rect;
        private bool free, is_selected, een, right_click;
        private Player owner;
        private TownMenu menu;

        private Random r;
        private float elapsed_time = 0f;
        private bool ai_wait_to_create;


        public Point GetPosition() { return rect.Location; }
        public void SetOwner(Player _owner) { if (owner == null) owner = _owner; }
        public Player GetOwner() { return owner; }
        public void SetFree(bool _b) { free = _b; }
        public bool is_dead()
        {
            return dead;
        } 
        #endregion

        public void SetPosition(Point _p) { rect.X = _p.X; rect.Y = _p.Y; }

        public Town(string _asset, Player _owner)
            : base(_asset,
                   new Rectangle(0, (Cons.HEIGHT - Cons.TOWN_SIZE) / 2, Cons.TOWN_SIZE, Cons.TOWN_SIZE),
                   new Rectangle(0, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),//Cons.TOWN_SIZE, Cons.TOWN_SIZE),
                   Color.White, 0.3f)
        {
            fanion = new Flag("flag_louis");
            display_flag = false;
            delay = 60; // 700
            time_since_last = 0;
            building_state = 3;
            elapsed = 0;
            blind_flag = false;
            life_max = 100;
            life = life_max;
            level = 1;

            //working_anim = new Animation(_asset, new Rectangle(0, 0, 96, 96), 3, 0, depth, false);
            //model = new Soldier("fighter_louis", 50, 75, fanion);
            //model.move_to(Cons.WIDTH / 2, Cons.HEIGHT / 2);
            r = new Random();

            een = false;
            ai_wait_to_create = false;
            is_selected = false;
            free = true;
            owner = _owner;

            if (!_owner.IsPlayer())
            {
                rect.X = Cons.WIDTH - Cons.TOWN_SIZE;
                menu = new TownMenu(this, new Rectangle(rect.X - 128 - 4, (Cons.HEIGHT - Cons.TOWN_SIZE) / 2 + 16, 128, 42));
            }
            else
            {
                rect.X = 0;
                menu = new TownMenu(this, new Rectangle(rect.X + 4, (Cons.HEIGHT - Cons.TOWN_SIZE) / 2 + 16, 128, 42));
            }
        }

        public void Update(GameTime _gameTime, int _mx, int _my, bool _mpressed, bool _rpressed)
        {
            Update1(_gameTime);

            Update2(_gameTime, _mx, _my, _mpressed, _rpressed);
        }

        private void Update1(GameTime _gameTime)
        {
            if (building_state < 3)
            {
                #region Build
                elapsed += _gameTime.ElapsedGameTime.Milliseconds;
                if (elapsed > 1000)
                {
                    elapsed -= 1000;
                    building_state++;
                    source.X = Cons.TOWN_SIZE * (3 + building_state);
                    if (building_state == 3)
                    {
                        source.X = 0;
                    }
                }
                #endregion
            }
            else
            {
            }

            if (display_flag)
            {
                fanion.Update(_gameTime);
            }

            if (life < 0)
            {
                dead = true;
            }
        }
        private void Update2(GameTime _gameTime, int _mx, int _my, bool _mpressed, bool _rpressed)
        {
            if (is_selected)
                Update_when_selected(_mx, _my, _rpressed);

            #region mouse
            if (rect.Contains(_mx, _my) || (is_selected && menu.Update(_mx, _my, _mpressed)))
            {
                if (!_mpressed)
                {
                    een = false;
                }
                else
                {
                    if (!een)
                    {
                        is_selected = true;
                        een = true;
                    }
                }
            }
            else
            {
                if (_mpressed)
                {
                    is_selected = false;
                }
            }
            #endregion
        }
        private void Update_when_selected(int _mx, int _my, bool _rpressed)
        {
                if (right_click && _rpressed)
                {
                    right_click = false;
                    owner.default_flag.set_new_pos(_mx, _my, Keyboard.GetState().IsKeyDown(Keys.LeftControl));
                }
                if (!_rpressed)
                {
                    right_click = true;
                }
        }

        public void Update_IA(GameTime _gameTime)
        {
            Update1(_gameTime);
        }

        public void Draw_flag()//or_not
        {
            if (display_flag)
                fanion.Draw();
        }
        public void set_new_flag_pos(int _x, int _y, bool _blindness)
        {
            blind_flag = _blindness;
            fanion.set_new_pos(_x, _y, _blindness);
        }
        private void level_up()
        {
            //working_anim.level_up(Cons.BUILDING_SIZE);
        }

        public override void Draw()
        {
            if (is_selected)
                menu.Draw();
            //if (building_state > 2 && time_since_last <= delay)
            //    working_anim.Draw(rect, color);
            //else
                base.Draw();
        }
    }
}
