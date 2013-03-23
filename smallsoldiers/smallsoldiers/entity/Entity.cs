using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace smallsoldiers.entity
{
    abstract class Entity
    {
        protected string asset;
        protected Rectangle rect;
        protected Rectangle source;
        protected Color color;
        protected float depth;

        public Entity(string _asset, Rectangle _rect, Color _color, float _depth)
        {
            asset = _asset;
            rect = _rect;
            source = Ressource.Get(asset).Bounds;
            color = _color;
            depth = _depth;
        }
        public Entity(string _asset, Rectangle _rect, Rectangle _source, Color _color, float _depth)
        {
            asset = _asset;
            rect = _rect;
            source = _source;
            color = _color;
            depth = _depth;
        }

        public virtual void Draw()
        {
            Ressource.Draw(asset, rect, source, color, depth);
        }

        public virtual void Draw(SpriteEffects _se)
        {
            Ressource.Draw(asset, rect, source, color, depth, _se);
        }
    }
}
