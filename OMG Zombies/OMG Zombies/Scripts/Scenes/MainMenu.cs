using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using OMG_Zombies.Scripts.UI;
using OMG_Zombies.Scripts.Utils;
using System;
using System.Collections.Generic;

namespace OMG_Zombies.Scripts.Scenes
{
    public class MainMenu : Scene
    {
        #region Campos e propriedades

        private Texture2D logo;
        private List<Button> buttons;

        #endregion


        #region Carregar menu

        public MainMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            PlayBackgroundSong();
            LoadLogo();
            LoadButtons();
        }

        private void PlayBackgroundSong()
        {
            try
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(content.Load<Song>("Sounds/Music"));
            }
            catch
            {
                throw new NotSupportedException("Erro: Impossível carregar música do jogo.");
            }
        }

        private void LoadLogo()
        {
            logo = content.Load<Texture2D>("Logos/logomenu");
        }

        private void LoadButtons()
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Button");
            SpriteFont buttonFont_normal = content.Load<SpriteFont>("Fonts/charybdis_normal");
            SpriteFont buttonFont_big = content.Load<SpriteFont>("Fonts/charybdis_big");

            Button quitGameButton = new Button(buttonTexture, buttonFont_normal)
            {
                //Position = new Vector2(Game1.ScreenCenter.X - buttonFont.Texture.Width / 2, 500),
                Position = new Vector2(Game1.ScreenCenter.X - 400, Game1.ScreenCenter.Y + 160),
                Text = "Creditos",
            };
            quitGameButton.Click += QuitGameButton_Click;

            Button playGameButton = new Button(buttonTexture, buttonFont_big)
            {
                //Position = new Vector2(Game1.ScreenCenter.X - buttonFont.Texture.Width / 2, 450),
                Position = new Vector2(Game1.ScreenCenter.X - 100, Game1.ScreenCenter.Y + 160),
                Text = "Jogar",
            };
            playGameButton.Click += PlayGameButton_Click;

            Button creditsGameButton = new Button(buttonTexture, buttonFont_normal)
            {
                //Position = new Vector2(Game1.ScreenCenter.X - buttonFont.Texture.Width / 2, 500),
                Position = new Vector2(Game1.ScreenCenter.X + 150, Game1.ScreenCenter.Y + 160),
                Text = "Sair",
            };
            creditsGameButton.Click += QuitGameButton_Click;

            buttons = new List<Button>()
            {
                playGameButton,
                creditsGameButton,
                quitGameButton,
            };
        }

        #endregion


        #region Eventos de input (mouse)

        private void PlayGameButton_Click(object sender, EventArgs e)
        {
            Game1.currentSceneType = SceneType.Gameplay;
            Game1.currentScene = new Gameplay(game, graphicsDevice, content);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        #endregion


        #region Atualizar menu

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }
        }

        #endregion


        #region Desenhar menu

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            DrawLogo(spriteBatch);
            DrawButtons(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void DrawLogo(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(logo, new Vector2(Game1.ScreenCenter.X - logo.Width / 2, Game1.ScreenCenter.Y - 265), Color.White);
        }

        private void DrawButtons(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }
        }

        #endregion
    }
}