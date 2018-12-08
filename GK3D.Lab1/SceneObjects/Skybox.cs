using GK3D.Lab1.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.SceneObjects
{
    public class Skybox : SceneObject
    {
        Model Model { get; set; }

        TextureCube SkyBoxTexture;

        Effect SkyBoxEffect;

        /// <summary>
        /// The size of the cube, used so that we can resize the box
        /// for different sized environments.
        /// </summary>
        private float size = 200f;

        public void Initialize(TextureCube skyBoxTexture, Effect effect)
        {
            SkyBoxTexture = skyBoxTexture;
            SkyBoxEffect = effect;
        }

        public override void LoadModel(ContentManager contentManager)
        {
            base.LoadModel(contentManager);
            Model = contentManager.Load<Model>("skyboxcube");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //(float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gameTime, Matrix world, Camera camera, Vector3 light1Position, Vector3 light2Position)
        {
            base.Draw(gameTime, world, camera, light1Position, light2Position);
            foreach (EffectPass pass in SkyBoxEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                // Draw all of the components of the mesh, but we know the cube really
                // only has one mesh
                foreach (ModelMesh mesh in Model.Meshes)
                {
                    // Assign the appropriate values to each of the parameters
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = SkyBoxEffect;
                        //part.Effect.Parameters["World"].SetValue(GetWorldMatrix(world));
                        part.Effect.Parameters["View"].SetValue(camera.ViewMatrix);
                        part.Effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);
                        part.Effect.Parameters["SkyBoxTexture"].SetValue(SkyBoxTexture);
                        part.Effect.Parameters["CameraPosition"].SetValue(camera.Position);
                        part.Effect.Parameters["World"].SetValue(
                            Matrix.CreateScale(size) * Matrix.CreateTranslation(camera.Position));
                        //part.Effect.Parameters["View"].SetValue(view);
                        //part.Effect.Parameters["Projection"].SetValue(projection);
                        //part.Effect.Parameters["SkyBoxTexture"].SetValue(skyBoxTexture);
                        //part.Effect.Parameters["CameraPosition"].SetValue(cameraPosition);
                    }

                    // Draw the mesh with the skybox effect
                    mesh.Draw();
                }
            }
        }
    }
}