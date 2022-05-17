using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OMG_Zombies.Scripts.Scenes;
using OMG_Zombies.Scripts.Sprites;
using OMG_Zombies.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace OMG_Zombies.Scripts.Managers
{
    public class Tilemap
    {
        #region Campos e propriedades

        private Level level;

        // cria o tilemap do nível,
        // o primeiro argumento é o números de caracteres de cada linha
        // e o segundo o número de linhas
        public Tile[,] grid;

        // comprimento do nível medido pelo tilemap
        public int Width
        {
            get => grid.GetLength(0);
        }

        // altura do nível medida pelo tilemap
        public int Height
        {
            get => grid.GetLength(1);
        }

        #endregion


        #region Carregar tilemap

        public Tilemap(Level level, Stream fileStream)
        {
            this.level = level;
            List<string> fileLines = GetFileLines(fileStream);

            CreateEmptyTilemap(fileLines);
            LoadTilemap(fileLines);
        }

        /// <summary>
        /// Obtém uma lista com as linhas do ficheiro do nível.
        /// </summary>
        private List<string> GetFileLines(Stream fileStream)
        {
            List<string> lines = new List<string>();
            int lineSize = 0;

            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                lineSize = line.Length;

                while (line != null)
                {
                    // se houver uma linha que não tem o mesmo tamanho (mesmo número de caracteres) das outras
                    if (line.Length != lineSize)
                    {
                        throw new Exception("Erro: As linhas têm tamanhos diferentes.");
                    }

                    lines.Add(line);
                    line = reader.ReadLine();
                }
            }

            return lines;
        }

        /// <summary>
        /// Criar o tilemap do nível vazio,
        /// a 1º célula do array é o comprimento (número de caracteres) de cada linha
        /// e o 2º o número de linhas.
        /// </summary>
        private void CreateEmptyTilemap(List<string> lines)
        {
            // como as linhas têm o mesmo tamanho, pode-se afirmar que o tamanho da primeira linha é igual às restantes
            int lineSize = lines[0].Length;

            // inicializa o array para o grid do tilemap
            grid = new Tile[lineSize, lines.Count];
        }

        /// <summary>
        /// Colocar cada caractere do ficheiro para o tilemap.
        /// </summary>
        private void LoadTilemap(List<string> lines)
        {
            // percorre o grid do tilemap
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // obtém o caractere de cada posição e carrega o tile correspondente
                    char tileType = lines[y][x];
                    grid[x, y] = LoadTile(tileType, x, y);
                }
            }
        }

        /// <summary>
        /// Carrega um tile de acordo com o caractere definido no ficheiro.
        /// </summary>
        private Tile LoadTile(char tileType, int x, int y)
        {
            switch (tileType)
            {
                // preenche com espaço vazio
                case '.':
                    return NewEmptyTile(CollisionType.transparent);

                // preenche com tile do tipo bloco
                case 'R':
                    return NewTile("Tiles/Level_0/R", CollisionType.block);
                case 'L':
                    return NewTile("Tiles/Level_0/L", CollisionType.block);
                case 'M':
                    return NewTile("Tiles/Level_0/M", CollisionType.block);
                case 'S':
                    return NewTile("Tiles/Level_0/S", CollisionType.block);
                case 'T':
                    return NewTile("Tiles/Level_0/T", CollisionType.block);

                // preenche com inimigo
                case 'V':
                    level.CreateEnemy(GetTileCollider(x, y), "Enemy");
                    return NewEmptyTile(CollisionType.transparent);

                // preenche com poção
                case 'P':
                    level.CreatePotion(GetTileCollider(x, y));
                    return NewEmptyTile(CollisionType.transparent);

                // Coloca o jogador na posição inicial
                case 'I':
                    level.CreatePlayer(GetTileCollider(x, y));
                    return NewEmptyTile(CollisionType.transparent);

                // insere a meta do nível
                case 'F':
                    level.CreateExit(GetTileCollider(x, y));
                    return NewTile("Tiles/exit", CollisionType.transparent);

                // caractere para o tile não encontrado
                default:
                    throw new NotSupportedException("Erro: Caractere para o tile não encontrado.");
            }
        }

        /// <summary>
        /// Cria um novo tile, definindo a textura e o tipo de colisão.
        /// </summary>
        private Tile NewTile(string name, CollisionType collision)
        {
            return new Tile(Game1.ContentManager.Load<Texture2D>(name), collision);
        }

        /// <summary>
        /// Cria um novo tile vazio (sem textura).
        /// </summary>
        private Tile NewEmptyTile(CollisionType collision)
        {
            return new Tile(null, collision);
        }

        #endregion


        #region Colisões do tilemap

        /// <summary>
        /// Obter o retângulo colisor do tile no grid do tilemap.
        /// </summary>
        public Rectangle GetTileCollider(int x, int y)
        {
            return new Rectangle(x * Tile.WIDTH, y * Tile.HEIGHT, Tile.WIDTH, Tile.HEIGHT);
        }

        public CollisionType GetTileType(int x, int y)
        {
            // se o jogador escapar na horizontal do ecrã
            if (x < 0 || x >= Width)
            {
                return CollisionType.block;
            }

            // se o jogador escapar do topo do ecrã, permite saltar e voltar a cair
            if (y < 0 || y >= Height)
            {
                return CollisionType.transparent;
            }

            return grid[x, y].type;
        }

        #endregion


        #region Desenhar tilemap

        public void Draw()
        {
            // For each tile position
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // If there is a visible tile in that position
                    Texture2D texture = grid[x, y].texture;

                    if (texture != null)
                    {
                        // Draw it in screen space.
                        Vector2 position = new Vector2(x, y) * Tile.size;
                        Game1.SpriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                }
            }
        }

        #endregion
    }
}