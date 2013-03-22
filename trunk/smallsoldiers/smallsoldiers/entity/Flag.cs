using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    class Flag : Entity
    {
        private List<Soldier> soldiers_linked;

        private Animation wind_anim;

        public Flag(string _asset)
            : base(_asset,
                   new Rectangle(Cons.WIDTH/2, Cons.HEIGHT/2, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   new Rectangle(0, 0, Cons.MAN_SIZE, Cons.MAN_SIZE),
                   Color.White, 0.5f)
        {
            soldiers_linked = new List<Soldier>();
            wind_anim = new Animation(asset, source, 5, 0, depth);
        }

        public int get_X()
        {
            return rect.X;
        }
        public int get_Y()
        {
            return rect.Y;
        }

        public void add_new_soldier(Soldier _s)
        {
            soldiers_linked.Add(_s);
        }
        public void set_new_pos(int _x, int _y)
        {
            rect.X = _x;
            rect.Y = _y;
            depth = 0.5f + ((float)(rect.Y + 32)) / 10000f;
            for (int i = 0; i < soldiers_linked.Count; i++)
			{
                soldiers_linked[i].go_to_flag();
            }
        }

        public void Update(GameTime _gameTime)
        {
            wind_anim.Update(_gameTime);
        }

        public override void  Draw()
        {
            wind_anim.Draw(rect);
        }
    }
}
