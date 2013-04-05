using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;

namespace smallsoldiers.gui
{
    enum e_MenuState { main, quit, game }

    class Menu : Entity
    {
        private Button_menu[] buttons;
        private e_MenuState menuState;

        public e_MenuState GetState() { return menuState; }

        public Menu() : base("pixel", new Rectangle(0,0,Cons.WIDTH, Cons.HEIGHT), Color.Black, Cons.DEPTH_HUD)
        {
            menuState = e_MenuState.main;
            buttons = new Button_menu[10];

            buttons[0] = new Button_menu(new Rectangle((Cons.WIDTH - 400) / 2, (Cons.HEIGHT - 75) / 2, 400, 75), "Quit");
            buttons[1] = new Button_menu(new Rectangle((Cons.WIDTH - 400) / 2, (Cons.HEIGHT - 75) / 2 - 75 - 2, 400, 75), "Play solo");
        }

        public void Update(Game1 _game1, Inputs _inputs)
        {
            switch (menuState)
            {
                case e_MenuState.main:
                    if (buttons[1].Selected(_inputs)) menuState = e_MenuState.game;
                    if (buttons[0].Selected(_inputs)) menuState = e_MenuState.quit;
                    break;
                case e_MenuState.quit:
                    _game1.Exit();
                    break;
                default:
                    break;
            }
        }

        public override void Draw(bool _isOffset)
        {
            switch (menuState)
            {
                case e_MenuState.main:

                    Ressource.DrawString("medium", "Small Soldiers", new Vector2((Cons.WIDTH - Ressource.GetFont("medium").MeasureString("Small Soldiers").X) / 2, 100), Color.Blue, Cons.DEPTH_HUD + 0.01f, false);

                    buttons[0].Draw(false);
                    buttons[1].Draw(false);
                    break;
                case e_MenuState.quit:
                    break;
                default:
                    break;
            }
            base.Draw(_isOffset);
        }
    }
}
