using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace GK3D.Lab1
{
    public class Bison : SceneObject
    {
        Model Model { get; set; }

        public void Initialize(Color color, float angle, Vector3 positionVector, Vector3 rotationVector)
        {
            Color = color;
            Angle = angle;
            PositionVector = positionVector;
            RotationVector = rotationVector;
        }

        public override void LoadModel(ContentManager contentManager)
        {
            base.LoadModel(contentManager);
            Model = contentManager.Load<Model>("Bison");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //(float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(Matrix world, Camera camera, Vector3 lpos1, Vector3 lpos2)
        {
            base.Draw(world, camera, lpos1, lpos2);
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Matrix.CreateScale(0.65f, 0.65f, 0.65f) * GetWorldMatrix(world);
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                    effect.EmissiveColor = Color.ToVector3();

                    effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight0.Direction = new Vector3(0.0f, -0.4f, 0);
                    effect.DirectionalLight0.DiffuseColor = new Vector3(0.0f, 1.0f, 0);
                    effect.DirectionalLight0.Enabled = true;

                    effect.DirectionalLight1.Enabled = true;
                    effect.DirectionalLight1.SpecularColor = Color.White.ToVector3();
                    effect.DirectionalLight1.Direction = -lpos1;
                    effect.DirectionalLight1.Enabled = true;
                    effect.SpecularPower = 100f;
                    effect.SpecularColor = Color.White.ToVector3();
                    effect.AmbientLightColor = new Vector3(0.0f, 0.02f, 0.0f);
                    effect.LightingEnabled = true;
                }
                mesh.Draw();
            }
        }
    }
}