using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.Particles
{
    public class ParticleAttractor
    {
        public int TextureIndex { get; set; }

        public Vector3 Position { get; set; }

        public float Time
        {
            get
            {
                return (float)ExplosionTime / ExplosionSpeed;
            }
        }

        public Vector3 StartPosition { get; set; }

        public Vector3 Velocity { get; set; }

        public int ExplosionTime { get; set; }

        public int ExplosionSpeed { get; set; }

        public int NumberOfAnimationTextures { get; set; }

        Random randomGenerator;

        public ParticleAttractor(Vector3 startPosition, int numberOfAnimationTextures)
        {
            randomGenerator = new Random(GetHashCode());
            NumberOfAnimationTextures = numberOfAnimationTextures;
            StartPosition = startPosition;
            Reset();
        }

        void Reset()
        {
            TextureIndex = 0;
            Position = StartPosition +
            new Vector3(
                (randomGenerator.NextDouble() > 0.5 ? 1 : -1) * 0.25f * (float)randomGenerator.NextDouble(),
                (randomGenerator.NextDouble() > 0.5 ? 1 : -1) * 0.25f * (float)randomGenerator.NextDouble(),
                (randomGenerator.NextDouble() > 0.5 ? 1 : -1) * 0.25f * (float)randomGenerator.NextDouble());
            ExplosionSpeed = randomGenerator.Next(1, 8);
            Velocity = new Vector3(
                (randomGenerator.NextDouble() > 0.5 ? 1 : -1) * 0.02f * (float)randomGenerator.NextDouble(), 
                0.001f + (float)randomGenerator.NextDouble() * 0.02f, 
                (randomGenerator.NextDouble() > 0.5 ? 1 : -1) * 0.02f * (float)randomGenerator.NextDouble());
        }

        public void Update()
        {
            Position += Velocity;
            Velocity = new Vector3(Velocity.X, Velocity.Y - 0.0003f, Velocity.Z);

            if (ExplosionTime++ == ExplosionSpeed)
            {
                ExplosionTime = 0;
                TextureIndex++;
            }
            if (TextureIndex >= NumberOfAnimationTextures)
            {
                Reset();
            }
        }
    }
}
