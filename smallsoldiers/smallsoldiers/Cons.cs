using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smallsoldiers
{
    enum e_GameMode
    {
        solo, multi
    }

    static class Cons
    {
        public static Random r = new Random();

        public const int MAN_SIZE = 32;
        public const int BUILDING_SIZE = MAN_SIZE * 3;
        public const int WIDTH = 1366;
        public const int HEIGHT = 768;
        public const int HOMELAND_SIZE = 200;
        public const int BATTLEFIELD_SIZE = WIDTH - 2 * HOMELAND_SIZE;
        public const int test_max_pop = 100;

        public const int entity_count = 10;

        public const float DEPTH_HUD = 0.8f;
        public const float FRAME_DURATION_SOLDIERS = 90; // Milliseconds
        public const float FRAME_DURATION_HIT = 150; // Milliseconds
        public const float FRAME_DURATION_SHOOT = 600; // Milliseconds
        public const float FRAME_DURATION_FLAGS = 150f; // Milliseconds

        public const float INCOME_DURATION = 5000f; // Milliseconds

        public static e_GameMode mode = e_GameMode.multi;
    }
}
