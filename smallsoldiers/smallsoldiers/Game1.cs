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

            if (inputs.GetIsPressed(Keys.M)) Cons.mode = e_GameMode.multi;
            if (inputs.GetIsPressed(Keys.L)) Cons.mode = e_GameMode.solo;

            MouseState mstate = Mouse.GetState();
            int mx = inputs.GetX();
            int my = inputs.GetY();
            bool mpressed = mstate.LeftButton == ButtonState.Pressed;
            bool mreleased = mstate.LeftButton == ButtonState.Released;
            bool rpressed = mstate.RightButton == ButtonState.Pressed;
            bool rreleased = mstate.RightButton == ButtonState.Released;

            hud.Update(p1, p2);

            p1.Update(gameTime, p2.army, inputs, music);
            p2.Update(gameTime, p1.army, inputs, music);

            call_of_duty.Update(gameTime, p1, p2);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Ressource.sb.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            p1.Draw();
            p2.Draw();
            call_of_duty.Draw();
            hud.Draw();
            Ressource.sb.End();
            base.Draw(gameTime);
        }
    }
}
