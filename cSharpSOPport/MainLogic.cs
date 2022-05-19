using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace cSharpSOPport
{
    class MainLogic
    {
        FieldSim fieldSim;
        Game1 game1;
        bool testRun;
        float[] testResults;
        bool start = true;
        int test, punkter, ialt;
        bool quick;
        int scale;
        bool UI = true;

        int width, height;
        Color ui_txtColor = Color.Orange;
        KeyboardState currentState, previousState;
        SpriteFont Arial12;
        int menuPick = 0;
        Dictionary<Keys, bool> tog = new Dictionary<Keys, bool>();
        Stopwatch stopwatch;

        public MainLogic(int width, int height, SpriteFont Arial12, Game1 game1)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            this.game1 = game1;
            this.Arial12 = Arial12;
            this.width = width;
            this.height = height;
            int res = 256;
            AddToggle();
            fieldSim = new FieldSim(res, 10, width, height, ui_txtColor, Arial12, stopwatch);

        }

        public void Update(GameTime gameTime)
        {
            HandleControls();
            HandleUI();

            if (!tog[Keys.Space] && !tog[Keys.T]) Console.WriteLine(fieldSim.Update());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!tog[Keys.T]) fieldSim.Draw(tog[Keys.V], tog[Keys.Space], tog[Keys.C], UI, spriteBatch);
            DrawUI();
        }

        void HandleControls()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
            UpdateToggle();

            if (Shift(Keys.R)) fieldSim = new FieldSim(128, 5, width, height, ui_txtColor, Arial12, stopwatch);
            if (tog[Keys.V] && Shift(Keys.C)) tog[Keys.V] = false;
            if (tog[Keys.C] && Shift(Keys.V)) tog[Keys.C] = false;
        }

        void HandleUI()
        {

        }

        void DrawUI()
        {

        }

        public void SkiftResolution(int width, int height)
        {
            this.width = width;
            this.height = height;
            fieldSim.SkiftResolution(width, height);
        }

        bool Shift(Keys key)
        {
            return currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
        }

        void AddToggle()
        {
            tog.Add(Keys.C, false);
            tog.Add(Keys.Space, false);
            tog.Add(Keys.V, false);
            tog.Add(Keys.T, false);
        }

        void UpdateToggle()
        {
            foreach (Keys x in tog.Keys)
            {
                if (Shift(x))
                {
                    tog[x] = !tog[x];
                    return;
                }
            }
        }
    }
}
