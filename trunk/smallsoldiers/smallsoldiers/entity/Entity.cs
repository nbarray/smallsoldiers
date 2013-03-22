using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.entity
{
    class Entity
    {
        private string asset;
        protected Rectangle rect;
        protected Rectangle source;
        protected Color color;

        public Entity(string _asset, Rectangle _rect, Color _color)
        {
            asset = _asset;
            rect = _rect;
            source = Ressource.Get(asset).Bounds;
            color = _color;
        }
        public Entity(string _asset, Rectangle _rect, Rectangle _source, Color _color)
        {
            asset = _asset;
            rect = _rect;
            source = _source;
            color = _color;
        }

        public void Draw()
        {
            Ressource.sb.Draw(Ressource.Get(asset), rect, source, color);
        }
    }
}
