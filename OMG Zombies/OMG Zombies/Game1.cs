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
        public static int _ScreenWidth = 1120;
        public static int _ScreenHeight = 640;
        public static Point _ScreenCenter = new Point(_ScreenWidth / 2, _ScreenHeight / 2);

        public static GraphicsDevice _GraphicsDevice;
        public static ContentManager _Content;
        public static SpriteBatch _SpriteBatch;
        public static GameTime _GameTime;

        // estado da cena atual
        public static SceneType _CurrentSceneType;
        public static Scene _CurrentScene;

        #endregion


        #region Carregar jogo

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;

            _Content = Content;
            _Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = _ScreenWidth;
            graphics.PreferredBackBufferHeight = _ScreenHeight;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // permite que sejam desenhadas texturas no ecrã
            _SpriteBatch = new SpriteBatch(GraphicsDevice);
            _GraphicsDevice = GraphicsDevice;

            // primeira cena aparecer
            _CurrentSceneType = SceneType.MainMenu;
            _CurrentScene = new MainMenu(this);
        }

        #endregion


        #region Atualizar jogo

        protected override void Update(GameTime gameTime)
        {
            _GameTime = gameTime;
            _CurrentScene.Update();

            base.Update(_GameTime);
        }

        #endregion


        #region Desenhar jogo

        protected override void Draw(GameTime gameTime)
        {
            // cor padrão do ecrã
            GraphicsDevice.Clear(Color.Black);

            _GameTime = gameTime;
            _CurrentScene.Draw();

            base.Draw(_GameTime);
        }

        #endregion
    }
}