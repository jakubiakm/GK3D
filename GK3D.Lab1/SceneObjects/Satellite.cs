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
            //(float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(Matrix world, Camera camera)
        {
            base.Draw(world, camera);
            for (int ind = 0; ind < PositionVectors.Count; ind++)
                foreach (ModelMesh mesh in Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = GetWorldMatrix(world, ind);
                        effect.View = camera.ViewMatrix;
                        effect.Projection = camera.ProjectionMatrix;
                        effect.DiffuseColor = Colors[ind].ToVector3();

                        //effect.EnableDefaultLighting();
                        //effect.PreferPerPixelLighting = true;
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