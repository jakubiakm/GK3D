using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GK3D.Lab1.Prymitives;

namespace GK3D.Lab1
{
    public class Cuboid : SceneObject
    {
        CubePrimitive _cuboid;

        float _width;
        float _height;
        float _depth;

        public Cuboid(float width, float height, float depth)
        {
            _width = width;
            _height = height;
            _depth = depth;
            ScaleVector = new Vector3(height, width, depth);
        }

        public void Initialize(GraphicsDevice device, Color color, float angle, Vector3 positionVector, Vector3 rotationVector, Texture2D texture)
        {
            _cuboid = new CubePrimitive(device, 2f);
            Color = color;
            Angle = angle;
            PositionVector = positionVector;
            RotationVector = rotationVector;
            Texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gameTime, Matrix world, Camera camera, Vector3 light1Position, Vector3 light2Position)
        {
            world = GetWorldMatrix(world);
            base.Draw(gameTime, world, camera, light1Position, light2Position);
            _cuboid.Draw(world, Color, camera, light1Position, light2Position, Texture, Options);
        }
    }
}
