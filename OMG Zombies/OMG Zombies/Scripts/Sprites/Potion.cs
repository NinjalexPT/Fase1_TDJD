using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using OMG_Zombies.Scripts.Scenes;

namespace OMG_Zombies.Scripts.Sprites
{
    /// <summary>
    /// A valuable item the player can collect.
    /// </summary>
    public class Potion
    {
        private Texture2D texture;
        private SoundEffect collectedSound;

        public Level Level
        {
            get { return level; }
        }
        Level level;

        // posição da poção
        private Vector2 position;

        // Gets the current position of this gem in world space
        // Position in world space of the bottom center of this gem
        private Rectangle collider;
        public Rectangle Collider
        {
            set => collider = value;
            get
            {
                int left = collider.X;
                int top = collider.Y;
                int right = collider.Width;
                int bottom = collider.Height;

                return new Rectangle(left, top, right, bottom);
            }
        }

        /// <summary>
        /// Controis uma nova poção.
        /// </summary>
        public Potion(Level level, Rectangle collider)
        {
            this.level = level;
            this.collider = collider;
            position = new Vector2(collider.X, collider.Y);

            LoadContent();
        }

        /// <summary>
        /// Loads the gem texture and collected sound.
        /// </summary>
        public void LoadContent()
        {
            texture = Game1.ContentManager.Load<Texture2D>("Sprites/Potion");
            collectedSound = Game1.ContentManager.Load<SoundEffect>("Sounds/GemCollected");
        }

        /// <summary>
        /// Called when this gem has been collected by a player and removed from the level.
        /// </summary>
        public void OnPotionCollected()
        {
            collectedSound.Play();
        }

        /// <summary>
        /// Draws a gem in the appropriate color.
        /// </summary>
        public void Draw()
        {
            Game1.SpriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}