using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;

namespace cSharpSOPport
{
    /*
        Den store forskel mellem basisknappen og barnet her, er tilføjelsen af tekst til midten af knappen, samt den ønskede funktionalitet at knappen skifter resolution og skærm
    */
    class ResButton : ButtonBase //Nedarver fra en basisknap
    {
        int posX, posY, x, y;
        float scale;

        public static SpriteFont Arial12;
        Game1 game1;

        public ResButton(Size2 size, Vector2 position, Game1 game1, int x, int y, float scale) : base(size, position) //Constructer der tilføjer funktionalitet over base-knappen
        {
            this.game1 = game1;
            this.x = x;
            this.y = y;
            this.scale = scale;
            Arial12 = Game1.content.Load<SpriteFont>("Arial12");
            posX = (int)(position.X + size.Width / 2 - (Arial12.MeasureString(x + " x " + y + " : " + scale)).X / 2);
            posY = (int)position.Y;
        }

        public override void Update(GameTime gameTime) //Base-knappens update og draw funktion bliver overridden for at tilføje ekstra funktionalitet
        {
            UpdateMus();

            if (Pressed)
            {
                SkiftResOScreen(x, y, scale);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawRectangle(spriteBatch); //Knappen tegnes
            DrawText(spriteBatch); //teksten inde i knappen tegnes
        }

        public void DrawText(SpriteBatch spriteBatch)
        {
            if (MouseOver) //Programmet føles responsive hvis knapperne reagerer på input før der overhovedet bliver trykket på en knap
            {
                spriteBatch.DrawString(Arial12, (x + " x " + y + " : " + scale), new Vector2(posX, posY), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(Arial12, (x + " x " + y + " : " + scale), new Vector2(posX, posY), Color.White);
            }
        }

        public void SkiftResOScreen(int x, int y, float scale) //Programmets tilstand går fra start-skærmen til hovedskærmen, og opløsningen af vinduet bliver ændret til det der blev valgt af brugeren
        {
            game1.SkiftResolution(x, y, scale);
        }
    }
}
