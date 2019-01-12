using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using GK3D.Lab1.Helpers;

namespace GK3D.Lab1
{
    public class Bison : SceneObject
    {
        Model Model { get; set; }

        Effect BisonEffect { get; set; }

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
            Model = contentManager.Load<Model>("Bison");
            BisonEffect = contentManager.Load<Effect>("effect");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //(float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gameTime, Matrix world, Camera camera)
        {
            base.Draw(gameTime, world, camera);
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (var part in mesh.MeshParts)
                {
                    part.Effect = BisonEffect;
                    part.Effect.Parameters["View"].SetValue(camera.ViewMatrix);
                    part.Effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);
                    part.Effect.Parameters["World"].SetValue(GetWorldMatrix(world));
                    //BasicEffectHelper.SetNormalBasicEffect(basicEffect, Color, Texture);
                }

                mesh.Draw();
            }
        }
    }
}