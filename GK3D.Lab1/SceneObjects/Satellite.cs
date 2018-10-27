using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GK3D.Lab1
{
    public class Satellite : SceneObject
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
            Model = contentManager.Load<Model>("Zenith_OBJ");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        public override void Draw(Matrix world, Camera camera)
        {
            base.Draw(world, camera);
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = GetWorldMatrix(world);
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                    effect.DiffuseColor = Color.ToVector3();

                    //effect.EnableDefaultLighting();
                    //effect.PreferPerPixelLighting = true;
                }
                mesh.Draw();
            }
        }


    }
}