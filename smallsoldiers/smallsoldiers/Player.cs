using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.gui;
using smallsoldiers.land;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;
using smallsoldiers.son;
using Microsoft.Xna.Framework.Input;

namespace smallsoldiers
{
    class Player
    {
        private string name;
        private List<Point> pushes;
        private Homeland home;
        private bool player;
        public Army army;
        public Flag default_flag;
        private float elapsedTime;

        private int camX, camY;

        private int army_population;
        private int income, benefice;

        public int GetIncome() { return income; }
        public void AddToIncome(int _i) { income += _i; }
        public void RemoveFromIncome(int _i) { income -= _i; }
        public int GetPopulation() { return army_population; }

        public bool IsPlayer() { return player; }
        public Player(string _name, bool _player)
        {
            home = new Homeland(_player, this);
            home.InitializeSlots();
            player = _player;
            army = new Army(_player);

            default_flag = new Flag("flag_louis");
            default_flag.set_new_pos(Cons.BATTLEFIELD_SIZE / 2, Cons.HEIGHT / 2, false);

            name = _name;
            army_population = 0;
            elapsedTime = 0f;
            camX = 0;
            camY = 0;

            pushes = new List<Point>();

            income = 200;
            benefice = 15;
        }

        public void Update(GameTime _gameTime, Army _ennemy, Inputs _inputs, Music _soundeffect, Hud _hud)
        {
            army_population = army.soldiers.Count;
            Update_income(_gameTime);

            if (!player) // joueur 2
            {
                Update_IA(_gameTime);
            }
            else
            {
                home.Update(_gameTime, _inputs, _hud);
            }
            army.Update(_gameTime, _ennemy, _soundeffect);
            //army.make_pushes(ref pushes);
            //army.push(pushes);
        }

        private void Update_IA(GameTime _gameTime)
        {
            home.Update_IA(_gameTime, this);
        }
        private void Update_income(GameTime _gameTime)
        {
            elapsedTime += _gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime > Cons.INCOME_DURATION) // 20 secondes
            {
                elapsedTime -= Cons.INCOME_DURATION;
                income += benefice;
            }
        }

        public void Draw()
        {
            home.Draw();
            army.Draw();
        }
    }
}
