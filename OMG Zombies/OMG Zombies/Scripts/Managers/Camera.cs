﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OMG_Zombies.Scripts.Managers
{
    public class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get => transform;
        }

        private Viewport viewport;

        // centro do ecrã, independente quando se move para direita ou esquerda no mapa
        private Vector2 center;
        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            if (position.X < viewport.Width / 2)
            {
                center.X = viewport.Width / 2;
            }
            else if (position.X > xOffset - (viewport.Width / 2))
            {
                center.X = xOffset - (viewport.Width / 2);
            }
            else
            {
                center.X = position.X;
            }

            if (position.Y < viewport.Height / 2)
            {
                center.Y = viewport.Height / 2;
            }
            else if (position.Y > yOffset - (viewport.Height / 2))
            {
                center.Y = yOffset - (viewport.Height / 2);
            }
            else
            {
                center.Y = position.Y;
            }

            transform = Matrix.CreateTranslation(new Vector3(
                -Center.X + (viewport.Width / 2),
                -Center.Y + (viewport.Height / 2),
                0));
        }
    }
}