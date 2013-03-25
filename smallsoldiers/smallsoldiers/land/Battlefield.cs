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

            int region_width = 300;
            int region_height = 300;

            int regions_width = Cons.BATTLEFIELD_SIZE / region_width;
            int regions_height = Cons.HEIGHT / region_height;

            regions = new Region[regions_width, regions_height];
            for (int i = 0; i < regions.GetLength(0); i++)
            {
                for (int j = 0; j < regions.GetLength(1); j++)
                {
                    regions[i, j] = new Region(Cons.HOMELAND_SIZE + i * region_width + i * 8,
                                               j * region_height + j * 8 + 32 * 2, region_width, region_height);
                }
            }
        }

        public void Update(GameTime _gameTime, Player _p1, Player _p2)
        {
            for (int i = 0; i < regions.GetLength(0); i++)
            {
                for (int j = 0; j < regions.GetLength(1); j++)
                {
                    regions[i, j].Update(_gameTime, _p1.army, _p2.army);
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
