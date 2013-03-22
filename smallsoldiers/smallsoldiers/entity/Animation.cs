using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    class Animation
    {
        private string asset;
        private Rectangle source;
        private float depth;

        private int frame_count;
        private int offsetX;

        private float elapsed;
        private float frame_time;

        public Animation(string _asset, Rectangle _source, int _frame_count, int _offsetX, float _depth)
        {
            asset = _asset;
            elapsed = 0;
            frame_count = _frame_count;
            frame_time = Cons.FRAME_DURATION;
            offsetX = _offsetX;
            source = _source;
            depth = _depth;
        }

        public void Update(GameTime _gameTime)
        {
            elapsed += _gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed > frame_time)
            {
                elapsed -= frame_time;
                source.X = offsetX + (source.X + Cons.MAN_SIZE) % (frame_count * Cons.MAN_SIZE);
            }
        }

        public void Draw(Rectangle _rect)
        {
            Ressource.Draw(asset, _rect, source, Color.White, depth);
        }
    }
}
