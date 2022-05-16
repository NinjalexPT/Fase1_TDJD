using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.UI
{
    public class Popup
    {
        private string texturePath;
        private Texture2D texture;
        // obtém a posição no centro do ecrã
        private Vector2 Position
        {
            get
            {
                Vector2 screenCenter = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2);
                Vector2 statusSize = new Vector2(texture.Width, texture.Height);

                return screenCenter - statusSize / 2;
            }
        }

        public Popup(string texturePath)
        {
            this.texturePath = texturePath;
            LoadContent();
        }

        private void LoadContent()
        {
            texture = Game1.ContentManager.Load<Texture2D>(texturePath);
        }

        public void Draw()
        {
            Game1.SpriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}