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

        private Rectangle button_upgrade, button_sell, button_warrior, button_archer, button_healer, button_produce;
        private bool produce_state;

        public bool GetProductionState() { return produce_state; }

        public SlotMenu(Slot _linked, Rectangle _rect)
            : base("slotmenu_bg", _rect, Color.Gray, Cons.DEPTH_HUD + 0.02f)
        {
            linked = _linked;
            button_sell = new Rectangle(rect.X + 12, rect.Y + rect.Height - 16, 32, 32);
            button_upgrade = new Rectangle(button_sell.X + 32 + 4, rect.Y + rect.Height - 16, 32, 32);
            button_produce = new Rectangle(button_upgrade.X + 32 + 4, rect.Y + rect.Height - 16, 32, 32);

            button_warrior = new Rectangle(rect.X + 12, rect.Y - 16, 32, 32);
            button_archer = new Rectangle(button_warrior.X + 32 + 4, rect.Y - 16, 32, 32);
            button_healer = new Rectangle(button_archer.X + 32 + 4, rect.Y - 16, 32, 32);

            een = false;
            produce_state = true;
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
                    else if (button_sell.Contains(_mx, _my))
                    {
                        if (linked.GetBuilding() != null)
                        {
                            linked.EreaseBuilding();
                            linked.SetFree(true);
                            linked.GetOwner().AddToIncome(1);
                        }
                    }
                    else if (button_produce.Contains(_mx, _my))
                    {
                        produce_state = !produce_state;
                    }
                    een = true;
                }
            }
            else
            {
                een = false;
            }
            return button_sell.Contains(_mx, _my) ||
                   button_upgrade.Contains(_mx, _my) ||
                   button_produce.Contains(_mx, _my) ||
                   button_warrior.Contains(_mx, _my) ||
                   button_archer.Contains(_mx, _my) ||
                   button_healer.Contains(_mx, _my);
        }

        public override void Draw()
        {
            base.Draw();
            Ressource.Draw("hud02", button_upgrade, new Rectangle(0, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.03f);
            Ressource.Draw("hud02", button_sell, new Rectangle(32, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.03f);
            Ressource.Draw("produce", button_produce, new Rectangle(produce_state ? 32 : 0, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.03f);

            Ressource.Draw("building_icone", button_warrior, new Rectangle(0, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.03f);
            Ressource.Draw("building_icone", button_archer, new Rectangle(32, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.03f);
            Ressource.Draw("building_icone", button_healer, new Rectangle(64, 0, 32, 32), Color.White, Cons.DEPTH_HUD + 0.03f);
        }
    }
}
