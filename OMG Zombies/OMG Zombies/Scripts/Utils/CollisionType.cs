namespace OMG_Zombies.Scripts.Utils
{
    /// <summary>
    /// Diz os tipos de colisões que existem.
    /// </summary>
    public enum CollisionType
    {
        /// <summary>
        /// Este tipo de colisão define que o tile é um objeto físico e que o jogador
        /// ou outros objetos não se podem sobrepor a ele.
        /// </summary>
        transparent = 0,

        /// <summary>
        /// Este tipo de colisão define que o tile é um objeto físico e que o jogador
        /// ou outros objetos não se podem sobrepor a ele.
        /// </summary>
        block = 1,
    }
}