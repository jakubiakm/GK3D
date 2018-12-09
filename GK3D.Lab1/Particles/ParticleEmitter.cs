using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.Particles
{
    public class ParticleEmitter
    {
        public int NumberOfParticles { get; }

        public int ParticleIndex { get; set; } = 0;

        List<Texture2D> ParticleTextures = new List<Texture2D>();

        Vector3 ParticlePosition { get; set; } = new Vector3(-0.4f, 3.05f, 0);

        public int Time { get; set; } = 0;

        public ParticleEmitter(int numberOfParticles)
        {
            NumberOfParticles = numberOfParticles;
        }

        public void Initialize(GraphicsDevice device, Texture2D animationTexture)
        {
            GenerateParticleAnimation(device, animationTexture);
        }

        public void Update()
        {
            ParticlePosition += new Vector3(0, 0.01f, 0);
            if (ParticleIndex > 49)
            {
                ParticleIndex = 0;
                ParticlePosition = new Vector3(-0.4f, 3.05f, 0);
            }
        }

        public void Draw(GraphicsDevice device, Camera camera, SpriteBatch spriteBatch)
        {
            if (Time++ == 2)
            {
                Time = 0;
                ParticleIndex++;
            }
            for (int i = 0; i != 1; i++)
            {
                int width = 10, height = 10;
                Texture2D rect = new Texture2D(device, width, height);

                BasicEffect basicEffect = new BasicEffect(device);
                Matrix combined = Matrix.Invert(camera.ViewMatrix);
                combined *= Matrix.CreateScale(0.01f);
                combined.Translation = ParticlePosition;
                basicEffect.World = combined;
                basicEffect.View = camera.ViewMatrix;
                basicEffect.Projection = camera.ProjectionMatrix;
                basicEffect.TextureEnabled = true;
                basicEffect.Alpha = 0.5f;
                spriteBatch.Begin(0, BlendState.AlphaBlend, null, DepthStencilState.DepthRead, RasterizerState.CullNone, basicEffect);
                Vector2 coor = new Vector2(0, 0);
                spriteBatch.Draw(ParticleTextures[ParticleIndex % 50], coor, Color.Blue);
                spriteBatch.End();
            }
        }

        private
        void GenerateParticleAnimation(GraphicsDevice device, Texture2D animationTexture)
        {
            int width = animationTexture.Width / 10;
            int height = animationTexture.Height / 5;
            ParticleTextures = new List<Texture2D>();

            for (int j = 0; j != 5; j++)
                for (int i = 0; i != 10; i++)
                {
                    var animation = new Texture2D(device, width, height, false, animationTexture.Format);

                    Rectangle sourceRectangle = new Rectangle(i * width, j * height, width, height);

                    Texture2D cropTexture = new Texture2D(device, sourceRectangle.Width, sourceRectangle.Height);
                    Color[] datan = new Color[sourceRectangle.Width * sourceRectangle.Height];
                    animationTexture.GetData(0, sourceRectangle, datan, 0, datan.Length);
                    animation.SetData(datan);

                    ParticleTextures.Add(animation);
                }
        }
    }
}
