using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace smallsoldiers.land
{
    class Battlefield
    {
        private Rectangle rect;
        private Region[,] regions;
        public Battlefield()
        {
            rect = new Rectangle(Cons.HOMELAND_SIZE - 32, 0, Cons.BATTLEFIELD_SIZE + 64, Cons.HEIGHT);

            int regions_width = Cons.BATTLEFIELD_SIZE / Cons.REGION_SIZE;
            int regions_height = Cons.HEIGHT / Cons.REGION_SIZE - 1;

            regions = new Region[regions_width, regions_height];
            for (int i = 0; i < regions.GetLength(0); i++)
            {
                for (int j = 0; j < regions.GetLength(1); j++)
                {
                    regions[i, j] = new Region("slot01", 
                                               Cons.HOMELAND_SIZE + i * Cons.REGION_SIZE + i * 8, 
                                               j * Cons.REGION_SIZE + j * 8 + 32 * 2);
                }
            }
        }

        public void Update(int _mx, int _my, bool _mpressed)
        {
            for (int i = 0; i < regions.GetLength(0); i++)
            {
                for (int j = 0; j < regions.GetLength(1); j++)
                {
                    regions[i, j].Update(_mx, _my, _mpressed);
                }
            }
        }

        public void Draw()
        {
            Ressource.Draw("bg01", rect, Color.White, 0f);
            for (int i = 0; i < regions.GetLength(0); i++)
            {
                for (int j = 0; j < regions.GetLength(1); j++)
                {
                    regions[i, j].Draw();
                }
            }
        }
    }
}
