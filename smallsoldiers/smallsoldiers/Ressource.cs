using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace smallsoldiers
{
    static class Ressource
    {
        private static Dictionary<string, Texture2D> textures;
        private static Dictionary<string, SpriteFont> fonts;
        private static Dictionary<string, SoundEffect> effects;

        public static SpriteBatch sb;
        private static ContentManager content;

        public static Texture2D Get(string _name) { return textures[_name]; }
        public static SpriteFont GetFont(string _name) { return fonts[_name]; }

        public static void Initialize(GraphicsDevice _device, ContentManager _content)
        {
            sb = new SpriteBatch(_device);
            content = _content;

            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            effects = new Dictionary<string, SoundEffect>();
        }

        public static void LoadContent()
        {
            textures.Add("bg01", content.Load<Texture2D>("image/bg01"));
            textures.Add("pixel", content.Load<Texture2D>("image/pixel"));
            textures.Add("fighter_louis", content.Load<Texture2D>("image/fighter_louis"));
            textures.Add("hud01", content.Load<Texture2D>("image/hud01"));
            textures.Add("flag01", content.Load<Texture2D>("image/flag01"));
            textures.Add("flag02", content.Load<Texture2D>("image/flag02"));

            fonts.Add("medium", content.Load<SpriteFont>("font/medium"));
        }


    }
}
