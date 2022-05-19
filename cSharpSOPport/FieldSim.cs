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

        public FieldSim(int N, int iter, int width, int height)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
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
            //println(timerS);
            return timerS;
        }

        public void Draw(bool showVel, bool paused, bool showVelF, bool UI, SpriteBatch spriteBatch)
        {
            /*textSize(40);
            background(0);

            fill(255);
            stroke(255);*/
            timerR = (int)stopwatch.ElapsedMilliseconds;
            if (!showVel && !showVelF) fluid.RenderD();
            else if (!showVelF) fluid.RenderV();
            else fluid.RenderVelF();
            timerR = (int)stopwatch.ElapsedMilliseconds - timerR;

            if (UI)
            {
                /*fill(255);
                stroke(255);
                if (paused) text("Sim time:      " + 0f, 10, 90);
                else text("Sim time:      " + ((int)Math.Round(timerS / 100000f) / 10000f), 10, 90);
                text("Render time: " + (timerR / 1000f), 10, 135);*/

                //fluid.renderV();
                //fluid.fadeD();

                timer = (int)stopwatch.ElapsedMilliseconds - timer;
                //text("Frametime: "+int(floor(timer/1000f))+"."+(timer-1000*int(floor(timer/1000f))),10, 45);
                /*text("Frametime:   " + (timer / 1000f), 10, 45);
                text("FrameRate:   " + Math.Floor(1f / ((timer / 1000f))), 500, 45);
                text("Resolution: " + N + "x" + N, 10, 180);*/
                //println(timer);
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
