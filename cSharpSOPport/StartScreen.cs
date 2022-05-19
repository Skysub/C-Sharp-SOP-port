using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace cSharpSOPport
{
    class StartScreen
    {
        ResButton R2160, R1440, R1080, R720, R540, R360, R144;

        public StartScreen(Game1 game1) //Initialiserer knapperne til at vælge resolution
        {
            R2160 = new ResButton(new Size2(400, 45), new Vector2(440, 80), 2160, game1);
            R1440 = new ResButton(new Size2(400, 45), new Vector2(440, 160), 1440, game1);
            R1080 = new ResButton(new Size2(400, 45), new Vector2(440, 240), 1080, game1);
            R720 = new ResButton(new Size2(400, 45), new Vector2(440, 320), 720, game1);
            R540 = new ResButton(new Size2(400, 45), new Vector2(440, 400), 540, game1);
            R360 = new ResButton(new Size2(400, 45), new Vector2(440, 480), 360, game1);
            R144 = new ResButton(new Size2(400, 45), new Vector2(440, 560), 144, game1);
        }

        public virtual void Update(GameTime gameTime) //Opdaterer knapperne så de reagerer til at blive moused over eller klikket
        {
            R2160.Update(gameTime);
            R1440.Update(gameTime);
            R1080.Update(gameTime);
            R720.Update(gameTime);
            R540.Update(gameTime);
            R360.Update(gameTime);
            R144.Update(gameTime);

        }

        public virtual void Draw(SpriteBatch spriteBatch)//Tegner knapperne
        {
            R2160.Draw(spriteBatch);
            R1440.Draw(spriteBatch);
            R1080.Draw(spriteBatch);
            R720.Draw(spriteBatch);
            R540.Draw(spriteBatch);
            R360.Draw(spriteBatch);
            R144.Draw(spriteBatch);
        }
    }
}