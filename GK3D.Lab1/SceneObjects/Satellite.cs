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
        public List<Vector3> ScaleVectors { get; set; } = new List<Vector3>();
        bool RightDirection = true;

        public void Initialize(Color color, float angle, Vector3 positionVector, Vector3 rotationVector, Vector3 scaleVector)
        {
            Colors.Add(color);
            Angles.Add(angle);
            PositionVectors.Add(positionVector);
            RotationVectors.Add(rotationVector);
            ScaleVectors.Add(scaleVector);
        }

        public override void LoadModel(ContentManager contentManager)
        {
            base.LoadModel(contentManager);
            Model = contentManager.Load<Model>("satellite_obj");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (PositionVectors[0].X > 10)
                RightDirection = false;
            else if (PositionVectors[0].X < -10)
                RightDirection = true;
            var move = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (RightDirection)
            {
                PositionVectors[0] = Vector3.Add(PositionVectors[0], new Vector3(move, move, 0));
                PositionVectors[1] = Vector3.Add(PositionVectors[1], new Vector3(-move, 0, 0));
            }
            else
            {
                PositionVectors[0] = Vector3.Add(PositionVectors[0], new Vector3(-move, -move, 0));
                PositionVectors[1] = Vector3.Add(PositionVectors[1], new Vector3(move, 0, 0));
            }
            //Angles[0] += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Angles[1] += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(Matrix world, Camera camera, Vector3 light1Position, Vector3 light2Position)
        {
            base.Draw(world, camera, light1Position, light2Position);
            for (int ind = 0; ind < PositionVectors.Count; ind++)
                foreach (ModelMesh mesh in Model.Meshes)
                {
                    foreach (BasicEffect basicEffect in mesh.Effects)
                    {

                        basicEffect.World = GetWorldMatrix(world, ind);
                        basicEffect.View = camera.ViewMatrix;
                        basicEffect.Projection = camera.ProjectionMatrix;
                        basicEffect.EmissiveColor = Colors[ind].ToVector3();
                        basicEffect.Alpha = Colors[ind].A / 255.0f;

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


                        if (Colors[ind].A < 255)
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

        public Matrix GetWorldMatrix(Matrix currentWorld, int ind)
        {
            Matrix combined = currentWorld;

            combined *= Matrix.CreateScale(ScaleVectors[ind].X, ScaleVectors[ind].Y, ScaleVectors[ind].Z);
            combined *= Matrix.CreateFromAxisAngle(new Vector3(1, 0, 0), RotationVectors[ind].X);
            combined *= Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), RotationVectors[ind].Y);
            combined *= Matrix.CreateFromAxisAngle(new Vector3(0, 0, 1), RotationVectors[ind].Z);
            combined *= Matrix.CreateTranslation(PositionVectors[ind].X, PositionVectors[ind].Y, PositionVectors[ind].Z);
            combined *= Matrix.CreateRotationZ(Angles[ind]);

            return combined;
        }
    }
}