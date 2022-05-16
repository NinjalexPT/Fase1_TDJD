using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OMG_Zombies.Scripts.Effects;
using OMG_Zombies.Scripts.Scenes;
using System;

namespace OMG_Zombies.Scripts.Sprites
{
    /// <summary>
    /// A monster who is impeding the progress of our fearless adventurer.
    /// </summary>
    public class Enemy
    {
        public Level Level
        {
            get => level;
        }
        Level level;

        // animações
        private Animation idleAnimation;
        private Animator animator;

        // Position in world space of the bottom center of this enemy.
        private Vector2 position;
        public Vector2 Position
        {
            get => position;
        }

        private Rectangle localBounds;
        // Gets a rectangle which bounds this enemy in world space.
        public Rectangle Collider
        {
            get
            {
                int left = (int)Math.Round(Position.X - animator.Origin.X) + localBounds.X;
                int top = (int)Math.Round(Position.Y - animator.Origin.Y) + localBounds.Y;
                int right = localBounds.Width;
                int bottom = localBounds.Height;

                return new Rectangle(left, top, right, bottom);
            }
        }

        /// <summary>
        /// Constroi um novo inimigo.
        /// </summary>
        public Enemy(Level level, Vector2 position, string spriteFolder)
        {
            this.level = level;
            this.position = position;

            LoadContent(spriteFolder);

            animator = new Animator();
            animator.PlayAnimation(idleAnimation);

            // Calculate bounds within texture size
            int width = (int)(idleAnimation.FrameWidth * 0.35);
            int left = (idleAnimation.FrameWidth - width) / 2;
            int height = (int)(idleAnimation.FrameHeight * 0.7);
            int top = idleAnimation.FrameHeight - height;
            localBounds = new Rectangle(left, top, width, height);
        }

        /// <summary>
        /// Loads a particular enemy sprite sheet and sounds.
        /// </summary>
        public void LoadContent(string spriteFolder)
        {
            // pasta das sprite sheets com as animações
            spriteFolder = "Sprites/" + spriteFolder + "/";

            // animações
            idleAnimation = new Animation(Game1.ContentManager.Load<Texture2D>(spriteFolder + "Idle"), 0.15f, true);
        }

        /// <summary>
        /// Draws the animated enemy.
        /// </summary>
        public void Draw()
        {
            animator.Draw(Position, SpriteEffects.None);
        }
    }
}