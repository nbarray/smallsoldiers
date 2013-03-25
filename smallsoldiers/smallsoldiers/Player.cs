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
        private Homeland home;
        private bool player;
        public Army army;
        public Flag default_flag;
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
            army = new Army(_player);
            default_flag = new Flag("flag_louis");
            default_flag.set_new_pos(Cons.BATTLEFIELD_SIZE / 2, Cons.HEIGHT / 2, false);
            elapsedTime = 0f;
            army_population = 0;
            income = 200;
            benefice = 15;
        }

        public void Update(GameTime _gameTime, Army _ennemy, int _mx, int _my, bool _mpressed, bool _rpressed, Music _soundeffect)
        {
            army_population = army.soldiers.Count;
            Update_income(_gameTime);

            if (!player) // joueur 2
            {
                if (Cons.mode == e_GameMode.solo)
                    Update_IA(_gameTime);
                else
                    Update_Joueur(PlayerIndex.Two, _gameTime);
            }
            else
            {
                if (Cons.mode == e_GameMode.solo)
                    home.Update(_gameTime, _mx, _my, _mpressed, _rpressed);
                else
                    Update_Joueur(PlayerIndex.One, _gameTime);
            }
            army.Update(_gameTime, _ennemy, _soundeffect);
        }

        private void Update_IA(GameTime _gameTime)
        {
            home.Update_IA(_gameTime, this);
        }

        float gpx = 0;
        float gpy = 0;
        public int GetGPY() { return (int)gpy; }
        public int GetGPX() { return (int)gpx; }
        private void Update_Joueur(PlayerIndex _index, GameTime _gameTime)
        {
            GamePadState gp = GamePad.GetState(_index);
            gpx += gp.ThumbSticks.Right.X * 20 + gp.ThumbSticks.Left.X * 5;
            gpy -= gp.ThumbSticks.Right.Y * 20 + gp.ThumbSticks.Left.Y * 5;
            home.Update(_gameTime, (int)gpx, (int)gpy, gp.Buttons.A == ButtonState.Pressed, gp.Buttons.B == ButtonState.Pressed);
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
