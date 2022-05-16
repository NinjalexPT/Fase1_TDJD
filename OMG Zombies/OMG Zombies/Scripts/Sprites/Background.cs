using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.Sprites
{
    public class Background
    {
        public Vector2 Velocity;
        private float layer;
        private Texture2D texture;

        public float Layer
        {
            get => layer;
            set => layer = value;
        }

        public Vector2 Position;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        public Background(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw()
        {
            Game1.SpriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
        }
    }
}