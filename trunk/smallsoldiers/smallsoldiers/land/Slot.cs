using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using smallsoldiers.entity;
using smallsoldiers.gui;

namespace smallsoldiers.land
{
    class Slot
    {
        private Rectangle rect;
        private bool free, is_selected, een, right_click;
        private Color color;
        private Building building;
        private Player owner;
        private SlotMenu menu;

        public Point GetPosition() { return rect.Location; }
        public void SetOwner(Player _owner) { if (owner == null) owner = _owner; }
        public Building GetBuilding() { return building; }
        public void EreaseBuilding() { building = null; }
        public void SetFree(bool _b) { free = _b; }

        public Slot(int _i, int _j)
        {
            rect = new Rectangle(_i, _j, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE);

            color = Color.Red;
            
            een = false;
            is_selected = false;

            building = null;
            free = true;
            owner = null;

            menu = new SlotMenu(this, new Rectangle(_i + Cons.BUILDING_SIZE + 4, _j, 128, 64));
        }

        public void AddBuilding(Building _b)
        {
            if (free)
            {
                building = _b;
                free = false;
            }
        }

        public void Update(GameTime _gameTime, int _mx, int _my, bool _mpressed, bool _rpressed)
        {
            if (is_selected)
                Update_when_selected(_mx, _my, _rpressed);

            #region mouse
            if (rect.Contains(_mx, _my) || (is_selected && menu.Update(_mx, _my, _mpressed)))
            {
                if (!_mpressed)
                {
                    een = false;
                    color = Color.Yellow;
                }
                else
                {
                    if (!een)
                    {

                        is_selected = true;
                        een = true;
                    }
                    color = Color.Purple;
                }
            }
            else
            {
                if (_mpressed)
                {
                    is_selected = false;
                }
                color = Color.Red;
            }
            #endregion

            if (building != null)
            {
                building.Update(_gameTime, owner.army);
            }
        }
        private void Update_when_selected(int _mx, int _my, bool _rpressed)
        {
            if (right_click && _rpressed)
            {
                right_click = false;
                building.set_new_flag_pos(_mx, _my);
            }
            if (!_rpressed)
            {
                right_click = true;
            }
        }
        public void Update_IA(GameTime _gameTime, Player _p)
        {
            if (building != null)
            {
                building.Update(_gameTime, _p.army);
            }
            else
            {
                AddBuilding(new Building("building_nicolas"));
                building.SetPosition(new Point(rect.X, rect.Y));
                building.set_new_flag_pos(rect.X - 300, rect.Y + 48);
            }
        }

        public void Draw()
        {
            if (is_selected)
                Draw_when_selected();

            if (free)
                Ressource.Draw("slot01", rect, color, 0.8f);
            else
            {
                if (building != null)
                {
                    building.Draw();
                    building.display_flag = is_selected;
                    building.Draw_flag();
                }
            }
        }
        private void Draw_when_selected()
        {
            menu.Draw();
        }
    }
}
