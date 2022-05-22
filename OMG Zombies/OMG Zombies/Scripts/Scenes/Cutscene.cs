using Microsoft.Xna.Framework.Input;
using OMG_Zombies.Scripts.Managers;
using OMG_Zombies.Scripts.UI;
using OMG_Zombies.Scripts.Utils;

namespace OMG_Zombies.Scripts.Scenes
{
    public class Cutscene : Scene
    {
        #region Campos e propriedades

        // imagem
        private Image background;

        // teclado
        private KeyboardManager keyboardManager;

        #endregion


        #region Carregar gameplay

        public Cutscene(Game1 game, Image background)
            : base(game)
        {
            this.background = background;

            LoadKeyboard();
            LoadContent();
        }

        public override void LoadContent()
        {
            LoadBackground();
        }

        private void LoadKeyboard()
        {
            keyboardManager = new KeyboardManager();
        }

        private void LoadBackground()
        {
            keyboardManager = new KeyboardManager();
        }

        #endregion


        #region Atualizar gameplay

        public override void Update()
        {
            UpdateKeyboard();

            if (keyboardManager.IsKeyPressed(Keys.Space))
            {
                Game1._currentSceneType = SceneType.Gameplay;
                Game1._currentScene = new Gameplay(game);
            }
        }

        private void UpdateKeyboard()
        {
            keyboardManager.Update();
        }

        #endregion


        #region Desenhar gameplay

        public override void Draw()
        {
            Game1._spriteBatch.Begin();

            DrawBackground();

            Game1._spriteBatch.End();
        }

        private void DrawBackground()
        {
            background.Draw();
        }

        #endregion
    }
}
