using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.Scenes
{
    public class Gameplay : Scene
    {
        #region Campos e propriedades

        protected Game1 game;
        protected GraphicsDevice graphicsDevice;
        protected ContentManager content;

        #endregion


        #region Carregar gameplay

        public Gameplay(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            LoadContent();
        }

        public override void LoadContent()
        {

        }

        #endregion


        #region Atualizar gameplay

        public override void Update(GameTime gameTime)
        {

        }

        #endregion


        #region Desenhar gameplay

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        #endregion
    }
}