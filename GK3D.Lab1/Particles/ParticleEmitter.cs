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
        List<ParticleAttractor> Attractors { get; set; }

        public int NumberOfParticles { get; }

        public int ParticleIndex { get; set; } = 0;

        List<Texture2D> ParticleTextures = new List<Texture2D>();

        Vector3 ParticlePosition { get; set; }

        public int Time { get; set; } = 0;

        public ParticleEmitter(int numberOfParticles)
        {
            NumberOfParticles = numberOfParticles;
            Attractors = new List<ParticleAttractor>();
        }

        public void Initialize(GraphicsDevice device, Texture2D animationTexture, Vector3 startPosition)
        {
            ParticlePosition = startPosition;
            GenerateParticleAnimation(device, animationTexture);
            for (int i = 0; i != NumberOfParticles; i++)
            {
                var attractor = new ParticleAttractor(ParticlePosition, ParticleTextures.Count);
                Attractors.Add(attractor);
            }
        }

        public void Update()
        {
            Attractors.ForEach(attractor => attractor.Update());
        }

        public void Draw(GraphicsDevice device, Camera camera, SpriteBatch spriteBatch)
        {
            Attractors.ForEach(
                attractor =>
                {
                    int width = 10, height = 10;
                    Texture2D rect = new Texture2D(device, width, height);

                    BasicEffect basicEffect = new BasicEffect(device);
                    Matrix combined = Matrix.Invert(camera.ViewMatrix);
                    combined *= Matrix.CreateScale(0.005f);
                    combined.Translation = attractor.Position;
                    basicEffect.World = combined;
                    basicEffect.View = camera.ViewMatrix;
                    basicEffect.Projection = camera.ProjectionMatrix;
                    basicEffect.TextureEnabled = true;
                    basicEffect.Alpha = 1f;
                    spriteBatch.Begin(0, BlendState.AlphaBlend, null, DepthStencilState.DepthRead, RasterizerState.CullNone, basicEffect);
                    Vector2 coor = new Vector2(0, 0);
                    spriteBatch.Draw(GetInterpolatedTexture(attractor.TextureIndex, attractor.Time, device), coor, Color.Blue);
                    spriteBatch.End();
                });
        }

        Texture2D GetInterpolatedTexture(int currentTextureIndex, float time, GraphicsDevice device)
        {
            if(currentTextureIndex + 1 >= ParticleTextures.Count)
                return ParticleTextures[currentTextureIndex % 50];
            int width = ParticleTextures[0].Width;
            int height = ParticleTextures[0].Height;
            Color[] sourceData = new Color[width * height];
            Color[] destinationData = new Color[width * height];
            Color[] targetData = new Color[width * height];
            ParticleTextures[currentTextureIndex].GetData(sourceData);
            ParticleTextures[currentTextureIndex + 1].GetData(destinationData);

            Texture2D targetTexture = new Texture2D(device, width, height);
            for(int i = 0; i != width * height; i++)
            {
                targetData[i] = new Color(
                    (int)((1 - time) * sourceData[i].R + time * destinationData[i].R),
                    (int)((1 - time) * sourceData[i].G + time * destinationData[i].G),
                    (int)((1 - time) * sourceData[i].B + time * destinationData[i].B),
                    (int)((1 - time) * sourceData[i].A + time * destinationData[i].A));
            }
            targetTexture.SetData(targetData);

            return targetTexture;
        }

        private void GenerateParticleAnimation(GraphicsDevice device, Texture2D animationTexture)
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
                    Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
                    animationTexture.GetData(0, sourceRectangle, data, 0, data.Length);
                    animation.SetData(data);

                    ParticleTextures.Add(animation);
                }
        }
    }
}
