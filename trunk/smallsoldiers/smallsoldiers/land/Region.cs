using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;

namespace smallsoldiers.land
{
    class Region : Entity
    {
        private int valeur;
        private Rectangle jauge, fill_j1, fill_j2;
        private Color color1, color2;

        public Region(string _asset, int _x, int _y, int _width, int _height)
            : base(_asset, new Rectangle(_x, _y, _width, _height), Color.White, 0.1f)
        {
            valeur = 0;
            color1 = Color.Blue;
            color2 = Color.Red;

            jauge = new Rectangle(rect.X + rect.Width / 2 - 8, rect.Y + rect.Height / 4, 8, rect.Height / 2);
            fill_j1 = jauge;
            fill_j2 = jauge;
            fill_j1.Height = 0;
            fill_j2.Height = 0;
            fill_j1.Y = jauge.Y + jauge.Height / 2;
            fill_j2.Y = jauge.Y + jauge.Height / 2;
        }

        public void Update(Army _army1, Army _army2)
        {
            int army1_count = _army1.HowMany(rect);
            int army2_count = _army2.HowMany(rect);
            Console.WriteLine(army1_count);
            valeur = (int)(Math.Log(army1_count) + 1) - (int)(Math.Log(army2_count) + 1) + valeur;
            if (valeur > 126)
                valeur = 126;
            if (valeur < -126)
                valeur = -126;
            color1 = new Color(126 - valeur, 126 - (Math.Abs(valeur)), 126 + valeur);

            //if (army1_count > army2_count)
            //{
            //    if (valeur < jauge.Height/ 2)
            //    valeur++;
            //}
            //else if(army2_count > army1_count)
            //{
            //    if(-valeur < jauge.Height / 2)
            //    valeur--;
            //}

            //if (valeur > 0)
            //    fill_j1.Height = valeur;
            //else
            //{
            //    fill_j2.Height = -valeur;
            //    fill_j2.Y = valeur;
            //}
        }

        public override void Draw()
        {
            Ressource.Draw("pixel", jauge, color1, Cons.DEPTH_HUD + 0.11f); // en fct de l'altitude
            //Ressource.Draw("pixel", fill_j1, color1, Cons.DEPTH_HUD + 0.12f); // en fct de l'altitude
            //Ressource.Draw("pixel", fill_j2, color2, Cons.DEPTH_HUD + 0.12f); // en fct de l'altitude

            base.Draw();
        }
    }
}
