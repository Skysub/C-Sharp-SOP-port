using Cocos2D;
using CocosDenshion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace cSharpSOPport
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static ContentManager content;
        public static bool start = true;
        public static SpriteFont Arial12;

        StartScreen startScreen;
        MainLogic mainLogic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;
        }

        protected override void LoadContent()//Loader skrifttypen der bruges til ald tekst i programmet og initialiserer vinduet samt skærmene.
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            Arial12 = Game1.content.Load<SpriteFont>("Arial12");

            startScreen = new StartScreen(this);
            mainLogic = new MainLogic(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Arial12, this);
        }

        protected override void Update(GameTime gameTime)
        {
            checkExit();

            if (start) //Styrer hvilken skærm der er aktiv
                startScreen.Update(gameTime);
            else
                mainLogic.Update(gameTime); ;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); //Skaber bagrundsfarven af startskærmen og clearer ellers skærmen før der tegnes noget nyt
            spriteBatch.Begin();

            if (start)// Styrer hvilken skærm der er aktiv
                startScreen.Draw(spriteBatch);
            else
                mainLogic.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void SkiftResolution(int width, int height)
        {
            start = false;
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
            mainLogic.SkiftResolution(width, height);
        }

        void checkExit()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }
    }
}