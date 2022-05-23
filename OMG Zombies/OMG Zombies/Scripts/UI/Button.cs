using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace OMG_Zombies.Scripts.UI
{
    public class Button
    {
        #region Campos e propriedades

        // sobre textura
        private Texture2D texture;
        private int width;
        private int height;
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }

        // sobre texto
        private SpriteFont font;
        public Color TextColor { get; set; }
        public Color BackgroundColor { get; set; }
        public string Text { get; set; }

        // sobre eventos
        public event EventHandler Click;
        private MouseState currentMouse;
        private MouseState previousMouse;

        #endregion


        #region Métodos

        public Button(Texture2D texture, SpriteFont font, int width, int height)
        {
            this.texture = texture;
            this.font = font;
            this.width = width;
            this.height = height;

            TextColor = Color.White;
            BackgroundColor = Color.White;
        }

        public Button(Texture2D texture, SpriteFont font, float scale)
        {
            this.texture = texture;
            this.font = font;

            TextColor = Color.White;
            BackgroundColor = Color.White;
        }

        public void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(Rectangle))
            {
                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public void Draw()
        {
            BackgroundColor = Color.White;

            Game1._spriteBatch.Draw(texture, Rectangle, BackgroundColor);

            if (!string.IsNullOrEmpty(Text))
            {
                float x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                float y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                Game1._spriteBatch.DrawString(font, Text, new Vector2(x, y), TextColor);
            }
        }

        #endregion
    }
}