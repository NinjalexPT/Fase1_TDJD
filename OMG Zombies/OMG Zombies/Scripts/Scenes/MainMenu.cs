using Microsoft.Xna.Framework;
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
                MediaPlayer.Play(Game1._content.Load<Song>("Sounds/Music"));
            }
            catch
            {
                throw new NotSupportedException("Erro: Impossível carregar música do jogo.");
            }
        }

        private void LoadLogo()
        {
            logo = Game1._content.Load<Texture2D>("Logos/logomenu");
        }

        private void LoadButtons()
        {
            Texture2D creditsButton_texture = Game1._content.Load<Texture2D>("Buttons/credits");
            Texture2D playGameButton_texture = Game1._content.Load<Texture2D>("Buttons/play");
            Texture2D quitGameButton_texture = Game1._content.Load<Texture2D>("Buttons/exit");

            SpriteFont buttonFont_normal = Game1._content.Load<SpriteFont>("Fonts/charybdis_normal");
            SpriteFont buttonFont_big = Game1._content.Load<SpriteFont>("Fonts/charybdis_big");

            Button creditsButton = new Button(creditsButton_texture, null)
            {
                Position = new Vector2(Game1._screenCenter.X - playGameButton_texture.Width / 2 - 250, Game1._screenCenter.Y + 150),
            };
            creditsButton.Click += CreditsButton_Click;

            Button playGameButton = new Button(playGameButton_texture, null)
            {
                Position = new Vector2(Game1._screenCenter.X - playGameButton_texture.Width / 2, Game1._screenCenter.Y + 150),
            };
            playGameButton.Click += PlayGameButton_Click;

            Button quitGameButton = new Button(quitGameButton_texture, null)
            {
                Position = new Vector2(Game1._screenCenter.X - playGameButton_texture.Width / 2 + 250, Game1._screenCenter.Y + 150),
            };
            quitGameButton.Click += QuitGameButton_Click;

            buttons = new List<Button>()
            {
                playGameButton,
                creditsButton,
                quitGameButton,
            };
        }

        #endregion


        #region Eventos de input (mouse)

        private void CreditsButton_Click(object sender, EventArgs e)
        {
            // TO DO
        }

        private void PlayGameButton_Click(object sender, EventArgs e)
        {
            Image background = new Image(Game1._content.Load<Texture2D>("Backgrounds/lvl2"), new Vector2(0, 0));
         
            Game1._currentSceneType = SceneType.Cutscene;
            Game1._currentScene = new Cutscene(game, background);
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
            Game1._spriteBatch.Begin();

            DrawLogo();
            DrawButtons();

            Game1._spriteBatch.End();
        }

        private void DrawLogo()
        {
            Game1._spriteBatch.Draw(logo, new Vector2(Game1._screenCenter.X - logo.Width / 2, Game1._screenCenter.Y - 265), Color.White);
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