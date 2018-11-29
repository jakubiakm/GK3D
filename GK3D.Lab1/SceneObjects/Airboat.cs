using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

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
                    
                    basicEffect.DirectionalLight0.Enabled = true;
                    basicEffect.DirectionalLight0.Direction = new Vector3(0.0f, -1000.0f, 0);
                    basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.0005f, 0.0005f, 0.0005f);

                    basicEffect.DirectionalLight1.Enabled = true;
                    basicEffect.DirectionalLight1.SpecularColor = Color.White.ToVector3();
                    basicEffect.DirectionalLight1.Direction = -light1Position;
                    basicEffect.DirectionalLight1.DiffuseColor = new Vector3(0.0f, 0.025f, 0.0f);

                    basicEffect.DirectionalLight2.Enabled = true;
                    basicEffect.DirectionalLight2.SpecularColor = Color.White.ToVector3();
                    basicEffect.DirectionalLight2.Direction = -light2Position;
                    basicEffect.DirectionalLight2.DiffuseColor = new Vector3(0.0f, 0.015f, 0.0f);

                    basicEffect.SpecularPower = 500f;
                    basicEffect.SpecularColor = Color.White.ToVector3();
                    basicEffect.AmbientLightColor = new Vector3(0.0f, 0.02f, 0.0f);
                    basicEffect.LightingEnabled = true;
                    
                    GraphicsDevice device = basicEffect.GraphicsDevice;
                    device.DepthStencilState = DepthStencilState.Default;



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