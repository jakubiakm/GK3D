using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GK3D.Lab1.Prymitives;
using GK3D.Lab1.Helpers;

namespace GK3D.Lab1
{
    public class Cuboid : SceneObject
    {
        Model Model { get; set; }

        Effect Effect { get; set; }


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
            Model = contentManager.Load<Model>("cube");
            Effect = contentManager.Load<Effect>("effect");
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
                    part.Effect = Effect;
                    part.Effect.Parameters["View"].SetValue(camera.ViewMatrix);
                    part.Effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);
                    part.Effect.Parameters["World"].SetValue(GetWorldMatrix(world));
                    part.Effect.Parameters["AmbientColor"].SetValue(Color.Green.ToVector4());
                    part.Effect.Parameters["AmbientIntensity"].SetValue(0.5f);
                }

                mesh.Draw();
            }
        }
    }
}
