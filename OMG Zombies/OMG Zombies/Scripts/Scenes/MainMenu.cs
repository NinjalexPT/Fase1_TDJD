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

        public MainMenu(Game1 game)
            : base(game)
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
                MediaPlayer.Play(Game1._Content.Load<Song>("Sounds/Music"));
            }
            catch
            {
                throw new NotSupportedException("Erro: Impossível carregar música do jogo.");
            }
        }

        private void LoadLogo()
        {
            logo = Game1._Content.Load<Texture2D>("Logos/logomenu");
        }

        private void LoadButtons()
        {
            Texture2D buttonTexture = Game1._Content.Load<Texture2D>("Button");
            SpriteFont buttonFont_normal = Game1._Content.Load<SpriteFont>("Fonts/charybdis_normal");
            SpriteFont buttonFont_big = Game1._Content.Load<SpriteFont>("Fonts/charybdis_big");

            Button quitGameButton = new Button(buttonTexture, buttonFont_normal)
            {
                //Position = new Vector2(Game1._ScreenCenter.X - buttonFont.Texture.Width / 2, 500),
                Position = new Vector2(Game1._ScreenCenter.X - 400, Game1._ScreenCenter.Y + 160),
                Text = "Creditos",
            };
            quitGameButton.Click += QuitGameButton_Click;

            Button playGameButton = new Button(buttonTexture, buttonFont_big)
            {
                //Position = new Vector2(Game1._ScreenCenter.X - buttonFont.Texture.Width / 2, 450),
                Position = new Vector2(Game1._ScreenCenter.X - 100, Game1._ScreenCenter.Y + 160),
                Text = "Jogar",
            };
            playGameButton.Click += PlayGameButton_Click;

            Button creditsGameButton = new Button(buttonTexture, buttonFont_normal)
            {
                //Position = new Vector2(Game1._ScreenCenter.X - buttonFont.Texture.Width / 2, 500),
                Position = new Vector2(Game1._ScreenCenter.X + 150, Game1._ScreenCenter.Y + 160),
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
            Game1._CurrentSceneType = SceneType.Gameplay;
            Game1._CurrentScene = new Gameplay(game);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        #endregion


        #region Atualizar menu

        public override void Update()
        {
            foreach (Button button in buttons)
            {
                button.Update();
            }
        }

        #endregion


        #region Desenhar menu

        public override void Draw()
        {
            Game1._SpriteBatch.Begin();

            DrawLogo();
            DrawButtons();

            Game1._SpriteBatch.End();
        }

        private void DrawLogo()
        {
            Game1._SpriteBatch.Draw(logo, new Vector2(Game1._ScreenCenter.X - logo.Width / 2, Game1._ScreenCenter.Y - 265), Color.White);
        }

        private void DrawButtons()
        {
            foreach (Button button in buttons)
            {
                button.Draw();
            }
        }

        #endregion
    }
}