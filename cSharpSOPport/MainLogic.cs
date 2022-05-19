using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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

        public MainLogic(int width, int height, SpriteFont Arial12, Game1 game1)
        {
            this.game1 = game1;
            this.Arial12 = Arial12;
            this.width = width;
            this.height = height;
            int res = 256;
            fieldSim = new FieldSim(res, 10, width, height);
        }

        public void Update(GameTime gameTime)
        {

            /*if (kb.Shift(10)) testRun = true;
            if (kb.Shift(81)) QuickRunTest();
            if (kb.Shift(87)) RunTestIte();*/


            HandleControls();
            HandleUI();

            //if (!kb.getToggle(32)) if (!kb.getToggle(84)) println(fieldSim.Update());


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!kb.getToggle(84)) fieldSim.Draw(kb.getToggle(86), kb.getToggle(32), kb.getToggle(67), UI, spriteBatch);
            DrawUI();
        }

        void HandleControls()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();

            /*if (kb.Shift(82)) fieldSim = new FieldSim(128, 5);
            if (kb.getToggle(86) && kb.Shift(67)) kb.setToggle(86, false);
            if (kb.getToggle(67) && kb.Shift(86)) kb.setToggle(67, false);*/
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

        void UpdateToggle()
        {
            foreach (Keys x in tog.Keys)
            {
                if (Shift(x))
                {
                    tog[x] = !tog[x];
                }
            }
        }
    }
}
