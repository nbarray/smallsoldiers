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

        public bool Update(GameTime _gameTime)
        {
            elapsed += _gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed > frame_time)
            {
                elapsed -= frame_time;
                indexX = offsetX + (indexX + 1) % frame_count;
                source.X = indexX * source.Width;
                return true;
            }
            else
                return false;
        }

        public void Draw(Rectangle _rect)
        {
            Ressource.Draw(asset, _rect, source, Color.White, depth);
        }
        public void Draw(Rectangle _rect, SpriteEffects _se)
        {
            Ressource.Draw(asset, _rect, source, Color.White, depth, _se);
        }
    }
}
