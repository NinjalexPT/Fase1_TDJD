using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.Sprites
{
    public class Background
    {
        private Texture2D texture;
        private Vector2 position;
        private float layer;

        public Background(Texture2D texture, Vector2 position, float layer)
        {
            this.texture = texture;
            this.position = position;
            this.layer = layer;
        }

        public void Draw()
        {
            Game1.SpriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
        }
    }
}