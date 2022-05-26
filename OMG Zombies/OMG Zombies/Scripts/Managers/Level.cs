using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OMG_Zombies.Scripts.Scenes;
using OMG_Zombies.Scripts.Sprites;
using OMG_Zombies.Scripts.UI;
using OMG_Zombies.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace OMG_Zombies.Scripts.Managers
{
    /// <summary>
    /// Controla um nível do jogo.
    /// Cada nível possui um fundo com várias camadas, um tilemap quadrado que possui um jogador e as plataformas.
    /// Controla ainda se ganhou ou perdeu e a pontuação do jogador.
    /// </summary>
    public class Level
    {
        #region Campos e Propriedes

        // fundos
        private List<Image> backgrounds;

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

        private bool levelFreezed = false;
        public bool LevelFreezed
        {
            get => levelFreezed;
            set => levelFreezed = value;
        }

        #endregion


        #region Carregar nível

        /// <summary>
        /// Controi um novo nível.
        /// </summary>
        public Level(Stream fileStream, int levelIndex, int seconds, int currentScore)
        {
            SetInitialTime(seconds);
            SetCurrentScore(currentScore);
            LoadContent(fileStream, levelIndex);
        }

        private void SetInitialTime(int seconds)
        {
            fullTime = TimeSpan.FromSeconds(seconds);
            currentTime = fullTime;
        }

        private void SetCurrentScore(int currentScore)
        {
            score = currentScore;
        }

        private void LoadContent(Stream fileStream, int levelIndex)
        {
            LoadSounds();
            LoadTilemap(fileStream);
            LoadBackgrounds(levelIndex);
        }

        private void LoadSounds()
        {
            completedLevelSound = Game1._content.Load<SoundEffect>("Sounds/win");
        }

        private void LoadTilemap(Stream fileStream)
        {
            tilemap = new Tilemap(this, fileStream);
        }

        private void LoadBackgrounds(int levelIndex)
        {
            int numberOfBackgrounds = tilemap.Width / Tile.WIDTH;
            backgrounds = new List<Image>();

            for (int i = 0; i < numberOfBackgrounds; i++)
            {
                backgrounds.Add(new Image(
                    Game1._content.Load<Texture2D>("Backgrounds/lvl" + levelIndex),
                    new Vector2(i * Game1._screenWidth, 0),
                    0.99f));
            }
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
        public void CreatePotion(Rectangle tileCollider, string filename)
        {
            potions.Add(new Potion(this, tileCollider, filename));
        }

        public void CreateExit(Rectangle tileCollider)
        {
            endPosition = RectangleHelper.GetOrigin(tileCollider);
        }

        #endregion


        #region Atualizar nível

        public void Update()
        {
            // serve para quando o nível estiver congelado, voltar a permitir jogar,
            // o nível fica congelado, caso o jogador esteja morto, ou perdeu por tempo ou completo o n+ivel
            // (por outras palavras o nível está congelado quando aparece uma popup) 
            if (Gameplay._keyboardManager.IsKeyPressed(Keys.Space))
            {
                levelFreezed = false;
            }

            if (!levelFreezed)
            {
                // se o jogador está vivo e ainda não completou o nível e ainda tem tempo
                if (player.IsAlive && !completedLevel && CurrentTime >= TimeSpan.Zero)
                {
                    DecrementTime();
                    UpdatePlayer();
                    UpdatePotions();
                    UpdateEnemies();

                    // se o jogador está a tocar no tile de fim do nível (no centro do limite inferior)
                    if (Player.IsOnGround && Player.Collider.Contains(endPosition))
                    {
                        CompleteLevel();
                    }
                }

                // se terminou o nível
                else if (completedLevel)
                {
                    CurrentTime = TimeSpan.Zero;
                    levelFreezed = true;
                }
                // se morreu ou ficou sem tempo
                else
                {
                    CurrentTime = TimeSpan.Zero;
                    levelFreezed = true;
                }
            }
        }

        /// <summary>
        /// Animates each enemy and allow them to kill the player.
        /// </summary>
        private void DecrementTime()
        {
            currentTime -= Game1._gameTime.ElapsedGameTime;
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
                    score += 1;
                    potion.OnPotionCollected();
                    potions.RemoveAt(i--);
                }
            }
        }

        private void CompleteLevel()
        {
            completedLevel = true;
            completedLevelSound.Play();
            player.OnPlayerCompletedLevel();
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
            DrawBackground();
            DrawTilemap();
            DrawPlayer();
            DrawPotions();
            DrawEnemies();
        }

        private void DrawBackground()
        {
            foreach (Image background in backgrounds)
            {
                background.Draw();
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