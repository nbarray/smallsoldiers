using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using smallsoldiers.land;
using smallsoldiers.entity;

namespace smallsoldiers.gui
{
    class SlotMenu : Entity
    {
        private Slot linked;
        private bool een;

        private Rectangle button_yes, button_no, button_warrior, button_archer, button_healer;

        public SlotMenu(Slot _linked, Rectangle _rect)
            : base("slotmenu_bg", _rect, Color.Gray, Cons.DEPTH_HUD)
        {
            linked = _linked;
            button_yes = new Rectangle(rect.X + 65, rect.Y + rect.Height - 16, 32, 32);
            button_no = new Rectangle(rect.X + 32, rect.Y + rect.Height - 16, 32, 32);

            button_warrior = new Rectangle(rect.X + 12, rect.Y + 8, 32, 32);
            button_archer = new Rectangle(button_warrior.X + 32 + 4, rect.Y + 8, 32, 32);
            button_healer = new Rectangle(button_archer.X + 32 + 4, rect.Y + 8, 32, 32);

            een = false;
        }

        public bool Update(int _mx, int _my, bool _mpressed)
        {
            if (_mpressed)
            {
                if (!een)
                {
                    if (button_warrior.Contains(_mx, _my))
                    {
                        linked.AddBuilding(new Building("building_nicolas", "fighter_louis", sold_type.Fighter, linked.GetPosition()));
                    }
                    else if (button_archer.Contains(_mx, _my))
                    {
                        linked.AddBuilding(new Building("building_nicolas", "ranger_louis", sold_type.Ranger, linked.GetPosition()));
                    }
                    else if (button_healer.Contains(_mx, _my))
                    {
                        linked.AddBuilding(new Building("building_nicolas", "healer_louis", sold_type.Healer, linked.GetPosition()));
                    }
                    else if (button_no.Contains(_mx, _my))
                    {
                        if (linked.GetBuilding() != null)
                        {
                            linked.EreaseBuilding();
                            linked.SetFree(true);
                            linked.GetOwner().AddToIncome(1);
                        }
                    }
                    een = true;
                }
            }
            else
            {
                een = false;
            }
            return rect.Contains(_mx, _my) || button_no.Contains(_mx, _my) || button_yes.Contains(_mx, _my);
        }

        public override void Draw()
        {
            base.Draw();
            Ressource.Draw("hud02", button_yes, new Rectangle(0, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.01f);
            Ressource.Draw("hud02", button_no, new Rectangle(32, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.01f);
            Ressource.Draw("building_icone", button_warrior, new Rectangle(0, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.01f);
            Ressource.Draw("building_icone", button_archer, new Rectangle(32, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.01f);
            Ressource.Draw("building_icone", button_healer, new Rectangle(64, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.01f);
        }
    }
}
