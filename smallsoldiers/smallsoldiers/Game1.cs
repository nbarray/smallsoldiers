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

namespace smallsoldiers
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        Player p1, p2;
        Battlefield call_of_duty;
        Hud hud;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            IsMouseVisible = true;
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

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            MouseState mstate = Mouse.GetState();
            int mx = mstate.X;
            int my = mstate.Y;
            bool mpressed = mstate.LeftButton == ButtonState.Pressed;
            bool mreleased = mstate.LeftButton == ButtonState.Released;

            hud.Update(p1, p2);

            p1.Update(hud, mx, my, mpressed);
            p2.Update(hud, mx, my, mpressed);

            call_of_duty.Update(mx, my, mpressed);

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
