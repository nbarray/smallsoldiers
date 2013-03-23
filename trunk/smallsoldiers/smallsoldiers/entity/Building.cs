﻿using System;
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
        private int delay, time_since_last;
        private Animation working_anim;

        public void SetPosition(Point _p) { rect.X = _p.X; rect.Y = _p.Y; }

        public Building(string _asset)
            : base(_asset,
                   new Rectangle(0, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   new Rectangle(0, 0, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE),
                   Color.White, 0.3f)
        {
            fanion = new Flag("flag_louis");
            display_flag = false;
            delay = 300;
            time_since_last = 0;
            working_anim = new Animation("building_nicolas", new Rectangle(0, 0, 96, 96), 3, 0, depth, false);
            //model = new Soldier("fighter_louis", 50, 75, fanion);
            //model.move_to(Cons.WIDTH / 2, Cons.HEIGHT / 2);
        }
        bool working = false;
        public void Update(GameTime _gameTime, Army _a)
        {
            time_since_last++;
            if (time_since_last >= delay)
            {
                working = _a.Add_soldier(new Soldier("fighter_louis", rect.X + 32, rect.Y + 64, fanion));
                time_since_last = 0;
                
            }
            if (working)
            {
                working_anim.Update(_gameTime);
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
            if (working)
                working_anim.Draw(rect);
            else
                base.Draw();
        }

    }
}
