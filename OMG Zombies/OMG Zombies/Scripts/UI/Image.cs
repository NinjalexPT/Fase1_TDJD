using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.UI
{
    public class Image
    {
        private Texture2D texture;
        private Vector2 position;
        private float layer;

        public Image(Texture2D texture, Vector2 position, float layer)
        {
            this.texture = texture;
            this.position = position;
            this.layer = layer;
        }

        public Image(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            layer = 1F;
        }

        public void Draw()
        {
            Game1._spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
        }
    }
}