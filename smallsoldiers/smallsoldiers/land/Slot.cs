using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using smallsoldiers.entity;

namespace smallsoldiers.land
{
    class Slot
    {
        private Rectangle rect;
        private bool free;
        private Color color;
        private Building building;

        public Slot(int _i, int _j)
        {
            rect = new Rectangle(_i, _j, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE);
            color = Color.Red;
            free = true;
            building = null;
        }

        public void AddBuilding(Building _b)
        {
            if (free)
            {
                building = _b;
                free = false;
            }
        }

        public void Update(int _mx, int _my, bool _mpressed)
        {
            if (rect.Contains(_mx, _my))
            {
                if (!_mpressed)
                    color = Color.Yellow;
                else
                    color = Color.Purple;
            }
            else
            {
                color = Color.Red;
            }
        }

        public void Draw()
        {
            if (free)
                Ressource.Draw("slot01", rect, color, 0.8f);
            else
                Ressource.Draw("building_nicolas", rect, new Rectangle(0, 0, 96, 96), Color.White, 0.8f);
        }
    }
}
