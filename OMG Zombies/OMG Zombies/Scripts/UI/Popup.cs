using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.UI
{
    public class Popup
    {
        private string texturePath;
        private Texture2D texture;
        private Vector2 position;

        public Popup(string texturePath, Vector2 position)
        {
            this.texturePath = texturePath;
            this.position = position;

            LoadContent();
        }

        private void LoadContent()
        {
            texture = Game1.ContentManager.Load<Texture2D>(texturePath);
        }

        public void Draw()
        {
            Game1.SpriteBatch.Draw(texture, position, Color.White);
        }
    }
}