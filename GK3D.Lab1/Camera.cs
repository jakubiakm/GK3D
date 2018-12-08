using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GK3D.Lab1
{
    public class Camera
    {
        public Vector3 Position { get; set; } = new Vector3(0f, 15f, 7f);

        //public Vector3 Position { get; set; } = new Vector3(-9.7f, 52f, 2); //new Vector3(0f, 15f, 7f);

        public Vector3 Direction { get; set; } = new Vector3(0, -1f, -.5f);

        //public Vector3 Direction { get; set; } = new Vector3(0.9f, -0.18f, -0.9f); //new Vector3(0, -1f, -.5f);

        public Vector3 Up { get; set; } = new Vector3(0.2f, 1.1f, 0); //Vector3.UnitX;

        float _moveSpeed;

        GraphicsDevice _graphicsDevice;

        public Matrix ViewMatrix
        {
            get
            {
                return Matrix.CreateLookAt(Position, Position + Direction, Up);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 0.01f;
                float farClipPlane = 1000;

                return Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, _graphicsDevice.Viewport.AspectRatio, nearClipPlane, farClipPlane);
            }
        }

        public Camera(GraphicsDevice graphicsDevice, float moveSpeed = 5)
        {
            _moveSpeed = moveSpeed;
            _graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            var speed = _moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var rotationSpeedMultiplier = 30;
            #region Change camera position
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position += Direction * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position -= Direction * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position += Vector3.Cross(Up, Direction) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position -= Vector3.Cross(Up, Direction) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Position += Up * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Position -= Up * speed;
            }
            #endregion
            #region Change camera directions
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                Direction = Vector3.Transform(Direction, Matrix.CreateFromAxisAngle(Vector3.Cross(Up, Direction), (-MathHelper.PiOver4 / 100) * speed * rotationSpeedMultiplier));
                Up = Vector3.Transform(Up, Matrix.CreateFromAxisAngle(Vector3.Cross(Up, Direction), (-MathHelper.PiOver4 / 100) * speed * rotationSpeedMultiplier));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                Direction = Vector3.Transform(Direction, Matrix.CreateFromAxisAngle(Vector3.Cross(Up, Direction), (MathHelper.PiOver4 / 100) * speed * rotationSpeedMultiplier));
                Up = Vector3.Transform(Up, Matrix.CreateFromAxisAngle(Vector3.Cross(Up, Direction), (MathHelper.PiOver4 / 100) * speed * rotationSpeedMultiplier));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                Direction = Vector3.Transform(Direction, Matrix.CreateFromAxisAngle(Up, (MathHelper.PiOver4 / 150) * speed * rotationSpeedMultiplier));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                Direction = Vector3.Transform(Direction, Matrix.CreateFromAxisAngle(Up, (-MathHelper.PiOver4 / 150) * speed * rotationSpeedMultiplier));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.U))
            {
                Up = Vector3.Transform(Up, Matrix.CreateFromAxisAngle(Direction, (MathHelper.PiOver4 / 150) * speed * rotationSpeedMultiplier));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                Up = Vector3.Transform(Up, Matrix.CreateFromAxisAngle(Direction, (-MathHelper.PiOver4 / 150) * speed * rotationSpeedMultiplier));
            }
            #endregion
        }
    }
}
