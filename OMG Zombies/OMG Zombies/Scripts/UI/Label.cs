using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.UI
{
    public class Label
    {
        private string fontPath;
        private SpriteFont font;
        private string text;
        private Vector2 position;
        private Color color;

        public Label(string fontPath, string text, Vector2 position, Color color)
        {
            this.fontPath = fontPath;
            this.text = text;
            this.position = position;
            this.color = color;

            LoadContent();
        }

        private void LoadContent()
        {
            font = Game1.ContentManager.Load<SpriteFont>(fontPath);
        }

        public void Draw()
        {
            Game1.SpriteBatch.DrawString(font, text, position, color);
        }
    }
}