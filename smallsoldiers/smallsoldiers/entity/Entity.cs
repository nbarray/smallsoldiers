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
            source = asset != "null" ? Ressource.Get(asset).Bounds : new Rectangle(0,0,0,0);
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

        public virtual void Draw(bool _isOffset)
        {
            if(asset != "null")
                Ressource.Draw(asset, rect, source, color, 0.5f + ((float)(rect.Y + rect.Height)) / 10000f, _isOffset);
        }
        public virtual void DrawDepth(bool _isOffset)
        {
            if (asset != "null")
                Ressource.Draw(asset, rect, source, color, depth, _isOffset);
        }
        public virtual void Draw(SpriteEffects _se, bool _isOffset)
        {
            if (asset != "null")
                Ressource.Draw(asset, rect, source, color, 0.5f + ((float)(rect.Y + rect.Height)) / 10000f, _se, _isOffset);
        }
        public virtual void Draw(SpriteEffects _se, int _alt, float _angle, bool _isOffset)
        {
            if (asset != "null")
                Ressource.Draw(asset, rect, source, color, 0.5f + ((float)(rect.Y + rect.Height + _alt)) / 10000f, _se, _angle, _isOffset);
        }
    }
}
