using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using smallsoldiers.entity;
using smallsoldiers.land;

namespace smallsoldiers.gui
{
    enum button_type { make_barrack, make_archery, make_temple, sell, upgrade };

    class Building_hud
    {
        private Button_action[,] buttons;
        private sold_type type;

        public Building_hud(sold_type _type)
        {
            type = _type;

            buttons = new Button_action[4, 6];

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    buttons[i, j] = null;
                }
            }

            buttons[0, 0] = new Button_action("building_icone", button_type.make_barrack,new Rectangle(500, Cons.HEIGHT - 160, 32, 32), new Rectangle(0, 0, 32, 32));
            buttons[1, 0] = new Button_action("building_icone", button_type.make_archery,new Rectangle(500 + 32 + 1, Cons.HEIGHT - 160, 32, 32), new Rectangle(32, 0, 32, 32));
            buttons[2, 0] = new Button_action("building_icone", button_type.make_temple, new Rectangle(500 + 64 + 2, Cons.HEIGHT - 160, 32, 32), new Rectangle(64, 0, 32, 32));

            // Sell & Produce/Stop
            buttons[0, 1] = new Button_action("yes_or_no_button", button_type.upgrade, new Rectangle(500 + 32 + 1, Cons.HEIGHT - 160 + 32 + 1, 32, 32), new Rectangle(0, 0, 32, 32));
            buttons[1, 1] = new Button_action("yes_or_no_button", button_type.sell, new Rectangle(500 + 2 * 32 + 2, Cons.HEIGHT - 160 + 32 + 1, 32, 32), new Rectangle(32, 0, 32, 32));
        }

        public void Update(Inputs _inputs, Slot _slot)
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i, j] != null)
                    {
                        if (buttons[i,j].IsSelected(_inputs))
                        {
                            switch (buttons[i,j].GetSpecificity())
                            {
                                case button_type.make_barrack:
                                    _slot.AddBuilding(new Building("fighter_louis", sold_type.Fighter, _slot.GetPosition()));
                                    break;
                                case button_type.make_archery:
                                    _slot.AddBuilding(new Building("ranger_louis", sold_type.Ranger, _slot.GetPosition()));
                                    break;
                                case button_type.make_temple:
                                    _slot.AddBuilding(new Building("healer_louis", sold_type.Healer, _slot.GetPosition()));
                                    break;
                                case button_type.sell:
                                    _slot.EraseBuilding();
                                    break;
                                case button_type.upgrade:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i, j] != null)
                        buttons[i, j].DrawDepth(false);
                }
            }
        }
    }
}
