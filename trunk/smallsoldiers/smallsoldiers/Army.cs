using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.entity;

namespace smallsoldiers
{
    class Army
    {
        public List<Soldier> soldiers;

        public Army()
        {
            soldiers = new List<Soldier>();
        }

        public void Add_soldier(Soldier _s)
        {
            soldiers.Add(_s);
        }

        public void Update()
        {
            foreach (Soldier item in soldiers)
            {
                item.Update();
            }
        }

        public void Draw()
        {
            foreach (Soldier item in soldiers)
            {
                item.Draw();
            }
        }
    }
}
