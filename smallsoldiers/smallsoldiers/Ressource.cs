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
            textures.Add("slot01", content.Load<Texture2D>("image/slot01"));
            textures.Add("homelands_nicolas", content.Load<Texture2D>("image/homelands_nicolas"));
            textures.Add("homelands_louis", content.Load<Texture2D>("image/homelands_louis"));
            textures.Add("building_nicolas", content.Load<Texture2D>("image/building_nicolas"));

            fonts.Add("medium", content.Load<SpriteFont>("font/medium"));
        }

        public static void Draw(string _asset, Rectangle _rect, Color _color, float _depth)
        {
            sb.Draw(textures[_asset], _rect, textures[_asset].Bounds, _color, 0f, new Vector2(), SpriteEffects.None, _depth);
        }
        public static void Draw(string _asset, Rectangle _rect, Rectangle _src, Color _color, float _depth)
        {
            sb.Draw(textures[_asset], _rect, _src, _color, 0f, new Vector2(), SpriteEffects.None, _depth);
        }
        public static void Draw(string _asset, Rectangle _rect, Color _color, float _depth, SpriteEffects _se)
        {
            sb.Draw(textures[_asset], _rect, textures[_asset].Bounds, _color, 0f, new Vector2(), _se, _depth);
        }
    }
}
