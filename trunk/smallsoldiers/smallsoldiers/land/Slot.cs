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
        private bool free, show_choice_menu, een;
        private Color color;
        private Building building;

        public Slot(int _i, int _j)
        {
            rect = new Rectangle(_i, _j, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE);
            choice_rect = new Rectangle(_i + Cons.BUILDING_SIZE + 4, _j, 32, 32);
            color = Color.Red;
            free = true;
            een = false;
            show_choice_menu = false;

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
                        show_choice_menu = !show_choice_menu;
                        AddBuilding(new Building("building_nicolas"));
                        een = true;
                    }
                    color = Color.Purple;
                }
            }
            else
            {
                if (_mpressed) { show_choice_menu = false; }
                color = Color.Red;
            }
        }

        public void Draw()
        {
            if (show_choice_menu)
            {
                Ressource.Draw("pixel", choice_rect, Color.White, Cons.DEPTH_HUD);
                
            }

            if (free)
                Ressource.Draw("slot01", rect, color, 0.8f);
            else
                Ressource.Draw("building_nicolas", rect, new Rectangle(0, 0, 96, 96), Color.White, 0.7f);
        }
    }
}
