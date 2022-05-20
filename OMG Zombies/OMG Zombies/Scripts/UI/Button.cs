using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace OMG_Zombies.Scripts.UI
{
    public class Button
    {
        #region Campos e propriedades

        private Texture2D texture;
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
                //return new Rectangle((int)Position.X, (int)Position.Y, 200, 100);
            }
        }

        private SpriteFont font;
        public Color TextColor { get; set; }
        public string Text { get; set; }

        public event EventHandler Click;
        private MouseState currentMouse;
        private MouseState previousMouse;

        #endregion


        #region Métodos

        public Button(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;

            TextColor = Color.White;
        }

        public void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            //isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                //isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public void Draw()
        {
            Color color = Color.Black;

            //if (isHovering)
            //    colour = Color.Gray;

            Game1._SpriteBatch.Draw(texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                float x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                float y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                Game1._SpriteBatch.DrawString(font, Text, new Vector2(x, y), TextColor);
            }
        }

        #endregion
    }
}