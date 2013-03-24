using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smallsoldiers.gui;
using smallsoldiers.land;
using smallsoldiers.entity;
using Microsoft.Xna.Framework;

namespace smallsoldiers
{
    class Player
    {
        private string name;
        private Homeland home;
        private bool player;
        public Army army;
        private Flag default_flag;
        private float elapsedTime;

        private int army_population;
        private int income, benefice;

        public int GetIncome() { return income; }
        public void AddToIncome(int _i) { income += _i; }
        public void RemoveFromIncome(int _i) { income -= _i; }
        public int GetPopulation() { return army_population; }

        public bool IsPlayer() { return player; }
        public Player(string _name, bool _player)
        {
            name = _name;
            home = new Homeland(_player, this);
            home.InitializeSlots();
            player = _player;
            army = new Army();
            default_flag = new Flag("flag_louis");
            elapsedTime = 0f;
            army_population = 0;
            income = 4;
            benefice = 1;
        }

        public void Update(GameTime _gameTime, Army _ennemy, int _mx, int _my, bool _mpressed, bool _rpressed)
        {
            army_population = army.soldiers.Count;
            Update_income(_gameTime);
            
            if (!player)
                Update_IA(_gameTime, this);
            else
                home.Update(_gameTime, _mx, _my, _mpressed, _rpressed);
            
            army.Update(_gameTime, _ennemy);
        }
        private void Update_IA(GameTime _gameTime, Player _p)
        {
            home.Update_IA(_gameTime, _p);
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
