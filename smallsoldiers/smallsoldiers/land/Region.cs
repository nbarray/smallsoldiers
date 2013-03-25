using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;

namespace smallsoldiers.land
{
    enum e_Region_type
    {
        none, food, ore
    }

    class Region : Entity
    {
        private int valeur;
        private Point[,] brush_pos;
        private e_Region_type type;

        private Rectangle rect_region_staff, rect_flag;
        private Color flag_color;
        private Animation anim_region_flag;
        
        public Region(int _x, int _y, int _width, int _height)
            : base("null", new Rectangle(_x, _y, _width, _height), Color.White, 0.1f)
        {
            valeur = 0;
            type = (e_Region_type)Cons.r.Next(3);

            rect_region_staff = new Rectangle(rect.X + (rect.Width + 4) / 2, rect.Y + (rect.Height + 64) / 2, 4, 64);
            anim_region_flag = new Animation("flag_region", new Rectangle(0, 0, 32, 32), 5, 0, Cons.DEPTH_HUD, true);
            rect_flag = new Rectangle(rect_region_staff.X + rect_region_staff.Width, rect_region_staff.Y + rect_region_staff.Height-32, 32, 32);
            flag_color = Color.White;

            switch (type)
            {
                case e_Region_type.none:
                    break;
                case e_Region_type.food:
                    //Food
                    int food_count = 5;
                    brush_pos = new Point[food_count, food_count];
                    for (int i = 0; i < food_count; i++)
                    {
                        for (int j = 0; j < food_count; j++)
                        {
                            brush_pos[i, j] = new Point(Cons.r.Next(rect.X, rect.X + rect.Width - 8), Cons.r.Next(rect.Y, rect.Y + rect.Height - 8));
                        }
                    }
                    break;
                case e_Region_type.ore:
                    //Ore
                    int ore_count = 2;
                    brush_pos = new Point[ore_count, ore_count];
                    for (int i = 0; i < ore_count; i++)
                    {
                        for (int j = 0; j < ore_count; j++)
                        {
                            brush_pos[i, j] = new Point(Cons.r.Next(rect.X, rect.X + rect.Width - 8), Cons.r.Next(rect.Y, rect.Y + rect.Height - 8));
                        }
                    }
                    break;
                default:
                    break;
            }

            
        }

        public void Update(GameTime _gameTime, Army _army1, Army _army2)
        {
            int army1_count = _army1.HowMany(rect);
            int army2_count = _army2.HowMany(rect);

            valeur = (int)(army1_count) - (int)(army2_count) + valeur;
            if (valeur > 32)
                valeur = 32;
            if (valeur < -32)
                valeur = -32;

            if(valeur != 0)
            anim_region_flag.Update(_gameTime);

            rect_flag.Y = (rect_region_staff.Y + rect_region_staff.Height - 32) - (Math.Abs(valeur));

            if (valeur > 0)
            {
                flag_color = Color.Blue;
            }
            else if(valeur < 0)
            {
                flag_color = Color.Red;
            }
            else
            {
                flag_color = Color.White;
            }
        }

        public override void Draw()
        {
            // flag de contrôle
            if (valeur != 0)
            {
                Ressource.Draw("flag_staff", rect_region_staff, Color.White, Cons.DEPTH_HUD);
                anim_region_flag.Draw(rect_flag, flag_color);

            }
            #region Entities de la région

            switch (type)
            {
                case e_Region_type.none:
                    break;
                case e_Region_type.food:
                    for (int i = 0; i < brush_pos.GetLength(0); i++)
                    {
                        for (int j = 0; j < brush_pos.GetLength(1); j++)
                        {
                            Ressource.Draw("buisson", new Rectangle(brush_pos[i, j].X,
                                brush_pos[i, j].Y, 16, 16), Color.Wheat, 
                                0.5f + ((float)(brush_pos[i, j].Y + rect.Height)) / 100000f);
                        }
                    }
                    break;
                case e_Region_type.ore:
                    for (int i = 0; i < brush_pos.GetLength(0); i++)
                    {
                        for (int j = 0; j < brush_pos.GetLength(1); j++)
                        {
                            Ressource.Draw("ore", new Rectangle(brush_pos[i, j].X, 
                                brush_pos[i, j].Y, 16, 16), Color.Wheat, 0.6f);
                        }
                    }
                    break;
                default:
                    break;
            } 
            #endregion

            base.Draw();
        }
    }
}
