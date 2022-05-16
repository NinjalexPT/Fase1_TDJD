using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OMG_Zombies.Scripts.Sprites;
using System;
using System.Collections.Generic;

namespace OMG_Zombies.Scripts.Managers
{
    public class ScrollingBackground
    {
        private List<Background> backgrounds;

        private bool constantSpeed;
        private float layer;
        private float scrollingSpeed;
        private float speed;

        public float Layer
        {
            get => layer;
            set
            {
                layer = value;

                foreach (Background background in backgrounds)
                {
                    background.Layer = layer;
                }
            }
        }

        public ScrollingBackground(Texture2D texture, float scrollingSpeed, bool constantSpeed = false)
            : this(new List<Texture2D>() { texture, texture }, scrollingSpeed, constantSpeed) { }

        public ScrollingBackground(List<Texture2D> textures, float scrollingSpeed, bool constantSpeed = false)
        {
            backgrounds = new List<Background>();

            for (int i = 0; i < textures.Count; i++)
            {
                var texture = textures[i];

                backgrounds.Add(new Background(texture)
                {
                    Position = new Vector2(i * texture.Width - Math.Min(i, i + 1), Game1.ScreenHeight - texture.Height),
                });
            }

            this.scrollingSpeed = scrollingSpeed;
            this.constantSpeed = constantSpeed;
        }

        public void Update()
        {
            ApplySpeed();
            CheckPosition();
        }

        private void ApplySpeed()
        {
            speed = (float)(scrollingSpeed * Game1.GameTime.ElapsedGameTime.TotalSeconds);

            if (!constantSpeed)
            {
                speed *= 2;
            }

            foreach (Background background in backgrounds)
            {
                background.Position.X -= speed;
            }
        }

        private void CheckPosition()
        {
            for (int i = 0; i < backgrounds.Count; i++)
            {
                Background background = backgrounds[i];

                if (background.Rectangle.Right <= 0)
                {
                    int index = i - 1;

                    if (index < 0)
                    {
                        index = backgrounds.Count - 1;
                    }

                    background.Position.X = backgrounds[index].Rectangle.Right - (speed * 2);
                    background.Position.X = backgrounds[index].Rectangle.Right - (speed * 2);
                }
            }
        }

        public void Draw()
        {
            foreach (Background background in backgrounds)
            {
                background.Draw();
            }
        }
    }
}