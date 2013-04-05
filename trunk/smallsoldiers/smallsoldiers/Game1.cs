using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using smallsoldiers.land;
using smallsoldiers.entity;
using smallsoldiers.gui;
using smallsoldiers.son;

namespace smallsoldiers
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        Player p1, p2;
        Battlefield call_of_duty;
        Hud hud;
        Music music;
        Inputs inputs;

        Menu menus;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            IsMouseVisible = true;
            //graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            //Load content in ressource class => go to it's class file then in the LoadContent function
            Ressource.Initialize(GraphicsDevice, Content);
            Ressource.LoadContent();

            //Initialize objects here
            p1 = new Player("nicolas", true);
            p2 = new Player("nicolas", false);
            call_of_duty = new Battlefield();
            hud = new Hud();
            music = new Music();
            inputs = new Inputs();
            menus = new Menu();

            base.Initialize();
        }

        protected override void UnloadContent()
        {
            music.UnloadInstance();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            inputs.Update(Mouse.GetState(), Keyboard.GetState());

            if (inputs.GetIsPressed(Keys.Escape))
                this.Exit();

            switch (menus.GetState())
            {
                case e_MenuState.main:
                case e_MenuState.quit:

                    menus.Update(this, inputs);

                    break;
                case e_MenuState.game:

                    if (inputs.GetIsPressed(Keys.M)) Cons.mode = e_GameMode.multi;
                    if (inputs.GetIsPressed(Keys.L)) Cons.mode = e_GameMode.solo;

                    hud.Update(p1, p2);
                    Hud.UpdateCam(inputs);

                    p1.Update(gameTime, p2.army, inputs, music, hud);
                    p2.Update(gameTime, p1.army, inputs, music, hud);

                    call_of_duty.Update(gameTime, p1, p2);

                    break;
                default:
                    break;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Ressource.sb.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            switch (menus.GetState())
            {
                case e_MenuState.main:
                case e_MenuState.quit:

                    menus.Draw(false);

                    break;
                case e_MenuState.game:

                    p1.Draw();
                    p2.Draw();
                    call_of_duty.Draw();
                    hud.Draw();

                    break;
                default:
                    break;
            }



            Ressource.sb.End();
            base.Draw(gameTime);
        }
    }
}
