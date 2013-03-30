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
        private Building_hud action_menu;
        private Player owner;

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
            action_menu = new Building_hud(sold_type.Fighter);

            building = null;
            free = true;
            owner = null;
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

        public void Update(GameTime _gameTime, Inputs _inputs, Flag _default_flag, Hud _hud)
        {
            if (is_selected)
                Update_when_selected(_inputs);

            #region mouse
            if (rect.Contains(_inputs.GetRelativeX(), _inputs.GetRelativeY()))
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
                if (_inputs.GetMLpressed() && !_hud.Contains(_inputs.GetAbsoluteX(), _inputs.GetAbsoluteY()))
                {
                    is_selected = false;
                }
            }
            #endregion

            if (building != null)
            {
                building.Update(_gameTime, owner.army, owner, _default_flag);
            }
        }

        private void Update_when_selected(Inputs _inputs)
        {
            Update_buttons(_inputs);
            if (building != null)
            {
                #region Positionner le fanion
                if (right_click && _inputs.GetMRpressed())
                {
                    right_click = false;
                    building.set_new_flag_pos(_inputs.GetRelativeX(), _inputs.GetRelativeY(), _inputs.GetIsPressed(Keys.LeftControl));
                }
                if (_inputs.GetMRreleased())
                {
                    right_click = true;
                }
                #endregion
            }
        }
        private void Update_buttons(Inputs _inputs)
        {
            action_menu.Update(_inputs, this);
        }

        public void Update_IA(GameTime _gameTime, Player _p, Flag _default_flag)
        {
            if (building != null)
            {
                building.Update(_gameTime, _p.army, _p, _default_flag);
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
                        color, 0.5f + ((float)(rect.Y + rect.Height)) / 10000f, true);
                else
                    Ressource.Draw("slot02", rect, new Rectangle(0, 0, 96, 96),
                        color, 0.5f + ((float)(rect.Y + rect.Height)) / 10000f, true);
            }
            else
            {
                if (building != null)
                {
                    building.Draw(true);
                    building.display_flag = is_selected;
                    building.Draw_flag();
                }
            }
        }
        private void Draw_when_selected()
        {
            action_menu.Draw();
        }
    }
}
