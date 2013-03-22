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

        Homeland zone_joueur_1, zone_joueur_2;
        Battlefield test;
        Hud test_hud;

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
            zone_joueur_1 = new Homeland(true);
            zone_joueur_2 = new Homeland(false);
            test = new Battlefield();
            test_hud = new Hud();

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

            zone_joueur_1.Update(mx, my, mpressed);
            zone_joueur_2.Update(mx, my, mpressed);
            test.Update(mx, my, mpressed);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Ressource.sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            zone_joueur_1.Draw();
            zone_joueur_2.Draw();
            test.Draw();
            test_hud.Draw();
            Ressource.sb.End();

            base.Draw(gameTime);
        }
    }
}
