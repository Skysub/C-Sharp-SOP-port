﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace cSharpSOPport
{
    class Fluid
    {
        MoreFluid mF;

        int N;
        int iter;
        int SCALE;

        int size;
        float dt;
        float diff;
        float visc;

        float[] s;
        float[] density;

        float[] Vx;
        float[] Vy;

        float[] Vx0;
        float[] Vy0;

        public Fluid(float dt, float diffusion, float viscosity, int N, int iter, int scale)
        {
            this.N = N;
            this.iter = iter;
            SCALE = scale;

            this.size = N;
            this.dt = dt;
            this.diff = diffusion;
            this.visc = viscosity;

            this.s = new float[N * N];
            this.density = new float[N * N];

            this.Vx = new float[N * N];
            this.Vy = new float[N * N];

            this.Vx0 = new float[N * N];
            this.Vy0 = new float[N * N];

            mF = new MoreFluid(N, iter);
        }

        public void step()
        {
            int N = this.size;
            float visc = this.visc;
            float diff = this.diff;
            float dt = this.dt;
            float[] Vx = this.Vx;
            float[] Vy = this.Vy;
            float[] Vx0 = this.Vx0;
            float[] Vy0 = this.Vy0;
            float[] s = this.s;
            float[] density = this.density;

            mF.diffuse(1, Vx0, Vx, visc, dt);
            mF.diffuse(2, Vy0, Vy, visc, dt);

            mF.project(Vx0, Vy0, Vx, Vy);

            mF.advect(1, Vx, Vx0, Vx0, Vy0, dt);
            mF.advect(2, Vy, Vy0, Vx0, Vy0, dt);

            mF.project(Vx, Vy, Vx0, Vy0);

            mF.diffuse(0, s, density, diff, dt);
            mF.advect(0, density, s, Vx, Vy, dt);
        }

        public void addDensity(int x, int y, float amount)
        {
            int index = IX(x, y);
            this.density[index] += amount;
        }

        public void addVelocity(int x, int y, float amountX, float amountY)
        {
            int index = IX(x, y);
            this.Vx[index] += amountX;
            this.Vy[index] += amountY;
        }

        public void RenderD()
        {
            //colorMode(HSB, 255);

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    float x = i * SCALE;
                    float y = j * SCALE;
                    float d = this.density[IX(i, j)];
                   /* fill((d + 50) % 255, 200, d);
                    noStroke();
                    square(x, y, SCALE);*/
                }
            }
        }

        public void RenderV()
        {

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    float x = i * SCALE;
                    float y = j * SCALE;
                    float vx = this.Vx[IX(i, j)];
                    float vy = this.Vy[IX(i, j)];
                   /* stroke(255);
                    if (sqrt(vx * vx + vy * vy) > 0.01)
                    {
                        line(x, y, x + vx * SCALE * 50, y + vy * SCALE * 50);
                    }*/
                }
            }
        }

        public void RenderVelF()
        {
            //colorMode(HSB, 255);

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    float x = i * SCALE;
                    float y = j * SCALE;
                    float vx = this.Vx[IX(i, j)];
                    float vy = this.Vy[IX(i, j)];
                    float d = (float) Math.Sqrt(vx * vx + vy * vy) * 3000;
                    /*fill((d + 50) % 255, 200, d);
                    noStroke();
                    square(x, y, SCALE);*/
                }
            }
        }

        public void fadeD()
        {
            for (int i = 0; i < this.density.Length; i++)
            {
                float d = density[i];
                density[i] = (float) Math.Clamp(d - 0.02, 0, 255);
            }
        }

        int IX(int x, int y)
        {
            x = Math.Clamp(x, 0, N - 1);
            y = Math.Clamp(y, 0, N - 1);
            return x + (y * N);
        }
    }
}
