using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using smallsoldiers.entity;
using smallsoldiers.gui;
using Microsoft.Xna.Framework.Input;

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

        private Random r;
        private float elapsed_time = 0f;
        private bool ai_wait_to_create;


        public Point GetPosition() { return rect.Location; }
        public void SetOwner(Player _owner) { if (owner == null) owner = _owner; }
        public Player GetOwner() { return owner; }
        public Building GetBuilding() { return building; }
        public void EreaseBuilding() { building = null; }
        public void SetFree(bool _b) { free = _b; }

        public Slot(int _i, int _j)
        {
            rect = new Rectangle(_i, _j, Cons.BUILDING_SIZE, Cons.BUILDING_SIZE);
            r = new Random();
            color = Color.White;

            een = false;
            ai_wait_to_create = false;
            is_selected = false;

            building = null;
            free = true;
            owner = null;

            if (_i > Cons.WIDTH / 2)
                menu = new SlotMenu(this, new Rectangle(_i - 128 - 4, _j + 16, 128, 42));
            else
                menu = new SlotMenu(this, new Rectangle(_i + Cons.BUILDING_SIZE + 4, _j + 16, 128, 42));
        }

        public void AddBuilding(Building _b)
        {
            if (free)
            {
                if (owner.GetIncome() >= 2)
                {
                    owner.RemoveFromIncome(2);
                    building = _b;
                    building.set_new_flag_pos(Cons.WIDTH / 2, Cons.HEIGHT / 2, false);
                    free = false;
                }
            }
        }

        public void Update(GameTime _gameTime, Inputs _inputs, Flag _default_flag)
        {
            if (is_selected)
                Update_when_selected(_inputs.GetX(), _inputs.GetY(), _inputs.GetMRpressed());

            #region mouse
            if (rect.Contains(_inputs.GetX(), _inputs.GetY()) || (is_selected && menu.Update(_inputs.GetX(), _inputs.GetY(), _inputs.GetMLpressed())))
            {
                if (_inputs.GetMLreleased())
                {
                    een = false;
                }
                else
                {
                    if (!een)
                    {
                        is_selected = true;
                        een = true;
                    }
                }
            }
            else
            {
                if (_inputs.GetMLpressed())
                {
                    is_selected = false;
                }
            }
            #endregion

            if (building != null)
            {
                building.Update(_gameTime, owner.army, owner, menu.GetProductionState(), _default_flag);
            }
        }

        private void Update_when_selected(int _mx, int _my, bool _rpressed)
        {
            if (building != null)
            {
                if (right_click && _rpressed)
                {
                    right_click = false;
                    building.set_new_flag_pos(_mx, _my, Keyboard.GetState().IsKeyDown(Keys.LeftControl));
                }
                if (!_rpressed)
                {
                    right_click = true;
                }

            }
        }
        public void Update_IA(GameTime _gameTime, Player _p, Flag _default_flag)
        {
            if (building != null)
            {
                building.Update(_gameTime, _p.army, _p, menu.GetProductionState(), _default_flag);
            }
            else
            {
                elapsed_time += _gameTime.ElapsedGameTime.Milliseconds;
                if (elapsed_time < 1000)
                    ai_wait_to_create = false;
                if (elapsed_time > 4000)
                {
                    elapsed_time -= elapsed_time;
                    if (!ai_wait_to_create)
                    {
                        ai_wait_to_create = true;
                        int i = Cons.r.Next(7);
                        if (i < 3)
                        {
                            AddBuilding(new Building("building_nicolas", "fighter_louis", sold_type.Fighter, GetPosition()));
                        }
                        else if (i > 3)
                        {
                            AddBuilding(new Building("building_nicolas", "ranger_louis", sold_type.Ranger, GetPosition()));
                        }
                        else
                        {
                            AddBuilding(new Building("building_nicolas", "healer_louis", sold_type.Healer, GetPosition()));
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            if (is_selected)
                Draw_when_selected();

            if (free)
            {
                if (is_selected)
                    Ressource.Draw("slot02", rect, new Rectangle(96, 0, 96, 96), 
                        color, 0.5f + ((float)(rect.Y + rect.Height)) / 10000f);
                else
                    Ressource.Draw("slot02", rect, new Rectangle(0, 0, 96, 96), 
                        color, 0.5f + ((float)(rect.Y + rect.Height)) / 10000f);
            }
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
