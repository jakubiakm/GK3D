using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GK3D.Lab1.Prymitives;

namespace GK3D.Lab1
{
    public class Hemisphere : SceneObject
    {
        SpherePrimitive _sphere;

        float _diameter;
        int _tesselation;

        public Hemisphere(float diameter = 5, int tesselation = 10)
        {
            _diameter = diameter;
            _tesselation = tesselation;
        }

        public void Initialize(GraphicsDevice device, Color color, float angle, Vector3 positionVector, Vector3 rotationVector)
        {
            _sphere = new SpherePrimitive(device, _diameter, _tesselation, 2);
            Color = color;
            Angle = angle;
            PositionVector = positionVector;
            RotationVector = rotationVector;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Rotate(0, (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
        }

        public override void Draw(Matrix world, Matrix view, Matrix projection)
        {
            base.Draw(world, view, projection);
            _sphere.Draw(GetWorldMatrix(world), view, projection, Color);
        }

        public void Rotate(float x, float y, float z)
        {
            RotationVector = Vector3.Add(RotationVector, new Vector3(x, y, z));
        }

        public void Move(float x, float y, float z)
        {
            PositionVector = Vector3.Add(PositionVector, new Vector3(x, y, z));
        }
    }
}
