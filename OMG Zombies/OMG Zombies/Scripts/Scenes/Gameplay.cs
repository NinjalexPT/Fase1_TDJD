using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OMG_Zombies.Scripts.Managers;
using OMG_Zombies.Scripts.Sprites;
using OMG_Zombies.Scripts.UI;
using OMG_Zombies.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace OMG_Zombies.Scripts.Scenes
{
    public class Gameplay : Scene
    {
        #region Campos e propriedades

        // teclado do jogo
        public static KeyboardManager _keyboardManager;

        // estado do nível atual do jogo
        private Level level;
        private const int numberOfLevels = 3;
        private int levelIndex = -1;
        private bool wasPlaying;

        // camara do jogo
        private static Camera camera;

        #endregion


        #region Carregar gameplay

        public Gameplay(Game1 game)
            : base(game)
        {
            LoadKeyboard();
            LoadContent();
        }

        private void LoadKeyboard()
        {
            _keyboardManager = new KeyboardManager();
        }

        public override void LoadContent()
        {
            LoadCamera();
            LoadNextLevel();
        }

        private void LoadCamera()
        {
            camera = new Camera(Game1._graphicsDevice.Viewport);
        }

        private void LoadNextLevel()
        {
            // se concluiu todos os níveis
            if (levelIndex == numberOfLevels - 1)
            {
                List<Image> storyboards = new List<Image>()
                {
                    new Image(Game1._content.Load<Texture2D>("Storyboards/storyend1"), new Vector2(0, 0)),
                    new Image(Game1._content.Load<Texture2D>("Storyboards/storyend2"), new Vector2(0, 0)),
                    new Image(Game1._content.Load<Texture2D>("Storyboards/storyend3"), new Vector2(0, 0)),
                };

                SceneType nextSceneType = SceneType.MainMenu;
                Scene nextScene = new MainMenu(game);

                Game1._currentSceneType = SceneType.Storyboard;
                Game1._currentScene = new Storyboard(game, storyboards, nextSceneType, nextScene);

                levelIndex = -1;
            }

            // índice do próximo nível
            levelIndex += 1;

            string levelPath = "Content/Levels/lvl" + levelIndex + ".txt";

            // carrega o nivel
            using (Stream fileStream = TitleContainer.OpenStream(levelPath))
            {
                level = new Level(fileStream, levelIndex);
            }
        }

        #endregion


        #region Atualizar gameplay

        public override void Update()
        {
            UpdateKeyboard();

            bool isPlaying = _keyboardManager.IsKeyPressed(Keys.Space);

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

            UpdateLevel();
            UpdateCamera();

            if (levelIndex - 1 == numberOfLevels)
            {
                Game1._currentSceneType = SceneType.MainMenu;
                Game1._currentScene = new MainMenu(game);
            }
        }

        private void UpdateKeyboard()
        {
            _keyboardManager.Update();
        }

        private void UpdateLevel()
        {
            level.Update();
        }

        private void UpdateCamera()
        {
            camera.Update(level.Player.Position, (int)level.Player.Position.X * Tile.WIDTH, 640);
        }

        #endregion


        #region Desenhar gameplay

        public override void Draw()
        {
            Game1._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            DrawLevel();
            DrawLabels();
            DrawPopups();

            Game1._spriteBatch.End();
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

            int xPosition = (int)camera.Center.X - 560;

            Label labelTime = new Label("Fonts/Hud", timeText, new Vector2(xPosition, 0f), Color.Yellow);
            labelTime.Draw();

            Label labelScore = new Label("Fonts/Hud", scoreText, new Vector2(xPosition, 25f), Color.Yellow);
            labelScore.Draw();
        }

        private void DrawPopups()
        {
            Popup currentPopup = null;
            Vector2 screenCenter = new Vector2((int)camera.Center.X  , (int)camera.Center.Y);

            if (level.CompletedLevel)
            {
                if (level.CompletedLevel)
                {
                    currentPopup = new Popup("Popups/youwin", screenCenter);
                }
                else
                {
                    currentPopup = new Popup("Popups/youlose", screenCenter);
                }
            }
            else if (!level.Player.IsAlive)
            {
                currentPopup = new Popup("Popups/youaredead", screenCenter);
            }

            if (currentPopup != null)
            {
                currentPopup.Draw();
            }
        }

        #endregion
    }
}