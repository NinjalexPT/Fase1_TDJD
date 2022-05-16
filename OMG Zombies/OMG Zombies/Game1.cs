using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using OMG_Zombies.Scripts.Managers;
using OMG_Zombies.Scripts.Scenes;
using OMG_Zombies.Scripts.UI;
using System;
using System.Collections.Generic;
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

        // variáveis globais do jogo
        public static int ScreenWidth = 1120;
        public static int ScreenHeight = 640;
        public static ContentManager ContentManager;
        public static SpriteBatch SpriteBatch;
        public static GameTime GameTime;
        public static KeyboardManager KeyboardManager;

        // estado do nível atual do jogo
        private Level level;
        private const int numberOfLevels = 3;
        private int levelIndex = -1;
        private bool wasPlaying;

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

            PlayBackgroundSong();

            LoadKeyboardManager();
            LoadNextLevel();
        }

        private void LoadKeyboardManager()
        {
            KeyboardManager = new KeyboardManager();
        }

        private void PlayBackgroundSong()
        {
            try
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(Content.Load<Song>("Sounds/Music"));
            }
            catch
            {
                throw new NotSupportedException("Erro: Impossível carregar música do jogo.");
            }
        }

        private void LoadNextLevel()
        {
            // se concluiu todos os níveis, volta para o primeiro nível
            if (levelIndex == numberOfLevels - 1)
            {
                levelIndex = -1;
            }

            // índice do próximo nível
            levelIndex += 1;

            string levelPath = "Content/Levels/" + levelIndex + ".txt";

            // carrega o nivel
            using (Stream fileStream = TitleContainer.OpenStream(levelPath))
            {
                level = new Level(fileStream, levelIndex);
            }
        }

        #endregion


        #region Atualizar jogo

        protected override void Update(GameTime gameTime)
        {
            PressKeyToExitGame();

            GameTime = gameTime;
            KeyboardManager.Update();

            bool isPlaying = KeyboardManager.IsKeyPressed(Keys.Space);

            if (!wasPlaying && isPlaying)
            {
                if (!level.Player.IsAlive)
                {
                    level.StartNewLife();
                }
                else if (level.CurrentTime == TimeSpan.Zero)
                {
                    if (level.CompletedLevel)
                    {
                        LoadNextLevel();
                    }
                }
            }

            wasPlaying = isPlaying;

            level.Update();

            base.Update(gameTime);
        }

        private void PressKeyToExitGame()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        #endregion


        #region Desenhar jogo

        protected override void Draw(GameTime gameTime)
        {
            // cor padrão do ecrã
            GraphicsDevice.Clear(Color.Black);

            GameTime = gameTime;

            SpriteBatch.Begin(SpriteSortMode.FrontToBack); // começa a desenhar

            DrawLevel();
            DrawLabels();
            DrawPopups();

            SpriteBatch.End(); // termina de desenhar

            base.Draw(gameTime);
        }

        private void DrawLevel()
        {
            level.Draw();
        }

        private void DrawLabels()
        {
            string timeText = "TIME: " +
                level.CurrentTime.Minutes.ToString("00") + ":" +
                level.CurrentTime.Seconds.ToString("00");

            string scoreText = "SCORE: " +
                level.Score.ToString();

            Label labelTime = new Label("Fonts/Hud", timeText, new Vector2(0f, 0f), Color.Yellow);
            labelTime.Draw();

            Label labelScore = new Label("Fonts/Hud", scoreText, new Vector2(0f, 25f), Color.Yellow);
            labelScore.Draw();
        }

        private void DrawPopups()
        {
            Popup currentPopup = null;

            if (level.CompletedLevel)
            {
                if (level.CompletedLevel)
                {
                    currentPopup = new Popup("Popups/you_win");
                }
                else
                {
                    currentPopup = new Popup("Popups/you_lose");
                }
            }
            else if (!level.Player.IsAlive)
            {
                currentPopup = new Popup("Popups/you_lose");
            }

            if (currentPopup != null)
            {
                currentPopup.Draw();
            }
        }

        #endregion
    }
}