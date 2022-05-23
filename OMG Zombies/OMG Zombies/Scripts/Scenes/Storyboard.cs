using Microsoft.Xna.Framework.Input;
using OMG_Zombies.Scripts.Managers;
using OMG_Zombies.Scripts.UI;
using OMG_Zombies.Scripts.Utils;
using System.Collections.Generic;

namespace OMG_Zombies.Scripts.Scenes
{
    public class Storyboard : Scene
    {
        #region Campos e propriedades

        // imagem
        private List<Image> storyboards;
        private Image currentStoryboard;
        private int currentIndex;

        // teclado
        private KeyboardManager keyboardManager;

        // próxima cena (depois de completar todas as storyboards)
        private SceneType nextSceneType;
        private Scene nextScene;

        #endregion


        #region Carregar gameplay

        public Storyboard(Game1 game, List<Image> storyboards, SceneType nextSceneType, Scene nextScene)
            : base(game)
        {
            this.storyboards = storyboards;
            this.nextSceneType = nextSceneType;
            this.nextScene = nextScene;

            currentIndex = 0;

            LoadKeyboard();
        }

        public override void LoadContent() { }

        private void LoadKeyboard()
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
                // se última storyboard está a ser mostrada
                if (currentIndex == storyboards.Count - 1)
                {
                    Game1._currentSceneType = nextSceneType;
                    Game1._currentScene = nextScene;
                }
                else // passa para a próxima storyboard
                {
                    currentIndex++;
                }
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

            DrawStoryboards();

            Game1._spriteBatch.End();
        }

        private void DrawStoryboards()
        {
            storyboards[currentIndex].Draw();
        }

        #endregion
    }
}
