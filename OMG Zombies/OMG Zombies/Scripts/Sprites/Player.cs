using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OMG_Zombies.Scripts.Effects;
using OMG_Zombies.Scripts.Scenes;
using OMG_Zombies.Scripts.Utils;
using System;

namespace OMG_Zombies.Scripts.Sprites
{
    /// <summary>
    /// Controla os movimentos e animações do jogador.
    /// </summary>
    public class Player
    {
        #region Campos e propriedes

        // o nível atual
        public Level Level
        {
            get { return level; }
        }
        private Level level;

        // animações
        private Animation idleAnimation;
        private Animation runAnimation;
        private Animation jumpAnimation;
        private Animation deadAnimation;

        // animar animações
        private Animator animator;
        private SpriteEffects flip = SpriteEffects.None;

        // sons
        private SoundEffect jumpSound;
        private SoundEffect dieSound;

        // se o jogador está vivo ou não
        private bool isAlive;
        public bool IsAlive
        {
            get => isAlive;
        }

        // posição do jogador
        private Vector2 position;
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        // velocidade do jogador para horizontar ou vertical
        private Vector2 velocity;
        public Vector2 Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        // movimento do jogador
        private float speed;
        public float Speed
        {
            get => speed;
        }

        // variável auxiliar para dizer se estava o jogador estava a correr,
        // é necessário para parar ou mover o fundo, quando o jogador está ou não parado
        private bool wasRunning;
        public bool WasRunning
        {
            get => wasRunning;
            set => wasRunning = value;
        }

        // estado do salto
        private bool isJumping;
        private float jumpTime;

        private const float GRAVITY = 3400f;
        private const float MAX_JUMP_TIME = 0.15f;
        private const float MAX_JUMP_SPEED = 500f;

        // se o jogador está a tocar no chão ou não
        private bool isOnGround;
        public bool IsOnGround
        {
            get => isOnGround;
        }

        // Calculate bounds within texture size
        private Rectangle frameBounds;

        // é o retângulo que limita o jogador no world space
        public Rectangle Collider
        {
            get
            {
                int left = (int)(Position.X - animator.Origin.X) + frameBounds.X;
                int top = (int)(Position.Y - animator.Origin.Y) + frameBounds.Y;
                int right = frameBounds.Width;
                int bottom = frameBounds.Height;

                return new Rectangle(left, top, right, bottom);
            }
        }

        private float previousBottom;

        private float layer;
        public float Layer
        {
            get => layer; 
            set => layer = value;
        }

        #endregion


        #region Carregar jogador

        /// <summary>
        /// Constroi um novo jogador.
        /// </summary>
        public Player(Level level, Vector2 position)
        {
            this.level = level;

            LoadContent();
            SetFrameBounds();
            ResetPlayer(position);
        }

        /// <summary>
        /// Carregar o conteúdo do jogador.
        /// </summary>
        public void LoadContent()
        {
            // sprite sheets animadas
            idleAnimation = new Animation(Game1.ContentManager.Load<Texture2D>("Sprites/Player/idle"), 0.1f, true);
            runAnimation = new Animation(Game1.ContentManager.Load<Texture2D>("Sprites/Player/run"), 0.1f, true);
            jumpAnimation = new Animation(Game1.ContentManager.Load<Texture2D>("Sprites/Player/jump"), 0.1f, false);
            deadAnimation = new Animation(Game1.ContentManager.Load<Texture2D>("Sprites/Player/dead"), 0.1f, false);

            // sons
            jumpSound = Game1.ContentManager.Load<SoundEffect>("Sounds/PlayerJump");
            dieSound = Game1.ContentManager.Load<SoundEffect>("Sounds/PlayerKilled");
        }

        /// <summary>
        /// Resetar o jogador para viver.
        /// </summary>
        public void ResetPlayer(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;

            animator = new Animator();
            animator.PlayAnimation(idleAnimation);

            isAlive = true;
        }

        private void SetFrameBounds()
        {
            int right = (int)(idleAnimation.FrameWidth * 0.55);
            int left = (idleAnimation.FrameWidth - right) / 2;
            int bottom = (int)(idleAnimation.FrameHeight * 0.8);
            int top = idleAnimation.FrameHeight - bottom;

            frameBounds = new Rectangle(left, top, right, bottom);
        }

        #endregion


        #region Atualizar jogador

        /// <summary>
        /// Atualizar o jogador.
        /// </summary>
        public void Update()
        {
            PressKey();

            Vector2 previousPosition = Position;

            ApplyPhysics();

            HandleTilemapCollisions();

            ResetVelocityIfCollide(previousPosition);

            if (isAlive && isOnGround)
            {
                if (Velocity.X > 0 || Velocity.X < 0)
                {
                    animator.PlayAnimation(runAnimation);
                }
                else
                {
                    animator.PlayAnimation(idleAnimation);
                }
            }

            ResetPhysicsApplied();
        }

        /// <summary>
        /// Pressionar teclas do movimento ou salto do jogador.
        /// </summary>
        private void PressKey()
        {
            // se foi para direita
            if (Game1.KeyboardManager.isKeyHeld(Keys.Right) ||
                Game1.KeyboardManager.isKeyHeld(Keys.D))
            {
                speed = 10000f;
                wasWalking = true;
            }
            // se foi para esquerda
            else if (Game1.KeyboardManager.isKeyHeld(Keys.Left) ||
                Game1.KeyboardManager.isKeyHeld(Keys.A))
            {
                speed = -10000f;
                wasWalking = true;
            }

            // se saltou
            if (Game1.KeyboardManager.isKeyHeld(Keys.Space) ||
                Game1.KeyboardManager.isKeyHeld(Keys.W))
            {
                isJumping = true;
            }
        }

        /// <summary>
        /// Atualizar a velocidade e posição do jogador,
        /// baseada nas teclas pressionadas e gravidade no salto.
        /// </summary>
        public void ApplyPhysics()
        {
            float elapsedTime = (float)Game1.GameTime.ElapsedGameTime.TotalSeconds;

            // aplica movimento para a direita ou esquerda
            velocity.X = speed * elapsedTime;

            // aplica movimento para cima
            // e previne que o salto tenha uma velocidade máxima e mínima
            velocity.Y = MathHelper.Clamp(velocity.Y + GRAVITY * elapsedTime, -MAX_JUMP_SPEED, MAX_JUMP_SPEED);
            velocity.Y = Jump(elapsedTime, velocity.Y);

            // atualiza posição
            Position += velocity * elapsedTime;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));
        }

        /// <summary>
        /// .
        /// </summary>
        private float Jump(float elapsedTime, float velocityY)
        {
            if (isJumping)
            {
                // começa ou continua a saltar
                if (isOnGround || jumpTime > 0f)
                {
                    if (jumpTime == 0f)
                    {
                        jumpSound.Play();
                    }

                    animator.PlayAnimation(jumpAnimation);
                    jumpTime += elapsedTime;
                }

                // se jogador está a subir
                if (jumpTime > 0f && jumpTime <= MAX_JUMP_TIME)
                {
                    velocityY = -700f;
                }
                else
                {
                    // atingiu o limite de tempo do salto
                    jumpTime = 0f;
                }
            }
            else
            {
                // continua sem pular ou cancela um salto em andamento
                jumpTime = 0f;
            }

            return velocityY;
        }

        /// <summary>
        /// Detects and resolves all collisions between the player and his neighboring
        /// tiles. When a collision is detected, the player is pushed away along one
        /// axis to prevent overlapping. There is some special logic for the Y axis to
        /// handle platforms which behave differently depending on direction of movement.
        /// </summary>
        private void HandleTilemapCollisions()
        {
            // Get the player's bounding rectangle and find neighboring tiles.
            Rectangle bounds = Collider;
            int leftTile = (int)Math.Floor((float)bounds.Left / Tile.WIDTH);
            int rightTile = (int)Math.Ceiling(((float)bounds.Right / Tile.WIDTH)) - 1;
            int topTile = (int)Math.Floor((float)bounds.Top / Tile.HEIGHT);
            int bottomTile = (int)Math.Ceiling(((float)bounds.Bottom / Tile.HEIGHT)) - 1;

            // Reset flag to search for ground collision.
            isOnGround = false;

            // For each potentially colliding tile,
            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    // If this tile is collidable,
                    CollisionType type = Level.Tilemap.GetTileType(x, y);

                    if (type != CollisionType.transparent)
                    {
                        // Determine collision depth (with direction) and magnitude.
                        Rectangle tileBounds = Level.Tilemap.GetTileCollider(x, y);
                        Vector2 depth = RectangleHelper.GetIntersectionDepth(bounds, tileBounds);

                        if (depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);

                            // Resolve the collision along the shallow axis.
                            if (absDepthY < absDepthX)
                            {
                                // If we crossed the top of a tile, we are on the ground.
                                if (previousBottom <= tileBounds.Top)
                                {
                                    isOnGround = true;
                                }

                                // Ignore platforms, unless we are on the ground.
                                if (type == CollisionType.block || isOnGround)
                                {
                                    // Resolve the collision along the Y axis.
                                    Position = new Vector2(Position.X, Position.Y + depth.Y);

                                    // Perform further collisions with the new bounds.
                                    bounds = Collider;
                                }
                            }
                            else if (type == CollisionType.block)
                            {
                                // Resolve the collision along the X axis.
                                Position = new Vector2(Position.X + depth.X, Position.Y);

                                // Perform further collisions with the new bounds.
                                bounds = Collider;
                            }
                        }
                    }
                }
            }

            // Save the new bounds bottom.
            previousBottom = bounds.Bottom;
        }

        private void ResetVelocityIfCollide(Vector2 previousPosition)
        {
            if (Position.X == previousPosition.X)
            {
                velocity.X = 0;
            }
            if (Position.Y == previousPosition.Y)
            {
                velocity.Y = 0;
            }
        }

        private void ResetPhysicsApplied()
        {
            speed = 0f;
            isJumping = false;
        }

        #endregion


        #region Eventos do nível

        /// <summary>
        /// Called when the player has been killed.
        /// </summary>
        public void OnPlayerDied(Enemy killedBy)
        {
            isAlive = false;

            if (killedBy != null)
            {
                dieSound.Play();
            }

            animator.PlayAnimation(deadAnimation);
        }

        #endregion


        #region Desenhar jogador

        /// <summary>
        /// Desenhar o jogador.
        /// </summary>
        public void Draw()
        {
            FlipPlayer();
            animator.Draw(Position, flip);
        }

        /// <summary>
        /// Virar jogador de acordo para onde se está a mover (para a esquerda ou para a direita).
        /// </summary>
        public void FlipPlayer()
        {
            if (Velocity.X > 0)
            {
                flip = SpriteEffects.None;
            }
            else if (Velocity.X < 0)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
        }

        #endregion
    }
}