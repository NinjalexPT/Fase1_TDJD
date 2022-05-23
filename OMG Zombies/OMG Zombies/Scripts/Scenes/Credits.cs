using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OMG_Zombies.Scripts.Managers;
using OMG_Zombies.Scripts.UI;
using OMG_Zombies.Scripts.Utils;

namespace OMG_Zombies.Scripts.Scenes
{
    public class Credits : Scene
    {
        #region Campos e propriedades

        // texto
        private string text;
        private Label label;

        // teclado
        private KeyboardManager keyboardManager;

        #endregion


        #region Carregar cena dos créditos

        public Credits(Game1 game, string text)
            : base(game)
        {
            this.text = text;
            LoadContent();
            LoadKeyboard();
        }

        public override void LoadContent()
        {
            LoadLabel();
        }

        private void LoadLabel()
        {
            Vector2 screenCenter = new Vector2(Game1._screenCenter.X, Game1._screenCenter.Y);

            label = new Label("Fonts/Hud", text, new Vector2(0,0) , Color.White);
            label.SetCenterTextInScreen(screenCenter);
        }

        private void LoadKeyboard()
        {
            keyboardManager = new KeyboardManager();
        }

        #endregion


        #region Atualizar cena dos créditos

        public override void Update()
        {
            UpdateKeyboard();

            if (keyboardManager.IsKeyPressed(Keys.Space))
            {
                Game1._currentSceneType = SceneType.MainMenu;
                Game1._currentScene = new MainMenu(game);
            }
        }

        private void UpdateKeyboard()
        {
            keyboardManager.Update();
        }

        #endregion


        #region Desenhar cena dos créditos

        public override void Draw()
        {
            Game1._spriteBatch.Begin();

            DrawLabel();

            Game1._spriteBatch.End();
        }

        private void DrawLabel()
        {
            label.Draw();
        }

        #endregion
    }
}
