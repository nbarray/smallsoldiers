﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.gui
{
    class Hud
    {
        Rectangle top_rect;
        Rectangle flag_rect01, flag_rect02, cursor_j1, cursor_j2;

        string p1_income;
        string p2_income;
        string p1_pop;
        string p2_pop;

        public Hud()
        {
            top_rect = new Rectangle(0, 0, Cons.WIDTH, 64);
            flag_rect01 = new Rectangle(10, -2, 64, 64);
            flag_rect02 = new Rectangle(Cons.WIDTH - 64 - 10, -2, 64, 64);

            p1_income = "0 g";
            p2_income = "0 g";

            p1_pop = "0 pop";
            p2_pop = "0 pop";

            cursor_j1 = new Rectangle(0, 0, 16, 16);
            cursor_j2 = new Rectangle(Cons.WIDTH, 0, 16, 16);
        }

        public void Update(Player _p1, Player _p2)
        {
            if (Cons.mode == e_GameMode.multi)
            {
                cursor_j1.X = _p1.GetGPX();
                cursor_j1.Y = _p1.GetGPY();

                cursor_j2.X = _p2.GetGPX();
                cursor_j2.Y = _p2.GetGPY();
            }

            p1_pop = _p1.GetPopulation().ToString() + " pop";
            p2_pop = _p2.GetPopulation().ToString() + " pop";

            p1_income = _p1.GetIncome().ToString() + " g";
            p2_income = _p2.GetIncome().ToString() + " g";
        }

        public void Draw()
        {
            if (Cons.mode == e_GameMode.multi)
            {
                Ressource.Draw("pixel", cursor_j1, Color.Blue, Cons.DEPTH_HUD + 0.1f);
                Ressource.Draw("pixel", cursor_j2, Color.DarkRed, Cons.DEPTH_HUD + 0.1f);
            }

            Ressource.Draw("hud01", top_rect, Color.White, Cons.DEPTH_HUD);
            Ressource.Draw("flag01", flag_rect01, Color.White, Cons.DEPTH_HUD + 0.01f);
            Ressource.Draw("flag02", flag_rect02, Color.White, Cons.DEPTH_HUD + 0.01f);

            Vector2 p1_income_position = new Vector2(flag_rect01.X + flag_rect01.Width + 4, flag_rect01.Y);
            Vector2 p2_income_position = new Vector2(flag_rect02.X - Ressource.GetFont("medium").MeasureString(p2_income).X - 4, flag_rect01.Y);
           
            Ressource.DrawString("medium", p1_income, p1_income_position, Color.Yellow, Cons.DEPTH_HUD + 0.01f);
            Ressource.DrawString("medium", p2_income, p2_income_position, Color.Yellow, Cons.DEPTH_HUD + 0.01f);

            Vector2 p1_pop_position = new Vector2(flag_rect01.X + flag_rect01.Width + 4, flag_rect01.Y + 32);
            Vector2 p2_pop_position = new Vector2(flag_rect02.X - Ressource.GetFont("medium").MeasureString(p2_pop).X - 4, flag_rect01.Y + 32);

            Ressource.DrawString("medium", p1_pop, p1_pop_position, Color.Yellow, Cons.DEPTH_HUD + 0.01f);
            Ressource.DrawString("medium", p2_pop, p2_pop_position, Color.Yellow, Cons.DEPTH_HUD + 0.01f);
        }
    }
}
