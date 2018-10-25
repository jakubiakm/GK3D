using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GK3D.Lab1
{
    public class SceneObject
    {
        public float Angle { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Matrix GetWorldMatrix(Matrix currentWorld)
        {
            Matrix combined = new Matrix(currentWorld.M11, currentWorld.M12, currentWorld.M13, currentWorld.M14,
                currentWorld.M21, currentWorld.M22, currentWorld.M23, currentWorld.M24,
                currentWorld.M31, currentWorld.M32, currentWorld.M33, currentWorld.M34,
                currentWorld.M41, currentWorld.M42, currentWorld.M43, currentWorld.M44);

            combined *= Matrix.CreateTranslation(X, Y, Z);
            combined *= Matrix.CreateRotationZ(Angle);
           
            return combined;
        }

        public virtual void LoadModel(ContentManager contentManager)
        {
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(Matrix world, Matrix view, Matrix projection)
        {

        }
    }
}
