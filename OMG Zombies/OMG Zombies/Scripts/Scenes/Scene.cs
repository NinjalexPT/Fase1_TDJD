using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.Scenes
{
    public abstract class Scene
    {
        #region Campos e propriedades

        protected Game1 game;
        protected GraphicsDevice graphicsDevice;
        protected ContentManager content;

        #endregion


        #region métodos

        public abstract void LoadContent();

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public Scene(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            this.content = Game1.ContentManager;
        }

        public abstract void Update(GameTime gameTime);

        #endregion
    }
}