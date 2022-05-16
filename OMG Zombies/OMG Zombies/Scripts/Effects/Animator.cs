using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace OMG_Zombies.Scripts.Effects
{
    /// <summary>
    /// Controla as animações das sprite sheets animadas.
    /// </summary>
    public class Animator
    {
        #region Campos e Propriedes

        // a animação que está atualmente a ser reproduzida
        private Animation animation;
        public Animation Animation
        {
            get => animation;
        }

        // é o índice do frame atual da animação
        private int frameIndex;
        public int FrameIndex
        {
            get => frameIndex;
        }

        // a quantidade de tempo (em segundos) que a atual frame foi mostrada
        private float frameTime;

        // Gets a texture origin at the bottom center of each frame
        public Vector2 Origin
        {
            get => new Vector2(Animation.FrameWidth / 2.0f, Animation.FrameHeight);
        }

        #endregion


        #region Mostrar animação

        /// <summary>
        /// Inicia ou continua a reprodução de uma animação.
        /// </summary>
        public void PlayAnimation(Animation animation)
        {
            // se animação ja estiver em execução, retorna para continuar a reproduzi-la
            if (Animation == animation)
            {
                return;
            }

            this.animation = animation;
            frameIndex = 0;
            frameTime = 0.0f;
        }

        /// <summary>
        /// Desenha a atual frame da animação.
        /// </summary>
        public void Draw(Vector2 position, SpriteEffects spriteEffects)
        {
            if (Animation == null)
            {
                throw new NotSupportedException("Erro: Nenhuma animação encontrada.");
            }

            frameTime += (float)Game1.GameTime.ElapsedGameTime.TotalSeconds;

            while (frameTime > Animation.TimeBetweenEachFrame)
            {
                frameTime -= Animation.TimeBetweenEachFrame;

                // Advance the frame index; looping or clamping as appropriate.
                if (Animation.IsLooping)
                {
                    frameIndex = (frameIndex + 1) % Animation.NumberOfFrames;
                }
                else
                {
                    frameIndex = Math.Min(frameIndex + 1, Animation.NumberOfFrames - 1);
                }
            }

            // Calculate the source rectangle of the current frame.
            Rectangle source = new Rectangle(FrameIndex * Animation.SpriteSheet.Height, 0, Animation.SpriteSheet.Height, Animation.SpriteSheet.Height);

            Game1.SpriteBatch.Draw(Animation.SpriteSheet, position, source, Color.White, 0f, Origin, 1f, spriteEffects, 1f);
        }

        #endregion
    }
}