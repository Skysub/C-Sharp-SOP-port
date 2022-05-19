using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;

namespace cSharpSOPport
{
    class ButtonBase
    {
        public bool MouseOver, Pressed;

        protected MouseState mouseStateNew = new MouseState();
        protected MouseState oldState;

        protected Vector2 recPos;
        protected Size2 size;

        public ButtonBase(Size2 size, Vector2 position)
        {
            Pressed = false;
            this.recPos = position;
            this.size = size;
        }

        public Rectangle BoundingBox //Med til at tjekke om musen er over knappen eller ej
        {
            get { return new Rectangle((int)recPos.X, (int)recPos.Y, (int)size.Width, (int)size.Height); }
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdateMus();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawRectangle(spriteBatch);
        }

        public void DrawRectangle(SpriteBatch spriteBatch)
        {
            if (MouseOver == true) //Med til at holde programmet følende responsivt overfor bruger input
                spriteBatch.DrawRectangle(recPos, size, Color.White, 25, 1);
            else
                spriteBatch.DrawRectangle(recPos, size, Color.Black, 25, 1f);
        }

        protected void UpdateMus() //Tjekker om musen trykker eller hover over knappen
        {
            mouseStateNew = Mouse.GetState();
            if (BoundingBox.Contains(mouseStateNew.Position) == true)
            {
                MouseOver = true;
                if (mouseStateNew.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    Pressed = true;
                }
            }
            else
            {
                MouseOver = false;
                Pressed = false;
            }
            oldState = mouseStateNew;
        }
    }
}
