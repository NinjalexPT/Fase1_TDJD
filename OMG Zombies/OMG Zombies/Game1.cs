using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OMG_Zombies.Scripts.Managers;
using OMG_Zombies.Scripts.Scenes;
using OMG_Zombies.Scripts.Sprites;
using OMG_Zombies.Scripts.UI;
using OMG_Zombies.Scripts.Utils;
using System.IO;

namespace OMG_Zombies
{
    /// <summary>
    /// Jogo inspirado no jogo: https://github.com/MonoGame/MonoGame.Samples
    /// </summary>
    public class Game1 : Game
    {
        #region Campos e propriedades

        // recursos padrão do monogame para criar os gráficos do jogo
        private GraphicsDeviceManager graphics;

        // variáveis globais
        public static int ScreenWidth = 1120;
        public static int ScreenHeight = 640;
        public static Point ScreenCenter = new Point(ScreenWidth / 2, ScreenHeight / 2);

        public static GraphicsDevice _GraphicsDevice;
        public static ContentManager ContentManager;
        public static SpriteBatch SpriteBatch;
        public static GameTime GameTime;

        // estado da cena atual
        public static SceneType currentSceneType;
        public static Scene currentScene;

        #endregion


        #region Carregar jogo

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;

            ContentManager = Content;
            ContentManager.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // permite que sejam desenhadas texturas no ecrã
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _GraphicsDevice = GraphicsDevice;

            // primeira cena aparecer
            currentSceneType = SceneType.MainMenu;
            currentScene = new MainMenu(this, graphics.GraphicsDevice, Content);
        }

        #endregion


        #region Atualizar jogo

        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            currentScene.Update(gameTime);

            base.Update(GameTime);
        }

        #endregion


        #region Desenhar jogo

        protected override void Draw(GameTime gameTime)
        {
            // cor padrão do ecrã
            GraphicsDevice.Clear(Color.Black);

            GameTime = gameTime;
            currentScene.Draw(gameTime, SpriteBatch);

            base.Draw(gameTime);
        }

        #endregion
    }
}