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
        bool _addColorIntensity;
        int _colorIntensity;

        public Sphere(float diameter = 5, int tesselation = 10)
        {
            _diameter = diameter;
            _tesselation = tesselation;
            _addColorIntensity = true;
            _colorIntensity = 0;
        }

        public void Initialize(GraphicsDevice device, Color color, float angle, Vector3 positionVector, Vector3 rotationVector, Texture2D texture)
        {
            _sphere = new SpherePrimitive(device, _diameter, _tesselation);
            Texture = texture;
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
        
        public override void Draw(GameTime gameTime, Matrix world, Camera camera, Vector3 light1Position, Vector3 light2Position)
        {
            world = GetWorldMatrix(world);
            base.Draw(gameTime, world, camera, light1Position, light2Position);
            _sphere.Draw(world, Color, camera, light1Position, light2Position, Texture, GetColorIntensity());
        }

        private float GetColorIntensity()
        {
            float maxIntensity = 100;
            if (_colorIntensity > maxIntensity)
                _addColorIntensity = false;
            if (_colorIntensity < 0)
                _addColorIntensity = true;
            if (_addColorIntensity)
                _colorIntensity++;
            else
                _colorIntensity--;
            return _colorIntensity / maxIntensity;
        }
    }
}
