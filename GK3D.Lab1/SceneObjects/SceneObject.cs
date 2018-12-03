
using GK3D.Lab1.SceneObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Lab1
{
    public class SceneObject
    {
        public Color Color { get; set; }
        public float Angle { get; set; }
        public Vector3 PositionVector { get; set; }
        public Vector3 RotationVector { get; set; }
        public Vector3 ScaleVector { get; set; } = Vector3.One;
        public Texture2D Texture { get; set; }
        public Options Options { get; set; }

        public Matrix GetWorldMatrix(Matrix currentWorld)
        {
            Matrix combined = currentWorld;

            combined *= Matrix.CreateScale(ScaleVector.X, ScaleVector.Y, ScaleVector.Z);
            combined *= Matrix.CreateFromAxisAngle(new Vector3(1, 0, 0), RotationVector.X);
            combined *= Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), RotationVector.Y);
            combined *= Matrix.CreateFromAxisAngle(new Vector3(0, 0, 1), RotationVector.Z);
            combined *= Matrix.CreateTranslation(PositionVector.X, PositionVector.Y, PositionVector.Z);
            combined *= Matrix.CreateRotationZ(Angle);

            return combined;
        }

        public virtual void LoadModel(ContentManager contentManager)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, Matrix world, Camera camera, Vector3 light1Position, Vector3 light2Position)
        {

        }
    }
}
