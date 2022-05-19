using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace cSharpSOPport
{
    class StartScreen
    {
        ResButton R1024, R512, RR512, RR256, Rny;

        public StartScreen(Game1 game1) //Initialiserer knapperne til at vælge resolution
        {
            R1024 = new ResButton(new Size2(400, 45), new Vector2(440, 100), game1, 1024, 1024, 4);
            R512 = new ResButton(new Size2(400, 45), new Vector2(440, 200), game1, 1024, 1024, 2);

            RR512 = new ResButton(new Size2(400, 45), new Vector2(440, 300), game1, 512, 512, 2);

            RR256 = new ResButton(new Size2(400, 45), new Vector2(440, 400), game1, 256, 256, 1);
            Rny = new ResButton(new Size2(400, 45), new Vector2(440, 500), game1, 1088, 1024, 4);
        }

        public virtual void Update(GameTime gameTime) //Opdaterer knapperne så de reagerer til at blive moused over eller klikket
        {

            R1024.Update(gameTime);
            R512.Update(gameTime);
            RR512.Update(gameTime);
            RR256.Update(gameTime);
            Rny.Update(gameTime);

        }

        public virtual void Draw(SpriteBatch spriteBatch)//Tegner knapperne
        {

            R1024.Draw(spriteBatch);
            R512.Draw(spriteBatch);
            RR512.Draw(spriteBatch);
            RR256.Draw(spriteBatch);
            Rny.Draw(spriteBatch);
        }
    }
}