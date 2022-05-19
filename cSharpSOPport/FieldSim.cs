using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace cSharpSOPport
{
    class FieldSim
    {
        int N;
        int iter;
        int scale;
        int width;
        int height;
        Fluid fluid;
        Random rd;

        float t = 0;
        int timer = 0, timerR = 0;
        long timerS = 0;
        Stopwatch stopwatch;
        Color ui_txtColor;
        SpriteFont Arial12;

        public FieldSim(int N, int iter, int width, int height, Color ui_txtColor, SpriteFont Arial12, Stopwatch sw)
        {
            this.Arial12 = Arial12;
            this.ui_txtColor = ui_txtColor;
            stopwatch = sw;
            rd = new Random();
            this.width = width;
            this.height = height;
            this.N = N;
            this.iter = iter;
            this.scale = (int)Math.Round((float)width / N);

            //standard viscosity er 0.0000001
            fluid = new Fluid(0.2f, 0, 0.0000001f, N, iter, scale);
        }

        public long Update()
        {
            int cx = 5;
            int cy = (int)Math.Round(0.5 * height / scale);
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    fluid.addDensity(cx + i, cy + j, rd.Next(50, 150));
                    //fluid.addDensity(cx+i, cy+j, 100);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                //float angle = noise(t) * TWO_PI * 2;
                //PVector v = PVector.fromAngle(angle);
                Vector2 v = new Vector2(1, 0);
                v = Vector2.Multiply(v, 0.1f);
                t += 0.01f;
                fluid.addVelocity(cx, cy, v.X, v.Y);
            }

            timerS = (int)stopwatch.ElapsedMilliseconds;
            fluid.step();
            timerS = (int)stopwatch.ElapsedMilliseconds - timerS;
            Console.WriteLine(timerS);
            return timerS;
        }

        public void Draw(bool showVel, bool paused, bool showVelF, bool UI, SpriteBatch spriteBatch)
        {
            timerR = (int)stopwatch.ElapsedMilliseconds;
            if (!showVel && !showVelF) fluid.RenderD(spriteBatch);
            else if (!showVelF) fluid.RenderV(spriteBatch);
            else fluid.RenderVelF(spriteBatch);
            timerR = (int)stopwatch.ElapsedMilliseconds - timerR;

            if (UI)
            {
                if (paused) spriteBatch.DrawString(Arial12, "Sim time:      " + 0f, new Vector2(10, 90), ui_txtColor);
                else spriteBatch.DrawString(Arial12, "Sim time:      " + ((int)Math.Round(timerS / 100000f) / 10000f), new Vector2(10, 90), ui_txtColor);
                spriteBatch.DrawString(Arial12, "Render time: " + (timerR / 1000f), new Vector2(10, 135), ui_txtColor);

                //fluid.renderV();
                //fluid.fadeD();

                timer = (int)stopwatch.ElapsedMilliseconds - timer;
                //text("Frametime: "+int(floor(timer/1000f))+"."+(timer-1000*int(floor(timer/1000f))),10, 45);
                spriteBatch.DrawString(Arial12, "Frametime:   " + (timer / 1000f), new Vector2(10, 45), ui_txtColor);
                String text = "FrameRate:   " + Math.Floor(1f / ((timer / 1000f)));
                spriteBatch.DrawString(Arial12, text, new Vector2(500, 45), ui_txtColor);
                spriteBatch.DrawString(Arial12, "Resolution: " + width + "x" + height, new Vector2(10, 180), ui_txtColor);
                timer = (int)stopwatch.ElapsedMilliseconds;
            }
        }

        public void SkiftResolution(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
