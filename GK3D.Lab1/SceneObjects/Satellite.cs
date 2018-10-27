using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace GK3D.Lab1
{
    public class Satellite : SceneObject
    {
        Model Model { get; set; }

        public List<Color> Colors { get; set; } = new List<Color>();
        public List<float> Angles { get; set; } = new List<float>();
        public List<Vector3> PositionVectors { get; set; } = new List<Vector3>();
        public List<Vector3> RotationVectors { get; set; } = new List<Vector3>();

        bool RightDirection = true;

        public void Initialize(Color color, float angle, Vector3 positionVector, Vector3 rotationVector)
        {
            Colors.Add(color);
            Angles.Add(angle);
            PositionVectors.Add(positionVector);
            RotationVectors.Add(rotationVector);
        }

        public override void LoadModel(ContentManager contentManager)
        {
            base.LoadModel(contentManager);
            Model = contentManager.Load<Model>("Zenith_OBJ");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (PositionVectors[0].X > 10)
                RightDirection = false;
            else if (PositionVectors[0].X < -10)
                RightDirection = true;
            if (RightDirection)
            {
                PositionVectors[0] = Vector3.Add(PositionVectors[0], new Vector3(0.15f, 0.15f, 0));
            }
            else
            {
                PositionVectors[0] = Vector3.Add(PositionVectors[0], new Vector3(-0.15f, -0.15f, 0));
            }
            //Angles[0] += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Angles[1] += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(Matrix world, Camera camera, Vector3 lpos1, Vector3 lpos2)
        {
            base.Draw(world, camera, lpos1, lpos2);
            for (int ind = 0; ind < PositionVectors.Count; ind++)
                foreach (ModelMesh mesh in Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {

                        effect.World = GetWorldMatrix(world, ind);
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

        public Matrix GetWorldMatrix(Matrix currentWorld, int ind)
        {
            Matrix combined = currentWorld;

            combined *= Matrix.CreateFromAxisAngle(new Vector3(1, 0, 0), RotationVectors[ind].X);
            combined *= Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), RotationVectors[ind].Y);
            combined *= Matrix.CreateFromAxisAngle(new Vector3(0, 0, 1), RotationVectors[ind].Z);
            combined *= Matrix.CreateTranslation(PositionVectors[ind].X, PositionVectors[ind].Y, PositionVectors[ind].Z);
            combined *= Matrix.CreateRotationZ(Angles[ind]);

            return combined;
        }
    }
}