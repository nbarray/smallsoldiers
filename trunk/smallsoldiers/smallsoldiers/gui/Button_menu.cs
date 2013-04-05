using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;

namespace smallsoldiers.gui
{
    enum e_Button_Type { link, }

    class Button_menu : Entity
    {
        private string text;
        private Vector2 position;

        public Button_menu(Rectangle _rect, string _text)
            : base("pixel", _rect, Color.DarkBlue, Cons.DEPTH_HUD + 0.01f)
        {
            text = _text;
            position = new Vector2(_rect.X + (_rect.Width - Ressource.GetFont("medium").MeasureString(text).X) / 2, _rect.Y + (_rect.Height - Ressource.GetFont("medium").MeasureString(text).Y)/ 2);
        }

        public bool Selected(Inputs _inputs)
        {
            return rect.Contains(_inputs.GetAbsoluteX(), _inputs.GetAbsoluteY()) && _inputs.GetMLpressed();
        }

        public override void Draw(bool _isOffset)
        {
            Ressource.DrawString("medium", text, position, Color.White, Cons.DEPTH_HUD + 0.02f, false);
            base.Draw(_isOffset);
        }
    }
}
