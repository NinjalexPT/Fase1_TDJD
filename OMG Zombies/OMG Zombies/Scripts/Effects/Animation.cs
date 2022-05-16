using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.Effects
{
    /// <summary>
    /// Representa uma sprite sheet animada.
    /// </summary>
    public class Animation
    {
        #region Campos e Propriedes

        private Texture2D spriteSheet;
        public Texture2D SpriteSheet
        {
            get => spriteSheet;
        }

        private float timeBetweenEachFrame;
        public float TimeBetweenEachFrame
        {
            get => timeBetweenEachFrame;
        }

        // indica se quando animação terminar, se deve continuar a reproduzi-la ou não
        private bool isLooping;
        public bool IsLooping
        {
            get => isLooping;
        }

        public int NumberOfFrames
        {
            get => SpriteSheet.Width / FrameHeight;
        }

        public int FrameWidth
        {
            get => SpriteSheet.Height;
        }

        public int FrameHeight
        {
            get => SpriteSheet.Height;
        }

        #endregion


        #region Criar animação

        /// <summary>
        /// Constroi uma nova animação.
        /// </summary>        
        public Animation(Texture2D spriteSheet, float timeBetweenEachFrame, bool isLooping)
        {
            this.spriteSheet = spriteSheet;
            this.timeBetweenEachFrame = timeBetweenEachFrame;
            this.isLooping = isLooping;
        }

        #endregion
    }
}