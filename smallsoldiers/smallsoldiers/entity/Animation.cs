using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace smallsoldiers.entity
{
    class Animation
    {
        private string asset;
        private Rectangle source;
        private float depth;

        private int frame_count;
        private int offsetX, indexX;

        private float elapsed;
        private float frame_time;

        public Animation(string _asset, Rectangle _source, int _frame_count, int _offsetX, float _depth, bool _isflag)
        {
            asset = _asset;
            elapsed = 0;
            frame_count = _frame_count;
            frame_time = Cons.FRAME_DURATION_SOLDIERS;
            if (_isflag)
                frame_time = Cons.FRAME_DURATION_FLAGS;
            offsetX = _offsetX;
            indexX = 0;
            source = _source;
            depth = _depth;
        }
        public Animation(string _asset, Rectangle _source, int _frame_count, int _offsetX, float _depth, 
            bool _isflag, bool _isranger)
        {
            asset = _asset;
            elapsed = 0;
            frame_count = _frame_count;
            frame_time = (_isranger)? Cons.FRAME_DURATION_SHOOT : Cons.FRAME_DURATION_HIT;
            if (_isflag)
                frame_time = Cons.FRAME_DURATION_FLAGS;
            offsetX = _offsetX;
            indexX = 0;
            source = _source;
            depth = _depth;
        }

        public bool Update(GameTime _gameTime)
        {
            elapsed += _gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed > frame_time)
            {
                elapsed = 0;
                indexX =  ((indexX + 1) % frame_count);
                source.X = (offsetX + indexX) * source.Width;
                return (source.X == 12 * 32);
            }
            else
                return false;
        }

        public void level_up(int size)
        {
            source.Y += size;
        }

        public void Draw(Rectangle _rect)
        {
            Ressource.Draw(asset, _rect, source, Color.White, 0.5f + ((float)(_rect.Y + _rect.Height)) / 10000f, true);
        }
        public void Draw(Rectangle _rect, Color _c)
        {
            Ressource.Draw(asset, _rect, source, _c, 0.5f + ((float)(_rect.Y + _rect.Height)) / 10000f, true);
        }
        public void Draw(Rectangle _rect, SpriteEffects _se)
        {
            Ressource.Draw(asset, _rect, source, Color.White, 0.5f + ((float)(_rect.Y + _rect.Height)) / 10000f, _se, true);
        }
    }
}
