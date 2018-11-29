using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GK3D.Lab1.Helpers;

namespace GK3D.Lab1
{
    public class Airboat : SceneObject
    {
        Model Model { get; set; }

        public void Initialize(Color color, float angle, Vector3 positionVector, Vector3 rotationVector, Vector3 scaleVector, Texture2D texture)
        {
            Color = color;
            Angle = angle;
            PositionVector = positionVector;
            RotationVector = rotationVector;
            ScaleVector = scaleVector;
            Texture = texture;
        }

        public override void LoadModel(ContentManager contentManager)
        {
            base.LoadModel(contentManager);
            Model = contentManager.Load<Model>("Arc170");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //(float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gameTime, Matrix world, Camera camera, Vector3 light1Position, Vector3 light2Position)
        {
            base.Draw(gameTime, world, camera, light1Position, light2Position);
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect basicEffect in mesh.Effects)
                {
                    basicEffect.Texture = Texture;
                    basicEffect.TextureEnabled = true;

                    basicEffect.World = GetWorldMatrix(world);
                    basicEffect.View = camera.ViewMatrix;
                    basicEffect.Projection = camera.ProjectionMatrix;
                    basicEffect.EmissiveColor = Color.ToVector3();
                    basicEffect.Alpha = Color.A / 255.0f;

                    BasicEffectHelper.AddLightToBasicEffect(basicEffect, light1Position, light2Position);

                    GraphicsDevice device = basicEffect.GraphicsDevice;
                    device.DepthStencilState = DepthStencilState.Default;

                    SamplerState sampler = new SamplerState
                    {
                        MipMapLevelOfDetailBias = 0f,
                        Filter = TextureFilter.MinPointMagLinearMipLinear
                    };

                    if (Color.A < 255)
                    {
                        // Set renderstates for alpha blended rendering.
                        device.BlendState = BlendState.AlphaBlend;
                    }
                    else
                    {
                        // Set renderstates for opaque rendering.
                        device.BlendState = BlendState.Opaque;
                    }

                }
                mesh.Draw();
            }
        }
    }
}