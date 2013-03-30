using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using smallsoldiers.entity;

namespace smallsoldiers.gui
{
    class Button_action : Entity
    {
        private button_type type;

        public button_type GetSpecificity() { return type; }

        public Button_action(string _asset, button_type _type, Rectangle _rect, Rectangle _src)
            : base(_asset, _rect, _src, Color.White, Cons.DEPTH_HUD + 0.1f)
        {
            type = _type;
        }

        public bool IsSelected(Inputs _inputs)
        {
            if (!_inputs.GetIsML() && _inputs.GetMLpressed())
            {
                _inputs.SetIsML(false);
                return rect.Contains(_inputs.GetAbsoluteX(), _inputs.GetAbsoluteY());
            }
            if (_inputs.GetIsML() && _inputs.GetMLreleased())
            {
                _inputs.SetIsML(false);
            }
            return false;
        }
    }
}
