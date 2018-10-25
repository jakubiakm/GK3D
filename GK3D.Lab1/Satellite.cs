using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GK3D.Lab1
{
    public class Satellite : SceneObject
    {
        Model Model { get; set; }

        public void Initialize(float angle = 0, float x = 0, float y = 0, float z = 0)
        {
            Angle = angle;
            X = x;
            Y = y;
            Z = z;  
        }

        public override void LoadModel(ContentManager contentManager)
        {
            base.LoadModel(contentManager);
            Model = contentManager.Load<Model>("Zenith_OBJ");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        // For now we'll take these values in, eventually we'll
        // take a Camera object
        //public void Draw(Vector3 cameraPosition, float aspectRatio)
        //{
        //    foreach (var mesh in model.Meshes)
        //    {
        //        foreach (BasicEffect effect in mesh.Effects)
        //        {
        //            effect.EnableDefaultLighting();
        //            effect.PreferPerPixelLighting = true;

        //            effect.World = Matrix.Identity;
        //            var cameraLookAtVector = Vector3.Zero;
        //            var cameraUpVector = Vector3.UnitZ;

        //            effect.View = Matrix.CreateLookAt(
        //                cameraPosition, cameraLookAtVector, cameraUpVector);

        //            float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
        //            float nearClipPlane = 1;
        //            float farClipPlane = 200;

        //            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
        //                fieldOfView, aspectRatio, nearClipPlane, farClipPlane);


        //        }

        //        // Now that we've assigned our properties on the effects we can
        //        // draw the entire mesh
        //        mesh.Draw();
        //    }
        //}

        public override void Draw(Matrix world, Matrix view, Matrix projection)
        {
            base.Draw(world, view, projection);
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = GetWorldMatrix(world);
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }


    }
}