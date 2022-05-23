using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.UI
{
    public class Popup
    {
        private string texturePath;
        private Texture2D texture;

        // centro do ecrã
        private Vector2 screenCenter;

        // centrar imagem
        public Vector2 Position
        {
            get
            {
                Vector2 textureSize = new Vector2(texture.Width, texture.Height);
                return screenCenter - textureSize / 2;
            }
        }

        public Popup(string texturePath, Vector2 screenCenter)
        {
            this.texturePath = texturePath;
            this.screenCenter = screenCenter;

            LoadContent();
        }

        private void LoadContent()
        {
            texture = Game1._content.Load<Texture2D>(texturePath);
        }

        public void Draw()
        {
            Game1._spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}