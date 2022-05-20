namespace OMG_Zombies.Scripts.Scenes
{
    public abstract class Scene
    {
        #region Campos e propriedades

        protected Game1 game;

        #endregion


        #region Métodos base

        public Scene(Game1 game)
        {
            this.game = game;
        }

        public abstract void LoadContent();

        public abstract void Draw();

        public abstract void Update();

        #endregion
    }
}