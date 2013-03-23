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

        private Rectangle button_yes, button_no;

        public SlotMenu(Slot _linked, Rectangle _rect)
            : base("pixel", _rect, Color.Gray, Cons.DEPTH_HUD)
        {
            linked = _linked;
            button_yes = new Rectangle(rect.X + rect.Width - 65, rect.Y + rect.Height - 16, 32, 32);
            button_no = new Rectangle(rect.X + rect.Width - 32, rect.Y + rect.Height - 16, 32, 32);

            een = false;
        }

        public bool Update(int _mx, int _my, bool _mpressed)
        {
            if (_mpressed)
            {
                if (!een)
                {
                    if (button_yes.Contains(_mx,_my))
                    {
                        linked.AddBuilding(new Building("building_nicolas"));
                        linked.SetFree(false);
                        linked.GetBuilding().SetPosition(linked.GetPosition());
                    }
                    else if (button_no.Contains(_mx, _my))
                    {
                        if (linked.GetBuilding() != null)
                        {
                            linked.EreaseBuilding();
                            linked.SetFree(true);
                        }
                    }
                    een = true;
                }
            }
            else
            {
                een = false;
            }
            return rect.Contains(_mx,_my) || button_no.Contains(_mx,_my) || button_yes.Contains(_mx,_my);
        }

        public override void Draw()
        {
            base.Draw();
            Ressource.Draw("hud02", button_yes, new Rectangle(0,0,32,32), Color.White, Cons.DEPTH_HUD + 0.01f);
            Ressource.Draw("hud02", button_no, new Rectangle(32,0,32,32), Color.White, Cons.DEPTH_HUD + 0.01f);
        }
    }
}
