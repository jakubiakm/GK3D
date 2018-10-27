using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GK3D.Lab1.Prymitives;

namespace GK3D.Lab1
{
    public class Sphere : SceneObject
    {
        SpherePrimitive _sphere;
        
        float _diameter;
        int _tesselation;

        public Sphere(float diameter = 5, int tesselation = 10)
        {
            _diameter = diameter;
            _tesselation = tesselation;
        }

        public void Initialize(GraphicsDevice device, Color color, float angle, Vector3 positionVector, Vector3 rotationVector)
        {
            _sphere = new SpherePrimitive(device, _diameter, _tesselation);
            Color = color;
            Angle = angle;
            PositionVector = positionVector;
            RotationVector = rotationVector;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        public override void Draw(Matrix world, Camera camera, Vector3 light1Position, Vector3 light2Position)
        {
            world = GetWorldMatrix(world);
            base.Draw(world, camera, light1Position, light2Position);
            _sphere.Draw(world, Color, camera, light1Position, light2Position);
        }
    }
}
