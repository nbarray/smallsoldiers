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
        private Rectangle rect, choice_rect;
        private bool free, is_selected, een;
        private Color color;
        private Building building;

        public Slot(int _i, int _j)
        {
            rect = new Rectangle(_i, _j, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE);
            choice_rect = new Rectangle(_i + Cons.BUILDING_SIZE + 4, _j, 32, 32);
            color = Color.Red;
            free = true;
            een = false;
            is_selected = false;

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

        public void Update(int _mx, int _my, bool _mpressed, Player p)
        {
            if (rect.Contains(_mx, _my))
            {
                if (!_mpressed)
                {
                    een = false;
                    color = Color.Yellow;
                }
                else
                {
                    if (!een && p.IsPlayer())
                    {
                        is_selected = !is_selected;
                        AddBuilding(new Building("building_nicolas"));
                        building.SetPosition(new Point(rect.X, rect.Y));
                        een = true;
                    }
                    color = Color.Purple;
                }
            }
            else
            {
                if (_mpressed) { is_selected = false; }
                color = Color.Red;
            }
            if (building != null)
            {
                building.Update(p.army);
            }
        }

        public void Draw()
        {
            if (is_selected)
            {
                //Ressource.Draw("pixel", choice_rect, Color.White, Cons.DEPTH_HUD);
            }

            if (free)
                Ressource.Draw("slot01", rect, color, 0.8f);
            else
            {
                building.Draw();
                building.Draw_flag();
            }
        }
    }
}
