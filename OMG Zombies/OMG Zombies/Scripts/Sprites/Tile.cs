using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OMG_Zombies.Scripts.Utils;

namespace OMG_Zombies.Scripts.Sprites
{
    /// <summary>
    /// Armazena a textura e o comportamente de um tile.
    /// </summary>
    public class Tile
    {
        public Texture2D texture;
        public CollisionType type;

        public const int WIDTH = 32;
        public const int HEIGHT = 32;

        public static Vector2 size = new Vector2(WIDTH, HEIGHT);

        /// <summary>
        /// Constroi um novo tile.
        /// </summary>
        public Tile(Texture2D texture, CollisionType type)
        {
            this.texture = texture;
            this.type = type;
        }
    }
}