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

        public void Initialize(GraphicsDevice device, Color color, float angle = 0, float x = 0, float y = 0, float z = 0)
        {
            _sphere = new SpherePrimitive(device, _diameter, _tesselation);
            Color = color;
            Angle = angle;
            X = x;
            Y = y;
            Z = z;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(Matrix world, Matrix view, Matrix projection)
        {
            base.Draw(world, view, projection);
            _sphere.Draw(GetWorldMatrix(world) * Matrix.CreateRotationY(Angle), view, projection, Color);
        }
    }
}
