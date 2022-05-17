using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using OMG_Zombies.Scripts.Managers;
using OMG_Zombies.Scripts.Sprites;
using OMG_Zombies.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace OMG_Zombies.Scripts.Scenes
{
    /// <summary>
    /// Controla um nível do jogo.
    /// Cada nível possui um fundo com várias camadas, um tilemap quadrado que possui um jogador e as plataformas.
    /// Controla ainda se ganhou ou perdeu e a pontuação do jogador.
    /// </summary>
    public class Level
    {
        #region Campos e Propriedes

        //private Texture2D[] layers;
        // The layer which entities are drawn on top of.
        //private const int EntityLayer = 2;

        // fundos por camadas
        private List<ScrollingBackground> scrollingBackgrounds;


        // jogador principal (heroi)
        private Player player;
        public Player Player
        {
            get => player;
        }

        private List<Enemy> enemies = new List<Enemy>();
        private List<Potion> potions = new List<Potion>();

        private Tilemap tilemap;
        public Tilemap Tilemap
        {
            get => tilemap;
        }

        // localizações chave do nível
        private Vector2 startPosition;
        private Vector2 endPosition;

        private bool completedLevel;
        public bool CompletedLevel
        {
            get => completedLevel;
        }

        private SoundEffect completedLevelSound;

        private TimeSpan fullTime;

        private TimeSpan currentTime;

        public TimeSpan CurrentTime
        {
            get => currentTime;
            set => currentTime = value;
        }

        private int score;
        public int Score
        {
            get => score;
        }

        #endregion


        #region Carregar nível

        /// <summary>
        /// Controi um novo nível.
        /// </summary>
        public Level(Stream fileStream, int levelIndex)
        {
            SetInitialTime(120);
            LoadContent(fileStream, levelIndex);
        }

        private void SetInitialTime(int seconds)
        {
            fullTime = TimeSpan.FromSeconds(seconds);
            currentTime = fullTime;
        }

        private void LoadContent(Stream fileStream, int levelIndex)
        {
            LoadBackgrounds(levelIndex);
            LoadSounds();
            LoadTilemap(fileStream);
        }

        private void LoadBackgrounds(int levelIndex)
        {
            scrollingBackgrounds = new List<ScrollingBackground>()
            {
                new ScrollingBackground(Game1.ContentManager.Load<Texture2D>("Backgrounds/lvl0_bg0"), 20f)
                {
                  Layer = 0.99f,
                },
                //new ScrollingBackground(Game1.ContentManager.Load<Texture2D>("Backgrounds/Floor"), 60f)
                //{
                //  Layer = 0.9f,
                //},
                //new ScrollingBackground(Game1.ContentManager.Load<Texture2D>("Backgrounds/Hills_Front"), 40f)
                //{
                //  Layer = 0.8f,
                //},
                //new ScrollingBackground(Game1.ContentManager.Load<Texture2D>("Backgrounds/Hills_Middle"), 30f)
                //{
                //  Layer = 0.79f,
                //},
                //new ScrollingBackground(Game1.ContentManager.Load<Texture2D>("Backgrounds/Clouds_Fast"), 25f, true)
                //{
                //  Layer = 0.78f,
                //},
                //new ScrollingBackground(Game1.ContentManager.Load<Texture2D>("Backgrounds/Hills_Back"), 0f)
                //{
                //  Layer = 0.77f,
                //},
                //new ScrollingBackground(Game1.ContentManager.Load<Texture2D>("Backgrounds/Clouds_Slow"), 10f, true)
                //{
                //  Layer = 0.7f,
                //},
                //new ScrollingBackground(Game1.ContentManager.Load<Texture2D>("Backgrounds/Sky"), 0f)
                //{
                //  Layer = 0.1f,
                //}
            };
        }

        private void LoadSounds()
        {
            completedLevelSound = Game1.ContentManager.Load<SoundEffect>("Sounds/ExitReached");
        }

        private void LoadTilemap(Stream fileStream)
        {
            tilemap = new Tilemap(this, fileStream);
        }

        /// <summary>
        /// Cria o jogador (heroi).
        /// </summary>
        public void CreatePlayer(Rectangle tileCollider)
        {
            startPosition = RectangleHelper.GetBottomCenter(tileCollider);
            player = new Player(this, startPosition);
        }

        /// <summary>
        /// Cria um inimigo.
        /// </summary>
        public void CreateEnemy(Rectangle tileCollider, string spriteFolder)
        {
            Vector2 position = RectangleHelper.GetBottomCenter(tileCollider);
            enemies.Add(new Enemy(this, position, spriteFolder));
        }

        /// <summary>
        /// Cria uma poção.
        /// </summary>
        public void CreatePotion(Rectangle tileCollider)
        {
            potions.Add(new Potion(this, tileCollider));
        }

        public void CreateExit(Rectangle tileCollider)
        {
            endPosition = RectangleHelper.GetOrigin(tileCollider);
        }

        #endregion


        #region Atualizar nível

        public void Update()
        {
            // se o jogador está e ainda não completou o nível
            if (player.IsAlive && !completedLevel)
            {
                DecrementTime();
                UpdateBackgrounds();
                UpdatePlayer();
                UpdatePotions();
                UpdateEnemies();

                // se o jogador está a tocar no centro do limite inferior do fim do níveld
                if (Player.IsOnGround && Player.Collider.Contains(endPosition))
                {
                    CompleteLevel();
                }
            }
            // se morreu ou já terminou o nível
            else
            {
                CurrentTime = TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Animates each enemy and allow them to kill the player.
        /// </summary>
        private void DecrementTime()
        {
            currentTime -= Game1.GameTime.ElapsedGameTime;
        }

        /// <summary>
        /// .
        /// </summary>
        private void UpdateBackgrounds()
        {
            // se o jogador estava a correr, permite mover o fundo,
            // senão estiver a correr, o fundo não se mexe
            if (Player.WasRunning)
            {
                foreach (ScrollingBackground scrollingBackground in scrollingBackgrounds)
                {
                    scrollingBackground.Update();
                }

                player.WasRunning = false;
            }
        }

        /// <summary>
        /// .
        /// </summary>
        private void UpdatePlayer()
        {
            Player.Update();
        }

        /// <summary>
        /// Animates each enemy and allow them to kill the player.
        /// </summary>
        private void UpdateEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                // Touching an enemy instantly kills the player
                if (enemy.Collider.Intersects(Player.Collider))
                {
                    player.OnPlayerDied(enemy);
                }
            }
        }

        /// <summary>
        /// .
        /// </summary>
        private void UpdatePotions()
        {
            for (int i = 0; i < potions.Count; ++i)
            {
                Potion potion = potions[i];

                // se tocar numa poção
                if (potion.Collider.Intersects(Player.Collider))
                {
                    score += 5;
                    potion.OnPotionCollected();
                    potions.RemoveAt(i--);
                }
            }
        }

        private void CompleteLevel()
        {
            completedLevel = true;
            completedLevelSound.Play();
        }

        /// <summary>
        /// Restores the player to the starting point to try the level again.
        /// </summary>
        public void StartNewLife()
        {
            currentTime = fullTime;
            Player.ResetPlayer(startPosition);
        }

        #endregion


        #region Desenhar nível

        public void Draw()
        {
            DrawBackgrounds();
            DrawTilemap();
            DrawPlayer();
            DrawPotions();
            DrawEnemies();
        }

        private void DrawBackgrounds()
        {
            foreach (ScrollingBackground scrollingBackground in scrollingBackgrounds)
            {
                scrollingBackground.Draw();
            }
        }

        private void DrawTilemap()
        {
            tilemap.Draw();
        }

        private void DrawPlayer()
        {
            player.Draw();
        }

        private void DrawPotions()
        {
            foreach (Potion potion in potions)
            {
                potion.Draw();
            }
        }

        private void DrawEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw();
            }
        }

        #endregion
    }
}