using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;

namespace smallsoldiers.land
{
    class Region : Entity
    {
        public Region(string _asset, int _x, int _y, int _width, int _height)
            : base(_asset, new Rectangle(_x, _y, _width, _height), Color.White, 1f)
        {

        }

        public void Update(int _mx, int _my, bool _mpressed)
        {
            if (rect.Contains(_mx, _my))
            {
                if (!_mpressed)
                    color = Color.White;
            }
            else
            {
                color = Color.SandyBrown;
            }
        }
    }
}
